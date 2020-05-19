DELETE FROM [dbo].[Apartment]
DELETE FROM [dbo].[User]

INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (1, N'admin', N'admin', N'Admin')
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (2, N'realtor', N'realtor', N'Realtor')
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role]) VALUES (3, N'client', N'client', N'Client')

INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId]) VALUES (1, N'Love Dovey', N'Beautiful apartment in the Bay Area!', CAST(1350 AS Decimal(18, 0)), CAST(2000.0000 AS Money), 3, CAST(38 AS Decimal(18, 0)), CAST(-122 AS Decimal(18, 0)), N'2020-03-01 00:00:00', 2)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId]) VALUES (2, N'High living', N'Come see the sights from up on high!', CAST(2000 AS Decimal(18, 0)), CAST(3540.0000 AS Money), 5, CAST(30 AS Decimal(18, 0)), CAST(-95 AS Decimal(18, 0)), N'2020-05-12 00:00:00', 2)
