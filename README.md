Web App Example

This is a repository with some features for a simple web app developed using Angular 2 and C# Web Api. It has a study purpose, including since unit tests, acceptance end to end tests through authentication, and other cool things! So far we are using VS Community 2015 and SQL Server Express 2012.

1- Clone it: git clone https://github.com/gabrielfbarros/web-app-example.git

2- Update the submodules: git submodule update --init

3- To run the unit tests of web api you need to have installed VS extension "NUnit 3 Test Adapter": Tools > Extensions and Updates > Select Online and search.

4- To run/create the specflow tests of web api you need to have installed VS extension "Specflow for Visual Studio 2015": Tools > Extensions and Updates > Select Online and search.

5- To run the Angular 2 web site you need to have node v4.x.x or higher and npm 3.x.x or higher installed. To check that run the commands node -v and npm -v in a terminal/console window. Older versions produce errors. You can get the latest versions of them in https://nodejs.org

6- To run the Angular 2 web site you need to have the dependencies and start the web site. To get those dependencies run in a terminal git window the following command in the web site folder level:  npm install. And when it's done run: npm start.

7- To run the web api, select its project as startup project and start the application in VS.

8- Use the db script to add a user 'admin' with a encrypted password 'adm'.