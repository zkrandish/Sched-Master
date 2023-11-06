CREATE TABLE [dbo].[Group] (
    [GroupId] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [CourseCode] NVARCHAR(50) NOT NULL,
    [UserId] INT NOT NULL,
    [Capacity] INT NOT NULL,
    
    CONSTRAINT FK_Program_CourseCode FOREIGN KEY ([CourseCode]) REFERENCES [Course] ([CourseCode]),
    CONSTRAINT FK_UserId FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId])
);
