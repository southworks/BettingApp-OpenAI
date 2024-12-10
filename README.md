# Betting App

Betting with Open AI POC leverages cutting-edge capabilities of OpenAI’s GPT-3.5 Turbo to redefine traditional sports betting. This project can replace or complement conventional mathematical modules with AI-driven insights, providing a robust and efficient foundation for sports betting pricing solutions.  

By integrating AI, the system achieves rapid pricing calculations, enhanced resource efficiency, and delivers actionable insights for bookmakers, revolutionizing how betting markets operate. 

Getting early and key market data can now be independent from other data providers and would allow bookmakers to derive other markets since this is now driven by AI. We have compared several football fixtures with high-street bookmaker prices and the accuracy of the odds generated are pretty close to them, which opens an opportunity to improve speed in market generation and availability, reduce dependencies with third parties, reduce the pricing model infrastructure where applies and therefore reduce costs and maintainability. 

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