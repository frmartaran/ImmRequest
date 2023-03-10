USE [master]
GO
/****** Object:  Database [ImmRequest]    Script Date: 6/24/2020 6:20:33 PM ******/
CREATE DATABASE [ImmRequest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ImmRequest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ImmRequest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ImmRequest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ImmRequest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ImmRequest] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ImmRequest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ImmRequest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ImmRequest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ImmRequest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ImmRequest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ImmRequest] SET ARITHABORT OFF 
GO
ALTER DATABASE [ImmRequest] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ImmRequest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ImmRequest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ImmRequest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ImmRequest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ImmRequest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ImmRequest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ImmRequest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ImmRequest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ImmRequest] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ImmRequest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ImmRequest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ImmRequest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ImmRequest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ImmRequest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ImmRequest] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ImmRequest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ImmRequest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ImmRequest] SET  MULTI_USER 
GO
ALTER DATABASE [ImmRequest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ImmRequest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ImmRequest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ImmRequest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ImmRequest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ImmRequest] SET QUERY_STORE = OFF
GO
USE [ImmRequest]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrators](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CitizenRequests]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CitizenRequests](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CitizenName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[AreaId] [bigint] NOT NULL,
	[TopicId] [bigint] NOT NULL,
	[TopicTypeId] [bigint] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CitizenRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fields]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fields](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ParentTypeId] [bigint] NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[RangeValues] [nvarchar](max) NULL,
	[DeletedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[End] [datetime2](7) NULL,
	[Start] [datetime2](7) NULL,
	[RangeEnd] [int] NULL,
	[RangeStart] [int] NULL,
	[IsMultipleSelectEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_Fields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestFieldValues]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestFieldValues](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FieldId] [bigint] NOT NULL,
	[ParentCitizenRequestId] [bigint] NOT NULL,
	[Values] [nvarchar](max) NULL,
 CONSTRAINT [PK_RequestFieldValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[AdministratorInSessionId] [bigint] NOT NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AreaId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopicTypes]    Script Date: 6/24/2020 6:20:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopicTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentTopicId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DeletedDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TopicTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200402002505_InitialMigration', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200404004809_AddSoftDeleteAndValuesToRequest', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200404010147_AddNameToTopic', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200407012608_IncludeAllTypesOfFields', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200410201506_AdminEmail', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200421150430_EditedCitizenRequestIdName', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200423223033_AddDefaultAdministrator', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200423231041_AddAreas', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200423231910_AddTopics', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200504013930_ChangedDeleteBehaviour', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200513005946_RemoveRequestNumber', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200514033712_EditedSessionAttributes', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200514120213_RequestFieldValuesDeleteBehaviorSpecified', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200514205938_OneToManyRelationshipWithFields', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200522015020_AddCreatedDates', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200522033924_AddedBoolFieldAndMultiSelector', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200522212638_ChangedValuesToList', N'3.1.3')
SET IDENTITY_INSERT [dbo].[Administrators] ON 

INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (1, N'Administrator', N'1234', N'administrator@global.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (2, N'Julie', N'1234', N'julie@email.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (3, N'Fran', N'1234', N'fran@email.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (4, N'Mark', N'1234', N'mark@email.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (5, N'Micaela', N'1234', N'mica@email.com')
SET IDENTITY_INSERT [dbo].[Administrators] OFF
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [Name]) VALUES (1, N'Espacios Publicos y Calles')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (2, N'Limpieza')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (3, N'Saneamiento')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (4, N'Transporte')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (5, N'Eventos')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (6, N'Educacion')
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[CitizenRequests] ON 

INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId], [CreatedDate]) VALUES (1, N'No hay luz en mi calle', N'Juliette', N'Juliette@gmail.com', N'099099099', 4, 1, 1, 1, CAST(N'2020-06-24T12:08:51.3156232' AS DateTime2))
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId], [CreatedDate]) VALUES (2, N'Poder árboles por favor, gracias', N'Juliette', N'Juliette@gmail.com', N'099099099', 1, 1, 2, 2, CAST(N'2020-06-24T12:10:00.5017992' AS DateTime2))
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId], [CreatedDate]) VALUES (3, N'Hay un perdida de gas en el edificio y demoraron mucho en resolverla', N'Juliette', N'Juliette@gmail.com', N'099099099', 1, 3, 11, 3, CAST(N'2020-06-24T12:10:59.6432038' AS DateTime2))
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId], [CreatedDate]) VALUES (4, N'Hubo un perdida en mi edificio. Help', N'Juliette', N'Juliette@gmail.com', N'099099099', 0, 3, 11, 3, CAST(N'2020-06-24T12:15:09.7093170' AS DateTime2))
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId], [CreatedDate]) VALUES (5, N'Muchos Paros', N'Fran', N'fran@gmai.com', N'099099099', 0, 6, 23, 10, CAST(N'2020-06-24T12:29:10.1200564' AS DateTime2))
SET IDENTITY_INSERT [dbo].[CitizenRequests] OFF
SET IDENTITY_INSERT [dbo].[Fields] ON 

INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart], [IsMultipleSelectEnabled]) VALUES (1, N'Cantidad de Lamparas', 1, N'NumberField', NULL, CAST(N'2020-06-24T17:38:06.8135016' AS DateTime2), 1, NULL, NULL, 10, 0, 0)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart], [IsMultipleSelectEnabled]) VALUES (2, N'Hay Quemadas', 1, N'BoolField', NULL, CAST(N'2020-06-24T17:38:06.8144595' AS DateTime2), 1, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart], [IsMultipleSelectEnabled]) VALUES (3, N'Fecha', 3, N'DateTimeField', NULL, CAST(N'2020-06-24T12:13:24.6496916' AS DateTime2), 1, CAST(N'2020-12-31T00:00:00.0000000' AS DateTime2), CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart], [IsMultipleSelectEnabled]) VALUES (4, N'Barrio', 3, N'TextField', N'["Pocitos","Punta Carretas","Centro","Ciudad Vieja","Otro"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart], [IsMultipleSelectEnabled]) VALUES (5, N'Fecha', 3, N'DateTimeField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-12-31T00:00:00.0000000' AS DateTime2), CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart], [IsMultipleSelectEnabled]) VALUES (6, N'Barrio', 15, N'TextField', N'["Pocitos","Punta Carretas","Centro"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Fields] OFF
SET IDENTITY_INSERT [dbo].[RequestFieldValues] ON 

INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [ParentCitizenRequestId], [Values]) VALUES (1, 1, 1, N'["5"]')
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [ParentCitizenRequestId], [Values]) VALUES (2, 2, 1, N'["true"]')
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [ParentCitizenRequestId], [Values]) VALUES (3, 4, 4, N'["Pocitos"]')
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [ParentCitizenRequestId], [Values]) VALUES (4, 5, 4, N'["2020-06-24","2020-06-23"]')
SET IDENTITY_INSERT [dbo].[RequestFieldValues] OFF
SET IDENTITY_INSERT [dbo].[Sessions] ON 

INSERT [dbo].[Sessions] ([Id], [Token], [AdministratorInSessionId]) VALUES (3, N'6d0f24ca-dd91-4f90-a04a-c2a721572bb4', 1)
SET IDENTITY_INSERT [dbo].[Sessions] OFF
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (1, 1, N'Alumbrado')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (2, 1, N'Arbolado y Plantacion')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (3, 1, N'Equipamiento Urbano')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (4, 1, N'Fuentes, Graffitis e Instalaciones')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (5, 1, N'Otros')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (6, 2, N'Estados de los contenedores')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (7, 2, N'Problemas de limpieza')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (8, 2, N'Solicitud de retiro de poda, escombros o residuos de gran tamaño')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (9, 2, N'Otros')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (10, 3, N'Bocas de tormenta')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (11, 3, N'Obstrucciones o Perdidas')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (12, 3, N'Otros')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (13, 4, N'Acoso Sexual')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (14, 4, N'Paradas')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (15, 4, N'Taxis, remixes, escolares')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (16, 4, N'Ambulancias')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (17, 4, N'Terminales')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (18, 4, N'Transporte Colectivo')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (19, 4, N'Otros')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (20, 3, N'Mantenimiento')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (21, 5, N'Gubernamental')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (22, 5, N'Internacionales')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (23, 6, N'Perdida de Clases')
INSERT [dbo].[Topics] ([Id], [AreaId], [Name]) VALUES (24, 6, N'Programas')
SET IDENTITY_INSERT [dbo].[Topics] OFF
SET IDENTITY_INSERT [dbo].[TopicTypes] ON 

INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (1, 1, N'Calles Ocuras', CAST(N'2020-06-24T17:38:06.8103115' AS DateTime2), 1, CAST(N'2020-06-24T11:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (2, 2, N'Podado de Árboles', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T12:00:39.0102680' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (3, 11, N'Perdidas de Gas', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (4, 11, N'Inundacion en edificio', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (5, 20, N'Mantenimiento de cañerias', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (6, 20, N'Otros', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (7, 21, N'Dias Festivos', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (8, 22, N'Deportivos', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (9, 22, N'Musicales', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T13:58:23.3687364' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (10, 23, N'Paros', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T12:27:44.1380390' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (11, 23, N'Feriados', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T12:27:44.1395035' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (12, 24, N'Incumplimiento de reglamento', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T12:27:44.2117596' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (13, 24, N'Falta de Innovacion', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T12:27:44.2117660' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (14, 24, N'Examenes', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T12:27:44.2117671' AS DateTime2))
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted], [CreatedAt]) VALUES (15, 1, N'Luz Rota', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-06-24T17:25:58.3006600' AS DateTime2))
SET IDENTITY_INSERT [dbo].[TopicTypes] OFF
/****** Object:  Index [IX_CitizenRequests_AreaId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_CitizenRequests_AreaId] ON [dbo].[CitizenRequests]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CitizenRequests_TopicId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_CitizenRequests_TopicId] ON [dbo].[CitizenRequests]
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CitizenRequests_TopicTypeId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_CitizenRequests_TopicTypeId] ON [dbo].[CitizenRequests]
(
	[TopicTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fields_ParentTypeId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Fields_ParentTypeId] ON [dbo].[Fields]
(
	[ParentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequestFieldValues_FieldId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_RequestFieldValues_FieldId] ON [dbo].[RequestFieldValues]
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequestFieldValues_ParentCitizenRequestId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_RequestFieldValues_ParentCitizenRequestId] ON [dbo].[RequestFieldValues]
(
	[ParentCitizenRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sessions_AdministratorInSessionId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_AdministratorInSessionId] ON [dbo].[Sessions]
(
	[AdministratorInSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Topics_AreaId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_Topics_AreaId] ON [dbo].[Topics]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TopicTypes_ParentTopicId]    Script Date: 6/24/2020 6:20:34 PM ******/
CREATE NONCLUSTERED INDEX [IX_TopicTypes_ParentTopicId] ON [dbo].[TopicTypes]
(
	[ParentTopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CitizenRequests] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [TopicId]
GO
ALTER TABLE [dbo].[CitizenRequests] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [TopicTypeId]
GO
ALTER TABLE [dbo].[CitizenRequests] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Fields] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DeletedDate]
GO
ALTER TABLE [dbo].[Fields] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Fields] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsMultipleSelectEnabled]
GO
ALTER TABLE [dbo].[RequestFieldValues] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [ParentCitizenRequestId]
GO
ALTER TABLE [dbo].[Sessions] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [AdministratorInSessionId]
GO
ALTER TABLE [dbo].[TopicTypes] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DeletedDate]
GO
ALTER TABLE [dbo].[TopicTypes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TopicTypes] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CitizenRequests]  WITH CHECK ADD  CONSTRAINT [FK_CitizenRequests_Areas_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[CitizenRequests] CHECK CONSTRAINT [FK_CitizenRequests_Areas_AreaId]
GO
ALTER TABLE [dbo].[CitizenRequests]  WITH CHECK ADD  CONSTRAINT [FK_CitizenRequests_Topics_TopicId] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topics] ([Id])
GO
ALTER TABLE [dbo].[CitizenRequests] CHECK CONSTRAINT [FK_CitizenRequests_Topics_TopicId]
GO
ALTER TABLE [dbo].[CitizenRequests]  WITH CHECK ADD  CONSTRAINT [FK_CitizenRequests_TopicTypes_TopicTypeId] FOREIGN KEY([TopicTypeId])
REFERENCES [dbo].[TopicTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CitizenRequests] CHECK CONSTRAINT [FK_CitizenRequests_TopicTypes_TopicTypeId]
GO
ALTER TABLE [dbo].[Fields]  WITH CHECK ADD  CONSTRAINT [FK_Fields_TopicTypes_ParentTypeId] FOREIGN KEY([ParentTypeId])
REFERENCES [dbo].[TopicTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Fields] CHECK CONSTRAINT [FK_Fields_TopicTypes_ParentTypeId]
GO
ALTER TABLE [dbo].[RequestFieldValues]  WITH CHECK ADD  CONSTRAINT [FK_RequestFieldValues_CitizenRequests_ParentCitizenRequestId] FOREIGN KEY([ParentCitizenRequestId])
REFERENCES [dbo].[CitizenRequests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequestFieldValues] CHECK CONSTRAINT [FK_RequestFieldValues_CitizenRequests_ParentCitizenRequestId]
GO
ALTER TABLE [dbo].[RequestFieldValues]  WITH CHECK ADD  CONSTRAINT [FK_RequestFieldValues_Fields_FieldId] FOREIGN KEY([FieldId])
REFERENCES [dbo].[Fields] ([Id])
GO
ALTER TABLE [dbo].[RequestFieldValues] CHECK CONSTRAINT [FK_RequestFieldValues_Fields_FieldId]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Administrators_AdministratorInSessionId] FOREIGN KEY([AdministratorInSessionId])
REFERENCES [dbo].[Administrators] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Administrators_AdministratorInSessionId]
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Areas_AreaId] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Areas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Areas_AreaId]
GO
ALTER TABLE [dbo].[TopicTypes]  WITH CHECK ADD  CONSTRAINT [FK_TopicTypes_Topics_ParentTopicId] FOREIGN KEY([ParentTopicId])
REFERENCES [dbo].[Topics] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopicTypes] CHECK CONSTRAINT [FK_TopicTypes_Topics_ParentTopicId]
GO
USE [master]
GO
ALTER DATABASE [ImmRequest] SET  READ_WRITE 
GO
