drop proc if exists dbo.GetTotalFilialSumPerPeriod
go
create proc [dbo].[GetTotalFilialSumPerPeriod]
											@start datetime2, 
											@end datetime2 
as
begin
	select
		f.Name as 'FilialName', sum(opa.Amount * p.Price) as 'Income'
	from
		dbo.[Order] o
		inner join dbo.Filial f on f.Id = o.FilialId
		inner join dbo.Order_Product_Amount opa on o.Id = opa.OrderId
		inner join dbo.Product p on p.Id = opa.ProductId
    where
		o.DateTime between @start and @end
	group
		by f.Name
end;
