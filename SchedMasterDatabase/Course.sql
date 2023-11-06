CREATE TABLE [dbo].[Course] (
    [CourseCode] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [CourseName] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [HoursPerWeek] INT NOT NULL
);
