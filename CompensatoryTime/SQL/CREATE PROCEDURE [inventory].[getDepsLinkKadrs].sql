USE [dbase1]
GO
/****** Object:  StoredProcedure [inventory].[getUsedDep]    Script Date: 09.06.2020 14:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin Georgiy
-- Edit date: <2020-06-09>
-- Description: Получение списка отделов основанных на кадрах
-- =============================================
CREATE PROCEDURE [inventory].[getDepsLinkKadrs] 
	@dateInvent date = '2020-04-01',
	@id_PersonnelType int
AS
BEGIN
	SET NOCOUNT ON



select cast(d.id as int) as id,ltrim(rtrim(d.name)) as cName from dbo.departments d inner join(
select id_departments from dbo.s_kadr where (id_WorkStatus in (2,4,7) or (id_WorkStatus = 5 and dateUnemploy>@dateInvent)) and id_PersonnelType = @id_PersonnelType
group by id_departments) as k on k.id_departments = d.id
order by d.name asc

END

