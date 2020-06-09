ALTER TABLE [dbo].[s_place] ADD payment numeric(6,2) null 
GO

ALTER TABLE [inventory].[j_scanerBlank] ADD id_Shop  int not null DEFAULT 1
GO
ALTER TABLE [inventory].[j_scanerBlank] ADD CONSTRAINT FK_j_scanerBlank_id_Shop FOREIGN KEY (id_Shop)  REFERENCES [dbo].[s_Shop] (id)
GO

ALTER TABLE [inventory].[j_scanerBlank] ADD id_spacing  int null 
GO


CREATE TABLE [inventory].[s_ExceptionType](
	[id]					int				IDENTITY(1,1) NOT NULL,
	[cName]					varchar(50)		not null,
	[isDop]					bit				not null,
 CONSTRAINT [PK_s_ExceptionType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [inventory].[s_ExceptionType] (cName,isDop)
values ('Суточник',0),('Развозка',1),('Ввод данных/сверка',1),('Контролер инв.',1)
GO


CREATE TABLE [inventory].[s_Exception](
	[id]					int				IDENTITY(1,1) NOT NULL,
	[id_kadr]				int				not null,
	[id_ttost]				int				not null,
	[id_shop]				int				not null,
	[id_ExceptionType]		int				not null,
	[Summa]					numeric(11,2)	null,
	[CountDays]				numeric(6,1)	null,
	[isDop]					bit				not null,
	[id_Creator]			int				not null,
	[DateCreate]			datetime		not null,
	[id_Editor]				int				null,
	[DateEdit]				datetime		null,
 CONSTRAINT [PK_s_Exception] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [inventory].[s_Exception] ADD CONSTRAINT FK_s_Exception_id_Creator FOREIGN KEY (id_Creator)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [inventory].[s_Exception] ADD CONSTRAINT FK_s_Exception_id_Editor FOREIGN KEY (id_Editor)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [inventory].[s_Exception] ADD CONSTRAINT FK_s_Exception_id_kadr FOREIGN KEY (id_kadr)  REFERENCES [dbo].[s_kadr] (id)
GO

ALTER TABLE [inventory].[s_Exception] ADD CONSTRAINT FK_s_Exception_id_ttost FOREIGN KEY (id_ttost)  REFERENCES [dbo].[j_ttost] (id)
GO

ALTER TABLE [inventory].[s_Exception] ADD CONSTRAINT FK_s_Exception_id_shop FOREIGN KEY (id_shop)  REFERENCES [dbo].[s_Shop] (id)
GO

ALTER TABLE [inventory].[s_Exception] ADD CONSTRAINT FK_s_Exception_id_ExceptionType FOREIGN KEY (id_ExceptionType)  REFERENCES [inventory].[s_ExceptionType] (id)
GO



CREATE TABLE [inventory].[j_CalcCompensatoryTime](
	[id]					int				IDENTITY(1,1) NOT NULL,
	[id_kadr]				int				not null,
	[id_ttost]				int				not null,
	[TimeWorked]			int				not null,
	[CompensatoryTime]		numeric(6,1)	not null,
	[id_Creator]			int				not null,
	[DateCreate]			datetime		not null,
 CONSTRAINT [PK_j_CalcCompensatoryTime] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [inventory].[j_CalcCompensatoryTime] ADD CONSTRAINT FK_j_CalcCompensatoryTime_id_Creator FOREIGN KEY (id_Creator)  REFERENCES [dbo].[ListUsers] (id)
GO

ALTER TABLE [inventory].[j_CalcCompensatoryTime] ADD CONSTRAINT FK_j_CalcCompensatoryTime_id_kadr FOREIGN KEY (id_kadr)  REFERENCES [dbo].[s_kadr] (id)
GO

ALTER TABLE [inventory].[j_CalcCompensatoryTime] ADD CONSTRAINT FK_j_CalcCompensatoryTime_id_ttost FOREIGN KEY (id_ttost)  REFERENCES [dbo].[j_ttost] (id)
GO