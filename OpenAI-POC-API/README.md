# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test

To run the API locally, follow these steps:

## Setting Up User Secrets

User Secrets allow you to store sensitive information securely during development. To configure User Secrets:

1. Open your terminal and navigate to the project directory.

2. Initialize User Secrets for your project:

    ```bash
    dotnet user-secrets init
    ```

3. Now, you can set your secrets using the following commands:

    ```bash
    dotnet user-secrets set "AzureOpenAI:ResourceUri" "resource_uri_here"
    dotnet user-secrets set "AzureOpenAI:ApiKey" "api_key_here"
    dotnet user-secrets set "CosmosDb:ConnectionString" "connection_string_here"
    ```

    Alternatively, you can manually edit the secrets file located at: `%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json`

4. Build the project: Run the following command to build the project:

    ```bash
    dotnet build
    ```

5. Run the project: To execute the API with HTTPS, use the following command:
    
    ```bash
    dotnet run --launch-profile "https"
    ```

## Set up Cosmos DB emulator locally using Docker (assuming Windows machine)

1. Go to the Microsoft official documentation page where Mongo will be automatically selected as your API:

[How to install Cosmos DB emulator locally](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=windows%2Ccsharp&pivots=api-mongodb)

2. To install the emulator, select the Docker (Linux container) option:

    ```bash
    docker pull mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest
    ```

Note that you will be downloading the image "latest" otherwise you will face a licence expired known issue [Evaluation period expired](https://stackoverflow.com/questions/74440386/azure-cosmos-db-emulator-linux-image-does-not-start-error-the-evaluation-perio)

3. Start the emulator through Powershell. The ports specified in the official docs do not correspond with the ones in Cosmos DB therefore there will be a slight change from port 10250 to 10255 and you will be using the latest image instead of the mongodb one.

    ```bash
    $parameters = @(
        "--publish", "8081:8081"
        "--publish", "10255:10255"
        "--env", "AZURE_COSMOS_EMULATOR_ENABLE_MONGODB_ENDPOINT=4.0"
        "--name", "windows-emulator"
        "--detach"
    )
    docker run @parameters mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest
    ```
4. To import the certificate you need to run the following command

    ```bash
    $parameters = @{
        Uri = 'https://localhost:8081/_explorer/emulator.pem'
        Method = 'GET'
        OutFile = 'emulatorcert.crt'
        SkipCertificateCheck = $True
    }
    Invoke-WebRequest @parameters

But as of Powershell version 5.1 the SkipCertificateCheck does not work. The workaround for this is to run the following command [See Stackoverflow explanation](https://stackoverflow.com/a/59924223/5293466):

    ```bash
    add-type @"
     using System.Net;
     using System.Security.Cryptography.X509Certificates;
     public class TrustAllCertsPolicy : ICertificatePolicy {
         public bool CheckValidationResult(
             ServicePoint srvPoint, X509Certificate certificate,
             WebRequest request, int certificateProblem) {
                 return true;
             }
      }
     "@
    [System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy
    Invoke-WebRequest https://localhost:8081/_explorer/emulator.pem
    ```

5. Navigate to [Cosmos DB Emulator](https://localhost:8081/_explorer/index.html) and go to the site although it shows to be insecure and you can access the dashboard

6. If you want to create a collection through the data explorer, the partition key should start with / like "/TenantId" for example, but that will fail. Instead you can do /'$v'/TenantId/'$v' and that will work.

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)