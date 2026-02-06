# Production Report API


**ProductionReportAPI** 
is a centralized backend service designed to integrate data from various manufacturing database sources within the PTB Auto internal environment. This API serves as the bridge between production machines, OEE systems, and inspection machines (AOI).

The system utilizes a **Multi-Database** architecture to separate data domains:
1.  **Station Status:** Real-time machine status monitoring.
2.  **PTB OEE Data:** Production efficiency calculations.
3.  **AOI (Automated Optical Inspection):** Quality control and visual inspection data.

##  Key Features

### 1. Multi-Database Integration
* Aggregates data from three separate databases (`station_status`, `ptb_oee_data`, `aoi`) into a single API gateway.
* Ensures data integrity across different systems.

### 2. OEE Calculation Engine
* Automatically calculates **Availability, Performance, and Quality**.
* Retrieves production logs from the `ptb_oee_data` database.
* Supports dynamic shift analysis.

### 3. AOI & Quality Tracking
* Connects directly to the `aoi` database to retrieve defect data.
* Serves visual inspection data (JSON & Image URLs) to the frontend.

### 4. Machine Status Timeline
* Visualizes machine activity timelines from the `station_status` database.
* Essential for tracking downtime and performance per workstation.

##  Tech Stack

* **Framework:** ASP.NET Core Web API
* **Database:** MySQL (Multi-connection)
* **ORM:** Entity Framework Core / ADO.NET
* **Server:** Internal Server (10.176.33.76)

##  Configuration & Installation

### 1. Clone Repository (Internal Network)
Ensure you are connected to the company's internal network/VPN.

```bash
git clone [http://10.176.33.76:3000/PTB_Auto/ProductionReportAPI.git](http://10.176.33.76:3000/PTB_Auto/ProductionReportAPI.git)
cd ProductionReportAPI
