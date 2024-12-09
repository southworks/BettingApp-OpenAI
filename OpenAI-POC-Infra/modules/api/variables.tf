variable "resource_group_name" {
  type = string
}

variable "location" {
  type = string
}

variable "openai_private_dns_zone_id" {
  type    = string
}

variable "openai_subnet_id" {
  type    = string
}

variable "openai_account_name" {
  type    = string
}

variable "custom_subdomain_name" {
  type    = string
}

variable "chat_model_name" {
  type    = string
}

variable "chat_model_version" {
  type    = string
  default = "0613"
}

variable "cosmosdb_account_name" {
  type    = string
}

variable "cosmosdb_name" {
  type    = string
}

variable "cosmosdb_subnet_id" {
  type    = string
}

variable "cosmosdb_private_dns_zone_id" {
  type    = string
}