USE [master]
GO
/****** Object:  Database [Simple]    Script Date: 19/02/2016 12:36:52 ******/
CREATE DATABASE [Simple]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Simple', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Simple.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Simple_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Simple_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Simple] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Simple].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Simple] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Simple] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Simple] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Simple] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Simple] SET ARITHABORT OFF 
GO
ALTER DATABASE [Simple] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Simple] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Simple] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Simple] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Simple] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Simple] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Simple] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Simple] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Simple] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Simple] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Simple] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Simple] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Simple] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Simple] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Simple] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Simple] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Simple] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Simple] SET RECOVERY FULL 
GO
ALTER DATABASE [Simple] SET  MULTI_USER 
GO
ALTER DATABASE [Simple] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Simple] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Simple] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Simple] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Simple] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Simple]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 19/02/2016 12:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Company_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[People]    Script Date: 21/05/2016 22:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeReference] [nvarchar](15) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PeopleAddresses]    Script Date: 25/05/2016 18:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeopleAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Location] [int] NOT NULL,
	[Line1] [nvarchar](50) NOT NULL,
	[Line2] [nvarchar](50) NOT NULL,
	[Town] [nvarchar](50) NOT NULL,
	[County] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](10) NOT NULL,
	[Telephone] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_PeopleAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Company] ON 

GO
INSERT [dbo].[Company] ([Id], [Code], [Name], [EmailAddress]) VALUES (1, N'ABC', N'Test Company', N'test@thecompany.com')
GO
INSERT [dbo].[Company] ([Id], [Code], [Name], [EmailAddress]) VALUES (2, N'DEF', N'The Building Company', N'bob@thebuilders.co.uk')
GO
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[People] ON 

GO
INSERT [dbo].[People] ([Id], [EmployeeReference], [Name], [IsActive]) VALUES (1, N'T001', N'Chris', 1)
GO
INSERT [dbo].[People] ([Id], [EmployeeReference], [Name], [IsActive]) VALUES (2, N'T002', N'Bob Martin', 1)
GO
INSERT [dbo].[People] ([Id], [EmployeeReference], [Name], [IsActive]) VALUES (3, N'T003', N'Martin Fowler', 1)
GO
INSERT [dbo].[People] ([Id], [EmployeeReference], [Name], [IsActive]) VALUES (4, N'T004', N'Simon', 0)
GO
INSERT [dbo].[People] ([Id], [EmployeeReference], [Name], [IsActive]) VALUES (5, N'T005', N'Craig', 1)
GO
INSERT [dbo].[People] ([Id], [EmployeeReference], [Name], [IsActive]) VALUES (6, N'T006', N'Mike', 0)
GO
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[PeopleAddresses] ON 

GO
INSERT [dbo].[PeopleAddresses] ([Id], [PersonId], [Location], [Line1], [Line2], [Town], [County], [Postcode], [Telephone]) VALUES (1, 4, 1, N'Booths Hall', N'', N'Knutsford', N'Cheshire', N'WA16 8GS', N'011320')
GO
INSERT [dbo].[PeopleAddresses] ([Id], [PersonId], [Location], [Line1], [Line2], [Town], [County], [Postcode], [Telephone]) VALUES (3, 1, 1, N'34 Parkhill Road', N'', N'Birkenhead', N'Wirral', N'CH45 6EE', N'0151 123 4567')
GO
INSERT [dbo].[PeopleAddresses] ([Id], [PersonId], [Location], [Line1], [Line2], [Town], [County], [Postcode], [Telephone]) VALUES (4, 1, 2, N'4 Booths Park', N'', N'Knutsford', N'Cheshire', N'QA12 5ED', N'01012313')
GO
SET IDENTITY_INSERT [dbo].[PeopleAddresses] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Company_Code]    Script Date: 21/05/2016 22:18:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Company_Code] ON [dbo].[Company]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PeopleAddresses]    Script Date: 25/05/2016 18:55:53 ******/
CREATE NONCLUSTERED INDEX [IX_PeopleAddresses] ON [dbo].[PeopleAddresses]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PeopleAddresses]  WITH CHECK ADD  CONSTRAINT [FK_PeopleAddresses_People] FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PeopleAddresses] CHECK CONSTRAINT [FK_PeopleAddresses_People]
GO
USE [master]
GO
ALTER DATABASE [Simple] SET  READ_WRITE 
GO
