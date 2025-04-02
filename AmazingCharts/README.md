# AmazingCharts EHR Application

## Project Overview
AmazingCharts is a modern Electronic Health Records (EHR) application built using Blazor WebAssembly. It provides a comprehensive solution for healthcare providers to manage patient information, appointments, lab results, messages, and billing.

## Architecture
The application follows a three-tier architecture:

1. **Presentation Layer**: Blazor WebAssembly client with MudBlazor components
2. **Business Logic Layer**: API service hosted at https://apiserviceswin20250318.azurewebsites.net/
3. **Data Access Layer**: Abstracted repositories that can connect to either SQL or FHIR data sources

### Key Components

#### Frontend
- **Blazor WebAssembly**: Core framework for the client-side application
- **MudBlazor**: UI component library providing a modern, responsive design
- **Services**: Client-side services for handling business logic and API communication

#### Backend
- **API Service**: RESTful API hosted on Azure
- **Data Sources**: Flexible data source strategy allowing switching between SQL and FHIR
- **Repository Pattern**: Abstraction layer for data access

## Features
- **Dashboard**: Overview of key metrics and recent activities
- **Patient Management**: Comprehensive patient information and history
- **Scheduling**: Appointment scheduling and management
- **Messaging**: Secure communication between providers
- **Lab Results**: View and manage patient lab results
- **Billing**: Claims processing and financial management
- **AI Assistant**: Clinical decision support and documentation assistance

## Technical Details

### Dependencies
- .NET 9.0
- Blazor WebAssembly
- MudBlazor 6.2.0
- HttpClient for API communication

### API Configuration
The application connects to the Azure-hosted API service at `https://apiserviceswin20250318.azurewebsites.net/`. This configuration is stored in `wwwroot/appsettings.json`.

### API Connection Fixes
The application has been updated to correctly connect to the real API endpoints:

1. **Endpoint Alignment**: All API endpoints in the `EhrApiClient` have been updated to match the exact paths defined in the `swagger.json` specification:
   - Fixed case sensitivity issues (e.g., changed `/api/addendums/{id}` to `/api/Addendum/{id}`)
   - Adjusted patient-specific endpoints to use the correct format
   - Ensured all paths match the exact API specification

2. **Health Check Mechanism**: Improved the API health check to use a valid endpoint:
   - Now uses `/api/Addendum/1` as the health check endpoint
   - Enhanced error logging for better diagnostics
   - Added detailed response content logging for failed health checks

3. **Automatic Fallback**: The system will automatically switch between live API data and mock data:
   - Starts with mock data by default
   - Periodically checks API availability every 30 seconds
   - Seamlessly transitions to live data when the API becomes available
   - Falls back to mock data if API connection is lost

### Recent Updates

### API Connection Fixes
- Updated all API endpoints to match the swagger.json specification
- Fixed case sensitivity issues in endpoint paths (e.g., changed `/api/addendums/{id}` to `/api/Addendum/{id}`)
- Adjusted patient-specific endpoints to use the correct format
- Enhanced the health check mechanism to use the valid endpoint `/api/Addendum/1`
- Improved error logging for better diagnostics during API health checks

### Startup Scripts
- Created a `restart.sh` script that:
  - Kills any running instances of the application and proxy server
  - Starts the Node.js proxy server and the Blazor application
  - Provides process IDs and access information
- Added a `setup.sh` script for first-time setup:
  - Installs proxy server dependencies
  - Makes the restart script executable
  - Checks for required dependencies

### Port Configuration
- Configured to use port 5002 for the Blazor application
- Proxy server runs on port 3000
- Updated all documentation to reflect these port changes

### Data Source Indicator
- Added a visual indicator showing whether the application is using real API data or mock data
- The application automatically switches between data sources based on API availability

### Startup Script
A convenient startup script (`restart.sh`) has been added to the project root to simplify the application startup process:

```bash
# From the project root directory
./restart.sh
```

The script performs the following actions:
1. Stops any running instances of the application and proxy server
2. Starts the Node.js proxy server in the background
3. Launches the Blazor application
4. Provides process IDs and access information

To use the script:
1. Ensure it has execute permissions: `chmod +x restart.sh`
2. Run it from the project root: `./restart.sh`
3. Access the application at: http://localhost:5002
4. Press Ctrl+C to stop all processes

### Mock Data & Fallback Strategy
The application implements a robust data strategy:

1. **ApiProxyService**: Acts as an intermediary between the application and the API endpoint
   - Attempts to connect to the live API first
   - Automatically falls back to mock data if the API is unavailable or returns an error
   - Provides seamless transition between live and mock data without disrupting the user experience

2. **Health Check Mechanism**: Monitors API connectivity
   - Periodically checks the API health endpoint
   - Updates the application state based on API availability
   - Ensures users always have access to data, even during API outages

3. **CORS Handling**: Two approaches for handling CORS issues
   - **Proxy Server**: A Node.js proxy server (`proxy-server.js`) that forwards requests to the API and handles CORS headers
   - **Direct API Access with Fallback**: Configures HttpClient with appropriate headers and falls back to mock data when needed

### Proxy Server
The application includes a simple Node.js proxy server to bypass CORS restrictions when accessing the API:

```javascript
// proxy-server.js
const http = require('http');
const https = require('https');
const url = require('url');

const PORT = 3000;
const TARGET_HOST = 'apiserviceswin20250318.azurewebsites.net';

// Create a proxy server that forwards requests to the API
const server = http.createServer((req, res) => {
  // Set CORS headers
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type, Authorization, X-Requested-With');
  
  // ... request handling logic ...
});

// Start the server
server.listen(PORT, () => {
  console.log(`Proxy server running at http://localhost:${PORT}`);
});
```

To use the proxy server:
1. Run `node proxy-server.js` from the project root
2. Update `wwwroot/appsettings.json` to use `http://localhost:3000/` as the API base URL

### Data Flow
1. User interacts with the Blazor WebAssembly client
2. Client-side services make requests to the ApiProxyService
3. ApiProxyService attempts to fetch data from the live API
4. If successful, live data is returned to the client
5. If unsuccessful, MockDataService provides fallback data
6. Results are displayed using MudBlazor components

## Current State (April 2025)

### Completed Work
- Basic application structure and navigation
- MudBlazor integration for UI components
- Dashboard with key metrics and widgets
- Patient, Schedule, Messages, Labs, and Billing pages
- AI Assistant panel for clinical decision support
- API client configuration for Azure backend

### Recent Fixes
- Resolved MudBlazor component issues (attribute casing, binding)
- Fixed route conflicts between Home and Dashboard components
- Updated API endpoint configuration to use Azure-hosted service
- Enhanced MudBlazor initialization to prevent JavaScript errors
  - Added custom JavaScript initialization to handle missing mudElementRef functions
  - Updated MudBlazor to version 6.2.0 for better .NET 9.0 compatibility
  - Fixed boolean attribute casing issues (changed "True" to "true")
- Improved type handling in Messages component

### Known Issues
- Async methods in service classes lack `await` operators (warnings only)
- Some MudBlazor components have attribute casing warnings

### Next Steps
1. Address remaining warnings in service classes
2. Implement comprehensive error handling
3. Add authentication and authorization
4. Create unit and integration tests
5. Optimize performance for large datasets
6. Enhance offline capabilities

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Modern web browser

### Running the Application
1. Clone the repository
2. Navigate to the AmazingCharts directory
3. Run `dotnet restore` to restore dependencies
4. Run `dotnet run` to start the application
5. Access the application at `http://localhost:5002`

## Data Source Configuration
The application is currently configured to use the Azure-hosted API service. The API service itself is configured to use FHIR as the data source, with the FHIR server URL set to `http://phoenix-fhir:8080/fhir`.
