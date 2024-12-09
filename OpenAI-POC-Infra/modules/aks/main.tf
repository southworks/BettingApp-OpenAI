resource "azurerm_user_assigned_identity" "aks_identity" {
  resource_group_name = var.resource_group_name
  location            = var.location
  name                = ""
}

resource "azurerm_role_assignment" "kubelet_identity_operator" {
  principal_id         = azurerm_user_assigned_identity.aks_identity.principal_id
  role_definition_name = "Managed Identity Operator"
  scope                = azurerm_user_assigned_identity.aks_identity.id
  depends_on = [
    azurerm_user_assigned_identity.aks_identity
  ]
}
resource "azurerm_kubernetes_cluster" "k8s" {
  # checkov:skip=CKV_AZURE_4:Monitoring not required for POC
  # checkov:skip=CKV_AZURE_6:Public access for simplicity
  # checkov:skip=CKV_AZURE_7:Network policy not required for POC
  # checkov:skip=CKV_AZURE_115:Public access for simplicity
  # checkov:skip=CKV_AZURE_116:Using kubenet for simplicity
  # checkov:skip=CKV_AZURE_117:Encryption not required for POC
  # checkov:skip=CKV_AZURE_141:Local admin for simplicity
  # checkov:skip=CKV_AZURE_168:We want a minimal-cost POC
  # checkov:skip=CKV_AZURE_170:We want a minimal-cost POC
  # checkov:skip=CKV_AZURE_227:Encryption not required for POC
  # checkov:skip=CKV2_AZURE_29:Using kubenet for simplicity
  # checkov:skip=CKV_AZURE_226:We want a minimal-cost POC
  # checkov:skip=CKV_AZURE_232:Skipping for simplicity
  name                      = ""
  location                  = var.location
  dns_prefix                = "openaiakscluster"
  resource_group_name       = var.resource_group_name
  automatic_channel_upgrade = "stable"
  workload_identity_enabled = true
  oidc_issuer_enabled       = true

  identity {
    type         = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.aks_identity.id]
  }

  default_node_pool {
    name                        = "agentpool"
    vm_size                     = "Standard_DS2_v2"
    enable_auto_scaling         = true
    min_count                   = 1
    max_count                   = 3
    temporary_name_for_rotation = "pooltemp"
    vnet_subnet_id              = var.nodes_subnet_id
  }

  network_profile {
    network_plugin    = "kubenet"
    load_balancer_sku = "standard"
  }

  kubelet_identity {
    client_id                 = azurerm_user_assigned_identity.aks_identity.client_id
    object_id                 = azurerm_user_assigned_identity.aks_identity.principal_id
    user_assigned_identity_id = azurerm_user_assigned_identity.aks_identity.id
  }

  key_vault_secrets_provider {
    secret_rotation_enabled = true
  }

  web_app_routing {
    
  }

  depends_on = [
    azurerm_role_assignment.kubelet_identity_operator    
  ]
}

resource "azurerm_role_assignment" "acr_pull" {
  principal_id         = azurerm_user_assigned_identity.aks_identity.principal_id
  role_definition_name = "AcrPull"
  scope                = var.acr_id

  depends_on = [
    azurerm_user_assigned_identity.aks_identity
  ]
}