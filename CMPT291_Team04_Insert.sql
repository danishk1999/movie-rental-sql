USE CMPT291_MovieRental;
GO


DELETE Rating;
DELETE Movie_Queue;
DELETE [Order];
DELETE Movie_Actor;
DELETE Actor;
DELETE Movie;
DELETE Customer;
DELETE Employee;


-- Employees
INSERT INTO Employee (SSN, LastName, FirstName, StartDate)
    VALUES ('111222333', 'Nguyen', 'Ava',  '2024-01-15');

INSERT INTO Employee (SSN, LastName, FirstName, StartDate)
    VALUES ('222333444', 'Patel',  'Liam', '2024-02-01');


-- Customers
INSERT INTO Customer (LastName, FirstName, Address, City, [State], ZipCode, Telephone, Email, AccountNumber, CreditCardNumber)
    VALUES ('Doe',  'John', '10 100 St', 'Edmonton', 'AB', 'T5J 1K6', '587-999-9999', 'john@example.com', 'ACCT-0001', '4111111111111111');

INSERT INTO Customer (LastName, FirstName, Address, City, [State], ZipCode, Telephone, Email, AccountNumber, CreditCardNumber)
    VALUES ('Doe', 'Jane', '20 101 St', 'Edmonton', 'AB', 'T5J 2K7', '780-999-9999', 'jane@example.com', 'ACCT-0002', '5555555555554444');


-- Actors
INSERT INTO Actor ([Name], Gender, Age) VALUES ('Chris Adams', 'M', 30);
INSERT INTO Actor ([Name], Gender, Age) VALUES ('Priya Singh', 'F', 33);
INSERT INTO Actor ([Name], Gender, Age) VALUES ('Marco Rossi', 'M', 40);
INSERT INTO Actor ([Name], Gender, Age) VALUES ('Yuki Tanaka', 'F', 25);


-- Movies (uses the sequence for MovieID)
INSERT INTO Movie (MovieID, MovieName, MovieType, Fee, NumOfCopy)
    VALUES (NEXT VALUE FOR Movie_MovieID_Seq, 'The Great Escape', 'Action', 19.99, 5);

INSERT INTO Movie (MovieID, MovieName, MovieType, Fee, NumOfCopy)
    VALUES (NEXT VALUE FOR Movie_MovieID_Seq, 'Laugh Out Loud',   'Comedy', 14.99, 3);

INSERT INTO Movie (MovieID, MovieName, MovieType, Fee, NumOfCopy)
    VALUES (NEXT VALUE FOR Movie_MovieID_Seq, 'Deep Feelings',    'Drama',  16.99, 4);

INSERT INTO Movie (MovieID, MovieName, MovieType, Fee, NumOfCopy)
    VALUES (NEXT VALUE FOR Movie_MovieID_Seq, 'Edge of World',    'Foreign', 12.00, 2);

/*
SELECT * FROM sys.sequences WHERE name = 'Movie_MovieID_Seq';
INSERT INTO Movie (MovieID, MovieName, MovieType, Fee, NumOfCopy)
   VALUES (NEXT VALUE FOR Movie_MovieID_Seq, 'Another Movie', 'Action', 9.99, 1);
*/

-- Movie_Actor casting (bridge M:N)
INSERT INTO Movie_Actor (MovieID, ActorID, RoleName)
SELECT m.MovieID, a.ActorID, 'Lead'
FROM Movie m
JOIN Actor a ON
    (m.MovieName = 'The Great Escape' AND a.[Name] IN ('Chris Adams', 'Priya Singh')) OR
    (m.MovieName = 'Laugh Out Loud'   AND a.[Name]     = 'Priya Singh')              OR
    (m.MovieName = 'Deep Feelings'    AND a.[Name] IN ('Marco Rossi', 'Yuki Tanaka')) OR
    (m.MovieName = 'Edge of World'    AND a.[Name]     = 'Yuki Tanaka');


-- Orders (one returned, one still out)
INSERT INTO [Order] (MovieID, CustomerID, EmployeeID, CheckoutDateTime, ReturnDateTime)
    VALUES (
        (SELECT MovieID    FROM Movie    WHERE MovieName = 'The Great Escape'),
        (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0001'),
        (SELECT TOP 1 EmployeeID FROM Employee ORDER BY EmployeeID),
        '2025-10-10 10:00', '2025-10-12 22:15'
    );

INSERT INTO [Order] (MovieID, CustomerID, EmployeeID, CheckoutDateTime, ReturnDateTime)
    VALUES (
        (SELECT MovieID    FROM Movie    WHERE MovieName = 'Laugh Out Loud'),
        (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0002'),
        (SELECT TOP 1 EmployeeID FROM Employee ORDER BY EmployeeID DESC),
        '2025-10-11 09:30', NULL
    );


-- Movie_Queue (per customer, ordered)
INSERT INTO Movie_Queue (CustomerID, MovieID, PositionInQueue)
    VALUES (
        (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0001'),
        (SELECT MovieID    FROM Movie    WHERE MovieName = 'The Great Escape'),
        1
    );

INSERT INTO Movie_Queue (CustomerID, MovieID, PositionInQueue)
    VALUES (
        (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0001'),
        (SELECT MovieID    FROM Movie    WHERE MovieName = 'Deep Feelings'),
        2
    );

INSERT INTO Movie_Queue (CustomerID, MovieID, PositionInQueue)
    VALUES (
        (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0002'),
        (SELECT MovieID    FROM Movie    WHERE MovieName = 'Laugh Out Loud'),
        1
    );

INSERT INTO Movie_Queue (CustomerID, MovieID, PositionInQueue)
    VALUES (
        (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0002'),
        (SELECT MovieID    FROM Movie    WHERE MovieName = 'Edge of World'),
        2
    );


-- Ratings (movie and actor)
INSERT INTO Rating (CustomerID, MovieID, ActorID, RatingValue)
    VALUES ( (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0001'),
             (SELECT MovieID    FROM Movie    WHERE MovieName = 'The Great Escape'),
             NULL, 5 );

INSERT INTO Rating (CustomerID, MovieID, ActorID, RatingValue)
    VALUES ( (SELECT CustomerID FROM Customer WHERE AccountNumber = 'ACCT-0002'),
             NULL,
             (SELECT ActorID FROM Actor WHERE [Name] = 'Priya Singh'),
             4 );

/*
test case 



SELECT 'Employees', COUNT(*) FROM Employee
UNION ALL SELECT 'Customers', COUNT(*) FROM Customer
UNION ALL SELECT 'Movies', COUNT(*) FROM Movie
UNION ALL SELECT 'Actors', COUNT(*) FROM Actor
UNION ALL SELECT 'Movie_Actor', COUNT(*) FROM Movie_Actor
UNION ALL SELECT 'Orders', COUNT(*) FROM [Order]
UNION ALL SELECT 'Movie_Queue', COUNT(*) FROM Movie_Queue
UNION ALL SELECT 'Rating', COUNT(*) FROM Rating;

SELECT o.OrderID, m.MovieName, c.FirstName + ' ' + c.LastName AS Customer,
    e.FirstName + ' ' + e.LastName AS Employee,
    o.CheckoutDateTime, o.ReturnDateTime
FROM [Order] o
JOIN Movie    m ON m.MovieID = o.MovieID
JOIN Customer c ON c.CustomerID = o.CustomerID
JOIN Employee e ON e.EmployeeID = o.EmployeeID
ORDER BY o.OrderID;

*/