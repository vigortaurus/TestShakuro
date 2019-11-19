/****** Script for SelectTopNRows command from SSMS  ******/
SELECT it.ItemId, it.ItemName, it.ItemPrice, Isnull(oit.[Count],0) as [Count]
FROM Items as it
left join OrdersItems as oit
on it.ItemId = oit.ItemId