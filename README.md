# Industrial IoT & OEE Monitoring System

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![Platform](https://img.shields.io/badge/platform-.NET%20%7C%20Vue.js-blue)
![License](https://img.shields.io/badge/license-MIT-orange)

## 📖 Project Overview

The **Industrial IoT System** is a full-stack solution designed to simulate and monitor manufacturing processes. It acts as a **Digital Twin**, capturing real-time telemetry from industrial machinery to calculate **Overall Equipment Effectiveness (OEE)**.

This project demonstrates the end-to-end flow of industrial data: from data generation (simulation) -> ingestion (API) -> storage (SQL) -> visualization (Dashboard).

### System Architecture
The system is built with a modular architecture that separates responsibilities between simulation, business logic, and data presentation:
1. Backend Service (ProductionReportAPI)
Acts as the central bridge (API Gateway) integrating data from various database sources.
* **Multi-Database Architecture:** Separates data domains for better integrity:
* **OEE Engine:** Automatically calculates **Availability**, **Performance**, and **Quality** metrics.
2. Frontend Dashboard
A reactive web-based user interface for operators and management.
* Data visualization using **ECharts**.
* Real-time update support (via SignalR/Polling).
* Report export features (PDF/Excel) and multi-language support (i18n).
3. IoT Simulator
A console application acting as a "Robot" agent on the production floor.
* Uses the **MQTT** protocol for data transmission.
* Simulates real-world scenarios: variable **Cycle Time**, defect probability (30% fail vs 70% pass), and machine error codes.




### Key Features
* **Real-time Dashboard:** Monitor machine status (Running, Idle, Down) and live production counts.
* **OEE Analysis:** Automatic calculation of Availability, Performance, and Quality metrics.
* **IoT Simulation:** A dedicated simulator module that generates realistic machine data streams (cycle times, temperature, error codes).
* **Historical Reporting:** View past production performance.

---

## 🛠️ Technology Stack

This project is built using a modern, scalable architecture:

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Backend** | **C# / ASP.NET Core** | RESTful API for business logic & data processing. |
| **Frontend** | **Vue.js (Vite)** | Reactive web dashboard for data visualization. |
| **Database** | **MySQL** | Relational database for storing production logs and OEE data. |
| **Simulation** | **.NET Console App** | Simulates IoT devices/PLCs sending telemetry data. |

---

## 🚀 Key Features
📊 **Dashboard Monitoring**
Live Machine Status: Visual indicators for machine states (Green: Running, Red: Down, Yellow: Idle).
OEE Analysis: Daily and per-shift OEE trend charts.
AOI Inspection: Gallery of visual inspection results (OK vs NG product images) fetched directly from the database.

🏭 **Production & Quality**
Defect Tracking: Specific tracking of defect types (e.g., scratch, dirty, shift).
Activity Timeline: Visualization of machine activity timelines for downtime analysis.
Automated Reporting: Capability to export production data to Excel/PDF formats.

🤖 **Intelligent Simulation (Digital Twin)**
Probabilistic Logic: The simulator doesn't just send random data; it follows realistic failure probability logic.
Dual-Agent Architecture: Consists of a Publisher (Robot) and a Subscriber (Data Logger) running asynchronously.

## ⚙️ **Installation & Setup Guide**
Follow these steps to run the system locally.

**Prerequisites**
* NET SDK 6.0 or later.
* Node.js & npm.
* MySQL Server (XAMPP/Laragon/Docker).

**1. Database Configuration**
Execute the SQL scripts found in the database/ folder or iot-simulator/schema.sql to create the necessary table structures (ptbproductiondata, etc.) in your MySQL instance.

**2. Running the Backend API**
```
cd backend-api
# Edit database connection strings in appsettings.json if necessary
dotnet restore
dotnet run
```
the API will run on http://localhost:5000 (or the configured port). 

**3. Running the Frontend Dashboard**
```
cd frontend-dashboard
npm install
npm run dev
```
Access the dashboard via your browser at the address shown (usually http://localhost:5173).

**4. Running the Simulator (For Dummy Data)**
```
cd iot-simulator/src/StationRunningStateService
dotnet run
```
The simulator will start sending fake telemetry data to the database, which will then appear on the Dashboard in real-time.




## 📂 Repository Structure

The project is organized into modular services:

```text
Industrial-IoT-System/
├── backend-api/          # ASP.NET Core Web API (Controllers, Services, Models)
├── frontend-dashboard/   # Vue.js Source Code (Components, Views, Store)
├── iot-simulator/        # C# Console App for generating dummy sensor data
├── database/             # SQL Scripts for schema creation and seeding
├── docs/                 # Architecture diagrams and technical documentation
├── .gitignore            # Git configuration
└── README.md             # Project documentation 
```

🔮 Future Roadmap
[ ] Integration of Telegram/Email notifications when a machine is Down.

[ ] Full containerization support using Docker & Docker Compose.

[ ] Implementation of user authentication (JWT) for Dashboard access.

👤 Author
Roy Siregar
Passionate about Industrial Automation, IoT, and Backend Engineering.

Note: Ensure all connection strings in appsettings.json (Backend) and Program.cs (Simulator) are adjusted to your local MySQL credentials before running the application.

