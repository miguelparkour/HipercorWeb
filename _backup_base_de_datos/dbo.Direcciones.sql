CREATE TABLE [dbo].[Direcciones] (
    [Email]  NVARCHAR (50)  NOT NULL,
    [CP]     VARCHAR (50)   NOT NULL,
    [IdProv] NVARCHAR (50)  NOT NULL,
    [IdMun]  NVARCHAR (50)  NOT NULL,
    [Calle]  NVARCHAR (MAX) NOT NULL
);

