resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.resource_group_location
}

module "network" {
  source              = "./modules/network"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
}

resource "azurerm_container_registry" "acr" {
  name                = "<container_registry_name>"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  # checkov:skip=CKV_AZURE_139:Public access for simplicity
  # checkov:skip=CKV_AZURE_163:We want a minimal-cost POC
  # checkov:skip=CKV_AZURE_164:We want a minimal-cost POC. Not supported in Basic
  # checkov:skip=CKV_AZURE_165:We want a minimal-cost POC. Not supported in Basic
  # checkov:skip=CKV_AZURE_166:We want a minimal-cost POC. Not supported in Basic
  # checkov:skip=CKV_AZURE_167:We want a minimal-cost POC. Not supported in Basic
  # checkov:skip=CKV_AZURE_233:We want a minimal-cost POC. Not supported in Basic
  # checkov:skip=CKV_AZURE_237:We want a minimal-cost POC. Not supported in Basic
  sku           = "Basic"
  admin_enabled = false
}

module "aks" {
  source              = "./modules/aks"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  acr_id              = azurerm_container_registry.acr.id
  nodes_subnet_id     = module.network.aks_nodes_subnet_id
}

module "automation" {
  source              = "./modules/automation"
  resource_group_name = azurerm_resource_group.rg.name
  resource_group_id   = azurerm_resource_group.rg.id
  location            = azurerm_resource_group.rg.location
  aks_cluster_id      = module.aks.aks_cluster_id
  aks_cluster_name    = module.aks.aks_cluster_name
}

module "api" {       
  source                       = "./modules/api"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  openai_private_dns_zone_id   = module.network.openai_private_dns_zone_id
  openai_subnet_id             = module.network.openai_subnet_id
  cosmosdb_private_dns_zone_id = module.network.cosmosdb_private_dns_zone_id
  cosmosdb_subnet_id           = module.network.cosmosdb_subnet_id
  depends_on                   = [azurerm_resource_group.rg]
}


data "azurerm_client_config" "current" {}
resource "azurerm_key_vault" "kv" {
  # checkov:skip=CKV2_AZURE_32:Not Required for POC
  # checkov:skip=CKV_AZURE_109:Not Required for POC
  # checkov:skip=CKV_AZURE_114:Not Required for POC
  # checkov:skip=CKV_AZURE_189:Not Required for POC  
  name                        = "<key_valut_name>"
  location                    = azurerm_resource_group.rg.location
  resource_group_name         = azurerm_resource_group.rg.name
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  sku_name                    = "standard"
  purge_protection_enabled    = true
  enable_rbac_authorization   = true
}

data "azurerm_cosmosdb_account" "cosmosdb" {
  name                = module.api.cosmosdb_account_name
  resource_group_name = azurerm_resource_group.rg.name
}

resource "azurerm_key_vault_secret" "mongo_connection_string" {
  # checkov:skip=CKV_AZURE_41:Not Required for POC
  name         = "<key_valut_secret_name>"
  value        = data.azurerm_cosmosdb_account.cosmosdb.primary_mongodb_connection_string
  key_vault_id = azurerm_key_vault.kv.id
  content_type = "text/plain"
}

resource "azurerm_user_assigned_identity" "namespace_identities" {
  for_each            = toset(var.namespaces)
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  name                = "${each.key}-identity"
}

resource "azurerm_federated_identity_credential" "api_federated_credentials" {
  for_each            = toset(var.namespaces)
  name                = "${each.key}-federated-credential"
  resource_group_name = azurerm_resource_group.rg.name
  audience            = ["api://AzureADTokenExchange"]
  issuer              = module.aks.oidc_issuer_url
  parent_id           = azurerm_user_assigned_identity.namespace_identities[each.key].id
  subject             = "system:serviceaccount:${each.key}:${each.key}-sa"
}

locals {
  api_identity = [for identity in azurerm_user_assigned_identity.namespace_identities : identity if identity.name == ""][0]
}
resource "azurerm_role_assignment" "workload_secret_read" {
  principal_id         = local.api_identity.principal_id
  role_definition_name = "Key Vault Secrets User"
  scope                = azurerm_key_vault.kv.id
}

data "azurerm_subscription" "primary" {}
resource "azurerm_role_definition" "mongo_manager" {
  name        = "Mongo Manager"
  scope       = data.azurerm_subscription.primary.id
  description = "Custom role for managing mongo collections"

  permissions {
    actions     = [
      "Microsoft.DocumentDB/databaseAccounts/mongodbDatabases/read",
      "Microsoft.DocumentDB/databaseAccounts/mongodbDatabases/collections/*"]
    not_actions = []
  }
}

resource "azurerm_role_assignment" "workload_mongo_manage" {
  principal_id         = local.api_identity.principal_id
  role_definition_name = azurerm_role_definition.mongo_manager.name
  scope                = module.api.cosmosdb_account_id
}