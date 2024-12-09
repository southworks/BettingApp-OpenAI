resource "azurerm_virtual_network" "vnet" {
  name                = "vnet"
  location            = var.location
  resource_group_name = var.resource_group_name
  address_space       = ["172.16.0.0/16"]
}

resource "azurerm_subnet" "aks_nodes_subnet" {
  # checkov:skip=CKV2_AZURE_31:Kubenet handles NSG for AKS subnet

  name                 = "aks_nodes_subnet"
  resource_group_name  = var.resource_group_name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = ["172.16.1.0/24"]
}

resource "azurerm_subnet" "openai_subnet" {
  # checkov:skip=CKV2_AZURE_31:We disabled outbound access and we will use a private endpoint for OpenAI

  name                            = "openai_subnet"
  resource_group_name             = var.resource_group_name
  virtual_network_name            = azurerm_virtual_network.vnet.name
  address_prefixes                = ["172.16.2.0/24"]
  default_outbound_access_enabled = false

  service_endpoints = ["Microsoft.CognitiveServices"]
}

resource "azurerm_subnet" "cosmosdb_subnet" {
  name                            = "cosmosdb-subnet"
  resource_group_name             = var.resource_group_name
  virtual_network_name            = azurerm_virtual_network.vnet.name
  address_prefixes                = ["172.16.3.0/24"]
  default_outbound_access_enabled = false
}

resource "azurerm_network_security_group" "cosmosdb_subnet_nsg" {
  name                = "cosmosdb-subnet-nsg"
  location            = var.location
  resource_group_name = var.resource_group_name
}

resource "azurerm_subnet_network_security_group_association" "cosmosdb_subnet_nsg" {
  subnet_id                 = azurerm_subnet.cosmosdb_subnet.id
  network_security_group_id = azurerm_network_security_group.cosmosdb_subnet_nsg.id
}

resource "azurerm_private_dns_zone" "zone" {
  name                = "privatelink.openai.azure.com"
  resource_group_name = var.resource_group_name
}

resource "azurerm_private_dns_zone_virtual_network_link" "link" {
  name                  = "openai-private-dns-zone"
  private_dns_zone_name = azurerm_private_dns_zone.zone.name
  resource_group_name   = var.resource_group_name
  virtual_network_id    = azurerm_virtual_network.vnet.id
}

resource "azurerm_private_dns_zone" "cosmosdb_zone" {
  name                = "privatelink.mongo.cosmos.azure.com"
  resource_group_name = var.resource_group_name
}

resource "azurerm_private_dns_zone_virtual_network_link" "cosmosdb_link" {
  name                  = "cosmosdb-private-dns-zone"
  private_dns_zone_name = azurerm_private_dns_zone.cosmosdb_zone.name
  resource_group_name   = var.resource_group_name
  virtual_network_id    = azurerm_virtual_network.vnet.id
}
