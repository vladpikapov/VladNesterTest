create database TestTaskDB;

use TestTaskDB;

--create table

create table PRODUCTS
(
	Id int identity(1,1) primary key,
	ProductName nvarchar(200) not null,
	ProductType nvarchar(20) check(ProductType IN('Food','Clothes','Household chemicals')),
	Country nvarchar(50) check(Country IN('Germany','Belarus','Russia','USA','UK','Egypt','Spain','Poland')),
	ProductCount int,
	constraint uniqueConstr unique nonclustered
	(
		ProductName,ProductType,Country
	)
);

create table ORDERS
(
	Id int identity(1,1) primary key,
	Orderer nvarchar(100),
	OrderStatus nvarchar(20) check(OrderStatus IN('Formation','Waiting for delivery','Delivered')),
	StartDate date not null,
	EndDate date
);

create table ORDERSPRODUCTS
(
	
	OrdersFK int foreign key references Orders(Id),
	ProductFK int foreign key references Products(Id),
	CountOrderedProducts int
);

--inserts


insert into PRODUCTS values('Jeans','Clothes','USA',4);
insert into ORDERS values ('Vlad Nester','Waiting for delivery','03.09.2020','03.11.2020');
insert into ORDERSPRODUCTS values(1,1);


--selects
select * from PRODUCTS;
select * from ORDERS;

select o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,count(p.ProductName)  as ProductCount from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id
group by o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate;

select * from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id;

select o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,count(p.ProductName) as ProductCount,p.ProductName,p.ProductType,p.Country from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id
group by o.Id,p.ProductName,p.ProductType,p.Country,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate;

select * from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id;

go
 
--procedures
create procedure AddProduct
	@Name nvarchar(200),
	@Type nvarchar(20),
	@Country nvarchar(50),
	@Count int
	as
	begin
	insert into PRODUCTS(ProductName, ProductType, Country, ProductCount) values (@Name, @Type, @Country, @Count);
	end;
go

go
exec AddProduct 'Apple','Food','Poland'
go

go
exec SelectOrderProducts 1;
go

go
create procedure SelectOrderProducts 
 @Id int
 as
begin
select p.Id,p.ProductName,p.ProductType,p.Country,op.CountOrderedProducts from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id where p.Id = @Id;
end;
go

go
create procedure SelectProducts
as
begin
	select * from PRODUCTS;
end;
go
create procedure AddOneProduct 
	@Id int
	as
	begin
	update PRODUCTS set ProductCount = ProductCount + 1 where Id = @Id
end;
go
create procedure DropOneProduct
	@Id int
	as
begin
	if((select PRODUCTS.ProductCount from PRODUCTS where Id = @Id) > 0)
	begin
		update PRODUCTS set ProductCount = ProductCount - 1 where Id = @Id
	end
end;
go		

go
create procedure AddOrder
	@Name nvarchar(100),
	@StartDate date,
	@ProdCount int,
	@IdProd int
	as
	begin
	if((select ProductCount from PRODUCTS where Id = @IdProd) >= @ProdCount)
	begin
	insert ORDERS(Orderer,OrderStatus, StartDate, EndDate) values (@Name,'Formation',@StartDate,null)
	insert ORDERSPRODUCTS(OrdersFK,ProductFK,CountOrderedProducts) values ((select top 1 Id from ORDERS where Orderer = @Name AND StartDate = @StartDate order by Id desc),@IdProd,@ProdCount);
	update PRODUCTS set ProductCount = ProductCount - @ProdCount;
	end
	end;
	go
exec AddOrder 'Vlad Nester','03.09.2020',4,1
	go
exec DropOneProduct 1;

	exec AddOneProduct 1;
--drop

drop table ORDERSPRODUCTS;
drop table ORDERS;
drop table PRODUCTS;
drop procedure SelectOrderProducts;
drop procedure AddProduct;
drop procedure AddOrder
drop procedure DropOneProduct;
drop procedure SelectProducts;

