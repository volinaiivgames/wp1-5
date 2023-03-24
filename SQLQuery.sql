-- Удалить таблицы, которые не имеют связей с другими таблицами:
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Purchases;
DROP TABLE IF EXISTS Goods;
DROP TABLE IF EXISTS TypeProduct;
DROP TABLE IF EXISTS Receipts;
-- Удалить таблицу Stock, которая связана с таблицей Employees:
DROP TABLE IF EXISTS Stock;
-- Удалить таблицу Employees:
DROP TABLE IF EXISTS Employees;
-- Удалить таблицы, которые не имеют связей с другими таблицами:
DROP TABLE IF EXISTS JobTitle;
DROP TABLE IF EXISTS Suppliers;
DROP TABLE IF EXISTS Clients;

-- Клиенты
CREATE TABLE Clients (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- Имя
adress VARCHAR(255) NOT NULL, -- Адрес
number VARCHAR(50) NOT NULL -- Контактный номер
);

-- Поставщики
CREATE TABLE Suppliers (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- Имя
adress VARCHAR(255) NOT NULL, -- Адрес
number VARCHAR(50) NOT NULL -- Контактный номер
);

-- Тип должности
CREATE TABLE JobTitle (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL -- Название
);

-- Сотрудники
CREATE TABLE Employees (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- Имя
jobTitle_id INT NOT NULL, -- Должность
number VARCHAR(50) NOT NULL, -- Контактный номер
date DATETIME DEFAULT GETDATE(), -- Дата приёма на работу
FOREIGN KEY (jobTitle_id) REFERENCES JobTitle(id) ON DELETE CASCADE
);

-- Склад
CREATE TABLE Stock (
id INT IDENTITY(0,1) PRIMARY KEY,
employee_id INT NOT NULL, -- Ответственный
warehouseСapacity INT NOT NULL, -- Вместимость склада
location VARCHAR(255) NOT NULL, -- Место положение
FOREIGN KEY (employee_id) REFERENCES Employees(id) ON DELETE CASCADE
);

-- Квитанции
CREATE TABLE Receipts (
id INT IDENTITY(0,1) PRIMARY KEY,
supplier_id INT NOT NULL, -- Поставщик
stock_id INT NOT NULL, -- Склад
count INT NOT NULL, -- Количество
date DATETIME DEFAULT GETDATE(), -- Дата
FOREIGN KEY (supplier_id) REFERENCES Suppliers(id) ON DELETE CASCADE,
FOREIGN KEY (stock_id) REFERENCES Stock(id) ON DELETE CASCADE
);

-- Тип товара
CREATE TABLE TypeProduct (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL -- Название
);

-- Товары
CREATE TABLE Goods (
id INT IDENTITY(0,1) PRIMARY KEY,
name VARCHAR(255) NOT NULL, -- Название
typeProduct_id INT NOT NULL, -- Тип
receipt_id INT NOT NULL, -- Квитанция
price INT NOT NULL, -- Цена
count INT NOT NULL, -- Количество
FOREIGN KEY (typeProduct_id) REFERENCES TypeProduct(id) ON DELETE CASCADE,
FOREIGN KEY (receipt_id) REFERENCES Receipts(id) ON DELETE CASCADE
);

-- Покупки
CREATE TABLE Purchases (
id INT IDENTITY(0,1) PRIMARY KEY,
employee_id INT NOT NULL, -- Сотрудник
goods VARCHAR(255) DEFAULT '[]' NOT NULL, -- Товары
client_id INT NOT NULL, -- Клиент
count VARCHAR(255) DEFAULT '[]' NOT NULL, -- Количество
total FLOAT NOT NULL, -- Итого к оплате
contributed FLOAT NOT NULL, -- Внесено
change FLOAT NOT NULL, -- Сдача
date DATETIME DEFAULT GETDATE(), -- Дата
FOREIGN KEY (employee_id) REFERENCES Employees(id),
FOREIGN KEY (client_id) REFERENCES Clients(id) ON DELETE CASCADE
);

-- Пользователи
CREATE TABLE Users (
	id INT IDENTITY(0,1) PRIMARY KEY,
	login VARCHAR(255) NOT NULL, -- Логин
	password VARCHAR(255) NOT NULL, -- Пароль
	employee_id INT NOT NULL, -- сотрудник
	FOREIGN KEY (employee_id) REFERENCES Employees(id) ON DELETE CASCADE
);




-- Заполнение таблицы: Клиенты
INSERT INTO Clients (name, adress, number) VALUES
	('Иванов Иван Иванович', 'Москва, ул. Ленина, д.10, кв.5', '+7(800)555-35-35'),
	('Петров Петр Петрович', 'Санкт-Петербург, ул. Пушкина, д.13, кв.21', '+7(800)555-33-22'),
	('Сидоров Сидор Сидорович', 'Екатеринбург, ул. Мира, д.1, кв.3', '+7(800)555-11-22'),
	('Козлова Анастасия Петровна', 'Краснодар, ул. Красная, д.8, кв.45', '+7(800)555-12-34'),
	('Иванова Мария Сергеевна', 'Новосибирск, ул. Гоголя, д.7, кв.9', '+7(800)555-11-33'),
	('Смирнов Андрей Алексеевич', 'Ростов-на-Дону, ул. Садовая, д.3, кв.11', '+7(800)555-22-44'),
	('Васильев Михаил Викторович', 'Омск, ул. Лесная, д.2, кв.5', '+7(800)555-33-44'),
	('Александрова Екатерина Ивановна', 'Самара, ул. Партизанская, д.10, кв.7', '+7(800)555-22-11'),
	('Кузнецов Павел Сергеевич', 'Уфа, ул. Кирова, д.7, кв.33', '+7(800)555-55-55'),
	('Новикова Алена Владимировна', 'Волгоград, ул. Советская, д.11, кв.23', '+7(800)555-44-44');

-- Заполнение таблицы: Поставщики
INSERT INTO Suppliers (name, adress, number) VALUES
	('ООО "Арматура"', 'Москва, ул. Пушкина, д.1', '+7(999)000-11-22'),
	('ООО "Оружейный комплекс"', 'Санкт-Петербург, пр-т Ленина, д.2', '7(999)000-22-33'),
	('ОАО "Калашников"', 'Ижевск, ул. Калашникова, д.3', '+7(999)000-33-44'),
	('ООО "Буллет"', 'Тула, ул. Максима Горького, д.4', '+7(999)000-44-55'),
	('ООО "Оружейный завод"', 'Ковров, ул. Советская, д.5', '+7(999)000-55-66'),
	('ООО "Стрельбище"', 'Нижний Новгород, ул. Рождественская, д.6', '+7(999)000-66-77');

-- Заполнение таблицы: Тип должности
INSERT INTO JobTitle (name) VALUES
	('Администратор магазина'),
	('Администратор склада'),
	('Кассир магазина');

-- Заполнение таблицы: Сотрудники
INSERT INTO Employees (name, jobTitle_id, number) VALUES
	('Иван Иванов', 0, '+7(999)123-45-67'),
	('Петр Петров', 1, '+7(999)234-56-78'),
	('Сергей Сидоров', 2, '+7(999)345-67-89');

-- Заполнение таблицы: Склад
INSERT INTO Stock (employee_id, warehouseСapacity, location) VALUES
	(1, 1000, 'Москва, ул. Ленина, д.10'),
	(1, 500, 'Санкт-Петербург, пр-т Ветеранов, д.20'),
	(1, 800, 'Екатеринбург, ул. Советская, д.30'),
	(1, 1200, 'Новосибирск, ул. Красный проспект, д.40'),
	(1, 600, 'Красноярск, ул. Красная, д.50'),
	(1, 900, 'Владивосток, ул. Пушкина, д.60');

-- Заполнение таблицы: Квитанции
INSERT INTO Receipts (supplier_id, stock_id, count) VALUES
	(0, 0, 100),
	(1, 1, 50),
	(2, 2, 80),
	(3, 3, 120),
	(4, 4, 60),
	(5, 5, 90);

-- Заполнение таблицы: Тип товара
INSERT INTO TypeProduct (name) VALUES
	('Пистолет'),
	('Винтовка'),
	('Пулемёт'),
	('Граната'),
	('Нож'),
	('Аксессуары');

-- Заполнение таблицы: Товары
INSERT INTO Goods (name, typeProduct_id, receipt_id, price, count) VALUES
	('Пистолет Glock 17 Gen 5', 0, 0, 45000, 10),
	('Пистолет CZ 75 Shadow 2', 0, 0, 55000, 5),
	('Винтовка Sako TRG 22', 0, 0, 180000, 2),
	('Винтовка Remington 700', 0, 0, 90000, 5),
	('Пулемёт PKM', 2, 2, 250000, 1),
	('Граната F1', 3, 3, 15000, 20),
	('Нож Cold Steel Recon 1', 4, 4, 5000, 10),
	('Оптический прицел Vortex Viper PST Gen II', 5, 0, 60000, 5),
	('Оптический прицел Schmidt & Bender PM II', 5, 1, 150000, 2),
	('Тактический фонарик Olight Warrior X Pro', 5, 2, 12000, 15);

-- Заполнение таблицы: Покупки
INSERT INTO Purchases (employee_id, goods, client_id, count, total, contributed, change) VALUES
	(0, '[0,1]', 0, '[1,1]', 100.00, 1000.00, 900.00);

-- Заполнение таблицы пользователей
INSERT INTO Users (login, password, employee_id) VALUES
	('Admin', '12345', 0),
	('Stock', '12345', 1),
	('Casir', '12345', 2);