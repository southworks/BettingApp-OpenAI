# Terraform Setup for OpenAI POC Infrastructure

This README provides instructions on how to set up and run Terraform for managing the OpenAI POC infrastructure in Azure.

## Prerequisites

Before running Terraform locally, ensure that you have the following installed:

- [Terraform CLI](https://learn.hashicorp.com/tutorials/terraform/install-cli)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- Access to the required Azure resources (Storage Account, Resource Groups, etc.)

### 1. **Clone the Repository**

Start by cloning the repository that contains the Terraform configuration files:

```bash
git clone https://southworks@dev.azure.com/southworks/OpenAI-POC/_git/OpenAI-POC-Infra
cd OpenAI-POC-Infra
```

### 2. **Azure Login**

You must log in to your Azure account via the Azure CLI:

```bash
az login
```

This will open a browser window for you to authenticate with your Azure account. Ensure that you have the necessary permissions, such as `Contributor` or `Storage Blob Data Contributor`, to access the Azure resources.

### 3. **Configure the Backend**

Terraform uses a remote backend in Azure to store the state file. The backend is configured in the `providers.tf` file. You should ensure that the following values are correct:

```hcl
terraform {
  backend "azurerm" {
    storage_account_name   = "<storage_account_name>"
    container_name         = "<container_name>"
    key                    = "<key>"
    resource_group_name    = "<resource_group_name>"
  }
}
```

All variables defined in the Terraform files are specified in the corresponding Variable Group inside the [Library](https://dev.azure.com/southworks/OpenAI-POC/_library?itemType=VariableGroups&view=VariableGroupView&variableGroupId=184&path=openaipoc) section of the pipeline itself in Azure DevOps. Ensure you have access to the Variable Group in Library > openaipoc > Variable Group to configure environment variables properly.


### 4. **Initialize Terraform**

Initialize the Terraform environment by downloading all required providers and configuring the backend:

```bash
terraform init
```

This command will set up Terraform to use the Azure Storage Account as the backend for the state file.

### 5. **Plan the Infrastructure Changes**

Once initialized, you can preview the changes Terraform will make by running the `terraform plan` command:

```bash
terraform plan
```

This will display any changes Terraform will make to the existing infrastructure, such as creating, updating, or destroying resources.

### 6. **Apply the Changes**

To apply the changes to the infrastructure, use the `terraform apply` command. You will need to confirm the operation by typing `yes`:

```bash
terraform apply
```

We currently have an Approval Environment in the [Azure Pipeline](https://dev.azure.com/southworks/OpenAI-POC/_environments/186?view=deployments), which means that once the approval is granted, the terraform apply process will be automatically triggered to apply the changes to the infrastructure.

## Useful Links

- [Terraform CLI Documentation](https://www.terraform.io/docs/cli/index.html)
- [Azure CLI Documentation](https://docs.microsoft.com/en-us/cli/azure/?view=azure-cli-latest)
