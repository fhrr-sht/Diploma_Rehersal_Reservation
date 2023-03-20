USE [Rehersal]
GO
/****** Object:  StoredProcedure [dbo].[CreateReservation]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateReservation]
      @Start [datetime]
      ,@End[datetime]
      ,@Title [nvarchar](max)
      ,@RoomId int 
      ,@Total decimal
      ,@PrimaryColor [nvarchar](max)
      ,@SecondaryColor [nvarchar](max)
      ,@CustomerId int
AS
BEGIN
BEGIN TRANSACTION;
SAVE TRANSACTION MySavePoint;
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[Reservation] ([Start]
      ,[End]
      ,[Title]
      ,[RoomId]
      ,[Total]
      ,[PrimaryColor]
      ,[SecondaryColor]
      ,[CustomerId])
  SELECT  @Start
      ,@End
      ,@Title 
      ,@RoomId  
      ,@Total 
      ,@PrimaryColor 
      ,@SecondaryColor 
      ,@CustomerId

	declare @ReservationId int =  SCOPE_IDENTITY()
	INSERT INTO [dbo].[Payment] ([ReservationId]
      ,[TotalAmount]
      ,[PaymentDate])
  SELECT  @ReservationId
		  ,@Total
		  ,GETDATE()

declare @PaymentId int =  SCOPE_IDENTITY()
INSERT INTO [dbo].[Receipt] ([ReservationId]
      ,[RoomId]
      ,[TotalPrice]
	  ,[PaymentId]
	  )
  SELECT  @ReservationId
		  ,@RoomId
		  ,@Total
		  ,@PaymentId

	COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCity]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCity] 
@CityID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 DELETE FROM [dbo].[City] WHERE [CityID] = @CityID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteOrder]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteOrder] 
@OrderID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 DELETE FROM [dbo].[Order] WHERE [OrderID] = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRehersal]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRehersal] 
@RehersalSpaseID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 DELETE FROM [dbo].[RehersalSpase] WHERE [RehersalSpaseID] = @RehersalSpaseID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRoom]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRoom] 
@RehersalRoomID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 DELETE FROM [dbo].[Room] WHERE [RehersalRoomID] = @RehersalRoomID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUser] 
@UserID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 UPDATE  [dbo].[User] 
 SET  [IsDeleted] = 1
 WHERE [UserID] = @UserID
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllRooms]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllRooms] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalRoomID]
      ,[RehersalRoomName] as Title
      ,[RehersalRoomSize]
      ,[RehersalSpaseID]
      ,[ImageURL]
      ,[PriceHour]
      ,[PathURL]
      ,[Description]
      ,[Address]
      ,[StartProgram]
      ,[EndProgram] 
	  FROM Room 
  
END
GO
/****** Object:  StoredProcedure [dbo].[GetCity]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCity] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [CityID], [CityName]
  FROM [dbo].[City]
END
GO
/****** Object:  StoredProcedure [dbo].[GetCityByID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCityByID] 
@CityID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [CityID],[CityName]
  FROM [dbo].[City] WHERE CityID = @CityID
END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerReservations]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomerReservations]
      @CustomerId int 
      
AS
BEGIN
select        
	r.Id,
	r.Title,
	r.Start,
	r.[End],
	r.Total,
	f.RehersalRoomName as Title,
	f.PathURL,
	f.ImageURL,
	f.Description,
	f.Address,
	f.StartProgram,
	f.EndProgram
from Reservation r 
join Room f on r.[RoomId] = f.[RehersalRoomID]
where r.CustomerId = @CustomerId

END
GO
/****** Object:  StoredProcedure [dbo].[GetCustomers]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCustomers] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [CustomerId]
      ,[EmailAddress]
      ,[FirstName]
      ,[LastName]
      ,[PhoneNo]
      ,[PasswordHash]
      ,[PasswordSalt]
  FROM [dbo].[Customer]
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrder]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOrder] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT  [OrderID]
      ,[RehersalSpaceID]
      ,[UserID]
      ,[OrderNumber]
      ,[RehersalRoomID]
      ,[DateStart]
      ,[DateEnd]
      ,[Price]
      ,[Comment]
  FROM [dbo].[Order]
END