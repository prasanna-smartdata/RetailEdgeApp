
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClient]
	
	-- Add the parameters for the stored procedure here
	@ClientId	 int,
	@ClientName nvarchar(80),
	@ResultsClientNum int,
	@KPILite bit,
	@LastDataReceived nvarchar(255),
	@Where nvarchar(90),
	@Comment  nvarchar(80),
	@Email nvarchar(255),
	@ActiveClient bit,
	@FocusReports bit,
	@SuppSystem  bit,
	@DoorCounter bit,
	@TheEdge bit,
	@Results bit,
	@SilentManagerSites bit,
	@MacroOked bit,
	@EdgeV43 bit,
	@RECEmail nvarchar(255),
	@EdgeV44 bit,
	@SQLServer bit,
	@Jewelsure bit,
	@SQLServerPOS bit,
	@USASuperStore  nvarchar(50),
	@AccountManager nvarchar(50),
	@TheirSupportEmail nvarchar(255),
	@AutoProcessData bit,
	@7ZipUsed bit,
	@HOStoreNumber int,
	@SuperStoreProcessingPriority  int,
	@SuperstoreActive bit,
	@EdgePulseAllowed bit,
	@MentoringClient bit ,
	@EdgeUSAId nvarchar(50),
	@State nvarchar(50),
	@LaybysOnPickup bit,
	@LastAccountManagerCall datetime,
	@ShowcaseWebsiteEnabled bit,
    @EdgePulseMembershipType nvarchar(20),
    @EdgePulseTrialEnd date,
    @BuyingGroupId int,
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
   SET [ResultsClientNum] = @ResultsClientNum 
      ,[ClientName] = @ClientName
      ,[KPILite] = @KPILite
      ,[LastDataReceived] = @LastDataReceived
      ,[Where] = @Where
      ,[Comment] = @Comment
      ,[Email] = @Email
      ,[ActiveClient] = @ActiveClient
      ,[FocusReports] = @FocusReports 
      ,[SuppSystem] = @SuppSystem
      ,[DoorCounter] = @DoorCounter
      ,[TheEdge] = @TheEdge
      ,[Results] = @Results
      ,[SilentManagerSites] = @SilentManagerSites
      ,[MacroOked] = @MacroOked
      ,[EdgeV43] = @EdgeV43
      ,[RECEmail] = @RECEmail  
      ,[EdgeV44] = @EdgeV44
      ,[SQLServer] = @SQLServer 
      ,[Jewelsure] = @Jewelsure 
      ,[SQLServerPOS] = @SQLServerPOS 
      ,[USASuperStore] = @USASuperStore   
      ,[AccountManager] = @AccountManager  
      ,[TheirSupportEmail] = @TheirSupportEmail   
      ,[AutoProcessData] = @AutoProcessData  
      ,[7ZipUsed] = @7ZipUsed  
      ,[HOStoreNumber] = @HOStoreNumber  
      ,[SuperStoreProcessingPriority] = @SuperStoreProcessingPriority    
      ,[SuperstoreActive] = @SuperstoreActive  
      ,[EdgePulseAllowed] = @EdgePulseAllowed  
      ,[MentoringClient] = @MentoringClient   
      ,[EdgeUSAId] = @EdgeUSAId  
      ,[State] = @State  
      ,[LaybysOnPickup] = @LaybysOnPickup  
      ,[LastAccountManagerCall] = @LastAccountManagerCall  
      ,[ShowcaseWebsiteEnabled] = @ShowcaseWebsiteEnabled  
      ,[EdgePulseMembershipType] = @EdgePulseMembershipType 
      ,[EdgePulseTrialEnd] = @EdgePulseTrialEnd  
      ,[BuyingGroupId] = @BuyingGroupId  
      ,[FakeClient] = @FakeClient  
      ,[SalesMaximum] = @SalesMaximum  
      ,[SalesMinimum] = @SalesMinimum  
      ,[StockMaximum] = @StockMaximum  
      ,[StockMinimum] = @StockMinimum  
      ,[SalesMinimumQty] = @SalesMinimumQty  
      ,[SalesMaximumQty] = @SalesMaximumQty  
 WHERE  ID=@ClientId
END
