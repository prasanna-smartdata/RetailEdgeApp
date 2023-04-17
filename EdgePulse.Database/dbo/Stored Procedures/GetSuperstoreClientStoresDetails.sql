
CREATE   PROCEDURE [dbo].[GetSuperstoreClientStoresDetails]
	@SuperstoreId int  
AS
	
	SELECT  css.SuperstoreGroupID,cs.ClientID,cs.ClientNum,cs.StoreID,cs.StoreName,css.DateCreated 
	FROM ClientSuperstores css
	INNER JOIN ClientStores cs ON css.ClientStoreID = cs.ID
	WHERE css.SuperstoreGroupID=@SuperstoreId