drop proc if exists dbo.GetRanOutProducts
go
create proc [dbo].[GetRanOutProducts]
as
begin
	select
		p.Id,
		p.Name,
		p.Price,
		p.TradeMark,
		p.CategoryId as 'Id',
		c.Name as 'Name',
		ct.Id as 'Id',
		ct.Name as 'Name'
	from
		dbo.[Order] o
		inner join dbo.Order_Product_Amount a on a.OrderId=o.Id
		inner join dbo.Product p on p.Id=a.ProductId
		inner join dbo.Category ct on ct.Id = p.ParentCategoryId
		inner join dbo.Category c on c.Id = p.CategoryId
	where
		p.Name not in
			(select
				p.Name
			from
				dbo.Product_Filial f 
				inner join dbo.Product p on p.Id=f.ProductId
			where
				f.Amount > 0)
end;
