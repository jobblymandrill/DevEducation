drop proc if exists dbo.Order_Add
go
create proc [dbo].[Order_Add]
								@filialId int,
								@filialCity nvarchar(30)
as
begin 
	insert into
		dbo.[Order]
			(DateTime,
			FilialId,
			FilialCity)
	values
		(getdate(),
		@filialId,
		@filialCity);
	select scope_identity();
end;
