CREATE TABLE Categoria(
	IdCategoria INT IDENTITY(1,1) NOT NULL,
	Descricao VARCHAR(MAX) NOT NULL,
	DataInsert DATETIME NULL DEFAULT(GETDATE()),
	Ativo BIT NULL DEFAULT((1)),
	CONSTRAINT PK_Categoria PRIMARY KEY (IdCategoria)
)