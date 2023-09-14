create database Ecommercedemo;
use Ecommercedemo;

--*********************Doubts**********************---- 
--subscription related to order
----product based subscription , multiple products subscription 
-----Discount id in product table





create table User_types (
Type_id int  primary key identity(1,1),
User_Type varchar(20)
);


insert into User_types(User_Type)
values('Admin'),
	('User'),
	('Vendor'),
	('Deliveryboy');
	

-- Users table to store customer information
create table Users (
	User_Id INT PRIMARY KEY IDENTITY(1, 1),
	user_type_id int not null,
    Username VARCHAR(50) UNIQUE not null,
	Password VARCHAR(100) not null, -- Store hashed and salted passwords,
    Firstname VARCHAR(50) not null,
    Lastname VARCHAR(50)not null,
    Mobile DECIMAL(10) not null,
    Email VARCHAR(50) UNIQUE not null,
	IsActive bit,
	CONSTRAINT FK_usertypeid_User_types FOREIGN KEY (user_type_id) REFERENCES User_types(Type_id)
	   );

create table Brand (
Brand_id int primary key identity(1,1),
Brand_Name varchar(150) not null
);



-- Categories table to classify grocery items
create table Category (
Category_Id int primary key identity(1,1),
Brand_Id int not null,
Category_Name varchar(50) not null,
Description varchar(500),
ImageURL VARCHAR(255) -- URL to category image
);

ALTER TABLE Category
ADD CONSTRAINT FK_Category_Brand
FOREIGN KEY (Brand_Id)
REFERENCES Brand(Brand_Id); -- Replace YourOtherTable with the actual name of the table containing Brand_Id



-- Products table to store  items
create table Product
(
Product_Id int primary key identity(1,1),
Category_Id  int not null,
Brand_Id int not null,
Product_Name varchar(50) not null,
StockQuantity INT not null,
Price decimal(10,2) not null,
Weight DECIMAL(8, 2),
Unit VARCHAR(20),
ImageURL VARCHAR(255) not null, 
IsAvailable bit,
ExpiryDate DATE not null,
ManufactureDate DATE,
Discount_id int,
Description varchar(1000),
CONSTRAINT FK_Category_Id_Category FOREIGN KEY (Category_Id) REFERENCES Category(Category_Id),
CONSTRAINT FK_Brand_Id_Brand FOREIGN KEY (Brand_Id) REFERENCES Brand(Brand_id)
);

ALTER TABLE Product
ADD CONSTRAINT FK_Category_Discount_id
FOREIGN KEY (Discount_id)
REFERENCES Discount(Discount_id);

ALTER TABLE Product
ALTER COLUMN Discount_id INT NOT NULL;


--------------------------------------------------


create table subscription_type
(
Subscription_Id int primary key identity(1,1),
Subscription_Type varchar(20) not null
);


CREATE TABLE ShoppingCarts (
    CartID INT PRIMARY KEY,
    UserID INT,
    CreatedAt DATETIME NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(User_Id)
);


-- ShoppingCartItems table to store items in user shopping carts
CREATE TABLE ShoppingCartItems (
    ItemID INT PRIMARY KEY,
    CartID INT,
    ProductID INT,
    Quantity INT NOT NULL,
	Price decimal(10,2),
    FOREIGN KEY (CartID) REFERENCES ShoppingCarts(CartID),
    FOREIGN KEY (ProductID) REFERENCES Product(Product_ID)
);

-- Coupons table to manage discount coupons
CREATE TABLE Coupons (
    CouponID INT PRIMARY KEY identity(1,1),
    Code VARCHAR(20) UNIQUE,
    Discount DECIMAL(5, 2), -- Discount percentage
    ExpiryDate DATE,
    Description TEXT
);

-- UserCoupons table to link users to coupons they've used
CREATE TABLE UserCoupons (
    UserCouponID INT PRIMARY KEy identity(1,1),
    UserID INT,
    CouponID INT,
    UsageDate date DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(User_Id),
    FOREIGN KEY (CouponID) REFERENCES Coupons(CouponID)
);

-- Orders table to store customer orders
create table Orders (
Order_Id int primary key identity(1,1),
User_id int not null,
Subscription_Type varchar(20)not null,
Total_Amount decimal(10,2) not null,
Order_Date date, 
Start_Date date not null,
End_Date date not null,
constraint Fk_UserId_User foreign key (User_id) references Users(User_Id),
);

-- OrderItems table to link products to orders
create table OrderItems(
ItemId int primary key identity(1,1),
Order_Id int not null ,
Product_Id int not null,
Quantity int ,
Subscription_Type varchar(20) not null,
Start_Date date not null,
End_Date date not null,
Product_Price decimal not null,
constraint Fk_OrderId_Orders foreign key (Order_Id) references Orders(Order_Id),
constraint Fk_ProductId_Products foreign key (Product_Id) references Product(Product_Id)
);

-- Payments table to store payment information
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY identity(1,1),
    OrderID INT not null,
    PaymentDate date DEFAULT CURRENT_TIMESTAMP,
    PaymentMethod VARCHAR(50) not null,
    Amount DECIMAL(10, 2) not null,
    TransactionID VARCHAR(100) not null,
    FOREIGN KEY (OrderID) REFERENCES Orders(Order_Id)
);

CREATE TABLE UserSubscriptions (
    UserSubscriptionID INT PRIMARY KEY,
    User_ID INT not null,
    Subscription_TypeId int not null,
	Order_id int not null,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL ,
	Subscription_Price decimal(10,2) not null,
    IsActive bit NOT NULL DEFAULT 1, -- Indicates if the subscription is active
    FOREIGN KEY (User_ID) REFERENCES Users(User_ID),
    FOREIGN KEY (Subscription_TypeId) REFERENCES subscription_type(Subscription_Id)
);

CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY Identity(1,1),
    UserID INT,
    ProductID INT,
    Rating INT, -- 1 to 5 rating
    Comment TEXT,
    ReviewDate date DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(User_Id),
    FOREIGN KEY (ProductID) REFERENCES Product(Product_Id)
);

-- Vendors table to manage product suppliers
CREATE TABLE Vendors (
    VendorID INT PRIMARY KEY identity(1,1),
    NameofVendor VARCHAR(100),
    Address VARCHAR(255),
    ContactEmail VARCHAR(100),
    ContactPhone VARCHAR(15),
    Description TEXT,
    LogoURL VARCHAR(255) -- URL to vendor logo
);

create table Log
(
LogId int primary key identity(1,1),
LogDate Datetime,
EventDescription varchar(300),
User_Id int,
foreign key (User_Id) references Users(User_Id),
CONSTRAINT CK_ValidEventDescription CHECK (EventDescription IN ('Login', 'Logoff', 'Passwordchanged',
																'Product_added','Product_Removed','Product_Discount_Changed'))
);

 
create table Discount
(
Discount_Id int primary key identity(1,1),
Discount_percentage decimal(10,2),
start_date date,
end_date date
);



select * from User_types;
select * from Users;
select * from Brand;
select * from Category;
select * from Coupons;
select * from Discount;
select * from Log;
select * from OrderItems;
select * from Orders;
select * from Payments;
select * from Product;
select * from Reviews;
select * from ShoppingCartItems;
select * from ShoppingCarts;
select * from subscription_type;
select * from UserCoupons;
select * from UserSubscriptions;
select * from Vendors;






















--create table Subcription_Prices(
--Subscription_Price_Id int primary key identity(1,1),
--Product_Id int,
--Subscription_Type_Id int,
--Amount decimal,
--DurationInDays INT ,
--Description varchar(500),
--CONSTRAINT FK_ProductId_Product FOREIGN KEY (Product_Id) REFERENCES Product(Product_Id),
--CONSTRAINT FK_SubscriptiontypeId_subscriptiontype FOREIGN KEY (Subscription_Type_Id) REFERENCES subscription_type(Subscription_Id)
--)

-- Reviews table for customer product reviews


-- SubscriptionPlans table to define available subscription plans
--CREATE TABLE SubscriptionPlans (
--    PlanID INT PRIMARY KEY AUTO_INCREMENT,
--    Name VARCHAR(100),
--    Description TEXT,
--    DurationInDays INT,
--    Price DECIMAL(10, 2),
--    MaxProducts INT,
--    FOREIGN KEY (MaxProducts) REFERENCES Products(ProductID)
--);



---- SubscriptionPayments table to track subscription payments
--CREATE TABLE SubscriptionPayments (
--    PaymentID INT PRIMARY KEY AUTO_INCREMENT,
--    UserSubscriptionID INT,
--    PaymentDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
--    Amount DECIMAL(10, 2),
--    PaymentMethod ENUM('CreditCard', 'PayPal', 'Stripe'),
--    TransactionID VARCHAR(100),
--    FOREIGN KEY (UserSubscriptionID) REFERENCES UserSubscriptions(UserSubscriptionID)
--);

















