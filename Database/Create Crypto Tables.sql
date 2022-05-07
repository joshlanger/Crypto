CREATE DATABASE Crypto

GO

USE Crypto

CREATE TABLE TradingPlatform(
TradingPlatformID INT IDENTITY(1,1) PRIMARY KEY,
TradingPlatformName VARCHAR(100)
);

CREATE TABLE TradingPlatformInterface(
TradingPlatformInterfaceID INT IDENTITY(1,1) PRIMARY KEY,
TradingPlatformID INT NOT NULL FOREIGN KEY REFERENCES TradingPlatform(TradingPlatformID),
APIKey VARCHAR(300),
APIBaseURL VARCHAR(300)
);

CREATE TABLE TradingPlatformAPIEndpoint(
TradingPlatformEndpointID INT IDENTITY(1,1) PRIMARY KEY,
TradingPlatformInterfaceID INT NOT NULL FOREIGN KEY REFERENCES TradingPlatformInterface(TradingPlatformInterfaceID),
Name VARCHAR(50),
URL VARCHAR(300),
Description VARCHAR(500)
);
