drop proc if exists dbo.GetProductsFromWareHouseNotPresentInMscAndSpb
go
create proc [dbo].[GetProductsFromWareHouseNotPresentInMscAndSpb]
as
begin
	select
		p.Id,
		p.Name,
		p.Price,
		p.TradeMark,
		p.CategoryId as 'Id',
		c.Name as 'Name',
		cd.Id as 'Id',
		cd.Name as 'Name'
	from
		dbo.Product p 
		inner join dbo.Product_Filial pf on pf.ProductId=p.Id 
		inner join dbo.Category c on c.Id = p.CategoryId
		inner join dbo.Category cd on cd.Id = p.ParentCategoryId
	where
		pf.FilialId=1 and p.Id not in 
		(select
			pf.ProductId
		 from
			dbo.Product_Filial pf
		 where
			pf.FilialId = 2 or pf.FilialId = 3)
end;
