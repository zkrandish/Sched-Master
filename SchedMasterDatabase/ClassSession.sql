CREATE TABLE [dbo].[ClassSessions]
(
    [ClassSessionId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [GroupId] NVARCHAR(50) NOT NULL,
    [Day] NVARCHAR(50) NOT NULL,
    [StartTime] TIME NOT NULL,
    [EndTime] TIME NOT NULL,
    
    CONSTRAINT FK_ClassSession_GroupId FOREIGN KEY ([GroupId]) REFERENCES [Group] ([GroupId]) ON DELETE CASCADE
);
