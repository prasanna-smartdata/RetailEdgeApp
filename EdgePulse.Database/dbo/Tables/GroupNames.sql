CREATE TABLE [dbo].[GroupNames] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [GroupNum]   INT           DEFAULT ((0)) NULL,
    [GroupName]  NVARCHAR (50) NULL,
    [DeptsToUse] NVARCHAR (1)  NULL,
    CONSTRAINT [GroupNames$Id] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 98),
    CONSTRAINT [SSMA_CC$GroupNames$DeptsToUse$disallow_zero_length] CHECK (len([DeptsToUse])>(0)),
    CONSTRAINT [SSMA_CC$GroupNames$GroupName$disallow_zero_length] CHECK (len([GroupName])>(0))
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[GroupNames]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupNames';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[GroupNames].[ID]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupNames', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[GroupNames].[GroupNum]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupNames', @level2type = N'COLUMN', @level2name = N'GroupNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[GroupNames].[GroupName]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupNames', @level2type = N'COLUMN', @level2name = N'GroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[GroupNames].[DeptsToUse]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupNames', @level2type = N'COLUMN', @level2name = N'DeptsToUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_SSMA_SOURCE', @value = N'RMHClients.[GroupNames].[Id]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'GroupNames', @level2type = N'CONSTRAINT', @level2name = N'GroupNames$Id';

