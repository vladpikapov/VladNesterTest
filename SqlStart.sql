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
	EndDate date
);


select * from PRODUCTS;
select * from ORDERS;

select o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,count(p.ProductName)  as ProductCount from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id
group by o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate;


select p.ProductName, p.ProductType, p.Country, count(*) as ProductCount from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id where o.Id = 2
group by p.ProductName,p.ProductType, p.Country;


select o.Id,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate,count(p.ProductName) as ProductCount,p.ProductName,p.ProductType,p.Country from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id
group by o.Id,p.ProductName,p.ProductType,p.Country,o.Orderer,o.OrderStatus,o.StartDate,o.EndDate;

select * from ORDERS o inner join ORDERSPRODUCTS op
on o.Id = op.OrdersFK inner join PRODUCTS p on op.ProductFK = p.Id;

insert into ORDERSPRODUCTS values(3,3);

create table ORDERSPRODUCTS
(
	
	OrdersFK int foreign key references Orders(Id),
	ProductFK int foreign key references Products(Id)
);

insert into PRODUCTS values(N'Джинсы',N'Одежда',N'Германия');
insert into ORDERS values ('dasdsasda',N'Доставлен','03.09.2020',null);
select * from ORDERS;

drop table ORDERSPRODUCTS;
drop table ORDERS;
drop table PRODUCTS;
