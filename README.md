# Phoenix-AmazingCharts EHR Application

A modern Electronic Health Records (EHR) application built with Blazor WebAssembly and MudBlazor, featuring a robust API fallback mechanism.

## Quick Start

Run the application with a single command:

```bash
./restart.sh
```

This script will:
1. Stop any running instances of the application and proxy server
2. Start the Node.js proxy server in the background
3. Launch the Blazor application
4. Provide process IDs and access information

## Project Structure

- **AmazingCharts/**: Main Blazor WebAssembly application
  - **ApiClient/**: API client implementation with fallback mechanism
  - **Components/**: UI components organized by feature
  - **Models/**: Data models
  - **Services/**: Business logic services
  - **wwwroot/**: Static assets and configuration

- **proxy/**: Node.js proxy server for handling CORS issues

## Key Features

- **Dashboard**: Overview of key metrics and recent activities
- **Patient Management**: Comprehensive patient information and history
- **Scheduling**: Appointment scheduling and management
- **Messaging**: Secure communication between providers
- **Lab Results**: View and manage patient lab results
- **Billing**: Claims processing and financial management
- **AI Assistant**: Clinical decision support and documentation assistance

## API Connection

The application connects to an Azure-hosted API service and implements a robust fallback mechanism:

1. **Live API Connection**: Attempts to connect to the real API endpoints
2. **Health Check**: Periodically checks API availability using `/api/Addendum/1` endpoint
3. **Automatic Fallback**: Seamlessly transitions between live data and mock data
4. **Visual Indicator**: Shows the current data source (API or mock)

## Development

### Prerequisites
- .NET 9.0 SDK
- Node.js (for the proxy server)

### First-time Setup

Run the setup script to install dependencies and prepare the environment:

```bash
./setup.sh
```

### Running the Application

1. **Using the restart script (recommended)**:
   ```bash
   ./restart.sh
   ```

2. **Manual startup**:
   - Start the proxy server:
     ```bash
     cd proxy
     node server.js
     ```
   - Start the Blazor application:
     ```bash
     cd Phoenix-AmazingCharts
     dotnet run --urls=http://localhost:5002
     ```

3. **Access the application at**: http://localhost:5002

## Recent Updates

- **API Connection Fixes**: Updated all API endpoints to match the swagger.json specification
- **Health Check Improvements**: Enhanced API health check mechanism using `/api/Addendum/1` endpoint
- **Startup Scripts**: Added convenient setup.sh and restart.sh scripts
- **Port Configuration**: Configured to use port 5002 for the Blazor app and port 3000 for the proxy
- **Project Renamed**: Renamed project from swagger-blank to Phoenix-AmazingCharts
- **Data Source Indicator**: Added visual indicator showing the current data source

## Detailed Documentation

For more detailed information, see the [AmazingCharts README](AmazingCharts/README.md).
