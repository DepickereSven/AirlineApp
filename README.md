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


# API

## GET Requests

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

The code to do this for one specified airline is: 
```c#
var item = db.Airlinecodes
                    .Find(id);
                var newItem = (new Airlines
                {
                    AirlineCode = item.Code,
                    AirlineName = item.Name
                });
```

The code to do this for all airlines is: 
```c#
var item = db.Airlinecodes
                    .Select(x => new
                    {
                        AirlineCode = x.Code,
                        AirlineName = x.Name
                    })
                    .ToArray();
```

### api/info/{AirlineCode}

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
 
 This will return something in this format:

```javascript
[
    {
        "airlineCode": "DL",
        "airlineName": "Delta Air Lines",
        "location": [
            {
                "hoofdkwartier": {
                    "city": "Atlanta",
                    "state": "Georgia"
                },
                "hub": {
                    "airport": "ATL",
                    "state": "Georgia"
                }
            }
        ],
        "history": [
            {
                "foundedInTheYear": "1924",
                "CeasedOperationsInTheYear": ""
            }
        ]
    }
]
```

The code to do this: 
```c#
var item = db.Airlinecodes
                .Where(x => x.Code == airlineCode)
                .Select(x => new
                {
                    AirlineCode = x.Code,
                    AirlineName = x.Name,
                    Location = db.Locatie
                    .Where(i => i.AirlineCode == airlineCode)
                    .Select(i => new
                    {
                        Hoofdkwartier = new
                        {
                            City = i.StadHoofkwartier,
                            State = i.StaatHoofkwartier
                        },
                        Hub = new
                        {
                            Airport = i.MainHub,
                            State = i.StaatMainHub
                        }
                    }),
                    History = db.Opgericht
                    .Where(p => p.AirlineCode == airlineCode)
                    .Select(p => new
                    {
                        FoundedInTheYear = p.Opgericht1,
                        CeasedOperationsInTheYear = p.Gestopt
                    }),

                });
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
        "date": "2011/01/01",
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

The code to do this: 
```c#
var item = db.FlightData
                .Where(x => x.Date == DateTime.Parse(dateStamp))
                .Select(x => new
                {
                    date = x.Date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")),
                    AirlineCode = x.AirlineCode,
                    Departure = new
                    {
                        Airport = x.DepatureAirport,
                        State = x.DepatureState,
                        Latitude = x.DepartureLatitude,
                        Longitude = x.DepatureLongitude
                    },
                    Arrival = new
                    {
                        Airport = x.ArrivalAirport,
                        State = x.ArrivalState,
                        Latitude = x.ArrivalLatitude,
                        Longitude = x.ArrivalLongitude
                    }
                });
```

### api/state/DS/{DepartureStateName} or api/state/AS/{ArrivalStateName}

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
 
This will return something in this format: 
```javascript
[
   {
        "date": "2011/01/01",
        "airlineCode": "AS",
        "departure": {
            "airport": "BNA",
            "state": "TN",
            "latitude": 36.12,
            "longitude": -86.67
        },
        "arrival": {
            "airport": "BWI",
            "state": "MD",
            "latitude": 39.17,
            "longitude": -76.66
        }
    }
]
```

The code to do this for DepartureStateName: 
```c#
var item = db.FlightData
                    .Where(x => x.DepatureState == stateName)
                    .Select(x => new
                    {
                        date = x.Date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")),
                        AirlineCode = x.AirlineCode,
                        Departure = new
                        {
                            Airport = x.DepatureAirport,
                            State = x.DepatureState,
                            Latitude = x.DepartureLatitude,
                            Longitude = x.DepatureLongitude
                        },
                        Arrival = new
                        {
                            Airport = x.ArrivalAirport,
                            State = x.ArrivalState,
                            Latitude = x.ArrivalLatitude,
                            Longitude = x.ArrivalLongitude
                        }
                    });
```


The code to do this for ArrivalStateName: 
```c#
var item = db.FlightData
                    .Where(x => x.ArrivalState == stateName)
                    .Select(x => new
                    {
                        date = x.Date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP")),
                        AirlineCode = x.AirlineCode,
                        Departure = new
                        {
                            Airport = x.DepatureAirport,
                            State = x.DepatureState,
                            Latitude = x.DepartureLatitude,
                            Longitude = x.DepatureLongitude
                        },
                        Arrival = new
                        {
                            Airport = x.ArrivalAirport,
                            State = x.ArrivalState,
                            Latitude = x.ArrivalLatitude,
                            Longitude = x.ArrivalLongitude
                        }
                    });
```
