# PokemonApp

## Description
The purpose of this app is to be able to retrieve details of a Pokemon based on their name. 
The app also enables you to get fun translations of the Pokemon's description.

## How to run the app
### Steps
- In order to run the app locally, clone the PokemonApp repository in Visual Studio (the app uses .Net 6)
- Pull changes from the master branch
- Run the app locally (There are two ways you can run the app locally. Either with Docker or in your local browser. The desired startup options can be 
toggled in Visual Studio)
- In order to run the app via Docker, make sure you have Docker Desktop is installed and running locally and that it is targeting Windows containers
- When the app is loaded, you will be presented with a Swagger UI. You can test out the app by calling its different endpoints via the Swagger UI

FYI, In order to run the app via Docker, make sure you have Docker Desktop installed and running locally and that it is targeting Windows containers

## Considerations for Production
- Externalise configuration
- Implement API Security to only enable authorised users to access the API
- Implement Infrastructure as Code in order to allow the provisioning of resources to different environments (prod and pre-prod)
- Implement a CI/CD pipeline
- Implement App Monitoring



