create database Ecommercedemo
use Ecommercedemo

--*********************Doubts**********************---- 
--subscription related to order
----product based subscription , multiple products subscription 
-----Discount id in product table





create table User_types (
Type_id int  primary key identity(1,1),
User_Type varchar(20)
)

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
	   )

-- Insert data for users
	   INSERT INTO Users (user_type_id, Username, Password, Firstname, Lastname, Mobile, Email, IsActive)
VALUES
(1, 'Krishna', 'pass@123', 'Krishna', 'Admin', 1234567890, 'admin@example.com', 1),
(2, 'Shiva', 'pass@123', 'Shiva', 'User', 9876543210, 'user@example.com', 1),
(3, 'Bhaskar', 'pass@123', 'Bhaskar', 'Vendor', 5555555555, 'vendor@example.com', 1);




create table Brand (
Brand_id int primary key identity(1,1),
Brand_Name varchar(150) not null
)
-- insert data into brand

INSERT INTO Brand (Brand_Name) VALUES
('Heritage'),
('Hutsun'),
('Amul');


-- Categories table to classify grocery items
create table Category (
Category_Id int primary key identity(1,1),
Category_Name varchar(50) not null,
Description varchar(500),
ImageURL VARCHAR(255) -- URL to category image
)

-- insert data into category
INSERT INTO Category (Category_Name, Description, ImageURL) VALUES
('Category1', 'Description for Category 1', '/images/category1.jpg'),
('Category2', 'Description for Category 2', '/images/category2.jpg'),
('Category3', 'Description for Category 3', '/images/category3.jpg');


create table Discount
(
Discount_Id int primary key identity(1,1),
Discount_percentage decimal(10,2),
start_date date,
end_date date
)

--insert data into discount
INSERT INTO Discount (Discount_percentage, start_date, end_date) VALUES
(0.00, '2023-09-01', '2023-09-30'),
(10.00, '2023-09-01', '2023-09-30'),
(15.00, '2023-09-01', '2023-09-30'),
(20.00, '2023-09-01', '2023-09-30');


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
Discount_id int not null,
Description varchar(1000),
CONSTRAINT FK_Category_Id_Category FOREIGN KEY (Category_Id) REFERENCES Category(Category_Id),
CONSTRAINT FK_Brand_Id_Brand FOREIGN KEY (Brand_Id) REFERENCES Brand(Brand_id),
CONSTRAINT FK_Discount_Id FOREIGN KEY (Discount_id) REFERENCES Discount(Discount_id)

)

-- insert data into products
INSERT INTO Product (Category_Id, Brand_Id, Product_Name, StockQuantity, Price, Weight, Unit, ImageURL, IsAvailable, ExpiryDate, ManufactureDate, Discount_id, Description)
VALUES
(1, 1, 'Product1', 100, 20.00, 0.5, 'kg', '/images/product1.jpg', 1, '2023-12-31', '2023-01-01', 1, 'Description for Product 1'),
(2, 2, 'Product2', 75, 25.00, 1.0, 'piece', '/images/product2.jpg', 1, '2023-12-31', '2023-01-01', 2, 'Description for Product 2'),
(3, 3, 'Product3', 50, 15.00, 0.25, 'liter', '/images/product3.jpg', 1, '2023-12-31', '2023-01-01', 3, 'Description for Product 3');




create table subscription_type
(
Subscription_Id int primary key identity(1,1),
Subscription_Type varchar(20) not null
)

INSERT INTO subscription_type (Subscription_Type) VALUES
('Monthly'),
('Annual'),
('Lifetime');



CREATE TABLE ShoppingCarts (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    CreatedAt DATETIME default current_timestamp,
    FOREIGN KEY (UserID) REFERENCES Users(User_Id)
)




INSERT INTO ShoppingCarts ( UserID) VALUES
( 2), -- User with ID 2 has a shopping cart
( 3), -- User with ID 3 has a shopping cart
( 1); -- User with ID 1 has a shopping cart



-- ShoppingCartItems table to store items in user shopping carts
CREATE TABLE ShoppingCartItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    FOREIGN KEY (CartID) REFERENCES ShoppingCarts(CartID),
    FOREIGN KEY (ProductID) REFERENCES Product(Product_ID)
);



INSERT INTO ShoppingCartItems ( CartID, ProductID, Quantity) VALUES
( 1, 4, 3), -- User 2's cart has 3 units of Product with ID 4
( 2, 2, 2), -- User 3's cart has 2 units of Product with ID 2
( 3, 5, 1); -- User 1's cart has 1 unit of Product with ID 5




-- Coupons table to manage discount coupons
CREATE TABLE Coupons (
    CouponID INT PRIMARY KEY identity(1,1),
    Code VARCHAR(20) UNIQUE,
    Discount DECIMAL(5, 2), -- Discount percentage
    ExpiryDate DATE,
    Description TEXT
);

INSERT INTO Coupons (Code, Discount, ExpiryDate, Description) VALUES
('COUPON10', 10.00, '2023-09-30', '10% off coupon'),
('COUPON15', 15.00, '2023-09-30', '15% off coupon'),
('COUPON20', 20.00, '2023-09-30', '20% off coupon');


-- UserCoupons table to link users to coupons they've used
CREATE TABLE UserCoupons (
    UserCouponID INT PRIMARY KEy identity(1,1),
    UserID INT,
    CouponID INT,
    UsageDate date DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(User_Id),
    FOREIGN KEY (CouponID) REFERENCES Coupons(CouponID)
);

INSERT INTO UserCoupons (UserID, CouponID) VALUES
(1, 1), -- User 1 used Coupon 1
(2, 2), -- User 2 used Coupon 2
(3, 3); -- User 3 used Coupon 3


-- Orders table to store customer orders

create table Orders (
Order_Id int primary key identity(1,1),
User_id int not null,
Subscription_Type_Id int not null,
Total_Amount decimal(10,2) not null,
Order_Date date, 
Start_Date date not null,
End_Date date not null,
constraint Fk_UserId_User foreign key (User_id) references Users(User_Id),
constraint Fk_SubscriptionId_Subscription foreign key (Subscription_Type_Id) references subscription_type(Subscription_Id)
)

INSERT INTO Orders (User_id, Subscription_Type_Id, Total_Amount, Order_Date, Start_Date, End_Date) VALUES
(1, 2, 100.00, '2023-09-14', '2023-09-15', '2024-09-14'),
(2, 1, 50.00, '2023-09-14', '2023-09-15', '2023-10-14'),
(3, 3, 75.00, '2023-09-14', '2023-09-15', '2024-09-14');



-- OrderItems table to link products to orders
create table OrderItems(
ItemId int primary key identity(1,1),
Order_Id int not null ,
Product_Id int not null,
Product_Price decimal not null,
Quantity int ,
Subscription_Type_Id int not null,
Start_Date date not null,
End_Date date not null,
constraint Fk_OrderId_Orders foreign key (Order_Id) references Orders(Order_Id),
constraint Fk_ProductId_Products foreign key (Product_Id) references Product(Product_Id),
constraint Fk_SubscriptionId_subscriptions foreign key (Subscription_Type_Id) references subscription_type(Subscription_Id)
)

INSERT INTO OrderItems (Order_Id, Product_Id, Product_Price, Quantity, Subscription_Type_Id, Start_Date, End_Date) VALUES
(1, 3, 25.00, 2, 2, '2023-09-15', '2023-10-15'), -- Order 1 includes Product 3
(2, 1, 20.00, 3, 1, '2023-09-15', '2024-09-15'), -- Order 2 includes Product 1
(3, 2, 15.00, 4, 3, '2023-09-15', '2024-09-15'); -- Order 3 includes Product 2


-- Payments table to store payment information
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY identity(1,1),
    OrderID INT not null,
    PaymentDate date DEFAULT CURRENT_TIMESTAMP,
    PaymentMethod VARCHAR(50) not null,
    Amount DECIMAL(10, 2) not null,
    TransactionID VARCHAR(100) not null,
	Payment_Status varchar(20)  CHECK (Payment_Status IN ('pending', 'completed', 'failed')),
    FOREIGN KEY (OrderID) REFERENCES Orders(Order_Id)
);


INSERT INTO Payments (OrderID, PaymentMethod, Amount, TransactionID, Payment_Status) VALUES
(1, 'Credit Card', 100.00, '1234567890', 'completed'),
(2, 'PayPal', 50.00, '9876543210', 'completed'),
(3, 'Credit Card', 75.00, '5555555555', 'completed');




CREATE TABLE UserSubscriptions (
    UserSubscriptionID INT identity(1,1) PRIMARY KEY,
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


INSERT INTO UserSubscriptions (User_ID, Subscription_TypeId, Order_id, StartDate, EndDate, Subscription_Price,IsActive) VALUES
(1, 3, 1, '2023-09-15', '2024-09-15', 75.00,1), -- User 1 has a lifetime subscription
(2, 2, 2, '2023-09-15', '2024-09-15', 100.00,1), -- User 2 has an annual subscription
(3, 1, 3, '2023-09-15', '2023-10-15', 50.00,1); -- User 3 has a monthly subscription


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


INSERT INTO Reviews (UserID, ProductID, Rating, Comment) VALUES
(1, 1, 5, 'Great product!'),
(2, 2, 4, 'Good value for money.'),
(3, 3, 5, 'Excellent quality.');


-- Vendors table to manage product suppliers
CREATE TABLE Vendors (
    VendorID INT PRIMARY KEY identity(1,1),
    NameofVendor VARCHAR(100),
	Brand_ID int ,
    Address VARCHAR(255),
    ContactEmail VARCHAR(100),
    ContactPhone VARCHAR(15),
    Description TEXT,
    LogoURL VARCHAR(255) -- URL to vendor logo,
	    FOREIGN KEY (Brand_ID) REFERENCES Brand(Brand_id)

);

INSERT INTO Vendors (NameofVendor, Brand_ID, Address, ContactEmail, ContactPhone, Description, LogoURL) VALUES
('Vendor1', 1, '123 Vendor St', 'vendor1@example.com', '123-456-7890', 'Vendor 1 description.', '/images/vendor1.jpg'),
('Vendor2', 2, '456 Vendor Rd', 'vendor2@example.com', '987-654-3210', 'Vendor 2 description.', '/images/vendor2.jpg'),
('Vendor3', 3, '789 Vendor Ave', 'vendor3@example.com', '555-555-5555', 'Vendor 3 description.', '/images/vendor3.jpg');


create table Log
(
LogId int primary key identity(1,1),
LogDate Datetime,
EventDescription varchar(300),
User_Id int,
foreign key (User_Id) references Users(User_Id),
CONSTRAINT CK_ValidEventDescription CHECK (EventDescription IN ('Login', 'Logoff', 'Passwordchanged','Product_added','Product_Removed','Product_Discount_Changed'))
);

INSERT INTO Log (LogDate, EventDescription, User_Id) VALUES
('2023-09-14 10:00:00', 'Login', 1),
('2023-09-14 12:30:00', 'Product_added', 2),
('2023-09-14 15:45:00', 'Order_placed', 3);


select * from User_types;
select * from Users;
select * from UserCoupons;
select * from UserSubscriptions;
select * from Product;
select * from Brand;
select * from Category;
select * from Discount;
select * from Log;
select * from OrderItems;
select * from Orders;
select * from Payments;
select * from Reviews;
select * from ShoppingCartItems;
select * from ShoppingCarts;
select * from User_types;
select * from subscription_type;
select * from Vendors;


