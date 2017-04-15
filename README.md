# ShoppingCart

This is a simple project with two pages:
 - Products page
 - Checkout page
 
Click on the cart icon after an item has been added. Refresh the page to refresh the cart.

# Prerequisites
* Visual Studio 2017
* [ASP.Net Core (Version 1.0.2)](https://www.microsoft.com/net/download)
* [DocumentDb Emulator ](https://docs.microsoft.com/en-us/azure/documentdb/documentdb-nosql-local-emulator) (or an Azure DocumentDb account)
* [Node.js (v 6.0 or higher)](https://nodejs.org/en/)  and npm (v3 or higher)
* [TypeScript (v 2.1 or higher)](https://www.typescriptlang.org/#download-links)
* [Webpack (v2)](https://webpack.js.org/)

### AppSettings DocumentDb Credentials for DocumentDb Emulator
* If you are using the credentials from your own DocumentDB, please replace the values of **Endpoint** and **Key** in the project appsettings.json file.  Otherwise, if you are using DocumentDB Emulator, these settings below are already set on the project.
```json
  "DocumentDb": {
    "Endpoint": "https://localhost:8081/",
    "Key": "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
    "Name": "shoppingcartdemo"
  }
```

# Setup Instructions
  
## Running using dotnet cli
1. Download/Clone the code
2. Verify that DocumentDB Emulator or your own DocumentDB instance is up and running
3. Open a Console Window and navigate to MainSite
4. Run "npm install"
5. Run "dotnet restore"
6. Run "dotnet build"
7. Run "dotnet run"
 
## Running using Visual Studio 2017
1. Download/Clone the code
2. Open the Solution in Visual Studio 2017 
3. Verify that DocumentDB Emulator or your own DocumentDB instance is up and running
4. From the launchSettings.json (./MainSite/Properties/launchSettings.json), copy the **applicationUrl**
5. In the app.config.ts (./MainSite/ClientApp/app/app.config.ts), paste the applicationUrl you just copied into the **apiEndpoint**
6. Right click package.json, and select **Restore Packages**
    * You can see progress in the Output window with **Bower/npm** as your selection in **Show output from:**
7. Right click the solution, select **Restore NuGet Packages**
8. Rebuild the solution
9. Click the Green Run Button / Hit F5