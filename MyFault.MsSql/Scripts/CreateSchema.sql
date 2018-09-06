USE [MYFAULT_DEV]

GO
/****** Object:  Table [dbo].[Fault]    Script Date: 9/4/2018 11:20:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fault](
	[FaultId] [uniqueidentifier] NOT NULL,
	[FaultHash] [varchar](64) NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[Kind] [varchar](255) NULL,
 CONSTRAINT [Fault_FaultId_pk] PRIMARY KEY CLUSTERED 
(
	[FaultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instance]    Script Date: 9/4/2018 11:20:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instance](
	[InstanceId] [uniqueidentifier] NOT NULL,
	[FaultId] [uniqueidentifier] NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
 CONSTRAINT [Instance_InstanceId_pk] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstanceData]    Script Date: 9/4/2018 11:20:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstanceData](
	[InstanceId] [uniqueidentifier] NOT NULL,
	[DataTypeId] [tinyint] NULL,
	[DataKey] [varchar](255) NOT NULL,
	[DataValue] [nvarchar](max) NULL,
 CONSTRAINT [InstanceData_InstanceId_DataKey_pk] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC,
	[DataKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstanceDataBinary]    Script Date: 9/4/2018 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstanceDataBinary](
	[InstanceId] [uniqueidentifier] NOT NULL,
	[DataKey] [varchar](255) NOT NULL,
	[DataContentType] [varchar](255) NULL,
	[DataContents] [varbinary](max) NULL,
 CONSTRAINT [InstanceDataBinary_InstanceId_Name_pk] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC,
	[DataKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Fault] ADD  CONSTRAINT [DF_Fault_FaultId]  DEFAULT (newid()) FOR [FaultId]
GO
ALTER TABLE [dbo].[Fault] ADD  CONSTRAINT [DF__Fault__FaultId__47DBAE45]  DEFAULT (newid()) FOR [FaultHash]
GO
ALTER TABLE [dbo].[Fault] ADD  CONSTRAINT [DF__Fault__CreatedTi__48CFD27E]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Instance] ADD  CONSTRAINT [DF__Instance__Instan__4BAC3F29]  DEFAULT (newid()) FOR [InstanceId]
GO
ALTER TABLE [dbo].[InstanceData]  WITH CHECK ADD  CONSTRAINT [InstanceData_Instance_InstanceId_fk] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
ALTER TABLE [dbo].[InstanceData] CHECK CONSTRAINT [InstanceData_Instance_InstanceId_fk]
GO
ALTER TABLE [dbo].[InstanceDataBinary]  WITH CHECK ADD  CONSTRAINT [InstanceDataBinary_Instance_InstanceId_fk] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
ALTER TABLE [dbo].[InstanceDataBinary] CHECK CONSTRAINT [InstanceDataBinary_Instance_InstanceId_fk]
GO
