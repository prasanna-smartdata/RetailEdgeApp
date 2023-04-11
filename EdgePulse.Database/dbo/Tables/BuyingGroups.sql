CREATE TABLE [dbo].[BuyingGroups] (
    [BuyingGroupId]   INT           NOT NULL,
    [BuyingGroupName] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_BuyingGroups] PRIMARY KEY CLUSTERED ([BuyingGroupId] ASC)
);

