CREATE DATABASE DevicesDatabase
GO

USE DevicesDatabase
GO

CREATE TABLE dbo.Device(
	Id int NOT NULL IDENTITY(1,1),
	Name varchar(max) NOT NULL,
	Brand varchar(200) NOT NULL,
	DeviceState integer NOT NULL,
	CreationTime Datetime NOT NULL,
	CONSTRAINT PK_DEVICES PRIMARY KEY (Id)
)
GO

