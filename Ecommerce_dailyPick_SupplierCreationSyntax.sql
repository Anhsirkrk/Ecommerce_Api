

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

ALTER TABLE orders 
ADD createdat datetime DEFAULT CURRENT_TIMESTAMP;

	
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



//  added on 26-10-2023

-- Create a stored procedure to insert payment, update order, and insert UserSubscription (if PaymentStatus is 'Success')
CREATE PROCEDURE SP_InsertPaymentAndUpdateOrderAndUserSubscription
    @OrderId INT,
    @PaymentDate DATETIME,
    @PaymentMethod VARCHAR(255),
    @Amount DECIMAL,
    @TransactionId VARCHAR(255),
    @PaymentStatus VARCHAR(255),
     @InsertedPaymentId INT OUTPUT -- Define an output parameter for the inserted OrderID 
	 ,@InsertedUserSubscriptionId INT OUTPUT 
  
  
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insert data into the Payments table
        INSERT INTO Payments (OrderID, PaymentDate, PaymentMethod, Amount, TransactionID, Payment_Status)
        VALUES (@OrderId, @PaymentDate, @PaymentMethod, @Amount, @TransactionId, @PaymentStatus)

		  -- Get the OrderId of the inserted Order
    SET @InsertedPaymentId = SCOPE_IDENTITY()

        -- Update the Orders table based on Payment status
        UPDATE Orders
        SET OrderPaymentStatus =
            CASE
                WHEN @PaymentStatus = 'completed' THEN 'Success'
                WHEN @PaymentStatus = 'pending' THEN 'Pending'
                WHEN @PaymentStatus = 'failed' THEN 'Payment Failed'
                ELSE OrderPaymentStatus
            END
        WHERE Order_Id = @OrderId;

        -- Insert data into the UserSubscriptions table if PaymentStatus is 'Success'
        IF @PaymentStatus = 'completed'
        BEGIN
            -- Retrieve necessary values from Orders table
             DECLARE @Subscription_TypeId INT;
        DECLARE @Subscription_Price DECIMAL;
        DECLARE @UserSubscriptionStartDate DATETIME;
        DECLARE @UserSubscriptionEndDate DATETIME;
        DECLARE  @UserId INT;

        SELECT @Subscription_TypeId = Subscription_Type_Id,
               @Subscription_Price = Total_Amount,
               @UserSubscriptionStartDate = Start_Date,
               @UserSubscriptionEndDate = End_Date,
               @UserId =User_id
        FROM Orders WHERE Order_Id = @OrderId;
            
            -- Insert data into the UserSubscriptions table
            INSERT INTO UserSubscriptions (User_ID, Subscription_TypeId, Order_id, StartDate, EndDate, Subscription_Price, IsActive)
            VALUES (@UserId, @Subscription_TypeId, @OrderId, @UserSubscriptionStartDate, @UserSubscriptionEndDate, @Subscription_Price, 1)

				  -- Get the OrderId of the inserted Order
    SET @InsertedUserSubscriptionId = SCOPE_IDENTITY()

        END

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT ERROR_MESSAGE(); 
    END CATCH
END

// added on 27-10-2023

create PROCEDURE SP_InsertSupplier_order_Table
    @Supplier_Id INT,
    @Order_Id INT,
    @Amount_per_Order DECIMAL,
    @Order_status varchar(50),
    @Order_Payment_status varchar(50),
    @Order_type int,
    @Order_startdate datetime,
    @Order_enddate datetime,
    @InsertedSupplier_order_ID INT OUTPUT -- Define an output parameter for the inserted OrderID
AS
BEGIN
    -- Insert data into the Order table
    INSERT INTO Supplier_order_Table(Supplier_Id,Order_Id ,Amount_per_Order,Order_status,Order_Payment_status,Order_type,Order_startdate,Order_enddate)
    VALUES ( @Supplier_Id ,
    @Order_Id ,
    @Amount_per_Order ,
    @Order_status ,
    @Order_Payment_status ,
    @Order_type ,
    @Order_startdate ,
    @Order_enddate )

    -- Get the OrderId of the inserted Order
    SET @InsertedSupplier_order_ID = SCOPE_IDENTITY()

END

