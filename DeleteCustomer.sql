Delete From Customers where CustomerId in
(Select OrderCustomerId from Orders where OrderId = 1)