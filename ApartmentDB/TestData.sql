DELETE FROM [dbo].[Apartment]
DELETE FROM [dbo].[User]

SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (1, N'admin', N'admin', N'Admin')
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (2, N'realtor', N'realtor', N'Realtor')
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (3, N'client', N'client', N'Client')
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (4, N'john', N'john', N'Realtor')
SET IDENTITY_INSERT [dbo].[User] OFF

SET IDENTITY_INSERT [dbo].[Apartment] ON
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (1, N'Love Dovey', N'Beautiful apartment in the Bay Area!', CAST(1350 AS Decimal(20, 2)), CAST(2000.0000 AS Money), 3, CAST(38 AS Decimal(20, 2)), CAST(-122 AS Decimal(20, 2)), N'2020-03-01 00:00:00', 2, 1)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (2, N'High living', N'Come see the sights from up on high!', CAST(2000 AS Decimal(20, 2)), CAST(3540.0000 AS Money), 5, CAST(30 AS Decimal(20, 2)), CAST(-95 AS Decimal(20, 2)), N'2020-05-12 00:00:00', 2, 0)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (3, N'Low living', N'Stay close to the pulse!', CAST(500 AS Decimal(20, 2)), CAST(999.9900 AS Money), 1, CAST(25 AS Decimal(20, 2)), CAST(-110 AS Decimal(20, 2)), N'2019-05-12 00:00:00', 2, 0)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (4, N'Next to the water', N'Relax with your own private dock.', CAST(2000 AS Decimal(20, 2)), CAST(1750.0000 AS Money), 2, CAST(28 AS Decimal(20, 2)), CAST(-105 AS Decimal(20, 2)), N'2020-4-25 00:00:00', 4, 1)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (5, N'Big yard', N'Let the kids run around outside!', CAST(3050 AS Decimal(20, 2)), CAST(1399.0000 AS Money), 7, CAST(20 AS Decimal(20, 2)), CAST(-103 AS Decimal(20, 2)), N'2018-8-15 00:00:00', 4, 0)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (6, N'Good schools', N'Give your children the education they deserve.', CAST(1875 AS Decimal(20, 2)), CAST(2500.0000 AS Money), 4, CAST(15 AS Decimal(15, 2)), CAST(-113 AS Decimal(20, 2)), N'2020-04-23 00:00:00', 4, 0)
SET IDENTITY_INSERT [dbo].[Apartment] OFF
