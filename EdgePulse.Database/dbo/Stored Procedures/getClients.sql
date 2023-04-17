USE [RMHClients]
GO
/****** Object:  StoredProcedure [dbo].[getClients]    Script Date: 4/17/2023 9:06:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER    PROCEDURE [dbo].[getClients]
 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
  Select Id, ClientNum,ClientName from Clients order by 2;
END
