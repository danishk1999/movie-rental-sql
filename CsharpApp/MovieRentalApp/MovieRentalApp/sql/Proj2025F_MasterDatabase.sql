USE [Proj2025F]
GO
ALTER TABLE [dbo].[Movie] DROP CONSTRAINT [CK__Movie__MovieType__160F4887]
GO
ALTER TABLE [dbo].[EmployeePhone] DROP CONSTRAINT [EmpPhonePeriod]
GO
ALTER TABLE [dbo].[CustomerPhone] DROP CONSTRAINT [CustPhonePeriod]
GO
ALTER TABLE [dbo].[Actor] DROP CONSTRAINT [CK__Actor__Gender__1DB06A4F]
GO
ALTER TABLE [dbo].[RentalRecord] DROP CONSTRAINT [FK__RentalRec__Movie__2739D489]
GO
ALTER TABLE [dbo].[RentalRecord] DROP CONSTRAINT [FK__RentalRec__Custo__2645B050]
GO
ALTER TABLE [dbo].[CustomerQueue] DROP CONSTRAINT [FK__CustomerQ__Movie__1AD3FDA4]
GO
ALTER TABLE [dbo].[CustomerQueue] DROP CONSTRAINT [FK__CustomerQ__Custo__19DFD96B]
GO
ALTER TABLE [dbo].[CustomerPhone] DROP CONSTRAINT [FK__CustomerP__Custo__123EB7A3]
GO
ALTER TABLE [dbo].[ActorRate] DROP CONSTRAINT [FK__ActorRate__Renta__29221CFB]
GO
ALTER TABLE [dbo].[ActorRate] DROP CONSTRAINT [FK__ActorRate__Actor__2A164134]
GO
ALTER TABLE [dbo].[ActorAppear] DROP CONSTRAINT [FK__ActorAppe__Movie__208CD6FA]
GO
ALTER TABLE [dbo].[ActorAppear] DROP CONSTRAINT [FK__ActorAppe__Actor__2180FB33]
GO
ALTER TABLE [dbo].[RentalRecord] DROP CONSTRAINT [DF__RentalRec__Check__245D67DE]
GO
ALTER TABLE [dbo].[EmployeePhone] DROP CONSTRAINT [DF__EmployeeP__Start__08B54D69]
GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [DF__Employee__Employ__3B40CD36]
GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [DF__Employee__Employ__3A4CA8FD]
GO
ALTER TABLE [dbo].[CustomerPhone] DROP CONSTRAINT [DF__CustomerP__Start__114A936A]
GO
ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [DF__Customer__Creati__0D7A0286]
GO
/****** Object:  Index [UQ__Customer__EE174589F5445191]    Script Date: 11/24/2025 11:42:54 PM ******/
ALTER TABLE [dbo].[CustomerQueue] DROP CONSTRAINT [UQ__Customer__EE174589F5445191]
GO
/****** Object:  Table [dbo].[RentalRecord]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RentalRecord]') AND type in (N'U'))
DROP TABLE [dbo].[RentalRecord]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movie]') AND type in (N'U'))
DROP TABLE [dbo].[Movie]
GO
/****** Object:  Table [dbo].[EmployeePhone]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeePhone]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeePhone]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[CustomerQueue]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerQueue]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerQueue]
GO
/****** Object:  Table [dbo].[CustomerPhone]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerPhone]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerPhone]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO
/****** Object:  Table [dbo].[ActorRate]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActorRate]') AND type in (N'U'))
DROP TABLE [dbo].[ActorRate]
GO
/****** Object:  Table [dbo].[ActorAppear]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActorAppear]') AND type in (N'U'))
DROP TABLE [dbo].[ActorAppear]
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 11/24/2025 11:42:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actor]') AND type in (N'U'))
DROP TABLE [dbo].[Actor]
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actor](
	[ActorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[DateOfBrith] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActorAppear]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorAppear](
	[MovieID] [int] NOT NULL,
	[ActorID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC,
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActorRate]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActorRate](
	[RentalRecordID] [int] NOT NULL,
	[ActorID] [int] NOT NULL,
	[ActorRate] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] NOT NULL,
	[LastName] [varchar](40) NOT NULL,
	[FirstName] [varchar](40) NOT NULL,
	[Address] [varchar](40) NULL,
	[City] [varchar](40) NULL,
	[Province] [nchar](2) NULL,
	[PostalCode] [nchar](6) NULL,
	[Email] [varchar](40) NOT NULL,
	[AccountNum] [nchar](10) NOT NULL,
	[CreditCardNum] [nchar](16) NOT NULL,
	[CreditCardExp] [nchar](4) NOT NULL,
	[CreditCardCvv] [nchar](3) NOT NULL,
	[CreationDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPhone]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPhone](
	[CustomerID] [int] NOT NULL,
	[PhoneNum] [nchar](10) NOT NULL,
	[PhoneType] [varchar](10) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[PhoneNum] ASC,
	[StartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerQueue]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerQueue](
	[CustomerID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
	[SortNum] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[OldEmployeeID] [int] NOT NULL,
	[SSN] [nchar](9) NOT NULL,
	[LastName] [varchar](40) NOT NULL,
	[FirstName] [varchar](40) NOT NULL,
	[Address] [varchar](40) NULL,
	[City] [varchar](40) NULL,
	[Province] [nchar](2) NULL,
	[PostalCode] [nchar](6) NULL,
	[StartDate] [date] NOT NULL,
	[EmployeeUsername] [varchar](40) NOT NULL,
	[EmployeePassword] [varchar](40) NOT NULL,
	[EmployeeID] [int] IDENTITY(2004,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePhone]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePhone](
	[EmployeeID] [int] NOT NULL,
	[PhoneNum] [nchar](10) NOT NULL,
	[PhoneType] [varchar](10) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[PhoneNum] ASC,
	[StartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[MovieName] [varchar](40) NOT NULL,
	[MovieType] [varchar](10) NOT NULL,
	[Fee] [numeric](6, 2) NOT NULL,
	[NumOfCopy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalRecord]    Script Date: 11/24/2025 11:42:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalRecord](
	[RentalRecordID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
	[CheckoutTime] [datetime] NOT NULL,
	[ReturnTime] [datetime] NULL,
	[MovieRate] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RentalRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Actor] ON 
GO
INSERT [dbo].[Actor] ([ActorID], [Name], [Gender], [DateOfBrith]) VALUES (2, N'Bruce Wills', N'M', CAST(N'1955-03-19' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Actor] OFF
GO
INSERT [dbo].[ActorAppear] ([MovieID], [ActorID]) VALUES (4, 2)
GO
INSERT [dbo].[ActorAppear] ([MovieID], [ActorID]) VALUES (5, 2)
GO
INSERT [dbo].[ActorAppear] ([MovieID], [ActorID]) VALUES (6, 2)
GO
INSERT [dbo].[Customer] ([CustomerID], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [Email], [AccountNum], [CreditCardNum], [CreditCardExp], [CreditCardCvv], [CreationDate]) VALUES (1012, N'mohammed', N'mohammad', N'N/A', N'N/A', N'AB', N'T5e4e3', N'moh@gmail.com', N'ACC123    ', N'0000111122223333', N'1225', N'123', CAST(N'2025-11-13' AS Date))
GO
INSERT [dbo].[Customer] ([CustomerID], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [Email], [AccountNum], [CreditCardNum], [CreditCardExp], [CreditCardCvv], [CreationDate]) VALUES (1014, N'nuhaali', N'nuhaali', N'N/A', N'N/A', N'AB', N'A1A1A1', N'nuhaa@gmail.com', N'ACC123    ', N'0000111122223333', N'1225', N'123', CAST(N'2025-11-13' AS Date))
GO
INSERT [dbo].[Customer] ([CustomerID], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [Email], [AccountNum], [CreditCardNum], [CreditCardExp], [CreditCardCvv], [CreationDate]) VALUES (1015, N'ridhi', N'ridhi', N'N/A', N'N/A', N'AB', N'A1A1A1', N'ridhi@gmail.com', N'ACC123    ', N'0000111122223333', N'1225', N'123', CAST(N'2025-11-14' AS Date))
GO
INSERT [dbo].[Customer] ([CustomerID], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [Email], [AccountNum], [CreditCardNum], [CreditCardExp], [CreditCardCvv], [CreationDate]) VALUES (1017, N'aldery', N'abod', N'N/A', N'N/A', N'AB', N'A1A1A1', N'abod aldery@gmail.com', N'ACC123    ', N'0000111122223333', N'1225', N'123', CAST(N'2025-11-19' AS Date))
GO
INSERT [dbo].[Customer] ([CustomerID], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [Email], [AccountNum], [CreditCardNum], [CreditCardExp], [CreditCardCvv], [CreationDate]) VALUES (1018, N'doe', N'john', N'N/A', N'N/A', N'AB', N'A1A1A1', N'john', N'ACC123    ', N'0000111122223333', N'1225', N'123', CAST(N'2025-11-22' AS Date))
GO
INSERT [dbo].[Customer] ([CustomerID], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [Email], [AccountNum], [CreditCardNum], [CreditCardExp], [CreditCardCvv], [CreationDate]) VALUES (1019, N'zuhair', N'abod', N'N/A', N'N/A', N'AB', N'A1A1A1', N'abdul@gmail.com', N'ACC123    ', N'0000111122223333', N'1225', N'123', CAST(N'2025-11-22' AS Date))
GO
INSERT [dbo].[CustomerPhone] ([CustomerID], [PhoneNum], [PhoneType], [StartTime], [EndTime]) VALUES (1012, N'7809019098', N'Mobile', CAST(N'2025-11-20T18:30:34.597' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerPhone] ([CustomerID], [PhoneNum], [PhoneType], [StartTime], [EndTime]) VALUES (1012, N'7890987654', N'Mobile', CAST(N'2025-11-20T18:30:43.050' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerPhone] ([CustomerID], [PhoneNum], [PhoneType], [StartTime], [EndTime]) VALUES (1017, N'7809019098', N'Mobile', CAST(N'2025-11-19T23:01:05.337' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerPhone] ([CustomerID], [PhoneNum], [PhoneType], [StartTime], [EndTime]) VALUES (1018, N'7809019098', N'Mobile', CAST(N'2025-11-22T14:50:20.393' AS DateTime), NULL)
GO
INSERT [dbo].[CustomerPhone] ([CustomerID], [PhoneNum], [PhoneType], [StartTime], [EndTime]) VALUES (1019, N'7809019095', N'Mobile', CAST(N'2025-11-22T14:52:51.573' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (1001, N'111222333', N'Smith', N'John', NULL, NULL, NULL, NULL, CAST(N'2024-10-29' AS Date), N'admin', N'1234', 2004)
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (1002, N'123456789', N'Smith', N'John', N'123 Main St', N'Edmonton', N'AB', N'A1A1A1', CAST(N'2024-01-01' AS Date), N'jsmith', N'pass123', 2005)
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (1003, N'234567890', N'Doe', N'Jane', N'12 Queen St', N'Edmonton', N'AB', N'A2A2A2', CAST(N'2024-02-01' AS Date), N'jdoe', N'hello123', 2006)
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (1004, N'345678901', N'Ali', N'Ahmed', N'55 Jasper Ave', N'Edmonton', N'AB', N'A3A3A3', CAST(N'2024-03-01' AS Date), N'ahmed', N'mypassword', 2007)
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (2001, N'123456789', N'Smith', N'John', N'123 Main St', N'Edmonton', N'AB', N'A1A1A1', CAST(N'2024-01-01' AS Date), N'jsmith', N'pass123', 2008)
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (2002, N'234567890', N'Doe', N'Jane', N'12 Queen St', N'Edmonton', N'AB', N'A2A2A2', CAST(N'2024-02-01' AS Date), N'jdoe', N'hello123', 2009)
GO
INSERT [dbo].[Employee] ([OldEmployeeID], [SSN], [LastName], [FirstName], [Address], [City], [Province], [PostalCode], [StartDate], [EmployeeUsername], [EmployeePassword], [EmployeeID]) VALUES (2003, N'345678901', N'Ali', N'Ahmed', N'55 Jasper Ave', N'Edmonton', N'AB', N'A3A3A3', CAST(N'2024-03-01' AS Date), N'ahmed', N'mypassword', 2010)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[EmployeePhone] ([EmployeeID], [PhoneNum], [PhoneType], [StartTime], [EndTime]) VALUES (1001, N'7807654321', N'Home', CAST(N'2025-11-12T17:43:43.520' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 
GO
INSERT [dbo].[Movie] ([MovieID], [MovieName], [MovieType], [Fee], [NumOfCopy]) VALUES (4, N'Die Hard', N'Action', CAST(3.50 AS Numeric(6, 2)), 5)
GO
INSERT [dbo].[Movie] ([MovieID], [MovieName], [MovieType], [Fee], [NumOfCopy]) VALUES (5, N'Die Hard 2', N'Action', CAST(4.50 AS Numeric(6, 2)), 4)
GO
INSERT [dbo].[Movie] ([MovieID], [MovieName], [MovieType], [Fee], [NumOfCopy]) VALUES (6, N'Die Hard 3', N'Action', CAST(5.50 AS Numeric(6, 2)), 3)
GO
SET IDENTITY_INSERT [dbo].[Movie] OFF
GO
/****** Object:  Index [UQ__Customer__EE174589F5445191]    Script Date: 11/24/2025 11:42:54 PM ******/
ALTER TABLE [dbo].[CustomerQueue] ADD UNIQUE NONCLUSTERED 
(
	[CustomerID] ASC,
	[SortNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[CustomerPhone] ADD  DEFAULT (getdate()) FOR [StartTime]
GO
ALTER TABLE [dbo].[Employee] ADD  DEFAULT ('tempuser') FOR [EmployeeUsername]
GO
ALTER TABLE [dbo].[Employee] ADD  DEFAULT ('temppass') FOR [EmployeePassword]
GO
ALTER TABLE [dbo].[EmployeePhone] ADD  DEFAULT (getdate()) FOR [StartTime]
GO
ALTER TABLE [dbo].[RentalRecord] ADD  DEFAULT (getdate()) FOR [CheckoutTime]
GO
ALTER TABLE [dbo].[ActorAppear]  WITH CHECK ADD FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actor] ([ActorID])
GO
ALTER TABLE [dbo].[ActorAppear]  WITH CHECK ADD FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[ActorRate]  WITH CHECK ADD FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actor] ([ActorID])
GO
ALTER TABLE [dbo].[ActorRate]  WITH CHECK ADD FOREIGN KEY([RentalRecordID])
REFERENCES [dbo].[RentalRecord] ([RentalRecordID])
GO
ALTER TABLE [dbo].[CustomerPhone]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerQueue]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerQueue]  WITH CHECK ADD FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[RentalRecord]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[RentalRecord]  WITH CHECK ADD FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[Actor]  WITH CHECK ADD CHECK  (([Gender]='M' OR [Gender]='F'))
GO
ALTER TABLE [dbo].[CustomerPhone]  WITH CHECK ADD  CONSTRAINT [CustPhonePeriod] CHECK  (([StartTime]<[EndTime]))
GO
ALTER TABLE [dbo].[CustomerPhone] CHECK CONSTRAINT [CustPhonePeriod]
GO
ALTER TABLE [dbo].[EmployeePhone]  WITH CHECK ADD  CONSTRAINT [EmpPhonePeriod] CHECK  (([StartTime]<[EndTime]))
GO
ALTER TABLE [dbo].[EmployeePhone] CHECK CONSTRAINT [EmpPhonePeriod]
GO
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD CHECK  (([MovieType]='Comedy' OR [MovieType]='Drama' OR [MovieType]='Action' OR [MovieType]='Foreign'))
GO
