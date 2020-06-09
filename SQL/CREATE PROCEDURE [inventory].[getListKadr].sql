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
-- Description: Получение списка  сотрудников
-- =============================================
CREATE PROCEDURE [inventory].[getListKadr] 
	@dateInvent date = '2020-04-01',
	@id_PersonnelType int
AS
BEGIN
	SET NOCOUNT ON

select 
	 k.id
	,k.id_departments 
	,isnull(ltrim(rtrim(k.lastname)) +' ','')+isnull(ltrim(rtrim(k.firstname)) +' ','')+' '+isnull(ltrim(rtrim(k.secondname)) +' ','') as fio
	,ltrim(rtrim(d.name)) as nameDeps
from 
	dbo.s_kadr k
		left join dbo.departments d on d.id = k.id_departments
where 
	(k.id_WorkStatus in (2,4,7) or (k.id_WorkStatus = 5 and k.dateUnemploy>@dateInvent)) and k.id_PersonnelType = @id_PersonnelType



END

