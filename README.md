<h1 align="center">Welcome to URL.S (Back-end) 👋</h1>
<p>
  <img alt="Version" src="https://img.shields.io/badge/version-1.0.0-blue.svg?cacheSeconds=2592000" />
  <a href="#" target="_blank">
    <img alt="License: ISC" src="https://img.shields.io/badge/License-ISC-yellow.svg" />
  </a>
</p>
<p align="center"><img src="https://github.com/RayanAlsh/URL-Short.WEB/assets/154235526/b71fef34-1a4f-48b8-bfe0-f180fc4886d4" width=35% height=35%>
</p>

> Streamline your link sharing with URL.S. Our platform makes shortening URLs as easy as possible. Use it anonymously for quick and hassle-free shortening, or sign up to save and manage your links effortlessly. Experience the convenience and control with URL.S today!

## ✨ Endpoints

### URLs Endpoints
#### * Some Endpoints require Bearer Token

| URL | Desc | HTTP METHOD | Params | Body | Auth |
|-----|------|-------------|--------|------|------|
| http://localhost:5031/api/Url | (Admin) Get All URLs | GET | - | - | Admin Token |
| http://localhost:5031/api/Url | Add New URL | POST | - | - | - |
| http://localhost:5031/:shorturl | Redirect to Original URL | GET | shorturl | - | - |
| http://localhost:5031/api/Url/:UserId | Get User URLs | GET | UserId | - | Admin/User Token |

### Users Endpoints
#### * Some Endpoints require Bearer Token

| URL | Desc | HTTP METHOD | Params | Body | Auth |
|-----|------|-------------|--------|------|------|
| http://localhost:5031/api/Users | (Admin) Get All Users | GET | - | - | Admin Token |
| http://localhost:5031/api/Users | Update a User | PUT | - | Id, Email?, Password?, Role? | Admin Token |
| http://localhost:5031/api/:userid | Delete a user | DELETE | userid | - | Admin Token |
| http://localhost:5031/api/Users/login | Login the Admin/User | POST | - | email, password | - |
| http://localhost:5031/api/Users/register | Register user | POST | - | email, password | - |

## Installation and usage
```sh
 - Make sure to have .NET 7+
 - Make sure to install SQL Server Management Studio
 - Change the appsettings.json default connection string to your own
 - The default admin account is admin@example.com, password: qwerty1234567
