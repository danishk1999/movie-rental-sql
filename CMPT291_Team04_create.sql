/*
   CMPT291_AS4_team04_create.sql
   Movie Rental System CREATE TABLE Script
*/

-- Drop children first, then parents
IF OBJECT_ID('dbo.Rating', 'U')      IS NOT NULL DROP TABLE dbo.Rating;
IF OBJECT_ID('dbo.Movie_Queue', 'U') IS NOT NULL DROP TABLE dbo.Movie_Queue;
IF OBJECT_ID('dbo.[Order]', 'U')     IS NOT NULL DROP TABLE dbo.[Order];
IF OBJECT_ID('dbo.Movie_Actor', 'U') IS NOT NULL DROP TABLE dbo.Movie_Actor;
IF OBJECT_ID('dbo.Actor', 'U')       IS NOT NULL DROP TABLE dbo.Actor;
IF OBJECT_ID('dbo.Movie', 'U')       IS NOT NULL DROP TABLE dbo.Movie;
IF OBJECT_ID('dbo.Customer', 'U')    IS NOT NULL DROP TABLE dbo.Customer;
IF OBJECT_ID('dbo.Employee', 'U')    IS NOT NULL DROP TABLE dbo.Employee;
IF OBJECT_ID('dbo.Movie_MovieID_Seq', 'SO') IS NOT NULL DROP SEQUENCE dbo.Movie_MovieID_Seq;
GO

/*
   Core reference tables
*/

CREATE TABLE Employee (
    EmployeeID INT IDENTITY(1,1),
    SSN        NCHAR(9)     NOT NULL,
    LastName   VARCHAR(40)  NOT NULL,
    FirstName  VARCHAR(40)  NOT NULL,
    StartDate  DATE         NOT NULL DEFAULT (GETDATE()),
    PRIMARY KEY (EmployeeID),
    UNIQUE (SSN)
);

CREATE TABLE Customer (
    CustomerID          INT IDENTITY(1,1),
    LastName            VARCHAR(40)  NOT NULL,
    FirstName           VARCHAR(40)  NOT NULL,
    [Address]             VARCHAR(120),
    City                VARCHAR(60),
    [State]             VARCHAR(40),
    ZipCode             VARCHAR(15),
    Telephone           VARCHAR(30),
    Email               VARCHAR(120),
    AccountNumber       VARCHAR(40)  NOT NULL,
    AccountCreationDate DATE         NOT NULL DEFAULT (CONVERT(date, GETDATE())),
    CreditCardNumber    VARCHAR(25),
    AvgRating           DECIMAL(3,2),
    PRIMARY KEY (CustomerID),
    UNIQUE (AccountNumber)
);

CREATE TABLE Actor (
    ActorID   INT IDENTITY(1,1),
    [Name]    VARCHAR(120) NOT NULL,
    Gender    CHAR(1)      CHECK (Gender IN ('M','F')),
    Age       INT          CHECK (Age IS NULL OR (Age BETWEEN 0 AND 125)),
    AvgRating DECIMAL(3,2),
    PRIMARY KEY (ActorID)
);

CREATE SEQUENCE Movie_MovieID_Seq START WITH 1000 INCREMENT BY 1;

CREATE TABLE Movie (
    MovieID   INT          NOT NULL,
    MovieName VARCHAR(120) NOT NULL,
    MovieType VARCHAR(10)  NOT NULL CHECK (MovieType IN ('Comedy','Drama','Action','Foreign')),
    Fee       NUMERIC(6,2) NOT NULL,
    NumOfCopy INT          NOT NULL CHECK (NumOfCopy >= 0),
    AvgRating DECIMAL(3,2),
    PRIMARY KEY (MovieID)
);

/* 
   Relation / transaction tables
*/

CREATE TABLE Movie_Actor (
    MovieID  INT NOT NULL,
    ActorID  INT NOT NULL,
    RoleName VARCHAR(120),
    PRIMARY KEY (MovieID, ActorID),
    FOREIGN KEY (MovieID) REFERENCES Movie (MovieID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ActorID) REFERENCES Actor (ActorID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [Order] (
    OrderID          INT IDENTITY(1,1),
    MovieID          INT        NOT NULL,
    CustomerID       INT        NOT NULL,
    EmployeeID       INT        NOT NULL,
    CheckoutDateTime DATETIME2  NOT NULL,
    ReturnDateTime   DATETIME2,
    PRIMARY KEY (OrderID),
    FOREIGN KEY (MovieID)    REFERENCES Movie    (MovieID),
    FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee (EmployeeID),
    CONSTRAINT CK_Order_Date CHECK (ReturnDateTime IS NULL OR ReturnDateTime >= CheckoutDateTime)
);

CREATE TABLE Movie_Queue (
    CustomerID       INT        NOT NULL,
    MovieID          INT        NOT NULL,
    PositionInQueue  INT        NOT NULL CHECK (PositionInQueue > 0),
    AddedAt          DATETIME2  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (CustomerID, PositionInQueue),
    UNIQUE (CustomerID, MovieID),
    FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID) ON DELETE CASCADE,
    FOREIGN KEY (MovieID)    REFERENCES Movie    (MovieID)    ON DELETE CASCADE
);

CREATE TABLE Rating (
    RatingID    INT IDENTITY(1,1),
    CustomerID  INT        NOT NULL,
    MovieID     INT,
    ActorID     INT,
    RatingValue TINYINT    NOT NULL CHECK (RatingValue BETWEEN 1 AND 5),
    RatedAt     DATETIME2  NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (RatingID),
    FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID) ON DELETE CASCADE,
    FOREIGN KEY (MovieID)    REFERENCES Movie    (MovieID)    ON DELETE CASCADE,
    FOREIGN KEY (ActorID)    REFERENCES Actor    (ActorID)    ON DELETE CASCADE,
    CONSTRAINT CK_Rating_Target CHECK (
        (MovieID IS NOT NULL AND ActorID IS NULL) OR
        (MovieID IS NULL AND ActorID IS NOT NULL)
    )
);

/* 
   Helper indexes
*/   
CREATE INDEX IX_Order_Customer ON [Order](CustomerID);
CREATE INDEX IX_Order_Movie    ON [Order](MovieID);
CREATE INDEX IX_Order_Employee ON [Order](EmployeeID);
CREATE INDEX IX_Queue_Lookup   ON Movie_Queue(CustomerID, MovieID);
CREATE INDEX IX_Rating_Movie   ON Rating(MovieID) WHERE MovieID IS NOT NULL;
CREATE INDEX IX_Rating_Actor   ON Rating(ActorID) WHERE ActorID IS NOT NULL;


/*
test cases


SELECT name FROM sys.tables ORDER BY name;
SELECT name FROM sys.sequences;
INSERT INTO Movie (MovieID, MovieName, MovieType, Fee, NumOfCopy)
VALUES (NEXT VALUE FOR Movie_MovieID_Seq, 'Test Movie', 'Action', 9.99, 2);

SELECT TOP 5 * FROM Movie ORDER BY MovieID DESC;

*/