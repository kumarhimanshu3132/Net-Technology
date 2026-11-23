# Experiment 11: Smart Campus Service Portal – Feature Extension

## AIM
To design and develop a complete, integrated enterprise web application combining ASP.NET Core MVC frontend architecture, RESTful Web API endpoints, and a SQL Server LocalDB relational database using `System.Data.SqlClient` in C# (.NET 8.0) for campus service request lifecycle management.

---

## Description
The **Smart Campus Service Portal** is a unified web solution created for university campuses to streamline maintenance, infrastructure, and facility issue reporting. The application seamlessly integrates three key architectural tiers:

1. **Database Layer (SQL Server LocalDB):** Connects to `(localdb)\MSSQLLocalDB` under database `B2026`. On application startup, it automatically verifies and provisions the `CampusServiceTbl` table schema with appropriate primary keys, data constraints, and pre-seeds five diverse campus service requests across key campus categories (Wi-Fi, Hostel Maintenance, Lab Issue, ID Card, and Library).
2. **REST API Layer (`Controllers/Api/CampusApiController.cs`):** Exposes clean, standardized RESTful endpoints (`GET`, `POST`, `PUT`, `DELETE`) allowing client systems and internal services to programmatically interact with service requests.
3. **MVC Frontend Layer (`Controllers/PortalController.cs` & `Views/Portal/`):** Implements an intuitive, responsive Bootstrap 5 dashboard that consumes the local REST API via `HttpClient` (demonstrating true MVC + API + Database decoupling). It includes real-time KPI metrics, category filtering, one-click ticket resolution, an entry form with inline validation, and a live client-side API status & JSON preview widget.

---

## Key Features
* **Full Three-Tier Integration:** Implements ASP.NET Core MVC consuming an internal REST API through `HttpClient` backed by SQL Server LocalDB ADO.NET data access.
* **Automated Database & Table Provisioning:** Automatically verifies database `B2026` and table `CampusServiceTbl` on application startup, seeding 5 realistic initial campus requests if the table does not exist.
* **RESTful Web API Endpoints:**
  * `GET /api/campus` &ndash; Retrieves all campus tickets with optional category filtering.
  * `GET /api/campus/{id}` &ndash; Retrieves detailed ticket metadata by ID.
  * `POST /api/campus` &ndash; Inserts a new campus service ticket with validated payloads.
  * `PUT /api/campus/{id}/status` &ndash; Updates ticket status (e.g., Open, In Progress, Resolved).
  * `DELETE /api/campus/{id}` &ndash; Deletes a service ticket.
* **Smart Campus Service Dashboard (`Index.cshtml`):**
  * Dynamic KPI metric cards (Total Tickets, Open, In Progress, Resolved).
  * Distinct status badges (`Open`, `In Progress`, `Resolved`) and priority tags (`High`, `Medium`, `Low`).
  * One-click category filter pills (All, Wi-Fi, Hostel Maintenance, Lab Issue, ID Card, Library).
  * Quick **"Mark Resolved"** action button for instant status transitions.
  * Safe ticket deletion action with confirmation prompt.
* **Raise Service Request Form (`Create.cshtml`):**
  * Clean form interface with client-side and server-side inline validation (`asp-validation-for`).
  * Live character counter for issue descriptions (0 / 250 characters).
  * Category and priority dropdown selectors with default selections.
* **Live API Status & JSON Preview Widget:**
  * Live client-side JavaScript `fetch('/api/campus')` integration with telemetry indicators (HTTP Status, Latency in ms, Record Count, Last Sync time).
  * Syntax-formatted JSON code preview demonstrating live REST API + Database data flow for clear verification and screenshots.

---

## Technologies Used
* **Framework:** ASP.NET Core MVC (.NET 8.0)
* **Programming Language:** C# 12
* **API Architecture:** RESTful Web API (`Microsoft.AspNetCore.Mvc.ApiController`)
* **Database:** Microsoft SQL Server LocalDB (`(localdb)\MSSQLLocalDB; Database=B2026`)
* **Data Access:** ADO.NET (`System.Data.SqlClient`)
* **HTTP Client:** `System.Net.Http.IHttpClientFactory`
* **Frontend UI:** HTML5, CSS3, JavaScript (Fetch API), Bootstrap 5, Bootstrap Icons
