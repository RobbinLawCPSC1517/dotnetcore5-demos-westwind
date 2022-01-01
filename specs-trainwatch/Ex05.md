# Train Watch - Ex05 - Database Querying, Filtering, and Sorting

> This is the **third** in a series of exercises where you will be building a website to manage information on trains. **Train Watch** is a community site for train lovers who want to keep up-to-date on trains across North America. They want to maintain a database of Engines and RailCars.
>
> **This set is cumulative**; future exercises in this series will build upon previous exercises.

## Overview

A key aspect of the site is to allow users to search the database to find information on various rail cars. Your task in this assignment is to provide that functionality.

Use the demos presented in class as a guide to implementing this exercise.

### Create Query and Crud Pages

Create an `Query.cshtml`/`Query.cshtml.cs` Razor Page. The Page Model class must declare in its constructor a dependency on the `TrainWatchServices` class.
Be sure to add a menu item so that this page can be navigated to using the main menu; use the text "Query" for the link.

Create an `Crud.cshtml`/`Crud.cshtml.cs` Razor Page. The Page Model class must declare in its constructor a dependency on the `TrainWatchServices` class.
Be sure to add a menu item so that this page can be navigated to using the main menu; use the text "Crud" for the link.

### Query Page to Search Rolling Stock

The `Query` page will display summary information on the rolling stock data in an HTML table with. Display the `ReportingMark`, `Owner`, `Capacity` and `InService` information. 

Allow the user to enter a partial search string to filter on any portion of the reporting mark data (e.g.: "BN" or "50"). Present all of the records that have any of the partial data in the reporting mark as a table.

Also, allow the user to pick a car type from a dropdown menu, and retrieve all of the cars of that type. Present all of the records of that car type in a table.

Only present the data as shown below:

| Reporting Mark | Owner               | Capacity | InService |
|----------------|---------------------|----------|-----------|
| BN 19117       | Burlington Northern | 192200   | True
| BN 95782       | Burlington Northern | 192200   | True
| BN 95831       | Burlington Northern | 192200   | True
| BN 95887       | Burlington Northern | 192200   | True
| BN 95914       | Burlington Northern | 192200   | True

In order to get all of this to work you have to create two new entity classes RailCarType.cs and RollingStock.cs (Entities), update the TrainWatchContext.cs (DLL), and add new services to TrainWatchServices.cs (BLL).
Use the demos presented in class as a guide to achieving this. Get this part to work before moving on.

To ensure that your web application works, build and run your project.

```csharp
# From the src/ folder
cd TrainWatch.Web
dotnet build
dotnet watch run
```

### Modify Query Page

Add an additional column for the search results of rolling stock in the `Query.cshtml` page. This column should have a link to the `Crud` page with the Reporting Mark of the rolling stock item being sent as a parameter. Remember that the primary key in the RollingStock table is the Reporting Mark and that it is a string.

| Action    | Reporting Mark | Owner               | Capacity | InService |
|-----------|----------------|---------------------|----------|-----------|
| [Edit](#) | BN 19117       | Burlington Northern | 192200   | True
| [Edit](#) | BN 95782       | Burlington Northern | 192200   | True
| [Edit](#) | BN 95831       | Burlington Northern | 192200   | True

Add a link with the text "Add New Rolling Stock" above the HTML table for adding a new record of rolling stock information to the database. This must also navigate to the `Crud` page, but without any Reporting Mark parameter.

### Crud Page

The page must accept an optional parameter for the reporting mark of the rail car. For this exercise present the parameter to the user in a success message.

Include a button called `Query Page` on the `Crud` page to navigate back to the `Query` page; use the text "Query Page" for the link. Implement this feature so that when the `Query Page` button is pressed on the `Crud` page, the user will be returned to the `Query` page. Pass appropriate parameters back to the `Query` page, so that the user will not have to retype the partial string or have to re-choose the rail car type from the drop down.
