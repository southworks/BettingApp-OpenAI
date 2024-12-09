terraform {
  required_version = ">=1.8"

  required_providers {
    azapi = {
      source  = "azure/azapi"
      version = "~>1.5"
    }
    azurerm = {
      source = "hashicorp/azurerm"
      version = "~>3.8"
    }
    random = {
      source = "hashicorp/random"
      version = "~>3.0"
    }
    time = {
      source  = "hashicorp/time"
      version = "0.12.1"
    }
    modtm = {
      source  = "Azure/modtm"
      version = "~> 0.3"
    }
  }

  backend "azurerm" {}
}

provider "azurerm" {
  features {}
}