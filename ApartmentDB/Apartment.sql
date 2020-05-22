CREATE TABLE [dbo].[Apartment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Area] DECIMAL(20, 2) NOT NULL, 
    [MonthlyPrice] MONEY NOT NULL, 
    [Rooms] INT NOT NULL, 
    [Latitude] DECIMAL(20, 2) NOT NULL, 
    [Longitude] DECIMAL(20, 2) NOT NULL, 
    [DateAdded] DATETIME2 NOT NULL, 
    [RealtorUserId] INT NOT NULL, 
    [IsRented] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Apartment_ToUser] FOREIGN KEY ([RealtorUserId]) REFERENCES [User]([Id])
)
