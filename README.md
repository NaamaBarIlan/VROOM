# VROOM

## Virtual Regional Open Office Manager

---

## Software Requirements

### Vision

In a time when more and more companies are moving towards a remote work environment, they need new ways to organize. 

VROOM makes it easier for small businesses to switch to all remote office operations, as people move towards unconventional work times.

VROOM offers companies the ability to track personnel flexible schedules more easily as well as keep an inventory on equipment that gets borrowed. 

### IN
  * Allow an easy way to store employee’s varied schedules
  * Allow an easy way to track what office equipment employees have taken home
  * Allows a subset of users to create, update, and delete schedules, equipment, and their relationships to employees.
  * Allows employees to set their own schedules.
  
###  OUT
  * Won’t manage sales
  * Will not track expenses
  * Won’t manage the hiring process

###  Minimum Viable Product 
* Able to view, update, create, and delete employee,  equipment, and employee-equipment as appropriate with the user’s permissions. 

* Include unit tests for all application services. 

### Stretch Goals

* Able to view, update, create, and delete schedules and employee-schedules. 
* Allow employees to have a different start/end time for each day of the week
* Create a separate entity for Branches separate from employees
* Create a separate entity for Titles separate from employees
* Create a separate entity for Depts separate from employees
* Add a client entity to track the relationship between employees, branches, and clients
* Add a software licensing entity to track which employees are using which software packages

### Functional Requirements
* Senior level staff can update, create, and delete all entities except employees, which can only be updated. They can view all employee, schedule, and equipment data.
* Managers can update employee schedules, and set whether an employee has, or has returned, a piece of equipment. They can view all employee, schedule, and equipment data.
* Employees can view their colleague’s branches and schedules. They view their own borrowed equipment, and view and update their own schedule.
* Clients can view all employee’s info, including branch information, and schedules.
* Anonymous users can view all branch information.

### Data Flow
There will be a controller for each entity implemented, which will take in a DTO object (when necessary) through an API route, and return data.

Each controller will use dependency injection to access a service object which will perform the actual database operations, and return DTO data back to the controller. The controller will not have access to actual entity data, but will only work with DTOs.

When a user signs in, they will create a token, and use that token to access the API data they have permission to access.

### Non-Functional Requirements 

#### Security

* We will employ ASP.NET Core Authorization and Identity to create Jwt tokens for users when they log in, and then those users will employ their token to gain access to the functionalities they have access to.
* Users can create an account with the login information stored in Identity or they can use an external login provider. Supported external login providers include Facebook, Google, Microsoft Account, and Twitter.

#### Testability

* We will employ xUnit to perform unit tests, and build the project using TDD.
* Every publicly accessible method in every service will be tested, with the unit tests written before the method(s).

#### Usability

* API will be RESTful and return predictable data for a given internal state.
* API routes will be logical and consistent, with correct spelling and consistent pluralization.

---

## Tools Used
Microsoft Visual Studio Community 2019 

- C#
- ASP.Net Core
- Entity Framework
- MVC
- xUnit


---
## Domain Model


![Domain Model](https://github.com/NaamaBarIlan/VROOM/blob/Staging/Assets/VROOM%20UMD.png)

---

### Overall Project Schema

![Database Schema](https://github.com/NaamaBarIlan/VROOM/blob/Staging/Assets/VROOM%20ERD.png)

---


## User Stories

User stories can be found in this [link](https://trello.com/b/x6A2dKi8/vroom-401-cf) to the project management board.

---

## Change Log

1.1: *Added an ERD and UMD images, software requirements and a link to user stories in a project management board* - 31 Jul 2020  

---

## Authors

* Michael Refvem
* Paul M Rest
* Na'ama Bar-Ilan

---
