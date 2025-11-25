USE MovieRentalDb;

-- Monthly earnings by quantity and dollar amount
select year(R.CheckoutTime) as RentalYear, month(R.CheckoutTime) as RentalMonth,
COUNT(*) as NumberOfRentals,
SUM(M.Fee) as TotalSales
from RentalRecord R, Movie M
where R.MovieID = M.MovieID
group by year(R.CheckoutTime), month(R.CheckoutTime)
order by RentalYear, RentalMonth;

-- ranks movies by number of rental records
select M.MovieID, M.MovieName, M.MovieType,
count(RR.RentalRecordID) as TotalRentals
from Movie M
left outer join RentalRecord RR on M.MovieID = RR.MovieID
group by M.MovieID, M.MovieName, M.MovieType
order by TotalRentals DESC; 


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