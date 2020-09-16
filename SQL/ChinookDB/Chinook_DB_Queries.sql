-- 1. List all customers (full names, customer ID, and country) who are not in the US
select FirstName + ' ' + LastName
as "Full Name" , CustomerId, Country
from Customer
where Country!='USA';

-- 2. List all customers from brazil
select *
from Customer
where Country='Brazil';

-- 3. List all sales agents
select *
from Employee
where Title='Sales Support Agent';

-- 4. Show a list of all countries in billing addresses on invoices.
select BillingCountry, BillingAddress
from Invoice;

-- 5. How many invoices were there in 2009, and what was the sales total for that year?
select SUM(Total) as 'Sales Total',
COUNT(InvoiceId) as 'Invoices in 2009'
from Invoice
where YEAR(InvoiceDate) = 2009;

-- 6. How many line items were there for invoice #37?
select sum(Quantity) as 'Line Items for Invoice #37'
from InvoiceLine
where InvoiceId=37;


-- 7. How many invoices per country?
select * from Invoice;

select BillingCountry, count(*) as 'Number of Invoices' from Invoice
group by BillingCountry;


-- 8. show total sales per country, ordered by highest sales first.
select SUM(Total) as 'Total Sales', BillingCountry
from Invoice
group by BillingCountry
order by SUM(Total) DESC;