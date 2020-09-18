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
-- Description: Получение списка исключённых сотрудников
-- =============================================
CREATE PROCEDURE [inventory].[getException] 
	@id_ttost int 
AS
BEGIN
	SET NOCOUNT ON


select 
	cast(0 as bit) as isSelect,
	e.id,
	e.id_kadr,
	e.id_shop,
	e.id_ExceptionType,
	s.cName as nameShop,
	isnull(ltrim(rtrim(k.lastname)) +' ','')+isnull(ltrim(rtrim(k.firstname)) +' ','')+' '+isnull(ltrim(rtrim(k.secondname)) +' ','') as fio,
	ltrim(rtrim(d.name)) as nameDeps,
	et.cName as nameExpType,
	e.CountDays,
	e.Summa,
	e.isDop,
	e.id_ttost
from 
	inventory.s_Exception e
		left join dbo.s_Shop s on s.id = e.id_shop
		left join dbo.s_kadr k on k.id = e.id_kadr
		left join dbo.departments d on d.id = k.id_departments
		left join inventory.s_ExceptionType et on et.id = e.id_ExceptionType
where 
	e.id_ttost = @id_ttost

END

