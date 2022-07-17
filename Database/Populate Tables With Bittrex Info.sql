USE [Crypto]
GO

DECLARE @TradingPlatformID INT
DECLARE @TradingPlatformInterfaceID INT

INSERT INTO [dbo].[TradingPlatform]
           ([TradingPlatformName])
     VALUES
           ('Bittrex')

SET @TradingPlatformID = SCOPE_IDENTITY();

INSERT INTO [dbo].[TradingPlatformInterface]
           ([TradingPlatformID]
           ,[APIKey]
		   ,[APISecret]
           ,[APIBaseURL])
     VALUES
           (@TradingPlatformID
           ,'INSERT YOUR API KEY HERE'
		   ,'INSERT YOUR API SECRET HERE'
           ,'https://api.bittrex.com/v3/')

SET @TradingPlatformInterfaceID = SCOPE_IDENTITY();

INSERT INTO [dbo].[TradingPlatformAPIEndpoint]
           ([TradingPlatformInterfaceID]
           ,[Name]
           ,[URL]
           ,[Description])
     VALUES
           (@TradingPlatformInterfaceID
           ,'Authentication'
           ,'/Authentication'
           ,'Grants token for subsequent API calls')




