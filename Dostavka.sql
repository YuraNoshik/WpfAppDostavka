create database Dostavka
go

use Dostavka
go


create table Roles
(
	RoleID int primary key identity,
	RoleName nvarchar(100) not null
)
go

create table Users
(
	userID int primary key identity,
	userSurname nvarchar(100) not null,
	userName nvarchar(100) not null,
	userPatronymic nvarchar(100) not null,
	userPhoneNumber nvarchar(12) not null,
	userAdress nvarchar(100) not null,
	userHomeNumber nvarchar(100) not null,
	userApartmentsNumber nvarchar(100) not null,
	userLogin nvarchar(max) not null,
	userPassword nvarchar(max) not null,
	userRole int foreign key references Roles(RoleID) not null
)
go

create table Stuffs
(
	stuffID int primary key identity,
	stuffSurname nvarchar(100) not null,
	stuffName nvarchar(100) not null,
	stuffPatronymic nvarchar(100) not null,
	stuffLogin nvarchar(max) not null,
	stuffPassword nvarchar(max) not null,
	stuffRole int foreign key references Roles(RoleID) not null
)
go

create table Producer
(
	producerID int primary key identity,
	producerName nvarchar(max) not null,
);
go

create table Products
(
	productID int primary key identity,
	producerID int foreign key references Producer(producerID) not null,
	productName nvarchar(max) not null,
	productDate date not null,
	productWarranty nvarchar(max) not null,
	productCost decimal(19,4) not null,
);
go

create table orderProducts
(
	orderProductsID int primary key identity,
	productID int foreign key references Products(productID) not null,
	amount nvarchar(max) not null
);
go


create table Orders
(
 orderID int primary key identity,
 stuffID int foreign key references Stuffs(stuffID) not null,
 userID int foreign key references Users(userID) not null,
 orderProductsID int foreign key references orderProducts(orderProductsID) not null,
 orderDate date not null,
 orderCost decimal(19,4) not null
);
go




/*
drop table Product
drop table Category
drop table Supplier
drop table Producer
drop table Photo

CREATE TABLE Employees
(
    Id int,
    Name varchar(50) not null,
    Photo varbinary(max) not null
)
 
INSERT INTO Employees (Id, Name, Photo) 
SELECT 10, 'John', BulkColumn 
FROM Openrowset( Bulk 'C:\photo.bmp', Single_Blob) as EmployeePicture
