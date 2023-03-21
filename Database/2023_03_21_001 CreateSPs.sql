GO
/****** Object:  StoredProcedure [dbo].[GetOrderByID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderByID] 
@OrderID  int
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
  FROM [dbo].[Order] WHERE OrderID = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderByRehersalID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOrderByRehersalID] 
@RehersalID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [OrderID]
      ,[RehersalSpaceID]
      ,[UserID]
      ,[OrderNumber]
      ,[RehersalRoomID]
      ,[DateStart]
      ,[DateEnd]
      ,[Price]
      ,[Comment]
  FROM [dbo].[Order] WHERE RehersalSpaceID= @RehersalID
END
GO
/****** Object:  StoredProcedure [dbo].[GetRehersal]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRehersal] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalSpaseID]
      ,[RehersalSpaseName]
      ,[CityID]
      ,[Adress]
  FROM [dbo].[RehersalSpase]
END
GO
/****** Object:  StoredProcedure [dbo].[GetRehersalByCityID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRehersalByCityID] 
@CityID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalSpaseID]
      ,[RehersalSpaseName]
      ,[CityID]
      ,[Adress]
  FROM [dbo].[RehersalSpase] WHERE CityID = @CityID
END
GO
/****** Object:  StoredProcedure [dbo].[GetRehersalByID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRehersalByID] 
@RehersalSpaseID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalSpaseID]
      ,[RehersalSpaseName]
      ,[CityID]
      ,[Adress]
  FROM [dbo].[RehersalSpase] WHERE RehersalSpaseID = @RehersalSpaseID
END
GO
/****** Object:  StoredProcedure [dbo].[GetReservationById]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetReservationById]
      @ReservationId int 
      
AS
BEGIN
select        
	r.Id,
	r.Title,
	r.Start,
	r.[End],
	r.Total,
	f.[RehersalRoomName] as Title,
	f.PathURL,
	f.ImageURL,
	f.Description,
	f.Address,
	f.StartProgram,
	f.EndProgram
from Reservation r 
join Room f on r.[RoomId] = f.[RehersalRoomID]
where r.Id = @ReservationId

END
GO
/****** Object:  StoredProcedure [dbo].[GetReservationsByRoomId]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetReservationsByRoomId]
      @RoomId int 
      
AS
BEGIN
select        
	Id = r.Id,
	r.Title,
	r.Start,
	r.[End],
	r.RoomId,
	r.Total,
	r.PrimaryColor,
	r.SecondaryColor,
	r.CustomerId
from Reservation r 
where r.RoomId = @RoomId

END
GO
/****** Object:  StoredProcedure [dbo].[GetRoomByID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRoomByID] 
@RehersalRoomID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalRoomID]
      ,[RehersalRoomName]
      ,[RehersalRoomSize]
      ,[RehersalSpaseID]
      ,[ImageURL]
      ,[PriceHour]
      ,[PathURL]
      ,[Description]
      ,[Address]
      ,[StartProgram]
      ,[EndProgram]
  FROM [dbo].[Room] WHERE RehersalRoomID = @RehersalRoomID
END
GO
/****** Object:  StoredProcedure [dbo].[GetRoomByRehersalID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRoomByRehersalID] 
@RehersalSpaceID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalRoomID]
      ,[RehersalRoomName] 
      ,[RehersalRoomSize]
      ,[RehersalSpaseID]
      ,[ImageURL]
      ,[PriceHour]
      ,[PathURL]
      ,[Description]
      ,[Address]
      ,[StartProgram]
      ,[EndProgram] 
  FROM [dbo].[Room] WHERE RehersalSpaseID = @RehersalSpaceID
END
GO
/****** Object:  StoredProcedure [dbo].[GetRooms]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRooms] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [RehersalRoomID]
      ,[RehersalRoomName]
      ,[RehersalRoomSize]
      ,[RehersalSpaseID]
      ,[ImageURL]
      ,[PriceHour]
      ,[PathURL]
      ,[Description]
      ,[Address]
      ,[StartProgram]
      ,[EndProgram]
  FROM [dbo].[Room] 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByID]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByID]
	 @UserID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [UserID], [UserType], [UserName], [UserMail], [UserPhone], [IsDeleted]
  FROM [dbo].[User] WHERE UserID = @UserID AND IsDeleted=0 
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [UserID], [UserType], [UserName], [UserMail], [UserPhone], [IsDeleted]
  FROM [dbo].[User] WHERE IsDeleted=0 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertCity]    Script Date: 20.03.2023 21:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertCity]
@CityName nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
INSERT INTO [dbo].[City] ([CityName])
  SELECT @CityName 
END