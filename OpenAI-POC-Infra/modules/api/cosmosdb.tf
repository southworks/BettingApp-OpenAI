resource "azurerm_cosmosdb_account" "acc" {
  # checkov:skip=CKV_AZURE_99:Public access for simplicity
  # checkov:skip=CKV_AZURE_100:Service-managed keys are enough for POC
  # checkov:skip=CKV_AZURE_101:Public access for simplicity
  name                               = var.cosmosdb_account_name
  location                           = var.location
  resource_group_name                = var.resource_group_name
  offer_type                         = "Standard"
  kind                               = "MongoDB"
  automatic_failover_enabled         = false
  access_key_metadata_writes_enabled = false
  free_tier_enabled                  = true
  public_network_access_enabled      = false
  is_virtual_network_filter_enabled  = true
  mongo_server_version               = 4.2 

  capabilities {
    name = "EnableMongo"
  }

  consistency_policy {
    consistency_level = "Eventual"
  }

  geo_location {
    location          = var.location
    failover_priority = 0
  }

  capacity {
    total_throughput_limit = 1000
  }
}


resource "azurerm_private_endpoint" "cosmosdb_private_endpoint" {
  name                = "cosmosdb-pe"
  location            = var.location
  resource_group_name = var.resource_group_name
  subnet_id           = var.cosmosdb_subnet_id
  private_dns_zone_group {
    name                 = "cosmosdb-dns-zone-group"
    private_dns_zone_ids = [ var.cosmosdb_private_dns_zone_id ]
  }

  private_service_connection {
    name                           = "cosmosdb-pe-connection"
    private_connection_resource_id = azurerm_cosmosdb_account.acc.id
    is_manual_connection           = false
    subresource_names              = ["mongodb"]
  }
}

resource "azurerm_cosmosdb_mongo_database" "mongodb" {
  name                = var.cosmosdb_name
  resource_group_name = azurerm_cosmosdb_account.acc.resource_group_name
  account_name        = azurerm_cosmosdb_account.acc.name
  
  autoscale_settings {
    max_throughput = 1000
  }
}
