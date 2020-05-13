drop proc if exists dbo.GetNeverOrderedProducts
go
create proc [dbo].[GetNeverOrderedProducts] as
begin
	select
		p.Id,
		p.Name,
		p.Price,
		p.TradeMark,
		c.Id as 'Id',
		c.[Name] as 'Name',
		cd.Id as 'Id',
		cd.[Name] as 'Name'
	from
		dbo.Product p
		inner join dbo.Category c on c.Id = p.CategoryId
		inner join dbo.Category cd on cd.Id = p.ParentCategoryId
	where
		p.Id not in 
			(select Id
				from dbo.Order_Product_Amount)
end;
