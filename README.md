# Industrial IoT & OEE Monitoring System

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![Platform](https://img.shields.io/badge/platform-.NET%20%7C%20Vue.js-blue)
![License](https://img.shields.io/badge/license-MIT-orange)

## 📖 Project Overview

The **Industrial IoT System** is a full-stack solution designed to simulate and monitor manufacturing processes. It acts as a **Digital Twin**, capturing real-time telemetry from industrial machinery to calculate **Overall Equipment Effectiveness (OEE)**.

This project demonstrates the end-to-end flow of industrial data: from data generation (simulation) -> ingestion (API) -> storage (SQL) -> visualization (Dashboard).

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