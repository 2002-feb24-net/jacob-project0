CREATE SCHEMA P0;
GO
--creating tables
CREATE TABLE P0.Customer
(
	ID INT IDENTITY NOT NULL,
	FirstName NVARCHAR(128) NOT NULL,
	LastName NVARCHAR(128) NOT NULL,
	CONSTRAINT PK_Customer_ID PRIMARY KEY (ID)
)
CREATE TABLE P0.StoreLocation
(
	ID INT IDENTITY NOT NULL,
	LocationName NVARCHAR(128) NOT NULL,
	CONSTRAINT PK_StoreLocation_ID PRIMARY KEY (ID)
)
CREATE TABLE P0.Product
(
	ID INT IDENTITY NOT NULL,
	StoreLocationID INT NOT NULL,
	Name NVARCHAR(128) NOT NULL,
	Price MONEY NOT NULL,
	Stock INT NOT NULL,
	CONSTRAINT PK_Product_ID PRIMARY KEY (ID),
	CONSTRAINT FK_StoreProduct_ID FOREIGN KEY (StoreLocationID) REFERENCES P0.StoreLocation (ID),
)
CREATE TABLE P0.Orders
(
	ID INT IDENTITY NOT NULL,
	StoreLocationID INT NOT NULL,
	CustomerID INT NOT NULL,
	ProductID INT NOT NULL,
	OrderTime TIMESTAMP NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT PK_Orders_ID PRIMARY KEY (ID),
	CONSTRAINT FK_StoreLocation_ID FOREIGN KEY (StoreLocationID) REFERENCES P0.StoreLocation (ID),
	CONSTRAINT FK_Customer_ID FOREIGN KEY (CustomerID) REFERENCES P0.Customer (ID),
	CONSTRAINT FK_Product_ID FOREIGN KEY (ProductID) REFERENCES P0.Product (ID)
)

DROP TABLE P0.Orders;

--doing some test examples
INSERT INTO P0.Customer (FirstName, LastName)
VALUES ('bob','example');

INSERT INTO P0.Customer (FirstName, LastName)
VALUES ('slim','jim');

INSERT INTO P0.StoreLocation (LocationName)
VALUES ('location example');

INSERT INTO P0.StoreLocation (LocationName)
VALUES ('Chilis');

INSERT INTO P0.Product (StoreLocationId, Name, Price, Stock)
VALUES ((SELECT ID FROM P0.StoreLocation WHERE LocationName = 'Chilis'),'Soap',15,50);
INSERT INTO P0.Product (StoreLocationId, Name, Price, Stock)
VALUES ((SELECT ID FROM P0.StoreLocation WHERE LocationName = 'Chilis'),'Salsa',10,11);

INSERT INTO P0.Product (StoreLocationId, Name, Price, Stock)
VALUES ((SELECT ID FROM P0.StoreLocation WHERE LocationName = 'location example'),'test product',10,100);
SELECT * FROM P0.Product;