drop proc if exists dbo.Order_GetById
go
create proc [dbo].[Order_GetById]
									@id bigint
as
begin
	select
		o.Id,
		o.DateTime,
		o.FilialId
	from dbo.[Order] o
	where o.Id = @id
end
