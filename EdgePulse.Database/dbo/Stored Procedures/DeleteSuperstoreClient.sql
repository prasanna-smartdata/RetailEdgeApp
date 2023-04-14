CREATE PROCEDURE [dbo].[DeleteSuperstoreClient]
	@ClientStoreID int,
	@SuperstoreGroupID int

AS
	 DELETE FROM [dbo].[ClientSuperstores]
      WHERE ClientStoreID=  @ClientStoreID and SuperstoreGroupID= @SuperstoreGroupID

 
