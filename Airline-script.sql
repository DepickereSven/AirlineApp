USE [Airlines]
GO
/****** Object:  Table [dbo].[airlinecodes]    Script Date: 5/06/2018 9:37:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[airlinecodes](
	[name] [varchar](50) NOT NULL,
	[code] [varchar](5) NOT NULL,
 CONSTRAINT [PK_airlinecodes] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlightData]    Script Date: 5/06/2018 9:37:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightData](
	[Airline_code] [varchar](5) NOT NULL,
	[Date] [date] NOT NULL,
	[Depature_Airport] [varchar](3) NOT NULL,
	[Arrival_Airport] [varchar](3) NOT NULL,
	[Depature_State] [varchar](3) NOT NULL,
	[Arrival_State] [varchar](3) NOT NULL,
	[Departure_Latitude] [float] NOT NULL,
	[Arrival_Latitude] [float] NOT NULL,
	[Depature_Longitude] [float] NOT NULL,
	[Arrival_Longitude] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[locatie]    Script Date: 5/06/2018 9:37:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locatie](
	[airlineCode] [varchar](5) NOT NULL,
	[stadHoofkwartier] [varchar](60) NOT NULL,
	[staatHoofkwartier] [varchar](60) NOT NULL,
	[mainHub] [varchar](5) NOT NULL,
	[staatMainHub] [varchar](60) NOT NULL,
 CONSTRAINT [PK_locatie] PRIMARY KEY CLUSTERED 
(
	[airlineCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opgericht]    Script Date: 5/06/2018 9:37:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opgericht](
	[airlineCode] [varchar](5) NOT NULL,
	[opgericht] [varchar](4) NOT NULL,
	[gestopt] [varchar](4) NULL,
 CONSTRAINT [PK_Opgericht_1] PRIMARY KEY CLUSTERED 
(
	[airlineCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FlightData]  WITH CHECK ADD  CONSTRAINT [FK_FlightData_airlinecodes] FOREIGN KEY([Airline_code])
REFERENCES [dbo].[airlinecodes] ([code])
GO
ALTER TABLE [dbo].[FlightData] CHECK CONSTRAINT [FK_FlightData_airlinecodes]
GO
ALTER TABLE [dbo].[locatie]  WITH CHECK ADD  CONSTRAINT [FK_locatie_airlinecodes] FOREIGN KEY([airlineCode])
REFERENCES [dbo].[airlinecodes] ([code])
GO
ALTER TABLE [dbo].[locatie] CHECK CONSTRAINT [FK_locatie_airlinecodes]
GO
ALTER TABLE [dbo].[Opgericht]  WITH CHECK ADD  CONSTRAINT [FK_Opgericht_airlinecodes] FOREIGN KEY([airlineCode])
REFERENCES [dbo].[airlinecodes] ([code])
GO
ALTER TABLE [dbo].[Opgericht] CHECK CONSTRAINT [FK_Opgericht_airlinecodes]
GO
