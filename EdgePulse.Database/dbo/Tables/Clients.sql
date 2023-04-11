CREATE TABLE [dbo].[Clients] (
    [ID]                           INT             IDENTITY (1, 1) NOT NULL,
    [ClientNum]                    INT             NOT NULL,
    [ResultsClientNum]             INT             CONSTRAINT [DF__Clients__Results__7F60ED59] DEFAULT ((0)) NULL,
    [ClientName]                   NVARCHAR (80)   NULL,
    [KPILite]                      BIT             CONSTRAINT [DF__Clients__KPILite__00551192] DEFAULT ((0)) NULL,
    [LastDataReceived]             NVARCHAR (255)  NULL,
    [Where]                        NVARCHAR (90)   NULL,
    [Comment]                      NVARCHAR (80)   NULL,
    [Email]                        NVARCHAR (255)  NULL,
    [ActiveClient]                 BIT             CONSTRAINT [DF__Clients__ActiveC__1CF15040] DEFAULT ((0)) NULL,
    [FocusReports]                 BIT             CONSTRAINT [DF__Clients__FocusRe__1DE57479] DEFAULT ((0)) NULL,
    [SuppSystem]                   BIT             CONSTRAINT [DF__Clients__SuppSys__1ED998B2] DEFAULT ((0)) NULL,
    [DoorCounter]                  BIT             CONSTRAINT [DF__Clients__DoorCou__1FCDBCEB] DEFAULT ((0)) NULL,
    [TheEdge]                      BIT             CONSTRAINT [DF__Clients__TheEdge__21B6055D] DEFAULT ((0)) NULL,
    [Results]                      BIT             CONSTRAINT [DF__Clients__Results__22AA2996] DEFAULT ((0)) NULL,
    [SilentManagerSites]           INT             CONSTRAINT [DF__Clients__SilentM__239E4DCF] DEFAULT ((0)) NULL,
    [MacroOked]                    BIT             CONSTRAINT [DF__Clients__MacroOk__24927208] DEFAULT ((0)) NULL,
    [EdgeV43]                      BIT             CONSTRAINT [DF__Clients__EdgeV43__25869641] DEFAULT ((0)) NULL,
    [RECEmail]                     NVARCHAR (255)  NULL,
    [EdgeV44]                      BIT             CONSTRAINT [DF__Clients__EdgeV44__267ABA7A] DEFAULT ((0)) NULL,
    [SQLServer]                    BIT             CONSTRAINT [DF__Clients__SQLServ__276EDEB3] DEFAULT ((0)) NULL,
    [Jewelsure]                    BIT             CONSTRAINT [DF__Clients__Jewelsu__286302EC] DEFAULT ((0)) NULL,
    [SQLServerPOS]                 BIT             CONSTRAINT [DF__Clients__SQLServ__29572725] DEFAULT ((0)) NULL,
    [USASuperStore]                NVARCHAR (50)   NULL,
    [AccountManager]               NVARCHAR (50)   NULL,
    [TheirSupportEmail]            NVARCHAR (255)  NULL,
    [AutoProcessData]              BIT             CONSTRAINT [DF__Clients__AutoPro__2A4B4B5E] DEFAULT ((0)) NULL,
    [7ZipUsed]                     BIT             CONSTRAINT [DF__Clients__7ZipUse__34C8D9D1] DEFAULT ((0)) NULL,
    [HOStoreNumber]                INT             NULL,
    [SSMA_TimeStamp]               ROWVERSION      NOT NULL,
    [SuperStoreProcessingPriority] INT             NULL,
    [SuperstoreActive]             BIT             CONSTRAINT [DF_Clients_IsSuperstoreActive] DEFAULT ((1)) NOT NULL,
    [EdgePulseAllowed]             BIT             CONSTRAINT [DF_Clients_EdgePulseAllowed] DEFAULT ((1)) NOT NULL,
    [MentoringClient]              BIT             CONSTRAINT [DF_Clients_MentoringClient] DEFAULT ((1)) NOT NULL,
    [EdgeUSAId]                    NVARCHAR (50)   NULL,
    [State]                        NVARCHAR (50)   NULL,
    [LaybysOnPickup]               BIT             CONSTRAINT [DF_Clients_LaybysOnPickup] DEFAULT ((0)) NOT NULL,
    [LastAccountManagerCall]       DATETIME        CONSTRAINT [DF_Clients_LastAccountManagerCall] DEFAULT ('1980-01-01') NOT NULL,
    [ShowcaseWebsiteEnabled]       BIT             CONSTRAINT [DF_Clients_ShowcaseWebsiteEnabled] DEFAULT ((0)) NOT NULL,
    [EdgePulseMembershipType]      NVARCHAR (20)   CONSTRAINT [DF_Clients_EdgePulseMemershipType] DEFAULT (N'Lite') NOT NULL,
    [EdgePulseTrialEnd]            DATE            NULL,
    [BuyingGroupId]                INT             NULL,
    [FakeClient]                   BIT             CONSTRAINT [DF_Clients_FakeClient] DEFAULT ((0)) NOT NULL,
    [SalesMaximum]                 DECIMAL (19, 4) NULL,
    [SalesMinimum]                 DECIMAL (19, 4) NULL,
    [StockMaximum]                 DECIMAL (19, 4) NULL,
    [StockMinimum]                 DECIMAL (19, 4) NULL,
    [SalesMinimumQty]              DECIMAL (19, 4) NULL,
    [SalesMaximumQty]              DECIMAL (19, 4) NULL,
    CONSTRAINT [Clients$PrimaryKey] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 98),
    CONSTRAINT [SSMA_CC$Clients$ClientName$disallow_zero_length] CHECK (len([ClientName])>(0)),
    CONSTRAINT [SSMA_CC$Clients$Comment$disallow_zero_length] CHECK (len([Comment])>(0)),
    CONSTRAINT [SSMA_CC$Clients$Where$disallow_zero_length] CHECK (len([Where])>(0)),
    CONSTRAINT [UK_Clients_ClientNum] UNIQUE NONCLUSTERED ([ClientNum] ASC) WITH (FILLFACTOR = 98)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[ID]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[ClientNum]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'ClientNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[ResultsClientNum]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'ResultsClientNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[ClientName]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'ClientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[KPILite]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'KPILite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[LastDataReceived]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'LastDataReceived';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[Where]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'Where';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[Comment]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[Email]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[ActiveClient]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'ActiveClient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[FocusReports]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'FocusReports';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[SuppSystem]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'SuppSystem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[DoorCounter]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'DoorCounter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[TheEdge]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'TheEdge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[Results]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'Results';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[SilentManagerSites]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'SilentManagerSites';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[MacroOked]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'MacroOked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[EdgeV43]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'EdgeV43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[RECEmail]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'RECEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[EdgeV44]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'EdgeV44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[SQLServer]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'SQLServer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[Jewelsure]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'Jewelsure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[SQLServerPOS]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'SQLServerPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[USASuperStore]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'USASuperStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[AccountManager]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'AccountManager';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[TheirSupportEmail]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'TheirSupportEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[AutoProcessData]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'AutoProcessData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[7ZipUsed]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'7ZipUsed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[HOStoreNumber]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'HOStoreNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'True if a demo or test client', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'COLUMN', @level2name = N'FakeClient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[Clients].[PrimaryKey]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Clients', @level2type = N'CONSTRAINT', @level2name = N'Clients$PrimaryKey';

