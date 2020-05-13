drop proc if exists dbo.GetMostPopularProductInEachCity 
go
create proc [dbo].[GetMostPopularProductInEachCity] as
begin
	select
		a.Name as City,
			(select top 1 p.Name as  'Самый популярный товар'
			from dbo.Product p
			inner join dbo.Order_Product_Amount opa on p.Id= opa.ProductId
			inner join dbo.[Order] o on opa.OrderId = o.Id
			inner join dbo.Filial f on f.Id=o.FilialId
			where
				a.Id=f.Id
			group by
				p.Name
			order by
				sum(opa.Amount) desc) as 'Product'
	from
		dbo.Filial a
	where
		a.Id != 1
end;
