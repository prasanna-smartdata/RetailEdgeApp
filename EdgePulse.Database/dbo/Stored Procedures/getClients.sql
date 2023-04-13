-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[getClients]
 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
  SELECT Clients.id,Clients.ClientNum, Clients.ClientName ,ClientStores.StoreName from Clients,ClientStores where Clients.ClientNum = ClientStores.ClientNum
  Order by 2
END
