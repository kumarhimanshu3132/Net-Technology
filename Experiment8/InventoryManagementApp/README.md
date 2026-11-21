# Experiment 8: Inventory Management Module

AIM: Develop Inventory Management Module using ADO.NET & CRUD Operations.

## Description

This project implements a full-stack Inventory Management Module in ASP.NET Core MVC connected directly to Microsoft SQL Server LocalDB (`B2026`) using raw ADO.NET classes without an ORM. It supports complete Create, Read, Update, and Delete (CRUD) operations with parameterized SQL queries.

Key Features:

- Direct database connectivity using `SqlConnection`, `SqlCommand`, and `SqlDataReader` (`System.Data.SqlClient`).
- Complete **CRUD Operations** (Add new inventory items, view live stock dashboard, edit item details, and delete records).
- Parameterized SQL queries to prevent SQL Injection and ensure safe data transactions.
- Server-side model validation using Data Annotations (`[Required]`) in `Inventory.cs`.

## Technologies Used

- ASP.NET Core MVC (.NET 8.0)
- C# & ADO.NET (`System.Data.SqlClient`)
- SQL Server LocalDB (`(localdb)\MSSQLLocalDB`)
- HTML5 / CSS3 / Bootstrap 5
