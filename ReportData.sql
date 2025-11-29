USE Proj2025F;

-- Monthly earnings by quantity and dollar amount
select year(R.CheckoutTime) as RentalYear, month(R.CheckoutTime) as RentalMonth,
COUNT(*) as NumberOfRentals,
SUM(M.Fee) as TotalSales
from RentalRecord R, Movie M
where R.MovieID = M.MovieID
group by year(R.CheckoutTime), month(R.CheckoutTime)
order by RentalYear, RentalMonth;

-- top 3 ranking movies by number of rental records
SELECT TOP 3
    M.MovieName AS MovieTitle,
    COUNT(R.MovieID) AS TimesRented
FROM RentalRecord R
JOIN Movie M ON R.MovieID = M.MovieID
WHERE MONTH(R.CheckoutTime) = @Month
  AND YEAR(R.CheckoutTime) = @Year
GROUP BY M.MovieName
ORDER BY TimesRented DESC;



-- Customers total rentals
select C.CustomerID, C.FirstName, C.LastName,
count(RR.RentalRecordID) as TotalRentals
from Customer C
left outer join RentalRecord RR on C.CustomerID = RR.CustomerID
group by C.CustomerID, C.FirstName, C.LastName
order by TotalRentals DESC;

-- movies currently in queue, encourage to add quantities of certain movies
select CQ.CustomerID, C.FirstName, C.LastName, CQ.MovieID, M.MovieName, CQ.SortNum,
C.CreationDate as QueueOrder
from CustomerQueue CQ, Customer C, Movie M
where CQ.CustomerID = C.CustomerID
	and CQ.MovieID = M.MovieID
order by CQ.CustomerID, CQ.SortNum;