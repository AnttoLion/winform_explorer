USE [master]
GO
/****** Object:  Database [dbo]    Script Date: 6/5/2023 17:46:23 ******/
CREATE DATABASE [dbo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\dbo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\dbo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [dbo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbo] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbo] SET  MULTI_USER 
GO
ALTER DATABASE [dbo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbo', N'ON'
GO
ALTER DATABASE [dbo] SET QUERY_STORE = ON
GO
ALTER DATABASE [dbo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [dbo]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[id] [int] NOT NULL,
	[active] [bit] NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[permissionOrders] [bit] NULL,
	[permissionInventory] [bit] NULL,
	[permissionReceivables] [bit] NULL,
	[permissionUsers] [bit] NULL,
	[permissionQuickbooks] [bit] NULL,
	[createdAt] [datetime] NULL,
	[createdBy] [int] NULL,
	[updatedAt] [datetime] NULL,
	[updatedBy] [int] NULL,
 CONSTRAINT [PK_Accouts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[active] [bit] NULL,
	[categoryName] [varchar](50) NOT NULL,
	[calculateAs] [int] NOT NULL,
	[createdAt] [datetime] NULL,
	[createdBy] [int] NOT NULL,
	[updatedAt] [datetime] NULL,
	[updatedBy] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditCodes]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditCodes](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerCreditCards]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerCreditCards](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPriceLevels]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPriceLevels](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[active] [int] NULL,
	[customerNumber] [varchar](3) NULL,
	[customerName] [varchar](50) NULL,
	[address1] [varchar](250) NULL,
	[address2] [varchar](250) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](2) NULL,
	[zipcode] [varchar](10) NULL,
	[businessPhone] [varchar](10) NULL,
	[businessPhoneExtension] [varchar](5) NULL,
	[fax] [varchar](10) NULL,
	[homePhone] [varchar](10) NULL,
	[email] [varchar](50) NULL,
	[dateOpened] [date] NOT NULL,
	[salesman] [varchar](50) NULL,
	[resale] [bit] NULL,
	[taxable] [bit] NULL,
	[sendStatements] [bit] NULL,
	[statementCustomerNumber] [varchar](50) NULL,
	[statementName] [varchar](50) NULL,
	[priceTierId] [int] NULL,
	[terms] [varchar](50) NULL,
	[limit] [varchar](50) NULL,
	[coreTracking] [varchar](50) NULL,
	[coreBalance] [decimal](18, 4) NULL,
	[priceCoreTotal] [bit] NULL,
	[accountType] [varchar](50) NULL,
	[poRequired] [bit] NULL,
	[creditCodeId] [int] NULL,
	[interestRate] [decimal](18, 4) NULL,
	[accountBalance] [decimal](16, 2) NULL,
	[yearToDatePurchases] [int] NULL,
	[yearToDateInterest] [decimal](16, 2) NULL,
	[dateLastPurchased] [date] NULL,
	[memo] [varchar](max) NULL,
	[archived] [bit] NULL,
	[createdAt] [datetime] NULL,
	[createdBy] [int] NOT NULL,
	[updatedAt] [datetime] NULL,
	[updatedBy] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerShipTos]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerShipTos](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[id] [int] NOT NULL,
	[active] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceTiers]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceTiers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[active] [bit] NULL,
	[name] [varchar](50) NULL,
	[profitMargin] [decimal](18, 4) NULL,
	[priceTierCode] [varchar](50) NULL,
	[createdAt] [datetime] NULL,
	[createdBy] [int] NOT NULL,
	[updatedAt] [datetime] NULL,
	[updatedBy] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalePrices]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalePrices](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesCostCores]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesCostCores](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKU]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKU](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[active] [bit] NULL,
	[sku] [varchar](15) NOT NULL,
	[category] [int] NOT NULL,
	[description] [varchar](25) NULL,
	[measurementUnit] [varchar](2) NULL,
	[weight] [decimal](18, 4) NULL,
	[costCode] [int] NOT NULL,
	[assetAccount] [int] NOT NULL,
	[taxable] [bit] NOT NULL,
	[manageStock] [bit] NOT NULL,
	[allowDiscounts] [bit] NOT NULL,
	[lastSold] [datetime] NULL,
	[orderFrom] [int] NOT NULL,
	[manufacturer] [int] NULL,
	[location] [varchar](10) NULL,
	[core] [decimal](18, 4) NULL,
	[commodityCode] [varchar](10) NULL,
	[commissionable] [bit] NOT NULL,
	[quantity] [int] NOT NULL,
	[qtyAllocated] [int] NOT NULL,
	[qtyAvailable] [int] NOT NULL,
	[qtyCritical] [int] NOT NULL,
	[qtyReorder] [int] NOT NULL,
	[soldMonthToDate] [int] NOT NULL,
	[soldYearToDate] [int] NOT NULL,
	[coreCost] [decimal](16, 2) NOT NULL,
	[inventoryValue] [decimal](16, 2) NOT NULL,
	[freezePrices] [bit] NOT NULL,
	[memo] [varchar](max) NULL,
	[subassemblyStatus] [bit] NOT NULL,
	[subassemblyPrint] [bit] NOT NULL,
	[archieved] [bit] NULL,
	[createdAt] [datetime] NULL,
	[createdBy] [int] NOT NULL,
	[updatedAt] [datetime] NULL,
	[updatedBy] [int] NOT NULL,
 CONSTRAINT [PK_SKU1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKUCostQtys]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKUCostQtys](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKUCrossReferences]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKUCrossReferences](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKUQuantityDiscounts]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKUQuantityDiscounts](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKUSerialLots]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKUSerialLots](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKUVendorCosts]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKUVendorCosts](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SumAssemblies]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SumAssemblies](
	[id] [int] NOT NULL,
	[bit] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxCodes]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxCodes](
	[id] [int] NOT NULL,
	[active] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 6/5/2023 17:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[active] [bit] NULL,
	[archived] [bit] NULL,
	[vendorName] [varchar](50) NULL,
	[address1] [varchar](250) NULL,
	[address2] [varchar](250) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](2552) NULL,
	[zipcode] [varchar](10) NULL,
	[businessPhone] [varchar](10) NULL,
	[fax] [varchar](10) NULL,
	[createdAt] [datetime] NULL,
	[createdBy] [int] NOT NULL,
	[updatedAt] [datetime] NULL,
	[updatedBy] [int] NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [dbo] SET  READ_WRITE 
GO
