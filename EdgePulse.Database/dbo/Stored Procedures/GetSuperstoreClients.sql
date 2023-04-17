CREATE PROCEDURE [dbo].[GetSuperstoreClients]
	@SuperstoreId int  
AS
	
	SELECT  * FROM ClientSuperstores WHERE SuperstoreGroupID=@SuperstoreId
 
