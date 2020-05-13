drop procedure if exists dbo.GetIncomeFromEachFilial
go
create proc [dbo].[GetIncomeFromEachFilial]
as
begin
	select
		f.Name as 'FilialName',
		sum(p.Price*pf.Amount) as 'Income'
	from
		dbo.Product p
		inner join dbo.Product_Filial pf on pf.ProductId = p.Id
		inner join dbo.Filial f on pf.FilialId = f.Id
	group by f.Name
end;
