CREATE TABLE [dbo].[ClientSuperstores] (
    [ID]                INT  NOT NULL,
    [ClientStoreID]     INT  NOT NULL,
    [SuperstoreGroupID] INT  NOT NULL,
    [DateCreated]       DATE CONSTRAINT [DF_ClientSuperstores_DateCreate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ClientSuperstores] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ClientSuperstores_ClientStores] FOREIGN KEY ([SuperstoreGroupID]) REFERENCES [dbo].[GroupNames] ([ID])
);

