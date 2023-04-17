CREATE PROCEDURE [dbo].[getClientStores]
	 
AS
	SELECT ClientStores.ID,Clients.ClientNum, Clients.ClientName, ClientStores.StoreID, ClientStores.StoreName from Clients,ClientStores where Clients.ClientNum = ClientStores.ClientNum
  Order by 2
RETURN 0
