USE [dbase1]
GO
/****** Object:  StoredProcedure [inventory].[getUsedDep]    Script Date: 09.06.2020 14:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin Georgiy
-- Edit date: <2020-06-09>
-- Description: Получение списка Магазинов
-- =============================================
CREATE PROCEDURE [inventory].[getShop] 
AS
BEGIN
	SET NOCOUNT ON

	select 
		 id
		,cName 
	from 
		dbo.s_Shop
	where isActive = 1

END

