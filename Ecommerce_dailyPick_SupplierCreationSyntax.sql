


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

alter table Orders add TimeSlot varchar(20)




