SELECT ProductName, UnitPrice*UnitsInStock AS N'Товаров на складе на сумму' FROM Products WHERE UnitsInStock>0
