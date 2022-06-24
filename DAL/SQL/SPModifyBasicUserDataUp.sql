CREATE PROCEDURE ModifyBasicUserData
@Name nvarchar(MAX),
@Surname nvarchar(MAX),
@MiddleName nvarchar(MAX),
@Address nvarchar(MAX),
@Birthday datetime2(7),
@PlaceOfBirth nvarchar(MAX),
@AdditionalData nvarchar(MAX),
@Photo varbinary(MAX)
AS
BEGIN
	DECLARE @Username nvarchar(MAX) = CURRENT_USER
	UPDATE Employees
	SET Name = @Name, Surname = @Surname, MiddleName = @MiddleName, Address = @Address, BirthdayDate = @Birthday, PlaceOfBirth = @PlaceOfBirth, AdditionalPassportData=@AdditionalData, Photo=@Photo
	WHERE Username = @Username
END