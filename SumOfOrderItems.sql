/****** Script for SelectTopNRows command from SSMS  ******/
SELECT ORD.OrderId,ORD.OrderCustomerId, ORD.OrderDate, SUM(OI.[Count]) AS SumCount, 
SUM(OI.[Count] * IT.ItemPrice) as Total
 FROM Orders AS ORD
 inner join OrdersItems AS OI
 ON ORD.OrderId = OI.OrderId
 inner join Items as IT
 ON OI.ItemId = IT.ItemId
 Group by ORD.OrderId,ORD.OrderCustomerId, ORD.OrderDate
 HAVING SUM(OI.[Count] * IT.ItemPrice) > 10000

 SELECT ORD.OrderId, ORD.OrderCustomerId, ORD.OrderDate, OI.[Count], IT.ItemPrice
 FROM Orders AS ORD
 inner join OrdersItems AS OI
 ON ORD.OrderId = OI.OrderId
 inner join Items as IT
 ON OI.ItemId = IT.ItemId
 and ORD.OrderId =1




 