CREATE TABLE [dbo].[Enrollment] (
    [UserId] INT NOT NULL,
    [GroupId] NVARCHAR(50) NOT NULL,
    [Grade] FLOAT NOT NULL,
    
    PRIMARY KEY ([UserId], [GroupId]),
    
    CONSTRAINT FK_Enrollment_UserId FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT FK_Enrollment_GroupId FOREIGN KEY ([GroupId]) REFERENCES [Group] ([GroupId]) ON DELETE CASCADE
);
