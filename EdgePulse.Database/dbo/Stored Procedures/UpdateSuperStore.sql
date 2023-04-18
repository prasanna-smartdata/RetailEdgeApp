/*
Do not change the database path or name variables.
Any sqlcmd variables will be properly substituted during 
build and deployment.
*/
CREATE PROCEDURE [dbo].[UpdateSuperStore]
@SuperstoreGroupID int,
@GroupName nvarchar(80),
@Status bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[GroupNames]
	SET
	[GroupName] = @GroupName, 
	[Status]    = @Status
	WHERE  ID=@SuperstoreGroupID
END