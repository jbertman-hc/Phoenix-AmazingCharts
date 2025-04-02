# Phoenix-AmazingCharts

A Blazor WebAssembly application designed to connect to the Amazing Charts SQL Server backend via an API.

## Project Overview

Phoenix-AmazingCharts is a healthcare application that provides a web interface for healthcare providers to manage patient information, appointments, lab results, messages, and billing. The application is designed with a data source strategy that allows for future implementation of different data sources.

## Architecture

The application follows a three-tier architecture:

1. **Presentation Layer**: Blazor WebAssembly client with MudBlazor components
2. **Business Logic Layer**: API service 
3. **Data Access Layer**: Repository pattern for data access

### Key Components

#### Frontend (Blazor WebAssembly)
- Located in the `AmazingCharts` directory
- Built with .NET 9.0 and MudBlazor 6.2.0
- Provides a responsive user interface
- Implements client-side services for API communication

#### API Proxy
- Located in the `proxy` directory
- Node.js proxy server to handle CORS issues
- Forwards requests to the Azure-hosted API service
- Runs on port 3000

#### Deployment Scripts
- `setup.sh`: First-time setup script that installs dependencies
- `restart.sh`: Convenience script to start both the proxy server and Blazor application

## Repository Contents

- **AmazingCharts/**: Main Blazor WebAssembly application
  - Components/: UI components
  - Models/: Data models
  - Services/: Client-side services
  - Pages/: Application pages
  - wwwroot/: Static assets

- **proxy/**: API proxy server
  - server.js: Node.js proxy implementation
  - package.json: Dependencies

- **Scripts**:
  - setup.sh: First-time setup script
  - restart.sh: Application startup script

- **Configuration**:
  - swagger.json: API specification
  - package.json: Node.js dependencies

## Current Status

The application is fully connected to the live API:

- Frontend UI is implemented with live data from the API
- API connection is configured and retrieving real data
- Visual indicator shows the live API connection status

## Features

- **Dashboard**: Display of key metrics and activities
- **Patient Management**: Patient information and history
- **Scheduling**: Appointment scheduling and management
- **Messaging**: Communication between providers
- **Lab Results**: Patient lab results
- **Billing**: Claims processing
- **Theme Settings**: Light/dark mode toggle for user preference

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Node.js and npm
- Modern web browser

### Setup and Running
1. Clone the repository
2. Run the setup script: `./setup.sh`
3. Start the application: `./restart.sh`
4. Access the application at http://localhost:5002

## API Configuration

The application is configured to connect to an Azure-hosted API service at `https://apiserviceswin20250318.azurewebsites.net/`. The proxy server handles CORS issues and forwards requests to this API.

## Data Source Strategy

The application is now configured to use the live API exclusively:

1. Connects directly to the API for all data
2. Provides error handling for API unavailability
3. Displays visual indication of the live API connection
4. Performs periodic health checks to monitor API status
