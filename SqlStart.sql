create database TestTaskDB;

use TestTaskDB;

--create table

create table PRODUCTS
(
	Id int identity(1,1) primary key,
	ProductName nvarchar(200) not null,
	ProductType nvarchar(20) check(ProductType IN('Food','Clothes','Tech')),
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
	EndDate date,
	constraint uniqueConstrOrder unique nonclustered
	(
		Orderer, StartDate
	)
);

create table ORDERSPRODUCTS
(
	
	OrdersFK int foreign key references Orders(Id),
	ProductFK int foreign key references Products(Id),
	CountOrderedProducts int
);
--selects
select * from PRODUCTS;
select * from ORDERS;
select * from ORDERSPRODUCTS;
go
 
--procedure
go
create procedure SelectOrderProducts 
 @Id int
 as
begin
select p.Id,p.ProductName,p.ProductType,p.Country,op.CountOrderedProducts from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id where o.Id = @Id;
end;
go
--drop

drop table ORDERSPRODUCTS;
drop table ORDERS;
drop table PRODUCTS;
drop procedure SelectOrderProducts;


