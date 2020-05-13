drop proc if exists dbo.Order_Product_Amount_Add
go
create proc [dbo].[Order_Product_Amount_Add]
	@productId bigint,
	@orderId bigint,
	@amount int
as
begin 
	insert into dbo.Order_Product_Amount
		(ProductId, OrderId, Amount)
	values
		(@productId,
		 @orderId,
		 @amount);
	select scope_identity();
end
