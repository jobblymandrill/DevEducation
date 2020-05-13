drop proc if exists dbo.Product_GetById
go
create proc [dbo].[Product_GetById]
									@id bigint
as
begin
	select p.Name
	from dbo.[Product] p
	where p.Id = @id
end
