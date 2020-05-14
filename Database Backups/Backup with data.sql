USE [ImmRequest]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/14/2020 7:36:45 PM ******/
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
/****** Object:  Table [dbo].[Administrators]    Script Date: 5/14/2020 7:36:45 PM ******/
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
/****** Object:  Table [dbo].[Areas]    Script Date: 5/14/2020 7:36:45 PM ******/
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
/****** Object:  Table [dbo].[CitizenRequests]    Script Date: 5/14/2020 7:36:45 PM ******/
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
 CONSTRAINT [PK_CitizenRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fields]    Script Date: 5/14/2020 7:36:45 PM ******/
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
 CONSTRAINT [PK_Fields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestFieldValues]    Script Date: 5/14/2020 7:36:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestFieldValues](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FieldId] [bigint] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ParentCitizenRequestId] [bigint] NOT NULL,
 CONSTRAINT [PK_RequestFieldValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 5/14/2020 7:36:45 PM ******/
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
/****** Object:  Table [dbo].[Topics]    Script Date: 5/14/2020 7:36:45 PM ******/
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
/****** Object:  Table [dbo].[TopicTypes]    Script Date: 5/14/2020 7:36:45 PM ******/
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
SET IDENTITY_INSERT [dbo].[Administrators] ON 

INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (1, N'Administrator', N'1234', N'administrator@global.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (2, N'francisco', N'heyJuliette', N'frmartaran@gmail.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (3, N'Juliette', N'1234', N'juliette@gmail.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (4, N'Admin', N'contraseña', N'admin@gmail.com')
INSERT [dbo].[Administrators] ([Id], [UserName], [Password], [Email]) VALUES (5, N'Sofia', N'8521', N'anotherAdmin@gmail.com')
SET IDENTITY_INSERT [dbo].[Administrators] OFF
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [Name]) VALUES (1, N'Espacios Publicos y Calles')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (2, N'Limpieza')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (3, N'Saneamiento')
INSERT [dbo].[Areas] ([Id], [Name]) VALUES (4, N'Transporte')
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[CitizenRequests] ON 

INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId]) VALUES (1, N'Arreglar luces', N'Juliette', N'juliette@gmail.com', N'123456789', 0, 2, 7, 1)
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId]) VALUES (2, N'Arreglar luces', N'Juliette', N'juliette@gmail.com', N'123456789', 0, 2, 7, 2)
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId]) VALUES (3, N'Mucha Basura', N'Juliette', N'alguien@gmail.com', N'123456789', 0, 2, 7, 3)
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId]) VALUES (4, N'Arboles cortan calles', N'Pedro', N'alguien@gmail.com', N'123456789', 0, 2, 7, 3)
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId]) VALUES (5, N'No se ve nada en la ruta 1', N'Martin', N'alguien@gmail.com', N'123456789', 0, 2, 7, 2)
INSERT [dbo].[CitizenRequests] ([Id], [Description], [CitizenName], [Email], [Phone], [Status], [AreaId], [TopicId], [TopicTypeId]) VALUES (6, N'Parada suspendida', N'Martin', N'alguien@gmail.com', N'123456789', 0, 1, 2, 7)
SET IDENTITY_INSERT [dbo].[CitizenRequests] OFF
SET IDENTITY_INSERT [dbo].[Fields] ON 

INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (1, N'Horario', 1, N'DateTimeField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-04-25T18:25:43.0000000' AS DateTime2), CAST(N'2020-04-23T18:25:43.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (2, N'cantidad de reparaciones', 1, N'NumberField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, 50, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (3, N'Barrios', 1, N'TextField', N'["Pocitos","Punta Carretas"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (4, N'Horario', 2, N'DateTimeField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'2020-05-10T18:25:43.0000000' AS DateTime2), CAST(N'2020-01-01T18:25:43.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (5, N'Dias', 2, N'TextField', N'["Lunes","Miercoles","Viernes"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (6, N'Cantidad Contenedores', 3, N'NumberField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, 15, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (7, N'Dias', 3, N'TextField', N'["Lunes","Miercoles","Viernes"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (8, N'Numeros de omnibuses', 7, N'NumberField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, 316, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (9, N'Barrios', 7, N'TextField', N'["Pocitos","Centro","Punta Carretas"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (10, N'Numeros de omnibuses', 8, N'NumberField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, 316, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (11, N'Barrios', 8, N'TextField', N'["Pocitos","Centro","Punta Carretas"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (12, N'Barrios', 9, N'TextField', N'["Pocitos","Centro","Punta Carretas"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (13, N'Barrios', 10, N'TextField', N'["Pocitos","Centro","Punta Carretas"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (14, N'Cantidad', 11, N'NumberField', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, 316, 1)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (15, N'Barrios', 11, N'TextField', N'["Pocitos","Centro","Punta Carretas"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[Fields] ([Id], [Name], [ParentTypeId], [Discriminator], [RangeValues], [DeletedDate], [IsDeleted], [End], [Start], [RangeEnd], [RangeStart]) VALUES (16, N'Calles', 12, N'TextField', N'["Av Italia","Av Brasil","Rivera"]', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Fields] OFF
SET IDENTITY_INSERT [dbo].[RequestFieldValues] ON 

INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [Value], [ParentCitizenRequestId]) VALUES (1, 1, N'2020-04-24T18:25:43.511Z', 1)
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [Value], [ParentCitizenRequestId]) VALUES (2, 2, N'5', 2)
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [Value], [ParentCitizenRequestId]) VALUES (3, 3, N'Pocitos', 3)
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [Value], [ParentCitizenRequestId]) VALUES (4, 8, N'121', 6)
INSERT [dbo].[RequestFieldValues] ([Id], [FieldId], [Value], [ParentCitizenRequestId]) VALUES (5, 9, N'Centro', 6)
SET IDENTITY_INSERT [dbo].[RequestFieldValues] OFF
SET IDENTITY_INSERT [dbo].[Sessions] ON 

INSERT [dbo].[Sessions] ([Id], [Token], [AdministratorInSessionId]) VALUES (1, N'7933fecd-6c84-4dc3-a223-deed70461c1c', 1)
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
SET IDENTITY_INSERT [dbo].[Topics] OFF
SET IDENTITY_INSERT [dbo].[TopicTypes] ON 

INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (1, 7, N'Reparacion de Luces', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (2, 7, N'Lipieza de calles', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (3, 7, N'Contenedores Sucios', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (4, 1, N'Arboles caidos', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (5, 2, N'Arboles caidos', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (6, 2, N'Poca Iluminacion', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (7, 2, N'Paradas de Omnibus', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (8, 3, N'Paradas de Omnibus', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (9, 4, N'Parades Sucias', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (10, 4, N'Grafitis', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (11, 10, N'Boca de tormentas tapadas', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[TopicTypes] ([Id], [ParentTopicId], [Name], [DeletedDate], [IsDeleted]) VALUES (12, 10, N'inundacion', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[TopicTypes] OFF
ALTER TABLE [dbo].[CitizenRequests] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [TopicId]
GO
ALTER TABLE [dbo].[CitizenRequests] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [TopicTypeId]
GO
ALTER TABLE [dbo].[Fields] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DeletedDate]
GO
ALTER TABLE [dbo].[Fields] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[RequestFieldValues] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [ParentCitizenRequestId]
GO
ALTER TABLE [dbo].[Sessions] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [AdministratorInSessionId]
GO
ALTER TABLE [dbo].[TopicTypes] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DeletedDate]
GO
ALTER TABLE [dbo].[TopicTypes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
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
