output "aks_nodes_subnet_id" {
  description = "Subnet ID for AKS nodes"
  value       = azurerm_subnet.aks_nodes_subnet.id
}

output "openai_subnet_id" {
  description = "Subnet ID for OpenAI services"
  value       = azurerm_subnet.openai_subnet.id
}

output "cosmosdb_subnet_id" {
  description = "Subnet ID for CosmosDB"
  value       = azurerm_subnet.cosmosdb_subnet.id
}

output "openai_private_dns_zone_id" {
  description = "Private DNS zone ID for OpenAI Services"
  value       = azurerm_private_dns_zone.zone.id
}

output "cosmosdb_private_dns_zone_id" {
  description = "Private DNS zone ID for Cosmos DB"
  value       = azurerm_private_dns_zone.cosmosdb_zone.id
}
