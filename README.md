<div align="center">
  <img src="img/icon.png" alt="Simple Asp .Net Core Identity Template" width="100" height="100">
</div>

## <img src="https://github.com/Anmol-Baranwal/Cool-GIFs-For-GitHub/assets/74038190/29fd6286-4e7b-4d6c-818f-c4765d5e39a9" width="25" style="margin-bottom: -5px;"> About The Project

ASP.NET Core Identity is the membership system for building ASP.NET Core web applications, including membership, login, and user data. ASP.NET Core Identity allows you to add login features to your application and makes it easy to customize data about the logged in user. You can find additional information in the ASP.NET Core Documentation.

```html
<!-- HTML Meta Tags -->
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta name="author" content="Sinan Özçelik">
<meta name="publisher" content="VS 2023">
<!-- Web Site Title -->
<title>Simple Asp .Net Core Identity Template</title>
<!-- Meta Open Graph -->
<meta property="og:locale" content="en_US" />
<meta property="og:type" content="website" />
<meta property="og:title" content="Homepage" />
<meta property="og:url" content="Simple Asp .Net Core Identity Template" />
<meta property="og:site_name" content="Simple Asp .Net Core Identity Template" />
```

## <img src="https://user-images.githubusercontent.com/74038190/212257467-871d32b7-e401-42e8-a166-fcfd7baa4c6b.gif" width ="25" style="margin-bottom: -5px;"> Features

- [x] CRUD Operations
- [x] Migration Operations
- [x] User Authentication
- [x] Register / Login Operations
- [x] Forgot Password
- [x] Roles Operations
- [x] SMTP E-Mail Sender


## <img src="https://media2.giphy.com/media/QssGEmpkyEOhBCb7e1/giphy.gif?cid=ecf05e47a0n3gi1bfqntqmob8g9aid1oyj2wr3ds3mg700bl&rid=giphy.gif" width ="25" style="margin-bottom: -5px;"> Build With

![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)
![Bootstrap](https://img.shields.io/badge/bootstrap-%238511FA.svg?style=for-the-badge&logo=bootstrap&logoColor=white)
![javascript](https://img.shields.io/badge/javascript%20-%23323330.svg?&style=for-the-badge&logo=javascript&logoColor=%23F7DF1E)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)

## <img src="https://user-images.githubusercontent.com/74038190/212257465-7ce8d493-cac5-494e-982a-5a9deb852c4b.gif" width ="25" style="margin-bottom: -5px;"> Installation

1. Check the database connection on the appsetting.json file. Customize the database connection path here according to your own computer. By default the database name is PortfolioDb. You can enter SMTP settings from your own e-mail service.

   ```json
   {
   "Logging": {
      "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
      }
   },
   "ConnectionStrings": {
      "DefaultConnection": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=IdentityDemoDb;Integrated Security=True; TrustServerCertificate=True; MultipleActiveResultSets=true"
   },
   "EmailSender": {
      "Host": "mail.alanadiniz.com",
      "Port": 587,
      "EnableSSL": false,
      "Username": "username",
      "Password": "password"
   },
   "AllowedHosts": "*"
   }
   ```
2. Type the add-migration command via the Package Manager Console.
   
   ```
   add-migration DbCreateFirst
   ```

3. Type the update-database command via the Package Manager Console.
   
   ```
   update-database
   ```
4. You can use the information below to enter the admin panel. You can use the /Users/Index address path for the admin panel.
   ```c#
   // --- Identity User Information --- //
   private const string adminUser = "sinanozcelik";
   private const string adminPassword = "Sinan123.";
   private const string adminEmailAddress = "info@sinanozcelik.com";

   // Add new user
   await userManager.CreateAsync(user, adminPassword);
   // Add "admin" role to new user
   await userManager.AddToRoleAsync(user, "admin");
   ```

## <img src="https://user-images.githubusercontent.com/74038190/235294019-40007353-6219-4ec5-b661-b3c35136dd0b.gif" width="30" style="margin-bottom: -5px;"> Contact Information

You can reach out to me using the following contact details:

[![Email](https://img.shields.io/badge/Email-sinanozcelik%40yaani.com-brightgreen)](mailto:sinanozcelik@yaani.com)

[![LinkedIn](https://img.shields.io/badge/LinkedIn-sinan--ozcelik-blue)](https://www.linkedin.com/in/sinan-ozcelik/)

I'm always open to development and collaboration. Feel free to reach out to me!