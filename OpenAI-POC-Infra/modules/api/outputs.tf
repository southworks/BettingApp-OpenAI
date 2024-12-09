output "openai_private_endpoint" {
  value = module.cognitive_services_account.private_endpoints
}

output "cosmosdb_account_id" {
  value       = azurerm_cosmosdb_account.acc.id
}

output "cosmosdb_account_name" {
  value       = azurerm_cosmosdb_account.acc.name
}