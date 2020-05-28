DELETE FROM [dbo].[Apartment]
DELETE FROM [dbo].[User]

SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role], [PasswordSalt]) VALUES (1, N'admin', N'OT672nTS108fBgHT9JWFEucWAv+dGEiS0X5E2G65TWI=', N'Admin', 0x4847DCB1F9537A6CF09F6B7F30FD3433)
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role], [PasswordSalt]) VALUES (2, N'realtor', N'1zjqPT0Db2SkhOS4OTS2/37QRQwe/ezFkDN+D47QIO4=', N'Realtor', 0xB58C2F7DC6CDA001632CE830858A4669)
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role], [PasswordSalt]) VALUES (3, N'client', N'0BBnc7tkaLphnwl207AJTLjQwrzV/sFftfHVvTv8GPw=', N'Client', 0x04057CF04E7A58C74D700539CBB6437F)
INSERT INTO [dbo].[User] ([Id], [Username], [Password], [Role], [PasswordSalt]) VALUES (4, N'john', N'0Ctl1f+LV7nzbNhH7iFn459y0Z8FZqAiBwlrkbluIaM=', N'Realtor', 0x6AA289DC007D1AF4FF6E4C9585685A48)
SET IDENTITY_INSERT [dbo].[User] OFF

SET IDENTITY_INSERT [dbo].[Apartment] ON
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (1, N'Love Dovey', N'Beautiful apartment in the Bay Area!', CAST(1350 AS Decimal(20, 2)), CAST(2000.0000 AS Money), 3, CAST(38 AS Decimal(20, 2)), CAST(-122 AS Decimal(20, 2)), N'2020-03-01 00:00:00', 2, 1)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (2, N'High living', N'Come see the sights from up on high!', CAST(2000 AS Decimal(20, 2)), CAST(3540.0000 AS Money), 5, CAST(30 AS Decimal(20, 2)), CAST(-95 AS Decimal(20, 2)), N'2020-05-12 00:00:00', 2, 0)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (3, N'Low living', N'Stay close to the pulse!', CAST(500 AS Decimal(20, 2)), CAST(999.9900 AS Money), 1, CAST(45 AS Decimal(20, 2)), CAST(-110 AS Decimal(20, 2)), N'2019-05-12 00:00:00', 2, 0)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (4, N'Next to the water', N'Relax with your own private dock.', CAST(2000 AS Decimal(20, 2)), CAST(1750.0000 AS Money), 2, CAST(28 AS Decimal(20, 2)), CAST(-105 AS Decimal(20, 2)), N'2020-4-25 00:00:00', 4, 1)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (5, N'Big yard', N'Let the kids run around outside!', CAST(3050 AS Decimal(20, 2)), CAST(1399.0000 AS Money), 7, CAST(20 AS Decimal(20, 2)), CAST(-103 AS Decimal(20, 2)), N'2018-8-15 00:00:00', 4, 0)
INSERT INTO [dbo].[Apartment] ([Id], [Name], [Description], [Area], [MonthlyPrice], [Rooms], [Latitude], [Longitude], [DateAdded], [RealtorUserId], [IsRented]) VALUES (6, N'Good schools', N'Give your children the education they deserve.', CAST(1875 AS Decimal(20, 2)), CAST(2500.0000 AS Money), 4, CAST(40 AS Decimal(15, 2)), CAST(-90 AS Decimal(20, 2)), N'2020-04-23 00:00:00', 4, 0)
SET IDENTITY_INSERT [dbo].[Apartment] OFF
