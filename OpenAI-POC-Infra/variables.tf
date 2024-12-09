variable "resource_group_location" {
  type        = string
  description = "Location of the resource group"
}

variable "resource_group_name" {
  type = string
  description = "Name of the resource group"
}

variable "namespaces" {
  description = "List of Kubernetes namespaces for which to create identities and federated credentials."
  type        = list(string)
}
