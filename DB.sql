USE TEST

/****** Object:  Table [dbo].[Customers] ******/
DROP TABLE [dbo].[Customers]

CREATE TABLE [dbo].[Customers]
(
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerFirstName] [nchar](10) NOT NULL,
	[CustomerLastName] [nchar](10) NOT NULL
) ON [PRIMARY]

/****** Object:  Table [dbo].[Items] ******/
DROP TABLE [dbo].[Items]

CREATE TABLE [dbo].[Items]
(
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nchar](10) NOT NULL,
	[ItemPrice] [int] NOT NULL
) ON [PRIMARY]

/****** Object:  Table [dbo].[Orders] ******/
DROP TABLE [dbo].[Orders]

CREATE TABLE [dbo].[Orders]
(
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderCustomerId] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL
) ON [PRIMARY]


/****** Object:  Table [dbo].[OrdersItems]******/
DROP TABLE [dbo].[OrdersItems]

CREATE TABLE [dbo].[OrdersItems]
(
	[OrderId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]

GO

INSERT INTO Customers
	VALUES 
		('Dana', 'Cohen'),
		('Michal', 'Yaari'),
		('Eitan', 'Foxx'),
		('Gilad', 'Sade'),
		('Ariel', 'Binyamin'),
		('Chen', 'Mizrahi'),
		('Dorin', 'Yechzceli'),
		('Benny', 'Binyamin'),
		('Michal', 'Ar-Lev'),
		('Guy', 'Dabush')

INSERT INTO Orders
	VALUES 
		(10, '2014-01-01'),
		(7, '2014-02-21'),
		(7, '2014-03-12'),
		(6, '2014-04-29'),
		(5, '2014-05-17'),
		(3, '2014-06-18'),
		(1, '2014-07-20'),
		(1, '2014-08-06'),
		(2, '2014-09-15'),
		(8, '2014-10-03'),
		(4, '2014-10-03'),
		(9, '2014-11-02')
	
INSERT INTO Items
	VALUES 
		('Item01', 199),
		('Item02', 1119),
		('Item03', 299),
		('Item04', 11),
		('Item05', 132),
		('Item06', 650),
		('Item07', 799),
		('Item08', 199),
		('Item09', 324),
		('Item10', 643),
		('Item11', 798),
		('Item12', 136),
		('Item13', 749),
		('Item14', 641),
		('Item15', 565),
		('Item16', 132),
		('Item17', 789)
		
INSERT INTO OrdersItems
	VALUES
		(12, 6, 10),
		(1, 6, 10),
		(1, 2, 5),
		(2, 3, 5),
		(3, 17, 3),
		(3, 2, 1),
		(3, 7, 8),
		(4, 6, 7),
		(4, 16, 3),
		(4, 8, 2),
		(4, 1, 100),
		(5, 1, 5),
		(5, 2, 3),
		(6, 3, 4),
		(7, 4, 9),
		(8, 5, 30),
		(8, 6, 20),
		(8, 9, 25),
		(8, 8, 11),
		(9, 12, 6),
		(9, 17, 89),
		(10, 15, 11),
		(11, 16, 30)
