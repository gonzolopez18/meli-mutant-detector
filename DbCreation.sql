USE [master]
GO
/****** Object:  Database [dnadb]    Script Date: 12/5/2021 13:28:24 ******/
CREATE DATABASE [dnadb]
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
USE [dnadb]
GO
/****** Object:  Table [dbo].[Dna]    Script Date: 12/5/2021 13:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dna](
	[Id] [uniqueidentifier] NOT NULL,
	[DnaSecuence] [varchar](max) NOT NULL,
	[IsMutant] [bit] NOT NULL,
 CONSTRAINT [PK_Dna] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stats]    Script Date: 12/5/2021 13:28:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stats](
	[Id] [uniqueidentifier] NOT NULL,
	[count_mutant_dna] [int] NOT NULL,
	[count_human_dna] [int] NOT NULL,
	[LastUpdate] [timestamp] NOT NULL,
 CONSTRAINT [PK_Stats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
