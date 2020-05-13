create proc [dbo].[ShowCategoriesWhereProductNumberIsMoreThanSomeNumber]
							@number int
as
begin
	select
		count(p.CategoryId) as HowMany,
		c.Id, c.Name,
		c.ParentId as 'Id',
		ct.Name as Name
	from dbo.Category c
		inner join dbo.Product p on p.CategoryId=c.Id
		inner join dbo.Category ct on ct.Id = c.ParentId
	group by
		c.Name,
		c.Id,
		c.ParentId,
		ct.Name
	having
		count(p.CategoryId) > @number
end;
