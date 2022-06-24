CREATE PROCEDURE RemoveAdminRights
@Username nvarchar(MAX)
AS
BEGIN
	DECLARE @DynamicSQL nvarchar(MAX) = 'ALTER ROLE AdminRole DROP MEMBER ' + QUOTENAME(@Username)
	EXEC (@DynamicSQL)
END