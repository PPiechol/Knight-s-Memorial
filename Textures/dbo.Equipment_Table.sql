CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(20) NOT NULL, 
    [Damage] INT NULL, 
    [Type_Of_Attack] CHAR(1) NULL, 
    [Icon] IMAGE NOT NULL
)
