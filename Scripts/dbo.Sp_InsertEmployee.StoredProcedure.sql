USE [Report]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertEmployee]    Script Date: 22-05-2024 14:23:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_InsertEmployee] (@name nvarchar(max),@salary int, @dept nvarchar(max)) as 

begin  

INSERT INTO [dbo].[Employee]
           ( [name]
           ,[salary]
           ,[department])
values(  @name ,@salary,@dept);

end
GO
