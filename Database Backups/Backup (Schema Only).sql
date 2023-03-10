USE [master]
GO
/****** Object:  Database [ImmRequest]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[Administrators]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[Areas]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[CitizenRequests]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[Fields]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[RequestFieldValues]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[Sessions]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[Topics]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Table [dbo].[TopicTypes]    Script Date: 6/24/2020 6:19:13 PM ******/
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
/****** Object:  Index [IX_CitizenRequests_AreaId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_CitizenRequests_AreaId] ON [dbo].[CitizenRequests]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CitizenRequests_TopicId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_CitizenRequests_TopicId] ON [dbo].[CitizenRequests]
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CitizenRequests_TopicTypeId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_CitizenRequests_TopicTypeId] ON [dbo].[CitizenRequests]
(
	[TopicTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fields_ParentTypeId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_Fields_ParentTypeId] ON [dbo].[Fields]
(
	[ParentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequestFieldValues_FieldId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_RequestFieldValues_FieldId] ON [dbo].[RequestFieldValues]
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequestFieldValues_ParentCitizenRequestId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_RequestFieldValues_ParentCitizenRequestId] ON [dbo].[RequestFieldValues]
(
	[ParentCitizenRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sessions_AdministratorInSessionId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_AdministratorInSessionId] ON [dbo].[Sessions]
(
	[AdministratorInSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Topics_AreaId]    Script Date: 6/24/2020 6:19:13 PM ******/
CREATE NONCLUSTERED INDEX [IX_Topics_AreaId] ON [dbo].[Topics]
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TopicTypes_ParentTopicId]    Script Date: 6/24/2020 6:19:13 PM ******/
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
