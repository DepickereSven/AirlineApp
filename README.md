# AirlineApp
MVC app for USA airlines

## Database 

![Database layout](https://i.imgur.com/6iWM5LB.jpg)

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
  * FlightData
    * Kolomen:
      * Airline_Code
      * Date
      * Depature_Airport
      * Arrival_Airport
      * Departure_State
      * Arrival_State
      * Departure_Latitude
      * Arrival_Latitude
      * Departure_Longitude
      * Arrival_Longitude

### Scaffold command

`Scaffold-DbContext "Data Source=[DESKTOP_NAME]\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True;"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context AirlinesContext -f`
