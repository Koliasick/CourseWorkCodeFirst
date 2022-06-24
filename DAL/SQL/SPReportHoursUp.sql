CREATE PROCEDURE ReportHours
@Date datetime2(7),
@Duration datetime2(7),
@Result bit OUTPUT
AS
BEGIN
	DECLARE @Username nvarchar(MAX) = CURRENT_USER
	DECLARE @UserID int = (SELECT Id from Employees
						   WHERE Username = @Username)			       
	DECLARE @QuantityOfReports int = (SELECT COUNT(*) FROM WorkTimes
									  WHERE EmployeeId = @UserID AND DATEDIFF(DAY,@Date,Date) = 0)
	IF @QuantityOfReports > 0
	BEGIN
		DECLARE @CurrentRecordId int = (SELECT WorkTimes.Id from WorkTimes
										WHERE EmployeeId = @UserID AND DATEDIFF(DAY,@Date,Date) = 0)
		DECLARE @CurrentDuration datetime2(7)
		SET @CurrentDuration = (SELECT WorkDuration from WorkTimes
								WHERE Id = @CurrentRecordId)
		DECLARE @SumOfDurations real
		SET @SumOfDurations = DATEPART(HOUR,@CurrentDuration) + DATEPART(HOUR,@Duration) + DATEPART(MINUTE,@CurrentDuration)/60.0 + DATEPART(MINUTE,@Duration)/60.0
		IF @SumOfDurations > 24
			SET @Result = 0
		ELSE
		BEGIN
			UPDATE WorkTimes
			SET WorkDuration = DATEADD(MINUTE,DATEPART(MINUTE,@Duration),DATEADD(HOUR,DATEPART(HOUR,@Duration),@CurrentDuration))
			WHERE Id = @CurrentRecordId
			SET @Result = 1
		END
	END
	ELSE
	BEGIN
		IF DATEPART(HOUR,@Duration) + DATEPART(MINUTE,@Duration)/60.0 < 24
		BEGIN	
			INSERT INTO WorkTimes (EmployeeId,Date,WorkDuration)
			VALUES (@UserID,@Date,@Duration)
			SET @Result = 1
		END
		ELSE
			SET @Result = 0
	END
END