USE [master]
GO
/****** Object:  Database [Appointments]    Script Date: 2020-05-20 21:05:12 ******/
CREATE DATABASE [Appointments]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Appointments', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Appointments.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Appointments_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Appointments_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Appointments] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Appointments].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Appointments] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Appointments] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Appointments] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Appointments] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Appointments] SET ARITHABORT OFF 
GO
ALTER DATABASE [Appointments] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Appointments] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Appointments] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Appointments] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Appointments] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Appointments] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Appointments] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Appointments] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Appointments] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Appointments] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Appointments] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Appointments] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Appointments] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Appointments] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Appointments] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Appointments] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Appointments] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Appointments] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Appointments] SET RECOVERY FULL 
GO
ALTER DATABASE [Appointments] SET  MULTI_USER 
GO
ALTER DATABASE [Appointments] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Appointments] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Appointments] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Appointments] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Appointments', N'ON'
GO
USE [Appointments]
GO
/****** Object:  Table [dbo].[Adimin]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adimin](
	[Aid] [int] NOT NULL,
	[Aloginname] [nvarchar](10) NOT NULL,
	[Apwd] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Adimin] PRIMARY KEY CLUSTERED 
(
	[Aid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Aid] [int] NOT NULL,
	[Uid] [int] NOT NULL,
	[Mid] [int] NOT NULL,
	[Atime] [datetime] NOT NULL,
	[Ttime] [datetime] NOT NULL,
	[Astate] [int] NOT NULL,
	[Anun] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[Aid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Cid] [int] NOT NULL,
	[Uid] [int] NOT NULL,
	[Mid] [int] NOT NULL,
	[Aid] [int] NOT NULL,
	[Ctime] [datetime] NOT NULL,
	[Comments] [nvarchar](100) NOT NULL,
	[Cstate] [int] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Cid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dept]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dept](
	[Did] [int] NOT NULL,
	[Dname] [nvarchar](20) NOT NULL,
	[Dspec] [nvarchar](300) NOT NULL,
	[Pdid] [int] NULL,
 CONSTRAINT [PK_Dept] PRIMARY KEY CLUSTERED 
(
	[Did] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hospital]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hospital](
	[Hid] [int] NOT NULL,
	[Hname] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Grade] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Hospital] PRIMARY KEY CLUSTERED 
(
	[Hid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mediciner]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mediciner](
	[Mid] [int] NOT NULL,
	[Mloginname] [nvarchar](20) NOT NULL,
	[Mpwd] [nvarchar](10) NOT NULL,
	[Mname] [nvarchar](20) NOT NULL,
	[Gender] [nvarchar](2) NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Mspec] [nvarchar](100) NULL,
	[MtimeA] [datetime] NULL,
	[MtimeB] [datetime] NULL,
	[MtimeC] [datetime] NULL,
	[Mcount] [int] NOT NULL,
	[Hid] [int] NOT NULL,
	[Did] [int] NOT NULL,
 CONSTRAINT [PK_Mediciner] PRIMARY KEY CLUSTERED 
(
	[Mid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[Qid] [int] NOT NULL,
	[Uid] [int] NOT NULL,
	[Mid] [int] NOT NULL,
	[Qtime] [datetime] NOT NULL,
	[Qcontent] [nvarchar](100) NOT NULL,
	[Qanswer] [nvarchar](100) NOT NULL,
	[Qstate] [int] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Qid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2020-05-20 21:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Uid] [int] NOT NULL,
	[Uloginname] [nvarchar](20) NOT NULL,
	[Upwd] [nvarchar](10) NOT NULL,
	[Uname] [nvarchar](20) NOT NULL,
	[Gender] [nvarchar](2) NOT NULL,
	[Uidentity] [nvarchar](20) NOT NULL,
	[Mobile] [nchar](10) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Mediciner] FOREIGN KEY([Mid])
REFERENCES [dbo].[Mediciner] ([Mid])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Mediciner]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_User] FOREIGN KEY([Uid])
REFERENCES [dbo].[User] ([Uid])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_User]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Appointment] FOREIGN KEY([Aid])
REFERENCES [dbo].[Appointment] ([Aid])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Appointment]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Mediciner] FOREIGN KEY([Mid])
REFERENCES [dbo].[Mediciner] ([Mid])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Mediciner]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([Uid])
REFERENCES [dbo].[User] ([Uid])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Dept]  WITH CHECK ADD  CONSTRAINT [FK_Dept_Dept1] FOREIGN KEY([Pdid])
REFERENCES [dbo].[Dept] ([Did])
GO
ALTER TABLE [dbo].[Dept] CHECK CONSTRAINT [FK_Dept_Dept1]
GO
ALTER TABLE [dbo].[Mediciner]  WITH CHECK ADD  CONSTRAINT [FK_Mediciner_Dept] FOREIGN KEY([Did])
REFERENCES [dbo].[Dept] ([Did])
GO
ALTER TABLE [dbo].[Mediciner] CHECK CONSTRAINT [FK_Mediciner_Dept]
GO
ALTER TABLE [dbo].[Mediciner]  WITH CHECK ADD  CONSTRAINT [FK_Mediciner_Hospital] FOREIGN KEY([Hid])
REFERENCES [dbo].[Hospital] ([Hid])
GO
ALTER TABLE [dbo].[Mediciner] CHECK CONSTRAINT [FK_Mediciner_Hospital]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Mediciner] FOREIGN KEY([Mid])
REFERENCES [dbo].[Mediciner] ([Mid])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Mediciner]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_User] FOREIGN KEY([Uid])
REFERENCES [dbo].[User] ([Uid])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_User]
GO
USE [master]
GO
ALTER DATABASE [Appointments] SET  READ_WRITE 
GO
