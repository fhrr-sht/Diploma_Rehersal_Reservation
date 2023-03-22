
GO
/****** Object:  StoredProcedure [dbo].[InsertCustomer]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertCustomer] 
@EmailAddress nvarchar(500),
  @FirstName nvarchar(500),
  @LastName nvarchar(500),
  @PhoneNo nvarchar(500),
  @PasswordHash varbinary(max),
  @PasswordSalt varbinary(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[Customer] (
      [EmailAddress]
      ,[FirstName]
      ,[LastName]
      ,[PhoneNo]
      ,[PasswordHash]
      ,[PasswordSalt]
	  )
	  select @EmailAddress
      ,@FirstName
      ,@LastName
      ,@PhoneNo
      ,@PasswordHash
      ,@PasswordSalt
   
END
GO
/****** Object:  StoredProcedure [dbo].[InsertOrder]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertOrder]
      @RehersalSpaceID int,
      @UserID int,
      @OrderNumber nvarchar(50),
      @RehersalRoomID int,
      @DateStart datetime,
      @DateEnd datetime,
      @Price money,
      @Comment nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[Order] ([RehersalSpaceID]
      ,[UserID]
      ,[OrderNumber]
      ,[RehersalRoomID]
      ,[DateStart]
      ,[DateEnd]
      ,[Price]
      ,[Comment])
  SELECT @RehersalSpaceID
      ,@UserID
      ,@OrderNumber
      ,@RehersalRoomID
      ,@DateStart
      ,@DateEnd
      ,@Price
      ,@Comment
END
GO
/****** Object:  StoredProcedure [dbo].[InsertRehersal]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertRehersal]
@RehersalSpaseName nvarchar(50),
@CityID int,
@Adress nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[RehersalSpase] ([RehersalSpaseName], [CityID],[Adress])
  SELECT @RehersalSpaseName, @CityID, @Adress  
END
GO
/****** Object:  StoredProcedure [dbo].[InsertRoom]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertRoom]
@RehersalRoomName nvarchar(50),
@RehersalRoomSize int,
@RehersalSpaseID int,
@ImageURL nvarchar(500),
@PriceHour decimal,
@PathURL nvarchar(500),
@Description nvarchar(max),
@Address nvarchar(500),
@StartProgram nvarchar(500),
@EndProgram nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[Room] ([RehersalRoomName]
      ,[RehersalRoomSize]
      ,[RehersalSpaseID]
      ,[ImageURL]
      ,[PriceHour]
      ,[PathURL]
      ,[Description]
      ,[Address]
      ,[StartProgram]
      ,[EndProgram])
  SELECT @RehersalRoomName, @RehersalRoomSize, @RehersalSpaseID, @ImageURL, @PriceHour, @PathURL, @Description, @Address,
  @StartProgram, @EndProgram
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertUser]
	  @UserType smallint
      ,@UserName nvarchar(50)
      ,@UserMail nvarchar(50)
      ,@UserPhone nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[User] ([UserType], [UserName], [UserMail], [UserPhone], [IsDeleted])
  SELECT  @UserType, @UserName, @UserMail, @UserPhone, 0
END
GO
/****** Object:  StoredProcedure [dbo].[SearchReservations]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchReservations]
      @Title nvarchar(500) 
      
AS
BEGIN
select        
	 r.Id,
	r.Title,
	r.Start,
	r.[End],
	r.RoomId,
	r.Total,
	r.PrimaryColor,
	r.SecondaryColor,
	r.CustomerId
from Reservation r 
where r.Title like N'%'+@Title+'%' 

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCity]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCity] 
@CityID int,
@CityName nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[City]
 SET  [CityName] = @CityName
    
  WHERE [CityID] = @CityID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrder]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateOrder] 
	  @OrderID int,
      @RehersalSpaceID int,
      @UserID int,
      @OrderNumber nvarchar(50),
      @RehersalRoomID int,
      @DateStart datetime,
      @DateEnd datetime,
      @Price money,
      @Comment nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[Order]
 SET [RehersalSpaceID] =@RehersalSpaceID
      ,[UserID] =@UserID
      ,[OrderNumber] =@OrderNumber
      ,[RehersalRoomID] =@RehersalRoomID
      ,[DateStart] =@DateStart
      ,[DateEnd] =@DateEnd
      ,[Price] =@Price
      ,[Comment] = @Comment
    
  WHERE [OrderID] = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRehersal]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRehersal] 
@RehersalSpaseID int,
@RehersalSpaseName nvarchar(50),
@CityID int,
@Adress nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[RehersalSpase]
 SET [RehersalSpaseName] = @RehersalSpaseName,
     [CityID] = @CityID,
     [Adress] = @Adress
  WHERE [RehersalSpaseID] = @RehersalSpaseID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRoom]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRoom] 
@RehersalRoomID int,
@RehersalRoomName nvarchar(50),
@RehersalSpaseID int,
@RehersalRoomSize int,
@ImageURL nvarchar(500),
@PriceHour decimal,
@PathURL nvarchar(500),
@Description nvarchar(max),
@Address nvarchar(500),
@StartProgram nvarchar(500),
@EndProgram nvarchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[Room]
 SET  [RehersalRoomName] = @RehersalRoomName,
[RehersalSpaseID] = @RehersalSpaseID,
[RehersalRoomSize] = @RehersalRoomSize ,
[ImageURL] = @ImageURL
      ,[PriceHour] = @PriceHour
      ,[PathURL] = @PathURL
      ,[Description] = @Description
      ,[Address] = @Address
      ,[StartProgram] = @StartProgram
      ,[EndProgram] = @EndProgram
  WHERE [RehersalRoomID] = @RehersalRoomID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	   @UserID int
	  ,@UserType smallint
      ,@UserName nvarchar(50)
      ,@UserMail nvarchar(50)
      ,@UserPhone nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [dbo].[User]
 SET [UserType] = @UserType,
     [UserName] = @UserName,
     [UserMail] = @UserMail,
	 [UserPhone] = @UserPhone
  WHERE [UserID] = @UserID
END
GO
