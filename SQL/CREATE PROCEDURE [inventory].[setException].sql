SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-04-25
-- Description:	Запись исключения сотрудника в журнал исключений
-- =============================================
CREATE PROCEDURE [inventory].[setException]		 
	@id int,
	@id_kadr int,
	@id_shop int,
	@id_ttost int,
	@id_ExceptionType int,
	@isDop bit,
	@Summa numeric(11,2) = null,
	@CountDays numeric(6,1) = null,	
	@id_user int,
	@result int = 0,
	@isDel int
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN

			IF EXISTS (select TOP(1) id from [inventory].[s_Exception] where id <> @id and id_kadr = @id_kadr and id_ttost=@id_ttost)
				BEGIN
					SELECT -1 as id;
					return;
				END			

			IF @id = 0
				BEGIN
					INSERT INTO [inventory].[s_Exception]  (id_kadr,id_shop,id_ttost,id_ExceptionType,isDop,Summa,CountDays,id_Creator,id_Editor,DateCreate,DateEdit)
					VALUES (@id_kadr,@id_shop,@id_ttost,@id_ExceptionType,@isDop,@Summa,@CountDays,@id_user,@id_user,GETDATE(),GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					
					UPDATE
						[inventory].[s_Exception] 
					set 
						id_kadr=@id_kadr,
						id_shop = @id_shop,
						id_ttost = @id_ttost,
						id_ExceptionType =@id_ExceptionType,
						isDop = @isDop,
						Summa = @Summa,
						CountDays = @CountDays,
						id_Editor = @id_user,
						DateEdit = GETDATE()
					where
						id = @id

					SELECT @id as id
					return;
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					IF NOT EXISTS(select TOP(1) id from [inventory].[s_Exception] where id = @id)
						BEGIN
							select -1 as id
							return;
						END
						
					select 0 as id
					return;
				END
			ELSE
				BEGIN					
					DELETE FROM [inventory].[s_Exception] where id = @id
					
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
