
USE [InvoiceManagement]
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 05/05/2020 21:07:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [uniqueidentifier] NOT NULL,
	[Supplier] [nvarchar](100) NULL,
	[DateIssued] [datetime] NOT NULL,
	[Currency] [nvarchar](3) NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


GO

INSERT INTO [dbo].[Invoice]  ([InvoiceId] ,[Supplier]   ,[DateIssued]  ,[Currency]    ,[Amount]  ,[Description])
     VALUES
        (NEWID(), 'suplier1' ,GETDATE()  ,'EUR' ,100  ,'desc tst1'),
		(NEWID(), 'suplier2' ,GETDATE()  ,'EUR' ,200  ,'desc tst2'),
        (NEWID(), 'suplier3' ,GETDATE()  ,'EUR' ,300  ,'desc tst3'),
	    (NEWID(), 'suplier4' ,GETDATE()  ,'EUR' ,400  ,'desc tst4'),
		(NEWID(), 'suplier5' ,GETDATE()  ,'EUR' ,500  ,'desc tst5'),
        (NEWID(), 'suplier6' ,GETDATE()  ,'USD' ,600  ,'desc tst6')
GO