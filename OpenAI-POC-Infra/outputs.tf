output "resource_group_name" {
  value = azurerm_resource_group.rg.name
}

output "acr_login_server" {
  value = azurerm_container_registry.acr.login_server
}

output "aks_cluster_name" {
  value = module.aks.aks_cluster_name
}

output "workload_identities_client_id" {
  description = "The client IDs of the user-assigned identities created for each namespace."
  value = join(",",[
    for ns in var.namespaces : "${ns}=${azurerm_user_assigned_identity.namespace_identities[ns].client_id}"
  ])
}