create database TestTaskDB;

use TestTaskDB;

create table PRODUCTS
(
	Id int identity(1,1) primary key,
	ProductName nvarchar(200),
	ProductCost float,
);

create table ORDERS
(
	Id int identity(1,1) primary key,
	OrderStatus nvarchar(10) check(OrderStatus IN(N'формирование',N'ожидание доставки',N'доставлен')),
	StartDate date not null,
	EndDate date,
);

drop table ORDERS;

create table ORDERSPRODUCTS
(
	ProductFK int foreign key references Products(Id),
	OrdersFK int foreign key references Orders(Id)
);

insert into ORDERS values ('доставлен','03.09.2020',null);

select * from ORDERS;