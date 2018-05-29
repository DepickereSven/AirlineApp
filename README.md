# AirlineApp
MVC app for USA airlines

## Database 

![Database layout](https://i.imgur.com/sYhJyyV.png)

[Zie script](Airline-script.sql)

Het bestaat uit 3 tabbelen:
  * airlinecodes
    * Kolomen:
      * name
      * code
  * locatie
    * Kolomen:
      * airlineCode
      * stadHoofkwartier
      * staatHoofkwartier
      * mainHub
      * staatMainHub
  * opgericht
    * Kolomen:
      * airlineCode
      * opgericht
      * gestopt

### Scaffold command

`Scaffold-DbContext "Data Source=[DESKTOP_NAME]\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True;"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context AirlinesContext -f`
