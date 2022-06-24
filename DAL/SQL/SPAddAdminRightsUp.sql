CREATE PROCEDURE AddAdminRights
@Username nvarchar(MAX)
AS
BEGIN
	DECLARE @DynamicSQL nvarchar(MAX) = 'ALTER ROLE AdminRole ADD MEMBER ' + QUOTENAME(@Username)
	EXEC (@DynamicSQL)
END