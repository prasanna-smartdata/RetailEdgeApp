﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


CREATE PROCEDURE [dbo].[getSuperstores]
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 SELECT [ID]
      ,[GroupNum]
      ,[GroupName]
      ,[DeptsToUse]
	  ,[Region]
	  ,[Status]
	  FROM GroupNames;
END
