CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [Subname] NVARCHAR(50) NULL, 
    [Birthday] DATE NULL
)
