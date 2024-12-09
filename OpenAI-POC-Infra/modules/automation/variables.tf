variable "resource_group_name" {
  description = "The name of the resource group"
  type        = string
}

variable "resource_group_id" {
  description = "The ID of the resource group"
  type        = string
}

variable "location" {
  description = "The location of the resource group"
  type        = string
}

variable "aks_cluster_id" {
  description = "Kubernetes cluster ID to assign permissions"
  type        = string
}

variable "aks_cluster_name" {
  description = "Kubernetes cluster name to set job schedule"
  type        = string
}

