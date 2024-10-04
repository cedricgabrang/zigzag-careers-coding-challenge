# README

## Project Overview

This project is a web application built using ASP.NET Core for both the API and the web interface, aimed at effectively managing library resources.

## Table of Contents

- [Tools and Technologies](#tools-and-technologies)
- [Why Blazor?](#why-blazor)
- [Architecture](#architecture)
  - [Library.API](#libraryapi)
  - [Library.Application](#libraryapplication)
  - [Library.Domain](#librarydomain)
  - [Library.Infrastructure](#libraryinfrastructure)
  - [Library.Shared](#libraryshared)
  - [Library.API.Tests (xUnit)](#libraryapitests-xunit)
  - [Library.Application.Tests (xUnit)](#libraryapplicationtests-xunit)
  - [Library.Web](#libraryweb)
- [Getting Started](#getting-started)
- [API Key](#api-key)

## Tools and Technologies

- **Visual Studio**: The primary IDE used for development
- **C# (.NET 8 and C# 12)**: The programming language and framework used
- **Entity Framework Core In-Memory**: Used as the database for testing and development, allowing easy data manipulation without a persistent database.

## Why Blazor?

Blazor was chosen for this project for the interface due to familiarity with the technology, as it is built on .NET. Additionally, Blazor facilitates ease of code reusability and allows for development within a single IDE.

## Architecture

This project implements a **Layered Architecture**, which divides the application into distinct layers, each responsible for different aspects of functionality.

### 1. Library.API
- This layer contains the ASP.NET Core Web API implementation, serving as the entry point for HTTP requests.

### 2. Library.Application
- This layer contains the business logic for managing library resources.

### 3. Library.Domain
- This layer contains the domain object `BookWithId`, which represents the core data structure of the application.

### 4. Library.Infrastructure
- This layer handles data access using Entity Framework In-Memory for testing and development purposes.

### 5. Library.API.Tests (xUnit)
- Contains unit tests for the API layer.

### 6. Library.Application.Tests (xUnit)
- Contains unit tests for the application layer, focusing on business logic correctness.

### 7. Library.Web
- Contains Blazor components and pages
 
### 8. Library.Shared
- **Description**: This layer contains shared resources, such as models and data transfer objects (DTOs), that are used across multiple layers of the application.

## Getting Started

Get started with the project:

1. Clone the repository:
   ```bash
   git clone https://github.com/cedricgabrang/zigzag-careers-coding-challenge.git

2. Configure multiple startup projects in Visual Studio:
    - Right-click on the solution in Solution Explorer.
    - Select **Properties**.
    - Go to the **Startup Project** section.
    - Choose **Multiple startup projects**.
    - Set both `Library.API` and `Library.Web` to **Start**.

## API Key
The API key can be found in the `Library.API/appsettings.json` file

## Preview
![image](https://github.com/user-attachments/assets/c35cf704-7aea-43fb-8e58-933fb7de7a5c)

