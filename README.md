# Movie Booking System (ASP.NET Core MVC)

This project was developed as part of the course **Modern Topics in Software Technology – Software for Mobile Devices** at the University of Piraeus (Academic Year 2023–2024, 7th Semester).

It is a role-based web application for managing movie screenings and ticket bookings, built with **ASP.NET Core MVC** and **Microsoft SQL Server**.

---

## 💻 Technologies Used

- ASP.NET Core MVC  
- Entity Framework Core  
- Microsoft SQL Server 2022  
- Visual Studio Code 2022  

---

## 🔑 Features

### 🔒 Authentication & Authorization

- Built-in **Register**, **Login**, **Logout** system using ASP.NET Identity.
- Role-based access:
  - **Admin**: Manages content admins, full access.
  - **Content Admin**: Manages movies, screenings, schedules.
  - **Member**: Views schedules, checks availability, books tickets.

### 🎬 Customer Functions

- View movie screening schedules.
- Check seat availability.
- Book tickets.
- View booking history.

### 🛠 Content Admin Functions

- View all available movies.
- Add new movies.
- Assign movies to screening rooms and times.
- Modify screening schedules.
- Edit customer details.

### ⚙ Admin Functions

- Add or remove Content Admins.
- Manage system roles.

---

## 📸 Screenshots

*Screenshots of key functions and interfaces can be added here.*

---

## 📂 Database Details

Using ASP.NET Identity:
- Tables: AspNetRole, AspNetUser, AspNetUserClaim, AspNetUserLogin, AspNetUserToken, and customized tables for Admins and Content Admins.

---

## 🚀 Getting Started

1. Clone the repository.
2. Open in Visual Studio Code 2022.
3. Set up the database in Microsoft SQL Server 2022.
4. Run migrations and update the database.
5. Run the project and navigate to the local URL.

---

## 📌 Notes

- Only Admin users can access `/Admins` routes.
- Members can only **read** movie availability; they cannot modify content.
- Unauthorized access attempts will redirect to **Access Denied**.

