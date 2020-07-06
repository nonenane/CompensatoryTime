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
-- Description: ѕолучение списка сотрудников которые отработали в морозилке за деньги!
-- =============================================
ALTER PROCEDURE [inventory].[getReportBonusPayment] 
	@id_ttost int 
AS
BEGIN
	SET NOCOUNT ON


select 
	 isnull(ltrim(rtrim(k.lastname)) +' ','')+isnull(ltrim(rtrim(k.firstname)) +' ','')+' '+isnull(ltrim(rtrim(k.secondname)) +' ','') as fio
	,ltrim(rtrim(d.name)) as nameDeps
	,p.place
	,isnull(p.payment,0) as payment
from 
	dbo.j_tspacing ts
		inner join dbo.s_place p on p.id = ts.id_place
		inner join dbo.j_spacing s on s.id_spacing = ts.id
		left join dbo.s_kadr k on k.id = s.id_kadr
		left join dbo.departments d on d.id = k.id_departments
where ts.id_ttost = @id_ttost and p.payment is not null and p.payment <> 0

END

