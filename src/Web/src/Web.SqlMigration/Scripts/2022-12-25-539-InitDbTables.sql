IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_Countries')
BEGIN
    CREATE TABLE tbl_Countries
    (
        Id INT IDENTITY(1, 1) PRIMARY KEY,
        Caption varchar(20) NOT NULL,
        Description varchar(100) NULL,
    )
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_Cities')
BEGIN
    CREATE TABLE tbl_Cities
    (
        Id INT IDENTITY(1, 1) PRIMARY KEY,
        CountryId INT FOREIGN KEY REFERENCES tbl_Countries(Id),
        Caption varchar(20) NOT NULL,
    )
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_Drivers')
BEGIN
    CREATE TABLE tbl_Drivers
    (
        Id INT IDENTITY(1, 1) PRIMARY KEY,
        FirstName varchar(20) NOT NULL,
        Surname varchar(30) NOT NULL,
        Patronymic varchar(30) NOT NULL,
        BirthDate DATETIME NULL
    )
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_Transports')
BEGIN
    CREATE TABLE tbl_Transports
    (
        Id INT IDENTITY(1, 1) PRIMARY KEY,
        Mark varchar(20) NOT NULL,
        CarNumber varchar(10) NOT NULL,
    )
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_Passengers')
BEGIN
    CREATE TABLE tbl_Passengers
    (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        FirstName varchar(20) NOT NULL,
        Surname varchar(30) NOT NULL,
        Patronymic varchar(30) NOT NULL,
        PhoneNumber varchar(15) NOT NULL,
    )
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_Rides')
BEGIN
    CREATE TABLE tbl_Rides
    (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        StartCityId INT FOREIGN KEY REFERENCES tbl_Cities(Id),
        FinishCityId INT FOREIGN KEY REFERENCES tbl_Cities(Id),
        StartDate DATETIME NOT NULL,
        FinishDate DATETIME NULL,
        DriverId INT FOREIGN KEY REFERENCES tbl_Drivers(Id),
        TransportId INT FOREIGN KEY REFERENCES tbl_Transports(Id),
    )
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tbl_PassengerRides')
BEGIN
    CREATE TABLE tbl_PassengerRides
    (
        PassengerId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES tbl_Passengers(Id),
        RideId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES tbl_Rides(Id),

        CONSTRAINT Pk_Passenger_Ride PRIMARY KEY (PassengerId, RideId)
    )
END