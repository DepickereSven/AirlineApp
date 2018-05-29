# AirlineApp
MVC app for USA airlines

## Database 

![Database layout](https://i.imgur.com/sYhJyyV.png)

[Zie](Airline-script.sql)

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
