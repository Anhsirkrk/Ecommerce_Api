USE [Ecommerce_dailyPick]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Country] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Area] [varchar](50) NULL,
	[Pincode] [varchar](10) NULL,
	[HouseNo] [varchar](300) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Username] [varchar](50) NULL,
	[MobileNumber] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[Brand_id] [int] IDENTITY(1,1) NOT NULL,
	[Brand_Name] [varchar](150) NOT NULL,
	[Imageurl] [varchar](300) NULL,
	[BrandDescription] [text] NULL,
	[Category_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Category_Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [varchar](50) NOT NULL,
	[Description] [varchar](500) NULL,
	[ImageURL] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coupons]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupons](
	[CouponID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NULL,
	[Discount] [decimal](5, 2) NULL,
	[ExpiryDate] [date] NULL,
	[Description] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[CouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[Discount_Id] [int] IDENTITY(1,1) NOT NULL,
	[Discount_percentage] [decimal](10, 2) NULL,
	[start_date] [date] NULL,
	[end_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Discount_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[EventDescription] [varchar](300) NULL,
	[User_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Order_Id] [int] NOT NULL,
	[Product_Id] [int] NOT NULL,
	[Product_Price] [decimal](18, 0) NOT NULL,
	[Quantity] [int] NULL,
	[Subscription_Type_Id] [int] NOT NULL,
	[Start_Date] [date] NOT NULL,
	[End_Date] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Order_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_id] [int] NOT NULL,
	[Subscription_Type_Id] [int] NOT NULL,
	[Total_Amount] [decimal](10, 2) NOT NULL,
	[Order_Date] [date] NULL,
	[Start_Date] [date] NOT NULL,
	[End_Date] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[PaymentDate] [date] NULL,
	[PaymentMethod] [varchar](50) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[TransactionID] [varchar](100) NOT NULL,
	[Payment_Status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Product_Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Id] [int] NOT NULL,
	[Brand_Id] [int] NOT NULL,
	[Product_Name] [varchar](50) NOT NULL,
	[ImageURL] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductItemDetails]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductItemDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NULL,
	[Unit] [varchar](20) NULL,
	[SizeOfEachUnit] [decimal](10, 0) NULL,
	[WeightOfEachUnit] [decimal](18, 0) NULL,
	[StockOfEachUnit] [decimal](18, 0) NULL,
	[PRICE] [decimal](18, 0) NULL,
	[IsAvailable] [bit] NULL,
	[ManufactureDate] [date] NULL,
	[ExpiryDate] [date] NULL,
	[Discount_id] [int] NULL,
	[Available_Quantity] [decimal](18, 0) NULL,
	[Description] [text] NULL,
	[AddedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ProductID] [int] NULL,
	[Rating] [int] NULL,
	[Comment] [text] NULL,
	[ReviewDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCartItems]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCartItems](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCarts]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCarts](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscription_type]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscription_type](
	[Subscription_Id] [int] IDENTITY(1,1) NOT NULL,
	[Subscription_Type] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Subscription_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_types]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_types](
	[Type_id] [int] IDENTITY(1,1) NOT NULL,
	[User_Type] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCoupons]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCoupons](
	[UserCouponID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[CouponID] [int] NULL,
	[UsageDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserCouponID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[user_type_id] [int] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Firstname] [varchar](50) NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
	[Mobile] [varchar](20) NULL,
	[Email] [varchar](50) NOT NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSubscriptions]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSubscriptions](
	[UserSubscriptionID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NOT NULL,
	[Subscription_TypeId] [int] NOT NULL,
	[Order_id] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Subscription_Price] [decimal](10, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserSubscriptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 12-Oct-23 1:46:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[VendorID] [int] IDENTITY(1,1) NOT NULL,
	[NameofVendor] [varchar](100) NULL,
	[Brand_ID] [int] NULL,
	[Address] [varchar](255) NULL,
	[ContactEmail] [varchar](100) NULL,
	[ContactPhone] [varchar](15) NULL,
	[Description] [text] NULL,
	[LogoURL] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (1, 3, N'India', N'Telangana', N'Hyderabad', N'Banjara Hills', N'500034', N'123 ABC Street', CAST(78.459100 AS Decimal(9, 6)), CAST(17.412900 AS Decimal(9, 6)), NULL, NULL)
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (2, 2, N'India', N'Telangana', N'Secunderabad', N'Begumpet', N'500003', N'456 XYZ Road', CAST(78.504400 AS Decimal(9, 6)), CAST(17.439900 AS Decimal(9, 6)), NULL, NULL)
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (4, 2, N'India', N'Telangana', N'Hyderabad', N'Kondapur', N'500084', N'Raginiresidency block b', CAST(78.692345 AS Decimal(9, 6)), CAST(17.467579 AS Decimal(9, 6)), NULL, NULL)
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (15, 2, N'India', N'Himachal Pradesh', N'Dharamsala', N'ko', N'582582', N'jnn', CAST(77.230899 AS Decimal(9, 6)), CAST(28.604134 AS Decimal(9, 6)), N'ashok', N'+919595959595')
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (16, 2, N'India', N'Gujarat', N'Bagasra', N'mkm', N'656565', N'mmm', CAST(77.226221 AS Decimal(9, 6)), CAST(28.607563 AS Decimal(9, 6)), N'swathi', N'+919569569562')
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (17, 2, N'India', N'Kerala', N'Cherthala', N'll', N'256987', N'lll', CAST(77.236307 AS Decimal(9, 6)), CAST(28.606809 AS Decimal(9, 6)), N'mamatha', N'+919494948585')
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (18, 2, N'India', N'Gujarat', N'Babra', N'lk', N'526232', N'bvhvgh', CAST(77.236307 AS Decimal(9, 6)), CAST(28.607141 AS Decimal(9, 6)), N'satwik', N'+919898989898')
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (19, 2, N'India', N'Jharkhand', N'Deogarh', N'ml', N'524875', N'bhbh', CAST(77.228539 AS Decimal(9, 6)), CAST(28.606907 AS Decimal(9, 6)), N'siva', N'+918585858585')
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (20, 2, N'India', N'Karnataka', N'Athni', N'jujubh', N'586692', N'knjn', CAST(77.232873 AS Decimal(9, 6)), CAST(28.605589 AS Decimal(9, 6)), N'mama', N'+919862532145')
INSERT [dbo].[Address] ([AddressID], [UserID], [Country], [State], [City], [Area], [Pincode], [HouseNo], [Longitude], [Latitude], [Username], [MobileNumber]) VALUES (21, 2, N'India', N'Himachal Pradesh', N'Dharamsala', N'km', N'563252', N'bjbjbj', CAST(77.234402 AS Decimal(9, 6)), CAST(28.610219 AS Decimal(9, 6)), N'ramaa', N'+919533255210')
SET IDENTITY_INSERT [dbo].[Address] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (8, N'Heritage', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/Heritage.png', N'heritage products', 11)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (9, N'Thirumala', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/Thirumala.png', N'thirumala brand milk', 11)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (10, N'Amul', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/Amul.png', N'Amul brand milk', 11)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (11, N'HindusthanTimes', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/HindustanTimes.png', N'ht brand', 12)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (12, N'Times Of India', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/TimesOfIndia.png', N'tms brand', 12)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (13, N'Egg Plant', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/EggPlant.jpg', N'Egg Plant  veggies', 13)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (14, N'Safal', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/Safal.png', N'Safal Veggies', 13)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (15, N'MilkyMist', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/MilkyMist.png', N'milkymist', 14)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (16, N'Nestle', N'C:/Users/HP/Source/Repos/Ecommerce_Api/Assests/Images/Brand_images/Nestle.jpg', N'Nestle brand', 14)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (17, N'Natures Basket', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Brand_images/NaturesBasket.jpg', N'provides all kinds of fruits', 15)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (18, N'Hatsun', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Brand_images/Hatsun.jpeg', N'provides the curd in different sizes', 14)
INSERT [dbo].[Brand] ([Brand_id], [Brand_Name], [Imageurl], [BrandDescription], [Category_id]) VALUES (19, N'ITC', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Brand_images/ITC.png', N'itc products ', 12)
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Category_Id], [Category_Name], [Description], [ImageURL]) VALUES (11, N'Milk', N'Milk Products', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Category_images/Milk.png')
INSERT [dbo].[Category] ([Category_Id], [Category_Name], [Description], [ImageURL]) VALUES (12, N'NewsPapers', N'Contains all publishers ', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Category_images/Newspapers.png')
INSERT [dbo].[Category] ([Category_Id], [Category_Name], [Description], [ImageURL]) VALUES (13, N'Vegetables', N'all kinda of veggies', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Category_images/Vegetables.png')
INSERT [dbo].[Category] ([Category_Id], [Category_Name], [Description], [ImageURL]) VALUES (14, N'Curd', N'curd', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Category_images/Curd.png')
INSERT [dbo].[Category] ([Category_Id], [Category_Name], [Description], [ImageURL]) VALUES (15, N'Fruits', N'Contains all kinds of Seasonal Fruits also', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Category_images/Fruits1.jpg')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (1, CAST(0.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (2, CAST(10.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (3, CAST(15.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (4, CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (5, CAST(0.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (6, CAST(10.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (7, CAST(15.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (8, CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (9, CAST(0.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (10, CAST(10.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (11, CAST(15.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
INSERT [dbo].[Discount] ([Discount_Id], [Discount_percentage], [start_date], [end_date]) VALUES (12, CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-09-01' AS Date), CAST(N'2023-09-30' AS Date))
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItems] ON 

INSERT [dbo].[OrderItems] ([ItemId], [Order_Id], [Product_Id], [Product_Price], [Quantity], [Subscription_Type_Id], [Start_Date], [End_Date]) VALUES (1, 2, 22, CAST(29 AS Decimal(18, 0)), 1, 1, CAST(N'2023-09-15' AS Date), CAST(N'2023-10-14' AS Date))
SET IDENTITY_INSERT [dbo].[OrderItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Order_Id], [User_id], [Subscription_Type_Id], [Total_Amount], [Order_Date], [Start_Date], [End_Date]) VALUES (1, 1, 2, CAST(100.00 AS Decimal(10, 2)), CAST(N'2023-09-14' AS Date), CAST(N'2023-09-15' AS Date), CAST(N'2024-09-14' AS Date))
INSERT [dbo].[Orders] ([Order_Id], [User_id], [Subscription_Type_Id], [Total_Amount], [Order_Date], [Start_Date], [End_Date]) VALUES (2, 2, 1, CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-09-14' AS Date), CAST(N'2023-09-15' AS Date), CAST(N'2023-10-14' AS Date))
INSERT [dbo].[Orders] ([Order_Id], [User_id], [Subscription_Type_Id], [Total_Amount], [Order_Date], [Start_Date], [End_Date]) VALUES (3, 3, 3, CAST(75.00 AS Decimal(10, 2)), CAST(N'2023-09-14' AS Date), CAST(N'2023-09-15' AS Date), CAST(N'2024-09-14' AS Date))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentID], [OrderID], [PaymentDate], [PaymentMethod], [Amount], [TransactionID], [Payment_Status]) VALUES (2, 2, CAST(N'2023-10-07' AS Date), N'online', CAST(50.00 AS Decimal(10, 2)), N'04J8PAY5RS', N'Pending')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Product_Id], [Category_Id], [Brand_Id], [Product_Name], [ImageURL]) VALUES (22, 11, 8, N'TonedMilk', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Product_images/TonedMilk.png')
INSERT [dbo].[Product] ([Product_Id], [Category_Id], [Brand_Id], [Product_Name], [ImageURL]) VALUES (23, 12, 12, N'Morning Edition', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Product_images/NPTimesOfIndia.jpg')
INSERT [dbo].[Product] ([Product_Id], [Category_Id], [Brand_Id], [Product_Name], [ImageURL]) VALUES (24, 11, 8, N'Gold Milk', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Product_images/GoldMilk.jpg')
INSERT [dbo].[Product] ([Product_Id], [Category_Id], [Brand_Id], [Product_Name], [ImageURL]) VALUES (25, 13, 14, N'Onions', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Product_images/Onions.jpg')
INSERT [dbo].[Product] ([Product_Id], [Category_Id], [Brand_Id], [Product_Name], [ImageURL]) VALUES (26, 14, 18, N'Curd(packets)', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Product_images/HatsunPacketCurd.jpg')
INSERT [dbo].[Product] ([Product_Id], [Category_Id], [Brand_Id], [Product_Name], [ImageURL]) VALUES (27, 15, 17, N'Green Apples', N'C:/Users/HP/OneDrive/Desktop/EcommerceApi_Images/Product_images/GreenApple.jpeg')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductItemDetails] ON 

INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (1, 22, N'ML', CAST(500 AS Decimal(10, 0)), CAST(500 AS Decimal(18, 0)), CAST(1500 AS Decimal(18, 0)), CAST(29 AS Decimal(18, 0)), 1, CAST(N'2023-10-02' AS Date), CAST(N'2023-11-01' AS Date), 1, CAST(1500 AS Decimal(18, 0)), N'500 ml packets', CAST(N'2023-10-02T11:20:03.560' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (2, 22, N'ML', CAST(1000 AS Decimal(10, 0)), CAST(1000 AS Decimal(18, 0)), CAST(1200 AS Decimal(18, 0)), CAST(57 AS Decimal(18, 0)), 1, CAST(N'2023-10-02' AS Date), CAST(N'2023-11-01' AS Date), 1, CAST(1200 AS Decimal(18, 0)), N'1000 ml packets', CAST(N'2023-10-02T11:20:22.267' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (3, 23, N'pcs', CAST(1 AS Decimal(10, 0)), CAST(1 AS Decimal(18, 0)), CAST(250 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), 1, CAST(N'2023-10-02' AS Date), CAST(N'2023-10-03' AS Date), 1, CAST(250 AS Decimal(18, 0)), N'morning edition of newspaper', CAST(N'2023-10-02T12:06:37.740' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (4, 24, N'ML', CAST(500 AS Decimal(10, 0)), CAST(500 AS Decimal(18, 0)), CAST(150 AS Decimal(18, 0)), CAST(30 AS Decimal(18, 0)), 1, CAST(N'2023-10-02' AS Date), CAST(N'2023-10-03' AS Date), 1, CAST(150 AS Decimal(18, 0)), N'500 ml gold', CAST(N'2023-10-02T13:23:29.373' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (5, 24, N'ML', CAST(1000 AS Decimal(10, 0)), CAST(1000 AS Decimal(18, 0)), CAST(100 AS Decimal(18, 0)), CAST(60 AS Decimal(18, 0)), 1, CAST(N'2023-10-02' AS Date), CAST(N'2023-10-03' AS Date), 1, CAST(100 AS Decimal(18, 0)), N'1000 ml gold', CAST(N'2023-10-02T13:23:29.380' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (6, 25, N'KG', CAST(5 AS Decimal(10, 0)), CAST(5 AS Decimal(18, 0)), CAST(500 AS Decimal(18, 0)), CAST(500 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(500 AS Decimal(18, 0)), N'5 KG BAGS OF ONIONS', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (7, 25, N'KG', CAST(10 AS Decimal(10, 0)), CAST(10 AS Decimal(18, 0)), CAST(1000 AS Decimal(18, 0)), CAST(950 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(1000 AS Decimal(18, 0)), N'10 KG BAGS OF ONIONS', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (8, 25, N'KG', CAST(20 AS Decimal(10, 0)), CAST(20 AS Decimal(18, 0)), CAST(1500 AS Decimal(18, 0)), CAST(1800 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(1500 AS Decimal(18, 0)), N'5 KG BAGS OF ONIONS', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (9, 25, N'KG', CAST(50 AS Decimal(10, 0)), CAST(50 AS Decimal(18, 0)), CAST(2000 AS Decimal(18, 0)), CAST(4200 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(2000 AS Decimal(18, 0)), N'5 KG BAGS OF ONIONS', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (10, 26, N'GM', CAST(110 AS Decimal(10, 0)), CAST(110 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(50 AS Decimal(18, 0)), N'110 GMS OF PACKET', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (11, 26, N'GM', CAST(200 AS Decimal(10, 0)), CAST(200 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(20 AS Decimal(18, 0)), N'200 GMS OF PACKET', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (12, 26, N'GM', CAST(400 AS Decimal(10, 0)), CAST(400 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), CAST(40 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(20 AS Decimal(18, 0)), N'400 GMS OF PACKET', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (13, 26, N'GM', CAST(500 AS Decimal(10, 0)), CAST(500 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(20 AS Decimal(18, 0)), N'500 GMS OF PACKET', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (14, 26, N'GM', CAST(1000 AS Decimal(10, 0)), CAST(1000 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), CAST(60 AS Decimal(18, 0)), 1, CAST(N'2023-10-04' AS Date), CAST(N'2023-12-04' AS Date), 1, CAST(20 AS Decimal(18, 0)), N'1000 GMS OF PACKET', CAST(N'2023-10-04T16:47:00.173' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (15, 27, N'KG', CAST(1 AS Decimal(10, 0)), CAST(1 AS Decimal(18, 0)), CAST(30 AS Decimal(18, 0)), CAST(80 AS Decimal(18, 0)), 1, CAST(N'2023-10-14' AS Date), CAST(N'2023-10-14' AS Date), 1, CAST(30 AS Decimal(18, 0)), N'1KG', CAST(N'2023-10-04T17:24:33.130' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (16, 27, N'KG', CAST(3 AS Decimal(10, 0)), CAST(3 AS Decimal(18, 0)), CAST(15 AS Decimal(18, 0)), CAST(240 AS Decimal(18, 0)), 1, CAST(N'2023-10-14' AS Date), CAST(N'2023-10-14' AS Date), 1, CAST(15 AS Decimal(18, 0)), N'3 KG', CAST(N'2023-10-04T17:24:33.130' AS DateTime))
INSERT [dbo].[ProductItemDetails] ([id], [Product_Id], [Unit], [SizeOfEachUnit], [WeightOfEachUnit], [StockOfEachUnit], [PRICE], [IsAvailable], [ManufactureDate], [ExpiryDate], [Discount_id], [Available_Quantity], [Description], [AddedDate]) VALUES (17, 27, N'KG', CAST(5 AS Decimal(10, 0)), CAST(5 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), CAST(370 AS Decimal(18, 0)), 1, CAST(N'2023-10-14' AS Date), CAST(N'2023-10-14' AS Date), 1, CAST(10 AS Decimal(18, 0)), N'5 1KG', CAST(N'2023-10-04T17:24:33.130' AS DateTime))
SET IDENTITY_INSERT [dbo].[ProductItemDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingCartItems] ON 

INSERT [dbo].[ShoppingCartItems] ([ItemID], [CartID], [ProductID], [Quantity]) VALUES (1, 1, 23, 1)
INSERT [dbo].[ShoppingCartItems] ([ItemID], [CartID], [ProductID], [Quantity]) VALUES (2, 1, 24, 1)
SET IDENTITY_INSERT [dbo].[ShoppingCartItems] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingCarts] ON 

INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (1, 2, CAST(N'2023-09-20T11:44:15.090' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (2, 3, CAST(N'2023-09-20T11:44:15.090' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (3, 1, CAST(N'2023-09-20T11:44:15.090' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (4, 3, CAST(N'2023-09-20T16:27:58.247' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (5, 3, CAST(N'2023-09-20T16:32:48.260' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (6, 3, CAST(N'2023-09-20T16:35:16.383' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (7, 11, CAST(N'2023-09-20T16:39:34.993' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (8, 11, CAST(N'2023-09-20T16:40:53.113' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (9, 11, CAST(N'2023-09-20T16:55:27.393' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (10, 17, CAST(N'2023-09-22T15:40:56.277' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (11, 19, CAST(N'2023-09-22T16:51:39.500' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (12, 20, CAST(N'2023-09-22T16:58:05.097' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (13, 21, CAST(N'2023-09-22T17:13:46.320' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (14, 22, CAST(N'2023-09-22T17:15:49.870' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (15, 23, CAST(N'2023-09-22T17:25:04.807' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (16, 24, CAST(N'2023-09-22T17:26:01.747' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (17, 25, CAST(N'2023-09-22T17:28:07.817' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (18, 26, CAST(N'2023-09-22T17:29:33.950' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (19, 27, CAST(N'2023-09-22T17:31:06.683' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (20, 28, CAST(N'2023-09-22T17:31:49.940' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (21, 29, CAST(N'2023-09-22T17:39:34.047' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (22, 30, CAST(N'2023-09-22T17:45:14.543' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (23, 31, CAST(N'2023-09-22T17:45:58.580' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (24, 32, CAST(N'2023-09-22T17:46:25.230' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (25, 33, CAST(N'2023-09-22T17:47:40.423' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (26, 34, CAST(N'2023-09-22T17:52:38.983' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (27, 35, CAST(N'2023-09-22T17:57:32.093' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (28, 36, CAST(N'2023-09-22T19:06:54.540' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (29, 37, CAST(N'2023-09-22T19:07:20.660' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (30, 38, CAST(N'2023-09-22T19:07:57.883' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (31, 39, CAST(N'2023-09-22T19:09:06.653' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (32, 40, CAST(N'2023-09-22T19:14:31.213' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (33, 41, CAST(N'2023-09-22T19:15:08.873' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (34, 42, CAST(N'2023-09-23T12:22:01.303' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (35, 43, CAST(N'2023-09-23T12:28:34.637' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (36, 45, CAST(N'2023-10-08T19:05:39.377' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (37, 46, CAST(N'2023-10-09T11:31:34.683' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (38, 47, CAST(N'2023-10-11T13:15:45.867' AS DateTime))
INSERT [dbo].[ShoppingCarts] ([CartID], [UserID], [CreatedAt]) VALUES (39, 48, CAST(N'2023-10-11T16:55:01.237' AS DateTime))
SET IDENTITY_INSERT [dbo].[ShoppingCarts] OFF
GO
SET IDENTITY_INSERT [dbo].[subscription_type] ON 

INSERT [dbo].[subscription_type] ([Subscription_Id], [Subscription_Type]) VALUES (1, N'Monthly')
INSERT [dbo].[subscription_type] ([Subscription_Id], [Subscription_Type]) VALUES (2, N'Annual')
INSERT [dbo].[subscription_type] ([Subscription_Id], [Subscription_Type]) VALUES (3, N'Lifetime')
SET IDENTITY_INSERT [dbo].[subscription_type] OFF
GO
SET IDENTITY_INSERT [dbo].[User_types] ON 

INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (1, N'Admin')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (2, N'User')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (3, N'Vendor')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (4, N'Deliveryboy')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (5, N'Admin')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (6, N'User')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (7, N'Vendor')
INSERT [dbo].[User_types] ([Type_id], [User_Type]) VALUES (8, N'Deliveryboy')
SET IDENTITY_INSERT [dbo].[User_types] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (1, 1, N'Krishna', N'pass@123', N'Krishna', N'Admin', N'1234567890', N'admin@example.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (2, 2, N'Shivaprakash', N'shiva123', N'shiva', N'prakash', N'111122', N'shiva@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (3, 3, N'Bhaskar', N'pass@123', N'Bhaskar', N'Vendor', N'5555555555', N'vendor@example.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (11, 1, N'KrishnaU', N'pass@123', N'KrishnaU', N'Admin', N'1234567890', N'amin@example.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (12, 2, N'ShivaU', N'pass@123', N'Shiva', N'User', N'9876543210', N'user1@example.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (13, 3, N'BhaskaUr', N'pass@123', N'Bhaskar', N'Vendor', N'5555555555', N'vendo1r@example.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (14, 1, N'vvsg', N'vvsg@123', N'vvsg', N'user', N'9491361441', N'vvsg@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (15, 1, N'vvsg1', N'vvsg@1234', N'vvsg1', N'uservvsg', N'+919491361441', N'vvsg1@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (16, 1, N'mamatha', N'mamath@1234', N'mamatha1', N'mamatha', N'+917680914489', N'mamatha@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (17, 1, N'surya@gmail.com', N'password', N'surya', N'g', N'+919640339222', N'surya@gmail.com', NULL)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (19, 1, N'umesh@gmail.com', N'password', N'umesh', N'null', N'+917894561231', N'umesh@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (20, 1, N'krishna@gmail.com', N'password', N'krishna', N'null', N'+918888888888', N'krishna@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (21, 1, N'vasmi@gmail.com', N'password', N'vamsi', N'null', N'+917777777777', N'vasmi@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (22, 1, N'akhil@gmail.com', N'password', N'akhil', N'null', N'+918529637412', N'akhil@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (23, 1, N'15@gmail.com', N'password', N'user15', N'null', N'+919999999915', N'15@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (24, 1, N'16@gmail.com', N'password', N'user16', N'null', N'+919999999916', N'16@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (25, 1, N'17@gmail.com', N'password', N'user17', N'null', N'+919999999917', N'17@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (26, 1, N'18@gmail.com', N'password', N'user18', N'null', N'+9999999918', N'18@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (27, 1, N'19@gmail.com', N'password', N'user19', N'null', N'+919999999919', N'19@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (28, 1, N'20@gmail.com', N'password', N'user20', N'null', N'+919999999920', N'20@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (29, 1, N'user21@gmail.com', N'password', N'user21', N'null', N'+919999999921', N'user21@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (30, 1, N'user23@gmail.com', N'password', N'user23', N'null', N'+919999999923', N'user23@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (31, 1, N'user24@gmail.com', N'password', N'user24', N'null', N'+919999999924', N'user24@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (32, 1, N'user25@gmail.com', N'password', N'user25', N'null', N'+919999999925', N'user25@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (33, 1, N'user26@gmail.com', N'password', N'user26', N'null', N'+919999999926', N'user26@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (34, 1, N'user27@gmail.com', N'password', N'user27', N'null', N'+919999999927', N'user27@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (35, 1, N'user30@gmail.com', N'password', N'user30', N'null', N'+919999999930', N'user30@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (36, 1, N'user31@gmail.com', N'password', N'user31', N'null', N'+919999999931', N'user31@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (37, 1, N'user32@gmail.com', N'password', N'user32', N'null', N'+919999999932', N'user32@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (38, 1, N'user33@gmail.com', N'password', N'user33', N'null', N'+919999999933', N'user33@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (39, 1, N'user34@gmail.com', N'password', N'user34', N'null', N'+919999999934', N'user34@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (40, 1, N'user35@gmail.com', N'password', N'user34', N'null', N'+919999999935', N'user35@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (41, 1, N'user36@gmail.com', N'password', N'user36', N'null', N'+919999999936', N'user36@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (42, 1, N'user37@gmail.com', N'password', N'user37', N'null', N'+919999999937', N'user37@gmail.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (43, 1, N'jane@example.com', N'password', N'jane', N'null', N'+919999999999', N'jane@example.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (45, 1, N'a@a.com', N'password', N'gudupu surya', N'null', N'+919490890244', N'a@a.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (46, 1, N'm@m.com', N'password', N'Madhura', N'null', N'+918897037576', N'm@m.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (47, 1, N'b@b.com', N'shiva123', N'bhaskar', N'null', N'+919491361442', N'b@b.com', 1)
INSERT [dbo].[Users] ([User_Id], [user_type_id], [Username], [Password], [Firstname], [Lastname], [Mobile], [Email], [IsActive]) VALUES (48, 1, N'ashok@gmail.com', N'password', N'ashok', N'null', N'+919533233031', N'ashok@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserSubscriptions] ON 

INSERT [dbo].[UserSubscriptions] ([UserSubscriptionID], [User_ID], [Subscription_TypeId], [Order_id], [StartDate], [EndDate], [Subscription_Price], [IsActive]) VALUES (1, 2, 1, 2, CAST(N'2023-09-15' AS Date), CAST(N'2023-10-14' AS Date), CAST(50.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[UserSubscriptions] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Coupons__A25C5AA732B10DC6]    Script Date: 12-Oct-23 1:46:04 PM ******/
ALTER TABLE [dbo].[Coupons] ADD UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E401826BAD]    Script Date: 12-Oct-23 1:46:04 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053407E15804]    Script Date: 12-Oct-23 1:46:04 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[ProductItemDetails] ADD  CONSTRAINT [DF_ProductItemDetails_AddedDate]  DEFAULT (getdate()) FOR [AddedDate]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT (getdate()) FOR [ReviewDate]
GO
ALTER TABLE [dbo].[ShoppingCarts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[UserCoupons] ADD  DEFAULT (getdate()) FOR [UsageDate]
GO
ALTER TABLE [dbo].[UserSubscriptions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[Brand]  WITH CHECK ADD FOREIGN KEY([Category_id])
REFERENCES [dbo].[Category] ([Category_Id])
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [Fk_OrderId_Orders] FOREIGN KEY([Order_Id])
REFERENCES [dbo].[Orders] ([Order_Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [Fk_OrderId_Orders]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [Fk_ProductId_Products] FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Product] ([Product_Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [Fk_ProductId_Products]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [Fk_SubscriptionId_subscriptions] FOREIGN KEY([Subscription_Type_Id])
REFERENCES [dbo].[subscription_type] ([Subscription_Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [Fk_SubscriptionId_subscriptions]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Fk_SubscriptionId_Subscription] FOREIGN KEY([Subscription_Type_Id])
REFERENCES [dbo].[subscription_type] ([Subscription_Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Fk_SubscriptionId_Subscription]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Fk_UserId_User] FOREIGN KEY([User_id])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Fk_UserId_User]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([Order_Id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Brand_Id_Brand] FOREIGN KEY([Brand_Id])
REFERENCES [dbo].[Brand] ([Brand_id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Brand_Id_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Category_Id_Category] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Category] ([Category_Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Category_Id_Category]
GO
ALTER TABLE [dbo].[ProductItemDetails]  WITH CHECK ADD FOREIGN KEY([Discount_id])
REFERENCES [dbo].[Discount] ([Discount_Id])
GO
ALTER TABLE [dbo].[ProductItemDetails]  WITH CHECK ADD FOREIGN KEY([Product_Id])
REFERENCES [dbo].[Product] ([Product_Id])
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([Product_Id])
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[ShoppingCartItems]  WITH CHECK ADD FOREIGN KEY([CartID])
REFERENCES [dbo].[ShoppingCarts] ([CartID])
GO
ALTER TABLE [dbo].[ShoppingCartItems]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([Product_Id])
GO
ALTER TABLE [dbo].[ShoppingCarts]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[UserCoupons]  WITH CHECK ADD FOREIGN KEY([CouponID])
REFERENCES [dbo].[Coupons] ([CouponID])
GO
ALTER TABLE [dbo].[UserCoupons]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_usertypeid_User_types] FOREIGN KEY([user_type_id])
REFERENCES [dbo].[User_types] ([Type_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_usertypeid_User_types]
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([Subscription_TypeId])
REFERENCES [dbo].[subscription_type] ([Subscription_Id])
GO
ALTER TABLE [dbo].[UserSubscriptions]  WITH CHECK ADD FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([User_Id])
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD FOREIGN KEY([Brand_ID])
REFERENCES [dbo].[Brand] ([Brand_id])
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [CK_ValidEventDescription] CHECK  (([EventDescription]='Product_Discount_Changed' OR [EventDescription]='Product_Removed' OR [EventDescription]='Product_added' OR [EventDescription]='Passwordchanged' OR [EventDescription]='Logoff' OR [EventDescription]='Login'))
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [CK_ValidEventDescription]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([Payment_Status]='failed' OR [Payment_Status]='completed' OR [Payment_Status]='pending'))
GO
