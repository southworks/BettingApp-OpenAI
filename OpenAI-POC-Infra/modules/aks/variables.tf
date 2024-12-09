variable "resource_group_name" {
  description = "The name of the resource group"
  type        = string
}

variable "location" {
  description = "The location of the resource group"
  type        = string
}

variable "acr_id" {
  description = "The ID of the container registry"
  type        = string
}

variable "nodes_subnet_id" {
  description = "The ID of the nodes subnet"
  type        = string
}
