


create table Supplier(
Supplier_Id int  primary key identity(1,1),
Name varchar(150),
Email varchar(80),
Mobile varchar(20),
JoinDate datetime DEFAULT CURRENT_TIMESTAMP,
RegistrationAmountPaid decimal,
Expiry_Date datetime,
StatusOfRegistration varchar(50),
PanCard varchar(20),
Licenceno varchar(30)
)


	ALTER TABLE Supplier
ADD CONSTRAINT CHK_StatusOfRegistration 
CHECK (StatusOfRegistration IN ('Pending', 'Approved', 'Rejected'));


ALTER TABLE Address
ADD Supplier_Id int;


ALTER TABLE Address
ADD CONSTRAINT FK_Supplier_Id
FOREIGN KEY (Supplier_Id)
REFERENCES Supplier(Supplier_Id);


create table SupplierPinCodes(
id int primary key identity(1,1),
Supplier_Id int foreign key references Supplier(Supplier_Id),
PinCodeOfSupply varchar(10)
)


create table SupplierBrand(
id int primary key identity(1,1),
Supplier_Id int foreign key references Supplier(Supplier_Id),
BrandIdOfSupply int foreign key references Brand(Brand_Id)
)


CREATE TABLE Supplier_order_Table (
    Supplier_order_ID INT PRIMARY KEY IDENTITY(1,1),
    Supplier_Id INT,
    Order_Id INT,
    Amount_per_Order DECIMAL,
    Order_status VARCHAR(50),
    Order_Payment_status VARCHAR(50),
	Order_type varchar(20),
	Order_startdate date,
	Order_enddate date,
    FOREIGN KEY (Supplier_Id) REFERENCES Supplier(Supplier_Id),
    FOREIGN KEY (Order_Id) REFERENCES Orders(Order_Id)
);


ALTER TABLE Supplier_order_Table
ADD CONSTRAINT CHK_Order_status 
CHECK (Order_status IN ('To be Delivered', 'Delivered', 'Cancelled By User','User Not AVailable' , 'Address Not Found' ,'Scheduled Changed' ));


ALTER TABLE Supplier_order_Table
ADD CONSTRAINT CHK_Supplier_Order_Payment_status 
CHECK (Order_Payment_status IN ('Payment Recieved', 'Pending from Admin', 'Pending from Bank' ,
					'Not Recieved' ));

ALTER TABLE OrderItems
ADD SizeOfProduct decimal(10,2);

ALTER TABLE Orders
ADD OrderPaymentStatus varchar(40);

ALTER TABLE Orders
ADD CONSTRAINT CHK_USer_Order_Payment_status 
CHECK (OrderPaymentStatus IN ('Success', 'Pending', 'Payment Failed' 
					));


// added below code on 18-10-2023

alter table Orders add TimeSlot varchar(20)


alter table Orders add AddressID int 

ALTER TABLE Orders
ADD CONSTRAINT FK_order_Address_Id
FOREIGN KEY (AddressID)
REFERENCES Address(AddressID);

alter table Orders add SupplierId int 

ALTER TABLE Orders
ADD CONSTRAINT FK_order_Supplier_Id
FOREIGN KEY (SupplierId)
REFERENCES Supplier(Supplier_Id);

// added on 19-10-2023
alter table Supplier_order_Table alter column Order_type int

// addded on 20-10-2023
ALTER PROCEDURE SpInsertOrderAndOrderItem
    @UserId INT,
    @SubscriptionTypeId INT,
    @TotalAmount DECIMAL,
    @OrderDate DATETIME,
    @StartDate DATETIME,
    @EndDate DATETIME,
    @OrderPaymentStatus VARCHAR(255),
    @TimeSlot VARCHAR(255),
    @AddressId INT,
    @SupplierId INT,
    @ProductId INT,
    @ProductPrice DECIMAL,
    @Quantity INT,
    @SizeOfProduct DECIMAL,
    @InsertedOrderId INT OUTPUT -- Define an output parameter for the inserted OrderID
AS
BEGIN
    -- Insert data into the Order table
    INSERT INTO Orders (User_id, Subscription_Type_Id, Total_Amount, Order_Date, Start_Date, End_Date, OrderPaymentStatus, TimeSlot, AddressID, SupplierId)
    VALUES (@UserId, @SubscriptionTypeId, @TotalAmount, @OrderDate, @StartDate, @EndDate, @OrderPaymentStatus, @TimeSlot, @AddressId, @SupplierId)

    -- Get the OrderId of the inserted Order
    SET @InsertedOrderId = SCOPE_IDENTITY()

    -- Insert data into the OrderItem table
    INSERT INTO OrderItems(Order_Id, Product_Id, Product_Price, Quantity, Subscription_Type_Id, Start_Date, End_Date, SizeOfProduct)
    VALUES (@InsertedOrderId, @ProductId, @ProductPrice, @Quantity, @SubscriptionTypeId, @StartDate, @EndDate, @SizeOfProduct)
END     

////
ALTER TABLE orders 
ADD createdat datetime DEFAULT CURRENT_TIMESTAMP;



