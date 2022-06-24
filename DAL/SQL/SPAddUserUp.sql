CREATE PROCEDURE AddUser
@Username nvarchar(MAX),
@Password nvarchar(MAX)
AS
BEGIN
    DECLARE @DynamicSQL nvarchar(MAX) = 'CREATE LOGIN ' + QUOTENAME(@Username) + ' WITH PASSWORD = ' + QUOTENAME(@Password,'''') 
    EXEC (@DynamicSQL)
    SET @DynamicSQL = 'CREATE USER ' + QUOTENAME(@Username) + ' FOR LOGIN ' + QUOTENAME(@Username)
    EXEC (@DynamicSQL)
    SET @DynamicSQL = 'ALTER ROLE UserRole ADD MEMBER ' + QUOTENAME(@Username)
    EXEC (@DynamicSQL)
END