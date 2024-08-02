Create database ShopBanGiay
CREATE TABLE Brands (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    BrandName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName VARCHAR(50) NOT NULL UNIQUE
);
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    proName VARCHAR(100),
	proImage NVARCHAR(200),
    BrandID INT,
    CategoryID INT,
    Size VARCHAR(10),
    Color VARCHAR(20),
    Price DECIMAL(10, 2),
    Stock INT,
    proDescription TEXT,
    FOREIGN KEY (BrandID) REFERENCES Brands(BrandID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);
drop table Customers
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address VARCHAR(100),
    City VARCHAR(50),
    Country VARCHAR(50)
);
drop table Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    OrderDate DATE,
    TotalAmount DECIMAL(10, 2),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    Quantity INT,
    UnitPrice DECIMAL(10, 2),	
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    CustomerID INT,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Insert data into Brands
INSERT INTO Brands (BrandName) VALUES ('Nike');
INSERT INTO Brands (BrandName) VALUES ('Adidas');
INSERT INTO Brands (BrandName) VALUES ('Puma');
INSERT INTO Brands (BrandName) VALUES ('Reebok');

-- Insert data into Categories
INSERT INTO Categories (CategoryName) VALUES ('Running Shoes');
INSERT INTO Categories (CategoryName) VALUES ('Casual Shoes');
INSERT INTO Categories (CategoryName) VALUES ('Formal Shoes');
INSERT INTO Categories (CategoryName) VALUES ('Sports Shoes');

-- Insert data into Products
INSERT INTO Products (Name, BrandID, CategoryID, Size, Color, Price, Stock, Description) VALUES
('Nike Air Max', 1, 1, '10', 'Black', 150.00, 100, 'Comfortable running shoes'),
('Adidas Ultraboost', 2, 1, '9', 'White', 180.00, 80, 'High-performance running shoes'),
('Puma Suede', 3, 2, '8', 'Blue', 120.00, 50, 'Classic casual shoes'),
('Reebok Classic', 4, 2, '11', 'Red', 130.00, 60, 'Retro style casual shoes');

-- Insert data into Customers
INSERT INTO Customers (FirstName, LastName, Email, Phone, Address, City, Country) VALUES
('John', 'Doe', 'john.doe@example.com', '123-456-7890', '123 Main St', 'New York', 'USA'),
('Jane', 'Smith', 'jane.smith@example.com', '098-765-4321', '456 Elm St', 'Los Angeles', 'USA');

-- Insert data into Orders
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES
(1, '2024-01-15', 300.00),
(2, '2024-02-20', 150.00);

-- Insert data into OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) VALUES
(1, 1, 2, 199.00),
(1, 1, 2, 200.00);
(1, 1, 2, 150.00),
(2, 3, 1, 120.00);

-- Insert data into Accounts
INSERT INTO Accounts (Username, Password, Role, CustomerID) VALUES
('johndoe', 'password123', 'Customer', 1),
('janesmith', 'password456', 'Customer', 2);
