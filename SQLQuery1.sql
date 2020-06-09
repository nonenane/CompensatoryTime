--select top(10) * from dbo.j_spacing where id_spacing = 26701

--select top(10) * from dbo.j_tspacing where id_ttost = 1804

--select * from inventory.s_Exception


DECLARE @id_ttost int  = 1773, @id_kadr int = 58239


select * from inventory.s_Exception

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

