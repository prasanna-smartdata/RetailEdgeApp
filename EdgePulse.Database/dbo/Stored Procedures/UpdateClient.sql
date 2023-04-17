
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[updateClient]
	
	-- Add the parameters for the stored procedure here
	@ClientId	 int,
	@ClientName nvarchar(80),
	@ResultsClientNum int,
	@KPILite bit,
	@Where nvarchar(90),
	@Comment  nvarchar(80),
	@Email nvarchar(255),
	@ActiveClient bit,
	@DoorCounter bit,
	@Results bit,
	@MacroOked bit,
	@RECEmail nvarchar(255),
	@SQLServer bit,
	@Jewelsure bit,
	@HOStoreNumber int,
	@EdgePulseAllowed bit,
	@State nvarchar(50),
    @FakeClient bit,
    @SalesMaximum decimal(19,4),
    @SalesMinimum decimal(19,4),
    @StockMaximum decimal(19,4),
    @StockMinimum decimal(19,4),
    @SalesMinimumQty decimal(19,4),
    @SalesMaximumQty decimal(19,4)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [dbo].[Clients]
   SET [ClientName] = @ClientName
       ,[ResultsClientNum] = @ResultsClientNum 
       ,[KPILite] = @KPILite
       ,[Where] = @Where
       ,[Comment] = @Comment
       ,[Email] = @Email
       ,[ActiveClient] = @ActiveClient
       ,[DoorCounter] = @DoorCounter
       ,[Results] = @Results
       ,[MacroOked] = @MacroOked
       ,[RECEmail] = @RECEmail  
       ,[SQLServer] = @SQLServer 
       ,[Jewelsure] = @Jewelsure 
       ,[HOStoreNumber] = @HOStoreNumber  
       ,[EdgePulseAllowed] = @EdgePulseAllowed  
       ,[State] = @State  
       ,[FakeClient] = @FakeClient  
       ,[SalesMaximum] = @SalesMaximum  
       ,[SalesMinimum] = @SalesMinimum  
       ,[StockMaximum] = @StockMaximum  
       ,[StockMinimum] = @StockMinimum  
       ,[SalesMinimumQty] = @SalesMinimumQty  
       ,[SalesMaximumQty] = @SalesMaximumQty  
 WHERE  ID=@ClientId
END
