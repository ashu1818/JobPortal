CREATE TABLE [dbo].[admin_login
]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [username] VARCHAR(50) NULL, 
    [password] VARCHAR(50) NULL
)
