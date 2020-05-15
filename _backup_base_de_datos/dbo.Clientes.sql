CREATE TABLE [dbo].[Clientes] (
    [Email]        NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (MAX) NOT NULL,
    [CuentaActiva] BIT            NULL,
    [Nombre]       NVARCHAR (50)  NULL,
    [Apellidos]    NVARCHAR (50)  NULL,
    [Movil]        NVARCHAR (50)  NULL,
    [Fijo]         NVARCHAR (50)  DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([Email] ASC)
);

