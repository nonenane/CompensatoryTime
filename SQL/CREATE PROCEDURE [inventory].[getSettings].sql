USE [dbase1]
GO
/****** Object:  StoredProcedure [Caramel].[getSettings]    Script Date: 20.04.2020 14:00:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin Georgiy
-- Create date: 2020-06-10
-- Description:	Получение настроек
-- =============================================
CREATE PROCEDURE [inventory].[getSettings]	
	 @id_prog int,
	 @id_value varchar(4)	 
AS
BEGIN
	SET NOCOUNT ON;

	select value from dbo.prog_config where id_prog = @id_prog and id_value = @id_value

	
END
