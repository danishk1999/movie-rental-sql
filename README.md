# 🎬 Movie Rental System
**Course:** CMPT 291 — Intro to Database Management  
**Institution:** MacEwan University  
**Team:** Abdulmajeed, Ridhi, Jenny, Danish  
**Status:** ✅ Completed

## Overview
A full-featured movie rental management system built with C# and SQL Server. 
The system allows employees to manage customers, movies, rentals, actors, 
and generate business reports through a Windows Forms GUI application.

## Features

### 👤 Customer Management
- Add, update, delete and search customers
- Support for multiple phone numbers per customer
- Full customer profile (name, address, email, account number, credit card)
- Search by first name, last name or phone number

### 🎬 Movie Management
- Add, edit and delete movies with type, fee and copy count
- Assign and manage actors per movie
- Search movies by name with live results

### 📋 Rental Management
- Rent and return movies with full history tracking
- Inventory automatically updated on return
- Movie rating system (1–5 stars) per rental
- Actor rating system per rental
- Safety check for existing rental history

### 📊 Business Reports
- **Report 1** — Monthly Earnings by selected year
- **Report 2** — Top 3 Rented Movies by month and year
- **Report 3** — Top 5 Customers by month and year
- **Report 4** — Top 3 Queued Movies with queue count
- **Report 5** — Top 3 Employees by rentals in selected month and year

## Technologies Used
- **C#** — Windows Forms application
- **SQL Server** — Database backend
- **T-SQL** — Stored procedures, JOINs, RANK() window functions
- **Windows Forms** — GUI design
- **.NET Framework** — Application framework

## Database Schema
Key tables include:
- `Customer` — Customer profiles, account and credit card info
- `Movie` — Movie inventory, type, fee and copy count
- `Actor` — Actor profiles with gender and age
- `RentalRecord` — Full rental and return history
- `ActorRate` — Actor ratings per rental
- `CustomerQueue` — Movie wishlist/queue per customer
- `Employee` — Staff accounts and login credentials

## Database Files
- `CMPT291_Team04_create.sql` — Full database schema
- `CMPT291_Team04_Insert.sql` — Sample data inserts
- `ReportData.sql` — Report query scripts

## Key SQL Queries
- Multi-table JOINs across Customer, Movie, Actor, RentalRecord
- `RANK()` window functions for top customer and employee reports
- Aggregate functions for monthly earnings
- `TOP N` queries for queue and rental rankings
- Stored procedures for common CRUD operations

## Screenshots
📄 See `Demo_File.pdf` for full walkthrough with screenshots of all screens

## Team
- Danish Kumar
- Abdulmajeed
- Ridhi
- Jenny
