# Betting App

The Betting with Open AI POC leverages cutting-edge capabilities of OpenAI’s GPT-3.5 Turbo to redefine traditional sports betting. This project replaces conventional mathematical modules with AI-driven insights, providing a robust and efficient foundation for sports betting pricing solutions. By integrating AI, the system achieves rapid odds calculations, enhanced resource efficiency, and delivers actionable insights for bookmakers. 

This project is structured around three main components.

### Frontend (OpenAI-POC-UI)

Handles user interactions for team and player selection, as well as displaying odds predictions. 
Technology Stack:
- React with Vite: For building the user interface 
- TypeScript: Ensures types safety and reliability in the UI codebase. 

For more details, read the [readme file](https://github.com/southworks/BettingApp-OpenAI/blob/main/OpenAI-POC-UI/README.md) for this project.

### Backend (OpenAI-POC-API): 

Processes player and team data, communicates with OpenAI for predictions, and manages match data storage. 
Technology Stack:
- .NET 8: Provides high-performance APIs for data processing and integration with external services. 
- OpenAI Integration: Generates lineup predictions and odds based on inputs. 
- REST API: Exposes endpoint for frontend communication and internal data processing. 

For more details, read the [readme file](https://github.com/southworks/BettingApp-OpenAI/blob/main/OpenAI-POC-API/README.md) for this project.

### Infrastructure (OpenAI-POC-Infra)

For hosting, scaling and managing resources. 
Technology Stack:
- Kubernetes.
- Terraform.
- CosmosDB.
- Azure DevOps.

For more details, read the [readme file](https://github.com/southworks/BettingApp-OpenAI/blob/main/OpenAI-POC-Infra/README.md) for this project.