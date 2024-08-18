# Arkance Systems

This is a School Management System API! This project aims to
provide an efficient and flexible API for managing various aspects of a school,
including teachers, classes, students, subjects, and course notes.

## Table of Contents

## Getting Started
* The project is built using ASP.NET 8.0 Core framework. I have used the Entity Framework Core for database access. It follows a RESTful API design pattern and provides various enpoints for accessing the different resources.
* Swagger documentation and [Postman Collection documentation is available]()

## Prerequisite
* This is the schema of the database. This will help you in better understanding the structure of the project

* ![Class Diagram](DBFile/classDiagram.png?raw=true "Class Diagram")
* * Visual Studio 2022.

* [Overview of Entity Framework Core - EF Core | Microsoft Learn](https://learn.microsoft.com/en-us/ef/core/)

* [Microsoft.AspNetCore.Mvc Namespace | Microsoft Learn 8.0]https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc?view=aspnetcore-8.0)

* Install necessary packages 
 
 ```CMD
 dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
 dotnet add package Microsoft.EntityFrameworkCore.Tools

 # EEntity core framework tools
 dotnet tool install --global dotnet-ef

 # Perform migrations
 dotnet ef migrations add <MigrationName>
 ```

## How to Run
1. This project is deployed online on this [Backend-API link]() 
2. Using Docker
 * The project can be run easily using `docker-compose up --build`
 * To stop you can use `docker-compose down` 

2.Using Visual Studio
 * Set up the database using the Script `college.sql` provided in the folder `DBFiles`.
	* **Optional :** Seed the database with randomized generated data using `test_data_college.sql`
 * Install the packages in the prerequisite sections
 * Perform the migrations as explained in the database section below
 * Run either using `http` or `https`.
 * The application listens on `http://localhost:5277/`

## Architecture
1. **Controllers**: The controllers handle incoming HTTP requests and provide appropriate responses. They contain the API endpoints for each resource.

2. **Models**: The models represent the entities in the system. They define the structure and properties of classes, students, notes, and other associated entities.

3. **DbAccess**: `ArkanceTestContext.cs` extends the `DbContext` class from Entity Framework Core. It provides access to the database and defines the relationships between entities.

4. **Database**: The project uses a Postgres database, `dbcon` is the environment variable used to set the connection string.

## File Structure

## 1. Database
The database used is Postgres. The model definition has been retranscribed from the class diagram above using `Microsoft.EntityFrameworkCore` `Scaffolding`

* You start by installing dotnet Entity Frameworke Core tools

                `dotnet tool install --global dotnet-ef`

* For scaffolding, importing an existing database using the EF

 ```CMD
 dotnet ef dbcontext scaffold "Host=your_host;Database=your_database;Username=your_username;Password=your_password" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
 ```

* Add a migration. This will generate the migration according to the database shcema defined in the file `ArkanceTestContext.cs`

                `dotnet ef migrations add <MigrationName>` 
* To remove the migration use `ef migrations remove`

* Anytime you modify or update the shema, a migration is performed automatically at the application startup or you can also do it manually wit

                `dotnet ef database update `

* After the migration, for the first execution, the database is seeded with sample data

## 2. API Design

### Data Models

#### Class Object

* Properties:
  * `id` (integer, format: int32): The ID of the class.
  * `niveau` (string): The name of the class.
  * `professeurId` (integer, format: int32): The ID of the school associated with the class.

#### Eleve Object

* Properties:
  * `id` (integer, format: int32): The ID of the student.
  * `nom` (string): The first name of the student.
  * `prenom` (string, nullable): The last name of the student.
  * `genre` (string): The genre.
  * `ClassId` (integer, format: int32): The ID of the school associated with the class.
  
#### Professeur Object

* Properties:
  * `id` (integer, format: int32): The ID of the student.
  * `nom` (string): The first name of the student.
  * `prenom` (string, nullable): The last name of the student.
  * `genre` (string): The genre.

#### Matiere Object

* Properties:
  * `id` (integer, format: int32): The ID of the subject.
  * `name` (string): The name of the subject.

#### Note Object

* Properties:
  * `id` (integer, format: int32): The ID of the course note.
  * `matiereId` (integer, format: int32): The ID of the subject associated with the course note.
  * `eleveId` (integer, format: int32): The ID of the student associated with the course note.
  * `valeur` (number, format: double, range[0,20]): The mark for the course note.

### EndPoints
* The endpoints listed here are the ones required for this exercise, The complete documentation of the endpoints can be found on [Postman Collection Documentation]()

## 3. Bonus
* Instead of creating objects manually by serializing each field of each model, I have use the Entity Framework effectively and I have also introduced Database Migrations
* I have added schema validation for all the different models.
* I have added additional query options not listed in the original exercise.
