CREATE PROCEDURE [dbo].[SaveSuperstoreClients]
	@ClientStoreID int,
	@SuperstoreGroupID int

AS

  IF NOT EXISTS (SELECT * FROM [ClientSuperstores] 
                    WHERE ClientStoreID = @ClientStoreID AND SuperstoreGroupID = @SuperstoreGroupID)
   BEGIN
     
	 INSERT INTO [dbo].[ClientSuperstores]
           ( 
           [ClientStoreID]
           ,[SuperstoreGroupID]
          )
     VALUES
           ( 
           @ClientStoreID 
           ,@SuperstoreGroupID )
   END