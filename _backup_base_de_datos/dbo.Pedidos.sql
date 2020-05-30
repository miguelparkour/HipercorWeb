CREATE TABLE [dbo].[Pedidos] (
    [IdPed]    NVARCHAR (MAX) NOT NULL,
    [Email]    VARCHAR (50)   NOT NULL,
    [IdProd]   INT            NOT NULL,
    [Cantidad] INT            NOT NULL,
    [Fecha]    DATETIME       NOT NULL,
    [CalleMun] VARCHAR (MAX)  NOT NULL
);
