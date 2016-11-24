# DapperDemo

This is a test project to demonstrate the use of Dapper with MySQL

Prerequisites:

- Visual studio (I tested with VS 2013, but ideally anything should work)
- MySQL
- Create a db named cleartaxdb in mysql
- Create a table with following SQL syntax
```CREATE TABLE `dbo.employee` (
   `Id` int(11) NOT NULL,
   `Name` varchar(50) NOT NULL,
   `City` varchar(50) NOT NULL,
   `Address` varchar(500) NOT NULL,
   PRIMARY KEY (`Id`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8```
 - Launch the project in visual studio
