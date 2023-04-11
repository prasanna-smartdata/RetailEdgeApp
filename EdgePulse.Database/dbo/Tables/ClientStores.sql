CREATE TABLE [dbo].[ClientStores] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [ClientID]  INT           NULL,
    [ClientNum] INT           NOT NULL,
    [StoreID]   INT           NOT NULL,
    [StoreName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ClientStores] PRIMARY KEY CLUSTERED ([ID] ASC)
);

