# AirlineApp
MVC app for USA airlines

## Database 

![Database layout](https://i.imgur.com/4JM8ojw.jpg)

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
      * FlightID
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


## API

### api/code/{AirlineCode}

Airline code can be:
 *  AA
 *  AS
 *  B6
 *  CO
 *  DL
 *  EV
 *  F9
 *  FL
 *  HA
 *  MQ
 *  OO
 *  UA
 *  US
 *  VX
 *  WN
 *  XE
 *  YV
 
Or you could `all` as parameter this will return all the airline companies

This will return something in this format:

```javascript
[
 {
        "code": "AA",
        "name": "American Airlines"
 }
]
```

### api/date/{YYYY-MM-DD}

Range of date is between 2011-01-01 and 2011-01-03. 
That is currently the only data that available is.
It's also limited to the Airline company AS.
The date format is like this `{YYYY-MM-DD}` by example `2011-01-01`.

This will return something in this format:
```javascript
[
   {
        "date": "2011-01-01T00:00:00",
        "airlineCode": "AS",
        "departure": {
            "airport": "ABQ",
            "state": "NM",
            "latitude": 35.04,
            "longitude": -106.6
        },
        "arrival": {
            "airport": "MCI",
            "state": "MO",
            "latitude": 39.29,
            "longitude": -94.71
        }
    }
]
```

### api/DS/{DepartureStateName} or api/AS/{ArrivalStateName}

Possible states are:

State | State | State | State | State
--- | --- | --- | --- | ---
 AL | HI | MA | NM | SD |
 AK | ID | MI | NY | TN |
 AZ | IL | MN | NC | TX |
 AR | IN | MS | ND | UT |
 CA | IA | MO | OH | VT |
 CO | KS | MT | OK | VA |
 CT | KY | NE | OR | WA |
 DE | LA | NV | PA | WV |
 FL | ME | NH | RI | WI |
 GA | MD | NJ | SC | WY |
 
