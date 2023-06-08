# Project Title: Paradise Mart

APIs of an ecommerce site built with ASP.NET Web API (Framework), Entity Framework (ORM) and Microsoft SQL Server (Database).

This project is made by following the Three-tier architecture. Moreover, the SOLID Principles are followed here. APIs are made by follwing the REST API rules (proper http verbs and status codes).

3 Layers:
1. Application Layer -> [AppLayer](https://github.com/zbmzubayer/paradise-mart-backend/tree/main/AppLayer)
2. Business Login Layer -> [BLL](https://github.com/zbmzubayer/paradise-mart-backend/tree/main/BLL)
3. Data Access Layer -> [DAL](https://github.com/zbmzubayer/paradise-mart-backend/tree/main/DAL)

## Functionalities

- Basic CRUD operations
- Data opeations are done using DTO (Data Transfer Object)
- Password Hashing for users (using SHA256 algorithm)
- Image upload, retrieve, update and delete
- Mail Service for users
- OTP Service for users (Forgot Password)
- JWT (JSON Web Token) Authentication and Authorization

## Packages used in this project

ORM

- [Entity Framework](https://www.nuget.org/packages/EntityFramework)

JWT Authentication and Authorization

- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt)
- [Microsoft.Owin.Security.Jwt](https://www.nuget.org/packages/Microsoft.Owin.Security.Jwt)
- [Microsoft.AspNet.WebApi.Owin ](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Owin)
- [Microsoft.Owin.Host.SystemWeb](https://www.nuget.org/packages/Microsoft.Owin.Host.SystemWeb)
