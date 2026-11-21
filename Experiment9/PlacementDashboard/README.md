# Experiment 9: Placement Dashboard using Data Controls

## AIM
To design and develop an interactive Campus Placement Dashboard web application using ASP.NET Core MVC (.NET 8.0), ADO.NET (`System.Data.SqlClient`), and Microsoft SQL Server (LocalDB) to demonstrate data-driven controls, dynamic filtering, sorting, CRUD operations, and reporting.

## Description
This project implements a complete campus placement management system and analytics dashboard for academic institutions. Built using ASP.NET Core MVC architecture, it connects to a local SQL Server database (`(localdb)\MSSQLLocalDB; Database=B2026`) via raw ADO.NET queries without an ORM. The system automatically provisions the required database and `PlacementTbl` table with realistic sample records upon first run. It delivers a rich data grid control with real-time searching, status filtering, multi-column sorting, complete record lifecycle management (Create, Read, Update, Delete), and an exportable/printable company-wise placement summary report.

## Key Features
- **Summary KPI Cards**: Real-time analytical metric cards displaying Total Students, Placed Count, Placement Rate (%), Highest Package (LPA), and Average Package (LPA).
- **Interactive Data Grid Control**:
  - Live text search across Student Name, Enrollment Number, Company Name, and Role.
  - Placement Status filter dropdown (`All`, `Placed`, `Pending`, `Rejected`).
  - Bi-directional column sorting (ASC/DESC) on Package, Student Name, Enrollment Number, Company Name, and Status.
  - Status badges with distinct visual indicators for quick identification.
- **Full CRUD Management**:
  - Add New Placement Record (`Create.cshtml`) with comprehensive field validation.
  - Edit Placement Record (`Edit.cshtml`) with pre-populated values and model validation.
  - Delete Placement Record (`Delete.cshtml`) with explicit safety confirmation.
- **Company-wise Placement Report (`Report.cshtml`)**:
  - Consolidated aggregate table grouped by recruiting companies.
  - Details candidates count, placed count, pending/rejected counts, roles offered, and package ranges.
  - Browser-native "Print / Save as PDF" button with dedicated `@media print` styling that formats the page for official documentation.
- **Automatic Database Initialization**:
  - Automatically verifies and creates database `B2026` and `PlacementTbl` on application startup.
  - Seeds 8 realistic Indian campus placement records if no data exists.
- **Modern Responsive UI**: Built with Bootstrap 5 and Bootstrap Icons for a modern, responsive layout on mobile, tablet, and desktop screens.

## Technologies Used
- **Framework**: ASP.NET Core MVC (.NET 8.0)
- **Programming Language**: C# 12
- **Data Access**: ADO.NET (`System.Data.SqlClient`) with parameterized SQL commands
- **Database**: Microsoft SQL Server LocalDB (`(localdb)\MSSQLLocalDB`, Database: `B2026`)
- **Frontend & Styling**: Razor View Engine (.cshtml), Bootstrap 5.3, Bootstrap Icons, Custom CSS
- **Client-Side Libraries**: jQuery, jQuery Validation, jQuery Unobtrusive Validation
