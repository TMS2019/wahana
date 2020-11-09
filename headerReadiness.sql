USE [TemplateProject]
GO
/****** Object:  StoredProcedure [dbo].[sp_Lookup_MPlanning]    Script Date: 12/12/2019 5:06:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].sp_Lookup_MReadinessHeader
  
	@VesselID as int = null,
	@VoyageID as int = null,
	@ShiftID as int = null,
	@ReadinessID as int = null,
	@VesselName as varchar(255) = null,
	@VoyageName as varchar(255) = null,
	@ShiftName as varchar(255) = null,
	@StartReadiness as varchar(255) = null,
	@EndReadiness as varchar(255) = null,
	@IsDeleted as bit = null
	
AS
BEGIN
	select
	a.VesselName,b.VoyageID,c.ShiftName,d.StartReadiness,d.EndReadiness
	FROM MVessel a with(nolock) inner join MVoyage b on a.VesselID=b.VoyageID join Mshift c on b.VoyageID=c.ShiftID join MReadiness d on c.ShiftID=d.ReadinessID 
	where (a.VesselID=@VesselID)and (b.VoyageID=@VoyageID)and(d.IsDeleted=@IsDeleted)
	--select a.materialname,a.materialdescription,b.uomname from mmaterial 
	--a inner join Muom b on a.materialid=b.Uomid where (a.materialid=@MaterialID)
	--where 
	--(BlID = @BlID)and (BLDate = @BLDate)
	--AND (@IsDeleted IS NULL OR IsDeleted = @IsDeleted)


END
