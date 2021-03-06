USE [master]
GO
/****** Object:  Database [is-fakultet]    Script Date: 25.7.2015 16:05:37 ******/
CREATE DATABASE [is-fakultet] ON  PRIMARY 
( NAME = N'is-fakultet', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\is-fakultet.mdf' , SIZE = 7168KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'is-fakultet_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\is-fakultet_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [is-fakultet] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [is-fakultet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [is-fakultet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [is-fakultet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [is-fakultet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [is-fakultet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [is-fakultet] SET ARITHABORT OFF 
GO
ALTER DATABASE [is-fakultet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [is-fakultet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [is-fakultet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [is-fakultet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [is-fakultet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [is-fakultet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [is-fakultet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [is-fakultet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [is-fakultet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [is-fakultet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [is-fakultet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [is-fakultet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [is-fakultet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [is-fakultet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [is-fakultet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [is-fakultet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [is-fakultet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [is-fakultet] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [is-fakultet] SET  MULTI_USER 
GO
ALTER DATABASE [is-fakultet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [is-fakultet] SET DB_CHAINING OFF 
GO
USE [is-fakultet]
GO
/****** Object:  Table [dbo].[Ispits]    Script Date: 25.7.2015 16:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ispits](
	[IspitID] [float] NOT NULL,
	[Naziv] [nvarchar](255) NULL,
 CONSTRAINT [PK_Ispit] PRIMARY KEY CLUSTERED 
(
	[IspitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prijavas]    Script Date: 25.7.2015 16:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prijavas](
	[BI] [nvarchar](255) NOT NULL,
	[IspitID] [float] NOT NULL,
	[Ocena] [float] NULL,
 CONSTRAINT [PK_Prijava] PRIMARY KEY CLUSTERED 
(
	[BI] ASC,
	[IspitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 25.7.2015 16:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[BI] [nvarchar](255) NOT NULL,
	[Ime] [nvarchar](255) NULL,
	[Prezime] [nvarchar](255) NULL,
	[Adresa] [nvarchar](255) NULL,
	[Grad] [nvarchar](255) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[BI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Ispits] ([IspitID], [Naziv]) VALUES (1, N'Matematika 1')
INSERT [dbo].[Ispits] ([IspitID], [Naziv]) VALUES (2, N'Programiranje 1')
INSERT [dbo].[Ispits] ([IspitID], [Naziv]) VALUES (3, N'Matematika 2')
INSERT [dbo].[Ispits] ([IspitID], [Naziv]) VALUES (4, N'Programiranje 2')
INSERT [dbo].[Ispits] ([IspitID], [Naziv]) VALUES (5, N'Baze Podataka')
INSERT [dbo].[Ispits] ([IspitID], [Naziv]) VALUES (6, N'Statistika')
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'03211', 1, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'03211', 2, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'03211', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'03211', 4, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'03211', 5, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'03211', 6, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'04711', 1, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'04711', 2, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'04711', 3, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'04711', 4, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'04711', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'04711', 6, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'12311', 1, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'12311', 2, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'12311', 3, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'12311', 4, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'12311', 5, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'12311', 6, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'14811', 1, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'14811', 2, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'14811', 3, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'14811', 4, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'14811', 5, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'14811', 6, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'15411', 1, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'15411', 2, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'15411', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'15411', 4, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'15411', 5, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'15411', 6, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'17311', 1, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'17311', 2, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'17311', 3, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'17311', 4, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'17311', 5, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'17311', 6, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'20311', 1, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'20311', 2, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'20311', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'20311', 4, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'20311', 5, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'20311', 6, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'22311', 1, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'22311', 2, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'22311', 3, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'22311', 4, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'22311', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'22311', 6, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24311', 1, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24311', 2, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24311', 3, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24311', 4, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24311', 5, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24311', 6, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24411', 1, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24411', 2, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24411', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24411', 4, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24411', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'24411', 6, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'29611', 1, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'29611', 2, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'29611', 3, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'29611', 4, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'29611', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'29611', 6, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'31211', 1, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'31211', 2, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'31211', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'31211', 4, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'31211', 5, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'31211', 6, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'32111', 1, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'32111', 2, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'32111', 3, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'32111', 4, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'32111', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'32111', 6, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'33111', 1, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'33111', 2, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'33111', 3, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'33111', 4, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'33111', 5, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'33111', 6, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'35011', 1, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'35011', 2, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'35011', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'35011', 4, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'35011', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'35011', 6, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'40111', 1, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'40111', 2, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'40111', 3, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'40111', 4, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'40111', 5, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'40111', 6, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'42211', 1, 10)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'42211', 2, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'42211', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'42211', 4, 8)
GO
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'42211', 5, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'42211', 6, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'43211', 1, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'43211', 2, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'43211', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'43211', 4, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'43211', 5, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'43211', 6, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'52111', 1, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'52111', 2, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'52111', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'52111', 4, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'52111', 5, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'52111', 6, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'55111', 1, 6)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'55111', 2, 7)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'55111', 3, 8)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'55111', 4, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'55111', 5, 9)
INSERT [dbo].[Prijavas] ([BI], [IspitID], [Ocena]) VALUES (N'55111', 6, 8)
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'03211', N'Todor', N'Latinovic', N'Zahumska 14', N'Kragujevac')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'04711', N'Janko', N'Veselinovic', N'Partizanska 53', N'Nis')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'12311', N'Petar', N'Petrovic', N'Svetogorska 13', N'Beograd')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'14811', N'Jovana', N'Ilic', N'Humska 44', N'Beograd')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'15411', N'Filip', N'Babic', N'Dimitrija Tucovica 21', N'Novi Sad')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'17311', N'Milica', N'Sretenovic', N'Zmaj Jovina 26', N'Nis')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'20311', N'Luka', N'Milojevic', N'Vladimira Rolovica 9', N'Nis')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'22311', N'Milos', N'Stojanovic', N'Svetozara Corovica 11', N'Beograd')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'24311', N'Goran', N'Copic', N'Studentska 12', N'Novi Sad')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'24411', N'Una', N'Spasic', N'Njegoseva 12', N'Novi Sad')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'29611', N'Igor', N'Kalinic', N'Vuka Karadzica 67', N'Nis')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'31211', N'Djordje', N'Vukadinovic', N'Ruzveltova 55', N'Novi Sad')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'32111', N'Marko', N'Rodic', N'Jove Ilica 22', N'Beograd')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'33111', N'Dragan', N'Markovic', N'Dalmatinska 33', N'Beograd')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'35011', N'Vladimir', N'Jovanovic', N'Mese Selimovica 3', N'Kragujevac')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'40111', N'Olja', N'Nesic', N'Gandijeva 133', N'Kragujevac')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'42211', N'Tanja', N'Misic', N'Ive Andrica 51', N'Kragujevac')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'43211', N'Ognjen', N'Tadic', N'Filipa Visnjica 41', N'Novi Sad')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'52111', N'Predrag', N'Arsic', N'Koce Kapetana 23', N'Kragujevac')
INSERT [dbo].[Students] ([BI], [Ime], [Prezime], [Adresa], [Grad]) VALUES (N'55111', N'Ana', N'Vujovic', N'Kosovska 8', N'Nis')
ALTER TABLE [dbo].[Prijavas]  WITH CHECK ADD  CONSTRAINT [FK_Prijava_Ispit] FOREIGN KEY([IspitID])
REFERENCES [dbo].[Ispits] ([IspitID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prijavas] CHECK CONSTRAINT [FK_Prijava_Ispit]
GO
ALTER TABLE [dbo].[Prijavas]  WITH CHECK ADD  CONSTRAINT [FK_Prijava_Student] FOREIGN KEY([BI])
REFERENCES [dbo].[Students] ([BI])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prijavas] CHECK CONSTRAINT [FK_Prijava_Student]
GO
USE [master]
GO
ALTER DATABASE [is-fakultet] SET  READ_WRITE 
GO
