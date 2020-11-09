select count(UserID) as total,GETDATE() as Tahun from MUser

select * from mmaterial
select * from Muom


select a.MaterialID,b.UomID form mmaterial a left join muom b on a.MaterialID=b.Uomid

select a.materialname,a.materialdescription,b.uomname from mmaterial a inner join Muom b on a.materialid=b.Uomid where a.materialid='1'