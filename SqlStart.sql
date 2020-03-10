create database TestTaskDB;

use TestTaskDB;

create table PRODUCTS
(
	Id int identity(1,1) primary key,
	ProductName nvarchar(200),
	ProductType nvarchar(20) check(ProductType IN(N'Еда',N'Одежда',N'Бытовая химия')),
	Country nvarchar(50) check(Country IN(N'Германия',N'Беларусь',N'Россия',N'США',N'Великобритания',N'Египет',N'Испания',N'Польша'))
);



create table ORDERS
(
	Id int identity(1,1) primary key,
	Orderer nvarchar(100),
	OrderStatus nvarchar(10) check(OrderStatus IN(N'Формирование',N'Ожидание доставки',N'Доставлен',N'Доставляется')),
	StartDate date not null,
	EndDate date,
);


create table ORDERSPRODUCTS
(
	ProductFK int foreign key references Products(Id),
	OrdersFK int foreign key references Orders(Id)
);

insert into PRODUCTS values(N'Апельсин',N'Еда',N'Египет');
insert into ORDERS values ('dasdsa',N'Доставлен','03.09.2020',null);
select * from ORDERS;

drop table ORDERSPRODUCTS;
drop table ORDERS;
drop table PRODUCTS;
