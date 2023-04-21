CREATE PROCEDURE [dbo].[AddSuperStore]
      @GroupName nvarchar(80),
      @Status bit,
      @DeptsToUse nvarchar(5)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO GroupNames(GroupName, Status, DeptsToUse)
    VALUES(@GroupName, @Status,@DeptsToUse)
END