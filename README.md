# VROOM

## Virtual Regional Open Office Manager

---

### Vision

In a time when more and more companies are moving towards a remote work environment, they need new ways to organize.

VROOM (Virtual Remote Open Office Manager) makes it easier for small businesses to switch to all remote office operations.

VROOM offers companies the ability to keep an inventory on equipment that gets borrowed as well as maintain a new registry of employees. 

### IN
  * Allow an easy way to track what office equipment employees have taken home
  * Allows a subset of users to create, update, and delete equipment items, and their relationships to employees
  * Allows emails to be sent automatically when employees are hired and when they check out office equipment
  
###  OUT
  * Won’t manage sales
  * Will not track expenses
  * Won’t manage the hiring process
  * Won't track employee's schedules

###  Minimum Viable Product 
* Able to view, update, create, and delete employee, equipment, and employee-equipment as appropriate with the user’s permissions. 

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

---

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
- SendGrid
- Swagger
- Razor views
- Microsoft Azure
- SQL Server


---
## Domain Model


![Domain Model](https://github.com/NaamaBarIlan/VROOM/blob/Staging/Assets/VROOM%20UMD.png)

---

### Overall Project Schema

![Database Schema](https://github.com/NaamaBarIlan/VROOM/blob/Staging/Assets/VROOM%20ERD.png)

---


## User Stories

User stories can be found in this [link](https://trello.com/b/x6A2dKi8/vroom-401-cf) to the project management board.

### User Roles & Permissions

#### Employees Table

| | Create | Read  | Update | Delete |
| ------------- |:-------------:| :-----:|:-------------:| :-----:|
| CEO  | Yes | Yes | Yes |  |
| Office Manager    | Yes | Yes | Yes |  |
| Employee  |  | Yes | Yes* |  |
* *Only for their own data

#### Equipment Table

| | Create | Read  | Update | Delete |
| ------------- |:-------------:| :-----:|:-------------:| :-----:|
| CEO  | Yes | Yes | Yes | Yes |
| Office Manager    |  | Yes | Yes |  |
| Employee  |  | Yes |  |  |

#### EmployeeEquipment Table

| | Create | Read  | Update |
| ------------- |:-------------:| :-----:|:-------------:|
| CEO  | Yes | Yes | Yes |
| Office Manager    | Yes | Yes | Yes |
| Employee  |  | Yes* | Yes* |

#### UserAccount Table

| | Login | Register  | AssignRole |
| ------------- |:-------------:| :-----:|:-------------:|
| CEO  | Yes | CEO, Office Manager, Employee | CEO, Office Manager, Employee |
| Office Manager    | Yes | Employee | Employee |
| Employee  | Yes |  |  |

---

## Change Log

##### 6 Aug 2020
- 5.5: *Changes to project file.*
- 5.4: *Form validation, split up login and register into separate pages.*
- 5.3: *Added an updated ERD and UMD images to Assets.*
- 5.2: *EmployeeEquipmentItem Status Rework.*
- 5.1: *Updated Startup.cs to include the JWT token in the SwaggerUI.*
- 5.0: *Partial email implementation for EmployeeEquipmentItems.*

##### 5 Aug 2020
- 4.7: *Full front end implementation.*
- 4.6: *More email methods implemented.*
- 4.5: *Azure Deployment Mods.*
- 4.4: *Added front end to the API.*
- 4.3: *EEItem Tests Complete.*
- 4.2: *Added and configured Swagger middleware in Startup.ConfigureServices and Startup.Configure. Installed Swashbuckle.AspNetCore Version 5.5.1*
- 4.1: *Added Views, razor view for Index.*
- 4.0: *Added Swagger UI via Swashbuckle.AspNetCore. It interprets Swagger JSON. It interprets Swagger JSONto build a rich, customizable experience for describing the web API functionality. It includes build-in test harnesses for public methods.*

##### 4 Aug 2020
- 3.6: *EmployeeEquipmentItem service testing - incomplete.*
- 3.5: *Added summary notes to the IEmployee Interface.*
- 3.4: *Policy layers added for EmployeeEquipmentItemController.cs. Added new welcome email message for new created personnel, relayed response back to corporate.*
- 3.3: *EmployeeEquipmentItem full CRUD. All CRUD methods tested in EquipmentItem table and Employee table.*
- 3.2: *Added SendGrid functionality.*
- 3.1: *Added AccountController: Register, Login, AssignRoleToUser, CreateToken, AuthenticateToken. Added AssignRoleDTO, LoginDTO, RegisterDTO. Added roles and permissions in Startup.cs, ApplicationUser.cs and RoleInitializer.cs. Seeded roles complete. Tested all AccountController user permissions.*
- 3.0: *Added error handling to the EmployeeController.*

##### 3 Aug 2020
- 2.9: *EmployeeEquipmentItem partial CRUD. EEItem CRUD, except for update.*
- 2.8: *Updated CreateEmployee method to return the new employee Id from the db.*
- 2.7: *Fixed an error in EquipmentItems create method. Creating a new EquipmentItem now returns the id of the new piece of equipment.*
- 2.6: *All CRUD operations tests are passing. Added 7 unit tests passing to the Employee service.*
- 2.5: *UnitTests written/passing for EquipmentItems routes.*
- 2.4: *Fixing extra index in EEI table.*
- 2.3: *Entity, DTO, and Interface.*
- 2.2: *Added properties to Employee.cs and EmployeeDTO.cs. Added seeded data migration. Merge branch 'Staging'.*
- 2.1: *Changes made for EquipmentItem Model. Changes made to EquipmentItem.cs, IEquipmentItem.cs, EquipmentItemDTO.cs and EquipmentItemRepository.cs. Set up EquipmentItemController.cs. Changes made to VROOMDbContext.cs and seed data added to the Database.*
- 2.0: *Initial commit, added configuration settings for Startup.cs and VROOMDbContext.cs, added Models, Employee.cs, EquipmentItem.cs, EmployeeEquipmentItem.cs, added according Interfaces, DTOs and Services. Initial data seeded into the Db. File tree established.*

##### 2 Aug 2020
- 1.2: *Added User Roles & Permissions to README*

##### 31 Jul 2020
- 1.1: *Added an ERD and UMD images, software requirements and a link to user stories in a project management board*

---

## Authors

* Michael Refvem
* Paul M Rest
* Na'ama Bar-Ilan

---
