-- ������� �������, ������� �� ����� ������ � ������� ���������:
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Purchases;
DROP TABLE IF EXISTS Goods;
DROP TABLE IF EXISTS TypeProduct;
DROP TABLE IF EXISTS Receipts;
-- ������� ������� Stock, ������� ������� � �������� Employees:
DROP TABLE IF EXISTS Stock;
-- ������� ������� Employees:
DROP TABLE IF EXISTS Employees;
-- ������� �������, ������� �� ����� ������ � ������� ���������:
DROP TABLE IF EXISTS JobTitle;
DROP TABLE IF EXISTS Suppliers;
DROP TABLE IF EXISTS Clients;

-- �������
CREATE TABLE Clients (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- ���
adress VARCHAR(255) NOT NULL, -- �����
number VARCHAR(50) NOT NULL -- ���������� �����
);

-- ����������
CREATE TABLE Suppliers (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- ���
adress VARCHAR(255) NOT NULL, -- �����
number VARCHAR(50) NOT NULL -- ���������� �����
);

-- ��� ���������
CREATE TABLE JobTitle (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL -- ��������
);

-- ����������
CREATE TABLE Employees (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- ���
jobTitle_id INT NOT NULL, -- ���������
number VARCHAR(50) NOT NULL, -- ���������� �����
date DATETIME DEFAULT GETDATE(), -- ���� ����� �� ������
FOREIGN KEY (jobTitle_id) REFERENCES JobTitle(id) ON DELETE CASCADE
);

-- �����
CREATE TABLE Stock (
id INT IDENTITY(0,1) PRIMARY KEY,
employee_id INT NOT NULL, -- �������������
warehouse�apacity INT NOT NULL, -- ����������� ������
location VARCHAR(255) NOT NULL, -- ����� ���������
FOREIGN KEY (employee_id) REFERENCES Employees(id) ON DELETE CASCADE
);

-- ���������
CREATE TABLE Receipts (
id INT IDENTITY(0,1) PRIMARY KEY,
supplier_id INT NOT NULL, -- ���������
stock_id INT NOT NULL, -- �����
count INT NOT NULL, -- ����������
date DATETIME DEFAULT GETDATE(), -- ����
FOREIGN KEY (supplier_id) REFERENCES Suppliers(id) ON DELETE CASCADE,
FOREIGN KEY (stock_id) REFERENCES Stock(id) ON DELETE CASCADE
);

-- ��� ������
CREATE TABLE TypeProduct (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL -- ��������
);

-- ������
CREATE TABLE Goods (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- ��������
typeProduct_id INT NOT NULL, -- ���
receipt_id INT NOT NULL, -- ���������
price INT NOT NULL, -- ����
count INT NOT NULL, -- ����������
FOREIGN KEY (typeProduct_id) REFERENCES TypeProduct(id) ON DELETE CASCADE,
FOREIGN KEY (receipt_id) REFERENCES Receipts(id) ON DELETE CASCADE
);

-- �������
CREATE TABLE Purchases (
id INT IDENTITY(0,1) PRIMARY KEY,
employee_id INT NOT NULL, -- ���������
goods VARCHAR(255) DEFAULT '[]' NOT NULL, -- ������
client_id INT NOT NULL, -- ������
count VARCHAR(255) DEFAULT '[]' NOT NULL, -- ����������
total FLOAT NOT NULL, -- ����� � ������
contributed FLOAT NOT NULL, -- �������
change FLOAT NOT NULL, -- �����
date DATETIME DEFAULT GETDATE(), -- ����
FOREIGN KEY (employee_id) REFERENCES Employees(id),
FOREIGN KEY (client_id) REFERENCES Clients(id) ON DELETE CASCADE
);

-- ������������
CREATE TABLE Users (
	id INT IDENTITY(0,1) PRIMARY KEY,
	login VARCHAR(255) NOT NULL, -- �����
	password VARCHAR(255) NOT NULL, -- ������
	employee_id INT NOT NULL, -- ���������
	FOREIGN KEY (employee_id) REFERENCES Employees(id) ON DELETE CASCADE
);




-- ���������� �������: �������
INSERT INTO Clients (name, adress, number) VALUES
	('������ ���� ��������', '������, ��. ������, �.10, ��.5', '+7(800)555-35-35'),
	('������ ���� ��������', '�����-���������, ��. �������, �.13, ��.21', '+7(800)555-33-22'),
	('������� ����� ���������', '������������, ��. ����, �.1, ��.3', '+7(800)555-11-22'),
	('������� ��������� ��������', '���������, ��. �������, �.8, ��.45', '+7(800)555-12-34'),
	('������� ����� ���������', '�����������, ��. ������, �.7, ��.9', '+7(800)555-11-33'),
	('������� ������ ����������', '������-��-����, ��. �������, �.3, ��.11', '+7(800)555-22-44'),
	('�������� ������ ����������', '����, ��. ������, �.2, ��.5', '+7(800)555-33-44'),
	('������������ ��������� ��������', '������, ��. ������������, �.10, ��.7', '+7(800)555-22-11'),
	('�������� ����� ���������', '���, ��. ������, �.7, ��.33', '+7(800)555-55-55'),
	('�������� ����� ������������', '���������, ��. ���������, �.11, ��.23', '+7(800)555-44-44');

-- ���������� �������: ����������
INSERT INTO Suppliers (name, adress, number) VALUES
	('��� "��������"', '������, ��. �������, �.1', '+7(999)000-11-22'),
	('��� "��������� ��������"', '�����-���������, ��-� ������, �.2', '7(999)000-22-33'),
	('��� "����������"', '������, ��. �����������, �.3', '+7(999)000-33-44'),
	('��� "������"', '����, ��. ������� ��������, �.4', '+7(999)000-44-55'),
	('��� "��������� �����"', '������, ��. ���������, �.5', '+7(999)000-55-66'),
	('��� "����������"', '������ ��������, ��. ��������������, �.6', '+7(999)000-66-77');

-- ���������� �������: ��� ���������
INSERT INTO JobTitle (name) VALUES
	('������������� ��������'),
	('������������� ������'),
	('������ ��������');

-- ���������� �������: ����������
INSERT INTO Employees (name, jobTitle_id, number) VALUES
	('���� ������', 0, '+7(999)123-45-67'),
	('���� ������', 1, '+7(999)234-56-78'),
	('������ �������', 2, '+7(999)345-67-89');

-- ���������� �������: �����
INSERT INTO Stock (employee_id, warehouse�apacity, location) VALUES
	(1, 1000, '������, ��. ������, �.10'),
	(1, 500, '�����-���������, ��-� ���������, �.20'),
	(1, 800, '������������, ��. ���������, �.30'),
	(1, 1200, '�����������, ��. ������� ��������, �.40'),
	(1, 600, '����������, ��. �������, �.50'),
	(1, 900, '�����������, ��. �������, �.60');

-- ���������� �������: ���������
INSERT INTO Receipts (supplier_id, stock_id, count) VALUES
	(0, 0, 100),
	(1, 1, 50),
	(2, 2, 80),
	(3, 3, 120),
	(4, 4, 60),
	(5, 5, 90);

-- ���������� �������: ��� ������
INSERT INTO TypeProduct (name) VALUES
	('��������'),
	('��������'),
	('������'),
	('�������'),
	('���'),
	('����������');

-- ���������� �������: ������
INSERT INTO Goods (name, typeProduct_id, receipt_id, price, count) VALUES
	('�������� Glock 17 Gen 5', 0, 0, 45000, 10),
	('�������� CZ 75 Shadow 2', 0, 0, 55000, 5),
	('�������� Sako TRG 22', 0, 0, 180000, 2),
	('�������� Remington 700', 0, 0, 90000, 5),
	('������ PKM', 2, 2, 250000, 1),
	('������� F1', 3, 3, 15000, 20),
	('��� Cold Steel Recon 1', 4, 4, 5000, 10),
	('���������� ������ Vortex Viper PST Gen II', 5, 0, 60000, 5),
	('���������� ������ Schmidt & Bender PM II', 5, 1, 150000, 2),
	('����������� ������� Olight Warrior X Pro', 5, 2, 12000, 15);

-- ���������� �������: �������
INSERT INTO Purchases (employee_id, goods, client_id, count, total, contributed, change) VALUES
	(0, '[0,1]', 0, '[1,1]', 100.00, 1000.00, 900.00);

-- ���������� ������� �������������
INSERT INTO Users (login, password, employee_id) VALUES
	('Admin', '12345', 0),
	('Stock', '12345', 1),
	('Casir', '12345', 2);