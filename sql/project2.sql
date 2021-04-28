USE AdvertisementDB_WebApp

/* MAIN DATABASE */
CREATE TABLE categories (
	category_ID char(3) NOT NULL,
	name VARCHAR(25) NOT NULL,
	PRIMARY KEY (category_ID)
);

CREATE TABLE statuses (
	status_ID char(2) NOT NULL,
	name VARCHAR(20) NOT NULL,
	PRIMARY KEY (status_ID)
);

CREATE TABLE users (
	user_ID VARCHAR(25) NOT NULL,
	first_name VARCHAR(15) NOT NULL,
	last_name VARCHAR(15) NOT NULL,
	PRIMARY KEY (user_ID)
);

CREATE TABLE moderators (
	user_ID VARCHAR(25) NOT NULL,
	PRIMARY KEY (user_ID),
	FOREIGN KEY (user_ID) REFERENCES users (user_ID)
);

CREATE TABLE advertisements (
	advertisement_ID INT IDENTITY(1, 1) NOT NULL,
	advTitle VARCHAR(35) NOT NULL,
	advDetails VARCHAR(100) NOT NULL,
	advDateTime DATETIME NOT NULL,
	price DECIMAL(18, 2) NOT NULL,
	category_ID char(3) NOT NULL,
	user_ID VARCHAR(25) NOT NULL,
	moderator_ID VARCHAR(25),
	status_ID char(2) NOT NULL,
	PRIMARY KEY (advertisement_ID),
	FOREIGN KEY (user_ID) REFERENCES users (user_ID),
	FOREIGN KEY (moderator_ID) REFERENCES moderators (user_ID),
	FOREIGN KEY (status_ID) REFERENCES statuses (status_ID)
);

INSERT INTO categories VALUES ('CAT', 'Cars and Trucks');
INSERT INTO categories VALUES ('HOU', 'Housing');
INSERT INTO categories VALUES ('ELC', 'Electronics');
INSERT INTO categories VALUES ('CCA', 'Child Care');

INSERT INTO statuses VALUES ('PN', 'Pending');
INSERT INTO statuses VALUES ('AC', 'Active');
INSERT INTO statuses VALUES ('DI', 'Disapproved');

INSERT INTO users VALUES ('Jsmith', 'John', 'Smith');
INSERT INTO users VALUES ('Ajackson', 'Ann', 'Jackson');
INSERT INTO users VALUES ('Rkale', 'Rania', 'Kale');
INSERT INTO users VALUES ('Sali', 'Samir', 'Ali');

INSERT INTO moderators VALUES ('Jsmith');
INSERT INTO moderators VALUES ('Ajackson');

INSERT INTO advertisements VALUES ('2010 Sedan Subaru', '2010 sedan car in great shape for sale', '2017-02-10', 6000, 'CAT', 'Rkale', 'Jsmith', 'AC');
INSERT INTO advertisements VALUES ('Nice Office Desk', 'Nice office desk for sale', '2017-02-15', 50.25, 'HOU', 'Rkale', 'Jsmith', 'AC');
INSERT INTO advertisements VALUES ('Smart LG TV for $200 ONLY', 'Smart LG TV 52 inches! Really cheap!', '2017-03-15', 200, 'ELC', 'Sali', 'Jsmith', 'AC');
INSERT INTO advertisements VALUES ('HD Tablet for $25 only', 'Amazon Fire Tablet HD', '2017-03-20', 25, 'ELC', 'Rkale', NULL, 'PN');
INSERT INTO advertisements VALUES ('Laptop for $100', 'Amazing HP laptop for $100', '2017-03-20', 100, 'ELC', 'Rkale', NULL, 'PN');


/* ASP DEPENDENCIES */
CREATE TABLE AspNetUsersLink (
	user_ID VARCHAR(25),
	aspUser_ID NVARCHAR(450)
);

CREATE LOGIN AdvertisementCustomer WITH PASSWORD = '!Password123';
CREATE USER AdvertisementCustomer FOR LOGIN AdvertisementCustomer;
GRANT SELECT, UPDATE, INSERT, EXEC TO AdvertisementCustomer;

/* PROCEDURES */
/* Return table consisting of a specific user's advertisements */
CREATE PROCEDURE [dbo].[getUserAdvertisements] @Id NVARCHAR(450)
AS
BEGIN
	SELECT A.advertisement_ID, A.advTitle, A.advDateTime, A.advDetails, A.price, A.category_ID, A.user_ID, A.moderator_ID, status_ID
		FROM AspNetUsers as U
			INNER JOIN AspNetUsersLink AS UL ON U.Id = UL.aspUser_ID
			INNER JOIN advertisements AS A ON UL.user_ID = A.user_ID
		WHERE @Id = U.Id
		ORDER BY A.status_ID, A.advDateTime;
END

/* Add new advertisement given its Title, Details, Price, Cat. ID, and User ID. */
CREATE PROCEDURE addAdvertisement @advTitle VARCHAR(35), @advDetails VARCHAR(100), @price DECIMAL(18, 2), @category_ID CHAR(3), @user_ID VARCHAR(25)
AS
BEGIN
  INSERT INTO advertisements (advTitle, advDetails, advDateTime, price, category_ID, user_ID, moderator_ID, status_ID)
    VALUES (@advTitle, @advDetails, GETDATE(), @price, @category_ID, @user_ID, NULL, 'PN');
END

/* Delete advertisement given its ID */
CREATE PROCEDURE delAdvertisement @advertisement_ID
AS
BEGIN
  DELETE FROM advertisements
    WHERE advertisement_ID = @advertisement_ID
END