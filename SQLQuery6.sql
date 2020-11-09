USE [TemplateProject]
GO
/****** Object:  StoredProcedure [dbo].[sp_Lookup_MUser]    Script Date: 12/11/2019 11:10:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[sp_Lookup_MMaterial]
	@MaterialID as int,
	@VesselID as int,
	@tmp as DATETIME=null,
	@VoyageID as int,
	--@TotalVoyage as int,
	@MaterialName as varchar(255) = null,
	@VesselName as varchar(255) = null,
	@MaterialDescription as varchar(255) = null,
	@IsDeleted as bit = null
AS
BEGIN
	SELECT d.VoyageID, a.MaterialID,a.MaterialName,b.UomID,b.UomName,c.VesselName
      ,a.[MaterialName]
      ,a.[MaterialDescription]
      ,a.[IsDeleted]
      ,a.[UserCreated]
      ,a.[DateCreated]
      ,a.[UserModified]
      ,a.[DateModified]
  FROM [TemplateProject].[dbo].[MMaterial] a inner join
  muom b on a.MaterialID=b.UomID left join MVessel c on b.UomID=c.VesselID left join MVoyage d on c.VesselID=d.VoyageID
  where (@MaterialID IS NULL OR MaterialID = @MaterialID) 
  select count(VoyageID) as total,year(GETDATE()) as tahun from MVoyage where (@VoyageID = @VoyageID) 
  --inner join MVessel c inner join MVoyage d 
END

--select count(VoyageID) as total,year(GETDATE()) as tahun from MVoyage
