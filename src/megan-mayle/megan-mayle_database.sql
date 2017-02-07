USE [megan-mayle]
GO

/****** Object:  Table [dbo].[BlogPosts]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BlogPosts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Slug] [varchar](50) NULL,
	[Title] [varchar](255) NULL,
	[DatePosted] [datetime] NOT NULL,
	[Excerpt] [nvarchar](max) NULL,
	[ImageUrl] [varchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

