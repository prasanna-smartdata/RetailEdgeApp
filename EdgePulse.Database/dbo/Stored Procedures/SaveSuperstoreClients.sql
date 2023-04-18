CREATE PROCEDURE [dbo].[SaveSuperstoreClients]
	@ClientStoreID int,
	@SuperstoreGroupID int

AS
	 INSERT INTO [dbo].[ClientSuperstores]
           ( 
           [ClientStoreID]
           ,[SuperstoreGroupID]
          )
     VALUES
           ( 
           @ClientStoreID 
           ,@SuperstoreGroupID )