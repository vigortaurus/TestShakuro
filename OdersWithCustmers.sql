/****** Script for SelectTopNRows command from SSMS  ******/
SELECT OrderId, OrderCustomerId ,OrderDate FROM [Orders]
where OrderCustomerId in (Select CustomerId from Customers)

  Select * from Customers