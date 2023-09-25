USE [marcos]
GO

/****** Object:  Table [dbo].[veiculo]    Script Date: 25/09/2023 09:22:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[veiculo_site](
	[id] [int] IDENTITY(1,1) NOT NULL,
    [id_rp] [int],
	[placa] [varchar](255) NULL,
	[descricao] [varchar](255) NULL,
	[url_imagem] [text] NULL,
	[marca] [varchar](255) NULL,
	[modelo] [varchar](255) NULL,
	[quantidade_lugares] [text] NULL
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


