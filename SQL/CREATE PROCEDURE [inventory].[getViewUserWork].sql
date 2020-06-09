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
-- Description: Получение журнала подсчёта сотрудника на дату инвенты
-- =============================================
CREATE PROCEDURE [inventory].[getViewUserWork] 
	@id_ttost int,
	@id_kadr int
AS
BEGIN
	SET NOCOUNT ON


select 
	ltrim(rtrim(p.place)) as place,
	ltrim(rtrim(d.name)) as nameDep,
	s.dttost_n,
	s.dttost_k,
	ts.dtost
from 
	dbo.j_tspacing ts 
	inner join dbo.j_spacing s on s.id_spacing = ts.id
	inner join dbo.s_place p on p.id = ts.id_place
	inner join dbo.departments d on d.id = ts.id_departments
where ts.id_ttost =  @id_ttost and s.id_kadr = @id_kadr
order by dtost desc

END

