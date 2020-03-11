create database TestTaskDB;

use TestTaskDB;

--create table

create table PRODUCTS
(
	Id int identity(1,1) primary key,
	ProductName nvarchar(200),
	ProductType nvarchar(20) check(ProductType IN('Food','Clothes','Household chemicals')),
	Country nvarchar(50) check(Country IN('Germany','Belarus','Russia','USA','UK','Egypt','Spain','Poland'))
);

create table ORDERS
(
	Id int identity(1,1) primary key,
	Orderer nvarchar(100),
	OrderStatus nvarchar(10) check(OrderStatus IN('Formation','Waiting for delivery','Delivered')),
	StartDate date not null,
	EndDate date
);

create table ORDERSPRODUCTS
(
	
	OrdersFK int foreign key references Orders(Id),
	ProductFK int foreign key references Products(Id)
);

--inserts


insert into PRODUCTS values('Jeans','Clothes','Germany');
insert into ORDERS values ('Vlad Nester','Delivered','03.09.2020','03.11.2020');
insert into ORDERSPRODUCTS values(1,2);


--selects
select * from PRODUCTS;
select * from ORDERS;

select o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,count(p.ProductName)  as ProductCount from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id
group by o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate;

select p.Id, p.ProductName, p.ProductType, p.Country, count(*) as ProductCount from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id where o.Id = 2
group by p.ProductName,p.ProductType, p.Country, p.Id;


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
	@Country nvarchar(50)
	as
	begin
	insert into PRODUCTS(ProductName, ProductType, Country) values (@Name, @Type, @Country);
	end;
go

go
exec AddProduct 'Apple','Food','Poland'
go

go
exec SelectOrderProducts 2;
go

go
create procedure SelectOrderProducts 
 @Id int
 as
begin
select p.Id, p.ProductName, p.ProductType, p.Country, count(*) as ProductCount from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id where o.Id = @Id
group by p.ProductName,p.ProductType, p.Country, p.Id;
end;
go

go
create procedure DeleteProduct
	@Id int
	as
begin
delete from PRODUCTS where Id = @Id
end;
go

--drop

drop table ORDERSPRODUCTS;
drop table ORDERS;
drop table PRODUCTS;
drop procedure SelectOrderProducts;
