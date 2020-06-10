SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-06-10
-- Description:	Перенос сотрудников на другую дату инвенты
-- =============================================
CREATE PROCEDURE [inventory].[transferKadrToNextInvent]		 
	@id int,
	@id_kadr int,
	@id_ttost int,
	@id_user int,
	@result int	
AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
		
	IF @result = 0 
		BEGIN
			IF EXISTS (select id from inventory.s_Exception where id_kadr = @id_kadr and id_ttost = @id_ttost)
			BEGIN
				select -1 as id;
				return;
			END	

			select 0 as id;
			return;
		END
	ELSE
		BEGIN
			DECLARE	@dateInvent date 
			select @dateInvent = dttost from dbo.j_ttost where id = @id_ttost


			IF NOT EXISTS(select id from dbo.s_kadr k where id = @id_kadr and (k.id_WorkStatus in (2,4,7) or (k.id_WorkStatus = 5 and k.dateUnemploy>@dateInvent)))
				BEGIN
					select -1 as id;
					return;
				END

			IF EXISTS (select id from inventory.s_Exception where id_kadr = @id_kadr and id_ttost = @id_ttost)
				BEGIN
					DECLARE @id_new int 
					select @id_new = id from inventory.s_Exception where id_kadr = @id_kadr and id_ttost = @id_ttost
					
					UPDATE 
						exIn
					SET 
						exIn.id_shop = exOut.id_shop,	
						exIn.id_ExceptionType = exOut.id_ExceptionType,
						exIn.isDop = exOut.isDop,
						exIn.Summa = exOut.Summa,
						exIn.CountDays = exOut.CountDays,
						exIn.id_Editor = @id_user,
						exIn.DateEdit = GETDATE()
					from 
						inventory.s_Exception as exIn
							inner join inventory.s_Exception as exOut on exOut.id_kadr = exIn.id_kadr  and exOut.id_ttost <> exIn.id_ttost
					where 
						--exIn.id_ttost = @id_ttost and exIn.id_kadr = @id_kadr and exOut.id = @id
						exIn.id =@id_new and exOut.id = @id
											   						 
					select @id_new as id;
					return;
				END	
			else
				BEGIN

					INSERT INTO inventory.s_Exception (id_ttost,id_shop,id_kadr,id_ExceptionType,isDop,Summa,CountDays,id_Creator,id_Editor,DateCreate,DateEdit)
						select @id_ttost,id_shop,id_kadr,id_ExceptionType,isDop,Summa,CountDays,@id_user,@id_user,GETDATE(),GETDATE()
						from inventory.s_Exception
						where id = @id
					
					select cast(SCOPE_IDENTITY() as int) as id
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
