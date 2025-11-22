USE MovieRentalDb;

select FORMAT(R.CheckoutTime, 'yyyy-MM') as MonthYear,
COUNT(*) as NumberOfRentals,
SUM(M.Fee) as TotalSales
from RentalRecord R
join Movie M on R.MovieID = M.MovieID
group by FORMAT(R.CheckoutTime, 'yyyy-MM')
order by MonthYear;