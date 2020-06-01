# Apartment Rental Application

This is a full stack Apartment Rental web application. This solution consists of three parts.

## Frontend

The frontend is a Nuxt.js-based Single Page Application (SPA). Nuxt.js is a Vue.js framework which allows for a convention-based approach to Vue.js development. Additionally, Nuxt.js provides an easy upgrade path from SPA to Server-Side Rendering or Static Site Generation.

The frontend is contained within the `ApartmentFrontend` folder. Additional instructions for setup can be found in the README in that folder.

## REST APIs

The APIs were RESTfully written using .NET Core 3.1 and were developed in Visual Studio 2019. Open `ApartmentRentalApp.sln` and view the `ApartmentRental` project to see the relevant code. Additional unit tests for this layer are contained within the `ApartmentRentalTest` project.

## Backend

The database is a SQL Server database. Schema and setup is contained in the Visual Studio database project `ApartmentDB`, which is subsequently contained within the `ApartmentRentalApp.sln` file.
