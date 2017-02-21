# contoso-webapi-core-angularjs
Some of the technologies featured: .NET Core MVC, MediatR, AutoMapper, Fixie, Respawn, EF Core, Angular UI Router, Karma, Jasmine.

The architecture of the project is inspired by [Miguel Castro's lecture](https://vimeo.com/113704451) on SPA Silos and the benefits of using them.

# To run the project

Assuming that you have restored the .NET packages.

01. Create an empty database and switch the connection string in the src/ContosoUniversityAngular/appsettings.json file with
your new database connection string.

02. Run Update-Database against your empty database.

03. Restore bower packages.

04. Either run the project from Visual Studio or execute 'dotnet run' in the root directory of the project.

The project should now be running fine.

# To run the .NET tests

Assuming that you have restored the .NET packages.

01. Create a copy of the database you are using for the project (the data inside doesn't matter, the schema is important) and switch the
connection string in the tests/ContosoUniversityAngular.IntegrationTests/appsettings.json file with the new one.

02. Run the tests using the Test Explorer in Visual Studio.

Everything should be fine and passing.

# To run the Angular tests

You should have Karma and npm installed.

01. Run npm install in the root directory of the project.

02. If you haven't, run 'bower install' in the same directory.

03. Execute 'karma start'

Everything should be up and running.
