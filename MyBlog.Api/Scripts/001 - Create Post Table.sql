CREATE TABLE [dbo].[Posts]
(
	[PostId] INT NOT NULL PRIMARY KEY, 
    [AuthorId] INT NOT NULL, 
    [Title] NVARCHAR(200) NOT NULL, 
    [Body] NVARCHAR(500) NOT NULL
)
