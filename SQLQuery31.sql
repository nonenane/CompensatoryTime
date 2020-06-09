DECLARE @dateInvent date = '2020-04-01'

select * from dbo.departments d inner join(
select id_departments from dbo.s_kadr where (id_WorkStatus in (2,4,7) or (id_WorkStatus = 5 and dateUnemploy>@dateInvent)) and id_PersonnelType = 1
group by id_departments) as k on k.id_departments = d.id
order by d.name asc