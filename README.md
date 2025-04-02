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

**IMPORTANT NOTE:** While the application is connected to the live API, many of the endpoints listed in this README are not yet implemented on the server side. The client-side interfaces (`IEhrApiClient` and `IApiProxyService`) define the required endpoints, but the actual server-side implementations for many features need to be developed.

## Data Source Strategy

The application is designed to use the live API exclusively:

1. Connects directly to the API for all data
2. Provides error handling for API unavailability
3. Displays visual indication of the live API connection
4. Performs periodic health checks to monitor API status

**Current Implementation Status:**
- The application successfully connects to the API and can retrieve data from existing endpoints
- Many of the endpoints listed in the "Required API Endpoints" section below need to be implemented on the server side
- The client-side interfaces and models have been prepared for all required functionality
- Server-side implementation is needed to complete the full feature set

## Required API Endpoints

The application requires the following API endpoints to function properly:

**Implementation Note:** The tables below indicate the current implementation status of each endpoint:
- **Available in API**: Endpoint exists in the swagger.json and is available in the current API
- **Implemented in Client**: Client-side code has been implemented to call this endpoint
- **Needs Implementation**: Endpoint needs to be implemented on the server side

### Patient Data Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Demographics/{id}` | GET | Get patient demographics by ID | Available in API, Implemented in Client |
| `/api/Demographics` | GET | Get all patients | Available in API, Implemented in Client |
| `/api/Demographics/search` | GET | Search patients by criteria | Needs Implementation |
| `/api/PatientIndex/{id}` | GET | Get patient index by ID | Available in API, Implemented in Client |
| `/api/PatientIndex/all` | GET | Get all patient indexes | Needs Implementation |

### Clinical Data Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Allergy/{id}` | GET | Get allergy by ID | Available in API, Implemented in Client |
| `/api/Allergy/patient/{patientId}` | GET | Get allergies by patient ID | Available in API, Implemented in Client |
| `/api/Medication/{id}` | GET | Get medication by ID | Available in API, Implemented in Client |
| `/api/Medication/patient/{patientId}` | GET | Get medications by patient ID | Available in API, Implemented in Client |
| `/api/LabResult/{id}` | GET | Get lab result by ID | Available in API, Implemented in Client |
| `/api/LabResult/patient/{patientId}` | GET | Get lab results by patient ID | Available in API, Implemented in Client |
| `/api/Problem/{id}` | GET | Get problem by ID | Needs Implementation |
| `/api/Problem/patient/{patientId}` | GET | Get problems by patient ID | Needs Implementation |
| `/api/Problem/patient/{patientId}/active` | GET | Get active problems by patient ID | Needs Implementation |
| `/api/Problem` | POST | Create a new problem | Needs Implementation |
| `/api/Problem/{id}/resolve` | PUT | Resolve a problem | Needs Implementation |
| `/api/Immunization/{id}` | GET | Get immunization by ID | Needs Implementation |
| `/api/Immunization/patient/{patientId}` | GET | Get immunizations by patient ID | Needs Implementation |
| `/api/Immunization` | POST | Create a new immunization | Needs Implementation |
| `/api/ClinicalNote/{id}` | GET | Get clinical note by ID | Needs Implementation |
| `/api/ClinicalNote/patient/{patientId}` | GET | Get clinical notes by patient ID | Needs Implementation |
| `/api/ClinicalNote/encounter/{encounterId}` | GET | Get clinical notes by encounter ID | Needs Implementation |
| `/api/ClinicalNote` | POST | Create a new clinical note | Needs Implementation |

### Prescription Endpoints (eRx)

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Prescription/{id}` | GET | Get prescription by ID | Needs Implementation |
| `/api/Prescription/patient/{patientId}` | GET | Get prescriptions by patient ID | Needs Implementation |
| `/api/Prescription/patient/{patientId}/active` | GET | Get active prescriptions by patient ID | Needs Implementation |
| `/api/Prescription` | POST | Create a new prescription | Needs Implementation |
| `/api/Prescription/{id}/refill` | PUT | Refill a prescription | Needs Implementation |

### Order Endpoints (CPOE)

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Order/{id}` | GET | Get order by ID | Needs Implementation |
| `/api/Order/patient/{patientId}` | GET | Get orders by patient ID | Needs Implementation |
| `/api/Order/pending` | GET | Get pending orders | Needs Implementation |
| `/api/Order` | POST | Create a new order | Needs Implementation |
| `/api/Order/{id}/status` | PUT | Update order status | Needs Implementation |

### Encounter Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Encounter/{id}` | GET | Get encounter by ID | Needs Implementation |
| `/api/Encounter/patient/{patientId}` | GET | Get encounters by patient ID | Needs Implementation |
| `/api/Encounter/patient/{patientId}/current` | GET | Get current encounter for patient | Needs Implementation |
| `/api/Encounter` | POST | Create a new encounter | Needs Implementation |
| `/api/Encounter/{id}/close` | PUT | Close an encounter | Needs Implementation |

### Referral Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Referral/{id}` | GET | Get referral by ID | Needs Implementation |
| `/api/Referral/patient/{patientId}` | GET | Get referrals by patient ID | Needs Implementation |
| `/api/Referral/pending` | GET | Get pending referrals | Needs Implementation |
| `/api/Referral` | POST | Create a new referral | Needs Implementation |
| `/api/Referral/{id}/status` | PUT | Update referral status | Needs Implementation |

### Insurance Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Insurance/{id}` | GET | Get insurance by ID | Needs Implementation |
| `/api/Insurance/patient/{patientId}` | GET | Get insurance by patient ID | Needs Implementation |
| `/api/Insurance/providers` | GET | Get all insurance providers | Needs Implementation |
| `/api/Insurance/verify` | POST | Verify insurance eligibility | Needs Implementation |

### Document Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Document/{id}` | GET | Get document by ID | Needs Implementation |
| `/api/Document/patient/{patientId}` | GET | Get documents by patient ID | Needs Implementation |
| `/api/Document` | POST | Upload a new document | Needs Implementation |

### Administrative Endpoints

| Endpoint | Method | Description | Implementation Status |
|----------|--------|-------------|----------------------|
| `/api/Appointment/{id}` | GET | Get appointment by ID | Available in API, Implemented in Client |
| `/api/Appointment/date-range` | GET | Get appointments by date range | Available in API, Implemented in Client |
| `/api/Message/{id}` | GET | Get message by ID | Available in API, Implemented in Client |
| `/api/Message/recipient/{recipientId}` | GET | Get messages by recipient ID | Available in API, Implemented in Client |
| `/api/User/{id}` | GET | Get user by ID | Needs Implementation |
| `/api/User` | GET | Get all users | Needs Implementation |
| `/api/User/username/{username}` | GET | Get user by username | Needs Implementation |
| `/api/User/authenticate` | POST | Authenticate user | Needs Implementation |
| `/api/Provider/{id}` | GET | Get provider by ID | Needs Implementation |
| `/api/Provider` | GET | Get all providers | Needs Implementation |
| `/api/Provider/specialty/{specialty}` | GET | Get providers by specialty | Needs Implementation |
| `/api/health` | GET | Check API health | Available in API, Implemented in Client |

## SQL Database Structure

The application connects to the Amazing Charts SQL Server database, which has the following key tables:

- **Demographics**: Contains patient demographic information
- **PatientIndex**: Links internal and external patient IDs
- **ListAllergies**: Patient allergies
- **ListMEDS**: Patient medications
- **LabResults**: Laboratory results
- **Scheduling**: Appointment information
- **PatientMessages**: Patient-provider communication
- **Users**: System users and authentication information
- **Providers**: Healthcare providers information
- **Prescriptions**: Medication prescriptions
- **Orders**: Clinical orders (CPOE)
- **ClinicalNotes**: Patient clinical notes
- **Immunizations**: Patient immunization records
- **Encounters**: Patient encounters/visits
- **Problems**: Patient problem list
- **Referrals**: Patient referrals to specialists
- **Insurance**: Patient insurance information
- **Documents**: Patient documents and attachments

To implement the missing endpoints, we need to:

1. Add methods to retrieve and manage data in the EhrApiClient
2. Update the ApiProxyService to support these methods
3. Create appropriate UI components to display and interact with the data
4. Implement proper error handling and validation for all endpoints

## Next Steps for API Implementation

To complete the full functionality of the Phoenix-AmazingCharts application, the following steps need to be taken for API implementation:

### 1. Server-Side Endpoint Implementation

The server-side API needs to be extended to include all the endpoints listed in the "Required API Endpoints" section that are currently marked as "Needs Implementation". This includes:

- User management endpoints
- Provider management endpoints
- Prescription (eRx) endpoints
- Order (CPOE) endpoints
- Clinical notes endpoints
- Immunization endpoints
- Encounter/visit endpoints
- Problem list endpoints
- Referral endpoints
- Insurance endpoints
- Document management endpoints

### 2. Data Model Implementation

The server-side data models corresponding to the client-side models need to be implemented:

- UserModel
- ProviderModel
- PrescriptionModel
- OrderModel
- ClinicalNoteModel
- ImmunizationModel
- EncounterModel
- ProblemModel
- ReferralModel
- InsuranceModel
- DocumentModel

### 3. Database Schema Extensions

The existing SQL database schema may need to be extended to support the new data models and functionality:

- Add new tables for entities not currently in the database
- Add relationships between existing and new tables
- Ensure proper indexing for performance
- Implement data validation constraints

### 4. API Documentation Updates

Once the server-side implementation is complete:

- Update the swagger.json file to include all new endpoints
- Update this README with accurate implementation status
- Document any API usage considerations or limitations

### 5. Testing and Validation

Comprehensive testing should be performed to ensure:

- All endpoints function as expected
- Error handling is robust
- Performance is acceptable under load
- Security considerations are addressed

### Implementation Priority

The recommended implementation order is:

1. Core clinical data endpoints (Problems, Immunizations, Clinical Notes)
2. Encounter management endpoints
3. Order and Prescription endpoints
4. User and Provider management endpoints
5. Referral and Insurance endpoints
6. Document management endpoints

This prioritization ensures that the most critical clinical functionality is implemented first, followed by administrative and supporting features.
