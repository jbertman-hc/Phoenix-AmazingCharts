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

The application is in development with the following status:

- Frontend UI is implemented with mock data
- API connection is configured but not yet retrieving real data
- Mock data services provide placeholder information for development and testing

## Features in Development

- **Dashboard**: Display of key metrics and activities
- **Patient Management**: Patient information and history
- **Scheduling**: Appointment scheduling and management
- **Messaging**: Communication between providers
- **Lab Results**: Patient lab results
- **Billing**: Claims processing

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

## Fallback Strategy

The application implements a fallback strategy:

1. Attempts to connect to the API
2. Falls back to mock data when the API is unavailable
3. Periodically checks API health
4. Provides visual indication of the current data source (API or mock)
