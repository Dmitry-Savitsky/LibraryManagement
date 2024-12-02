# Library Management System

This project consists of a Backend built with ASP.NET and a Frontend built with React.

---
# How to run:

## Backend (ASP.NET Application)

1. **Set up the Database:**
   - Сreate a database named `LibraryManagement` on MySQL server.

2. **Configure the Connection String:**
   
   - Update the connection string in `library-management-backend/LibraryManagement/appsettings.json` to connect to the database:
     
     ```json
     "ConnectionStrings": {
       "LibraryManagementConnectionString": "server=127.0.0.1;database=LibraryManagement;user=root;password=;"
     }
     ```
3. **Run the Project**
     ```
      dotnet run
     ```
---

## Frontend (React Application)

1. **Install Dependencies:**
   - Navigate to the frontend directory:
     
     ```
     cd library-management-frontend
     ```
   - Install all necessary dependencies:
     
     ```
     npm install
     ```

2. **Start the Application:**
   - Run the frontend project:
     
     ```
     npm start
     ```

---

