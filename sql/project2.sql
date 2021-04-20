USE PROJ2;



CREATE TABLE `categories`(
	`category_ID` char(3) NOT NULL,
	`name` VARCHAR(25) NOT NULL,
	PRIMARY KEY (`category_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE TABLE `statuses`(
	`status_ID` char(2) NOT NULL,
	`name` VARCHAR(20) NOT NULL,
	PRIMARY KEY (`status_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE TABLE `users`(
	`user_ID` VARCHAR(25) NOT NULL,
	`first_name` VARCHAR(15) NOT NULL,
	`last_name` VARCHAR(15) NOT NULL,
	PRIMARY KEY (`user_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE TABLE `moderators`(
	`user_ID` VARCHAR(25) NOT NULL,
	PRIMARY KEY (`user_ID`),
	FOREIGN KEY (`user_ID`) REFERENCES `users` (`user_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `advertisements`(
	`advTitle` VARCHAR(35) NOT NULL,
	`advDetails` VARCHAR(100) NOT NULL,
	`advDateTime` DATETIME NOT NULL,
	`price` DOUBLE NOT NULL,
	`category_ID` char(3) NOT NULL,
	`user_ID` VARCHAR(25) NOT NULL,
	`moderator_ID` VARCHAR(25),
	`status_ID` char(2) NOT NULL,
	PRIMARY KEY (`advTitle`),
	FOREIGN KEY (`user_ID`) REFERENCES `users` (`user_ID`),
	FOREIGN KEY (`moderator_ID`) REFERENCES `moderators` (`user_ID`),
	FOREIGN KEY (`status_ID`) REFERENCES `statuses` (`status_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


INSERT INTO `categories` VALUES ('CAT', 'Cars and Trucks');
INSERT INTO `categories` VALUES ('HOU', 'Housing');
INSERT INTO `categories` VALUES ('ELC', 'Electronics');
INSERT INTO `categories` VALUES ('CCA', 'Child Care');

INSERT INTO `statuses` VALUES ('PN', 'Pending');
INSERT INTO `statuses` VALUES ('AC', 'Active');
INSERT INTO `statuses` VALUES ('DI', 'Disapproved');

INSERT INTO `users` VALUES ('Jsmith', 'John', 'Smith');
INSERT INTO `users` VALUES ('Ajackson', 'Ann', 'Jackson');
INSERT INTO `users` VALUES ('Rkale', 'Rania', 'Kale');
INSERT INTO `users` VALUES ('Sali', 'Samir', 'Ali');

INSERT INTO `moderators` VALUES ('Jsmith');
INSERT INTO `moderators` VALUES ('Ajackson');

INSERT INTO `advertisements` VALUES ('2010 Sedan Subaru', '2010 sedan car in great shape for sale', '2017-02-10', 6000, 'CAT', 'Rkale', 'Jsmith', 'AC');
INSERT INTO `advertisements` VALUES ('Nice Office Desk', 'Nice office desk for sale', '2017-02-15', 50.25, 'HOU', 'Rkale', 'Jsmith', 'AC');
INSERT INTO `advertisements` VALUES ('Smart LG TV for $200 ONLY', 'Smart LG TV 52 inches! Really cheap!', '2017-03-15', 200, 'ELC', 'Sali', 'Jsmith', 'AC');
INSERT INTO `advertisements` VALUES ('HD Tablet for $25 only', 'Amazon Fire Tablet HD', '2017-03-20', 25, 'ELC', 'Rkale', NULL, 'PN');
INSERT INTO `advertisements` VALUES ('Laptop for $100', 'Amazing HP laptop for $100', '2017-03-20', 100, 'ELC', 'Rkale', NULL, 'PN');

