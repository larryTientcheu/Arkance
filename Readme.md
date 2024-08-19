

<!-- TOC --><a name="arkance-systems"></a>
# Arkance Systems

This is a School Management System API! This project aims to
provide an efficient and flexible API for managing various aspects of a school,
including teachers, classes, students, subjects, and course notes.

## API access 

- Local: http://localhost:5277/
- Production: https://arkance-production.up.railway.app
- [Postman collection](https://elements.getpostman.com/redirect?entityId=25226724-01c53be4-4d11-4408-a994-4a214fffe65a&entityType=collection)

<!-- TOC --><a name="table-of-contents"></a>
## Table of Contents
<!-- TOC start (generated with https://github.com/derlin/bitdowntoc) -->

- [Getting Started](#getting-started)
- [Prerequisite](#prerequisite)
- [How to Run](#how-to-run)
- [Architecture](#architecture)
- [File Structure](#file-structure)
- [1. Database](#1-database)
- [2. API Design](#2-api-design)
   * [Data Models](#data-models)
      + [Class Object](#class-object)
      + [Eleve Object](#eleve-object)
      + [Professeur Object](#professeur-object)
      + [Matiere Object](#matiere-object)
      + [Note Object](#note-object)
   * [EndPoints](#endpoints)
      + [Lister les professeurs par matière enseignée.](#lister-les-professeurs-par-matière-enseignée)
      + [Lister les élèves par classe.](#lister-les-élèves-par-classe)
      + [Lister les notes d'un élève.](#lister-les-notes-dun-élève)
      + [Lister tous les élèves (trié dans l'ordre alphabétique par nom puis prénom).](#lister-tous-les-élèves-trié-dans-lordre-alphabétique-par-nom-puis-prénom)
      + [Ajouter un élève.](#ajouter-un-élève)
      + [Ajouter une note d'un élève.](#ajouter-une-note-dun-élève)
      + [Modifier une note d'un élève.](#modifier-une-note-dun-élève)
   * [All Endpoints](#all-endpoints)
   * [Classes](#classes)
      + [Get All Classes](#get-all-classes)
      + [Create a Class](#create-a-class)
      + [Get a Specific Class](#get-a-specific-class)
      + [Update a Class](#update-a-class)
      + [Delete a Class](#delete-a-class)
   * [Eleves (Students)](#eleves-students)
      + [Get All Students](#get-all-students)
      + [Create a Student](#create-a-student)
      + [Get a Specific Student](#get-a-specific-student)
      + [Update a Student](#update-a-student)
      + [Delete a Student](#delete-a-student)
   * [Matieres (Subjects)](#matieres-subjects)
      + [Get All Subjects](#get-all-subjects)
      + [Create a Subject](#create-a-subject)
      + [Get a Specific Subject](#get-a-specific-subject)
      + [Update a Subject](#update-a-subject)
      + [Delete a Subject](#delete-a-subject)
   * [Notes (Grades)](#notes-grades)
      + [Get All Grades](#get-all-grades)
      + [Create a Grade](#create-a-grade)
      + [Get a Specific Grade](#get-a-specific-grade)
      + [Update a Grade](#update-a-grade)
      + [Delete a Grade](#delete-a-grade)
   * [Professeurs (Teachers)](#professeurs-teachers)
      + [Get All Teachers](#get-all-teachers)
      + [Create a Teacher](#create-a-teacher)
      + [Get a Specific Teacher](#get-a-specific-teacher)
      + [Update a Teacher](#update-a-teacher)
      + [Delete a Teacher](#delete-a-teacher)
- [3. Bonus](#3-bonus)

<!-- TOC end -->

## Getting Started

* The project is built using ASP.NET 8.0 Core framework. I have used the Entity Framework Core for database access. It follows a RESTful API design pattern and provides various enpoints for accessing the different resources.
* [Swagger documentation]() and [Postman Collection documentation is available]()

## Prerequisite

* This is the schema of the database. This will help you in better understanding the structure of the API

* ![Class Diagram](DBFiles/ClassDiagramOOM.png?raw=true "Conceptual Class Diagram")

* ![Database Schema](DBFiles/dbSchema.png?raw=true "Database Schema")

* **Visual Studio 2022.**

* [Overview of Entity Framework Core - EF Core | Microsoft Learn](https://learn.microsoft.com/en-us/ef/core/)

* [Microsoft.AspNetCore.Mvc Namespace | Microsoft Learn 8.0](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc?view=aspnetcore-8.0)

* Install necessary packages 
  
  ```CMD
  dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  
  # EEntity core framework tools
  dotnet tool install --global dotnet-ef
  
  # Perform migrations If no previous migration folder exists. If it exists ignore this step.
  dotnet ef migrations add <MigrationName>
  ```

## How to Run

1. This project is deployed online on this [Backend-API link](https://arkance-production.up.railway.app) 
 * With [swager docs](https://arkance-production.up.railway.app/swagger/index.html)
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
  - `id` (integer, int32): The ID of the class.
  - `niveau` (string, required): The name of the class.
  - `professeurId` (integer, int32, required): The ID of the school associated with the class.

#### Eleve Object

* Properties:
  - `id` (integer, int32): The ID of the student.
  - `nom` (string, nullable): The first name of the student.
  - `prenom` (string, nullable) (string, nullable): The last name of the student.
  - `genre` (string, nullable): The genre.
  - `classeId` (integer, int32, required): The ID of the school associated with the class.

#### Professeur Object

* Properties:
  - `id` (integer, int32): The ID of the student.
  - `nom` (string, required): The first name of the student.
  - `prenom` (string, required): The last name of the student.
  - `genre` (string, nullable): The genre.

#### Matiere Object

* Properties:
  - `id` (integer, int32): The ID of the subject.
  - `nom` (string, required): : The name of the subject.

#### Note Object

* Properties:
  - `id` (integer, int32): The ID of the course note.
  - `valeur` (number, double, nullable, min: 0, max: 20): The mark for the course note.
  - `eleveId` (integer, int32, required): The ID of the student associated with the course note.
  - `matiereId` (integer, int32, required): The ID of the subject associated with the course note.
  - `appreciation` (string, nullable): (Insight based on note value)

### EndPoints

* The endpoints listed here are the ones required for this exercise, The complete documentation of the endpoints can be found on [Postman Collection Documentation]()
#### Lister les professeurs par matière enseignée.
- **GET** `/api/Matieres/{id}?professeurs=True`
- **Parameters**:
  - `id` (path, required): Integer
  - `professeurs` (query, optional): Boolean
- **Response**: [Matiere](#matiere) object

#### Lister les élèves par classe.
- **GET** `/api/Classes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
  - `eleves` (query, optional): Boolean
- **Response**: [Classe](#classe) object

#### Lister les notes d'un élève.
- **GET** `/api/Eleves/{id}?notes=True`
- **Parameters**:
  - `id` (path, required): Integer
  - `notes` (query, optional): Boolean
- **Response**: [Eleve](#eleve) object

#### Lister tous les élèves (trié dans l'ordre alphabétique par nom puis prénom).
- **GET** `/api/Eleves?sorted=True`
- **Parameters**:
  - `sorted` (query, optional): Boolean
- **Response**: Array of [Eleve](#eleve) objects

#### Ajouter un élève.
- **POST** `/api/Eleves`
- **Request Body**: [Eleve](#eleve) object
- **Response**: Created [Eleve](#eleve) object

#### Ajouter une note d'un élève.
- **POST** `/api/Notes`
- **Request Body**: [Note](#note) object
- **Response**: Created [Note](#note) object

#### Modifier une note d'un élève.
- **PUT** `/api/Notes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Request Body**: [Note](#note) object
- **Response**: OK


### All Endpoints

* This is are all the endpoints served by the API.

### Classes

#### Get All Classes

- **GET** `/api/Classes`
- **Response**: Array of [Classe](#classe) objects

#### Create a Class

- **POST** `/api/Classes`
- **Request Body**: [Classe](#classe) object
- **Response**: Created [Classe](#classe) object

#### Get a Specific Class

- **GET** `/api/Classes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
  - `eleves` (query, optional): Boolean
- **Response**: [Classe](#classe) object

#### Update a Class

- **PUT** `/api/Classes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Request Body**: [Classe](#classe) object
- **Response**: OK

#### Delete a Class

- **DELETE** `/api/Classes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: OK

### Eleves (Students)

#### Get All Students

- **GET** `/api/Eleves`
- **Parameters**:
  - `sorted` (query, optional): Boolean
- **Response**: Array of [Eleve](#eleve) objects

#### Create a Student

- **POST** `/api/Eleves`
- **Request Body**: [Eleve](#eleve) object
- **Response**: Created [Eleve](#eleve) object

#### Get a Specific Student

- **GET** `/api/Eleves/{id}`
- **Parameters**:
  - `id` (path, required): Integer
  - `notes` (query, optional): Boolean
- **Response**: [Eleve](#eleve) object

#### Update a Student

- **PUT** `/api/Eleves/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Request Body**: [Eleve](#eleve) object
- **Response**: OK

#### Delete a Student

- **DELETE** `/api/Eleves/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: OK

### Matieres (Subjects)

#### Get All Subjects

- **GET** `/api/Matieres`
- **Response**: Array of [Matiere](#matiere) objects

#### Create a Subject

- **POST** `/api/Matieres`
- **Request Body**: [Matiere](#matiere) object
- **Response**: Created [Matiere](#matiere) object

#### Get a Specific Subject

- **GET** `/api/Matieres/{id}`
- **Parameters**:
  - `id` (path, required): Integer
  - `professeurs` (query, optional): Boolean
- **Response**: [Matiere](#matiere) object

#### Update a Subject

- **PUT** `/api/Matieres/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Request Body**: [Matiere](#matiere) object
- **Response**: OK

#### Delete a Subject

- **DELETE** `/api/Matieres/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: OK

### Notes (Grades)

#### Get All Grades

- **GET** `/api/Notes`
- **Response**: Array of [Note](#note) objects

#### Create a Grade

- **POST** `/api/Notes`
- **Request Body**: [Note](#note) object
- **Response**: Created [Note](#note) object

#### Get a Specific Grade

- **GET** `/api/Notes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: [Note](#note) object

#### Update a Grade

- **PUT** `/api/Notes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Request Body**: [Note](#note) object
- **Response**: OK

#### Delete a Grade

- **DELETE** `/api/Notes/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: OK

### Professeurs (Teachers)

#### Get All Teachers

- **GET** `/api/Professeurs`
- **Response**: Array of [Professeur](#professeur) objects

#### Create a Teacher

- **POST** `/api/Professeurs`
- **Request Body**: [Professeur](#professeur) object
- **Response**: Created [Professeur](#professeur) object

#### Get a Specific Teacher

- **GET** `/api/Professeurs/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: [Professeur](#professeur) object

#### Update a Teacher

- **PUT** `/api/Professeurs/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Request Body**: [Professeur](#professeur) object
- **Response**: OK

#### Delete a Teacher

- **DELETE** `/api/Professeurs/{id}`
- **Parameters**:
  - `id` (path, required): Integer
- **Response**: OK

## 3. Bonus

* Instead of creating objects manually by serializing each field of each model, I have use the Entity Framework effectively and I have also introduced Database Migrations
* I have added schema validation for all the different models.
* I have added additional query options not listed in the original exercise.

## 4. What's next ?

* More tests
* 