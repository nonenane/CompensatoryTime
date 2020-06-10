USE [dbase1]
GO
/****** Object:  StoredProcedure [Caramel].[setSettings]    Script Date: 20.04.2020 14:00:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin Georgiy
-- Create date: 2020-06-10
-- Description:	Запись настроек
-- =============================================
ALTER PROCEDURE [inventory].[setSettings]	
	 @id_prog int,
	 @id_value varchar(4),
	 @type_value varchar(15),
	 @value_name varchar(50),
	 @value varchar(150),
	 @comment varchar(150),
	 @isDel bit
AS
BEGIN
	SET NOCOUNT ON;

	IF @isDel = 1 
		BEGIN
			DELETE FROM dbo.prog_config where id_prog  =@id_prog and id_value  = @id_value
		END
	ELSE
		BEGIN
			IF NOT EXISTS (select id from dbo.prog_config where id_prog = @id_prog and id_value = @id_value)
				INSERT INTO dbo.prog_config (id_prog,id_value,type_value,value_name,value,comment)
					values (@id_prog,@id_value,@type_value,@value_name,@value,@comment)
			ELSE
				UPDATE dbo.prog_config set value = @value where id_prog = @id_prog and id_value = @id_value
		END

	
END
