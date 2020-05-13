drop procedure if exists dbo.GetIncomeFromRussiaAndFromForeignCountries
go
create proc [dbo].[GetIncomeFromRussiaAndFromForeignCountries]
as
begin
    select
        [0] as 'IncomeFromRussia',
        [1] as 'IncomeFromOutsideRussia'
    from (
        select sum(p.Price*op.Amount) as total,
        c.IsForeign
        from dbo.Product p
        inner join dbo.Order_Product_Amount op on op.ProductId = p.Id
        inner join dbo.[Order] o on o.Id = op.OrderId
        inner join dbo.Filial c on c.Id = o.FilialId
        where c.IsForeign is not null
        group by c.IsForeign) as s
   pivot
    (avg(total) for s.IsForeign IN ([0], [1])) as PivotTable;
end;
