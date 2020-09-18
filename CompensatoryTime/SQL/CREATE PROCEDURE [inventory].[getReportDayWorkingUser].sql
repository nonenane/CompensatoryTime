USE [dbase1]
GO
/****** Object:  StoredProcedure [inventory].[getUsedDep]    Script Date: 09.06.2020 14:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin Georgiy
-- Edit date: <2020-06-10>
-- Description: Получение списка сотрудников которые отработали сутки со сканером!
-- =============================================
ALTER PROCEDURE [inventory].[getReportDayWorkingUser] 
	@id_ttost int 
AS
BEGIN
	SET NOCOUNT ON


DECLARE @date datetime 
	select @date = dttost from dbo.j_ttost where id = @id_ttost



select 
		s.id,
		case when cast(SUBSTRING(cast(dttost_n as varchar(5)),0, CHARINDEX('.',cast(dttost_n as varchar(5)))) as int)>7 then
		dateadd(minute,cast(SUBSTRING(cast(dttost_n as varchar(5)),CHARINDEX('.',cast(dttost_n as varchar(5)))+1,2) as int),
		dateadd(hour,cast(SUBSTRING(cast(dttost_n as varchar(5)),0, CHARINDEX('.',cast(dttost_n as varchar(5))))as int),@date))
		else
		dateadd(minute,cast(SUBSTRING(cast(dttost_n as varchar(5)),CHARINDEX('.',cast(dttost_n as varchar(5)))+1,2) as int),
		dateadd(hour,cast(SUBSTRING(cast(dttost_n as varchar(5)),0, CHARINDEX('.',cast(dttost_n as varchar(5))))as int)+24,@date))
		end as dttost_n,
		s.id_spacing
INTO 
	#TMP
from (
select 
	s.id,
	max(s.dttost_n)as dttost_n,
	s.id_kadr,
	s.id_spacing
from dbo.j_spacing  s inner join dbo.j_tspacing ts on ts.id = s.id_spacing
where ts.id_ttost = @id_ttost
group by 
	s.id_kadr,
	s.id,
	s.id_spacing) as s

select 
	b.id_Shop,
	s.cName as nameShop,
	b.id_kadr,
	isnull(ltrim(rtrim(k.lastname)) +' ','')+isnull(ltrim(rtrim(k.firstname)) +' ','')+' '+isnull(ltrim(rtrim(k.secondname)) +' ','') as fio,
	isnull(d.id,0) as id_dep,
	isnull(d.name,'') as nameDeps,
	case when b.id_spacing is null then b.timeStart  else 
	case when b.timeStart> p.dttost_n then b.timeStart else p.dttost_n end end timeStart,
	b.timeEnd
from 
		inventory.s_Exception ex
		inner join inventory.j_scanerBlank  b on b.id_kadr = ex.id_kadr and b.id_ttost = ex.id_ttost
		left join dbo.s_Shop s on s.id = b.id_Shop
		left join dbo.s_kadr k on k.id = b.id_kadr
		--left join dbo.j_spacing p on p.id= b.id_spacing
		left join #TMP p on p.id= b.id_spacing
		left join dbo.j_tspacing ts on ts.id = p.id_spacing
		left join dbo.departments d on d.id  =  ts.id_departments

where 
	b.timeStart is not null and b.timeEnd is not null and --b.id_ttost = @id_ttost
	ex.id_ttost = @id_ttost and ex.id_ExceptionType = 1

DROP TABLE #TMP

END

