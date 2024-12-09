module "cognitive_services_account" {
  source = "git::https://github.com/Azure/terraform-azurerm-avm-res-cognitiveservices-account.git?ref=b71f3b299a3b4bbf13143d566b2b82edca977a30"

  resource_group_name = var.resource_group_name
  location            = var.location
  name                = var.openai_account_name
  kind                = "OpenAI"
  sku_name            = "S0"

  cognitive_deployments = {
    "gpt-35-turbo" = {
      name = var.chat_model_name
      model = {
        format  = "OpenAI"
        name    = var.chat_model_name
        version = var.chat_model_version
      }
      scale = {
        type     = "Standard"
        capacity = 10
      }
    }
  }

  network_acls = {
    default_action = "Deny"
    virtual_network_rules = toset([{
      subnet_id = var.openai_subnet_id
    }])
  }

  private_endpoints = {
    pe_endpoint = {
      name                            = "openaipoc-pe"
      private_dns_zone_resource_ids   = toset([var.openai_private_dns_zone_id])
      private_service_connection_name = "openaipoc-pe-connection"
      subnet_resource_id              = var.openai_subnet_id
    }
  }
}
