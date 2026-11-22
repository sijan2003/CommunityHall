# CommunityHall

A hall booking web application built with ASP.NET Core MVC and Entity Framework Core (SQLite).

## Features

- Browse available halls and facilities
- Create and manage bookings
- Track booking status
- Admin views for facilities and bookings

## Tech Stack

- ASP.NET Core MVC (C#)
- Entity Framework Core
- SQLite
- Razor views, HTML, CSS, JavaScript

## Getting Started

Prerequisites: .NET SDK 8.0+

```bash
git clone https://github.com/sijan2003/CommunityHall.git
cd CommunityHall
dotnet restore
dotnet ef database update
dotnet run
```

The app runs at the URL shown in the console (usually https://localhost:5001).

## Project Structure

- `Controllers/` — Booking, Facilities, Home, Status
- `Models/` — entity models and `BookingContext`
- `Migrations/` — EF Core migrations
- `Views/` — Razor views
- `wwwroot/` — static assets
