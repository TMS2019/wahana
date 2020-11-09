declare
        @TableName sysname = 'MUser',
        @NameSpace varchar(250) = 'test'

declare @Result varchar(max) = 'using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ' + @NameSpace + '
{
        public class ' + @TableName + '
        {'

        select @Result += '
                [Display(Name = "' + t.ColumnName + '")]'
                , @Result +=
                case
                when t.is_nullable = 1 then '
                [Required(ErrorMessage = "' + t.ColumnName + ' is required!")]'
                else ''
                end
                , @Result += '
                public ' + NullableSign1 + ColumnType + NullableSign2 + ' ' + t.ColumnName + ' { get; set; }
                '
        from
        (
                select
                        replace(col.name, ' ', '_') ColumnName,
                        column_id ColumnId,
                        case
                                when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')
                                then 'Nullable<'
                                else ''
                        end NullableSign1,
                        case typ.name
                                when 'bigint' then 'long'
                                when 'binary' then 'byte[]'
                                when 'bit' then 'bool'
                                when 'char' then 'string'
                                when 'date' then 'DateTime'
                                when 'datetime' then 'DateTime'
                                when 'datetime2' then 'DateTime'
                                when 'datetimeoffset' then 'DateTimeOffset'
                                when 'decimal' then 'decimal'
                                when 'float' then 'float'
                                when 'image' then 'byte[]'
                                when 'int' then 'int'
                                when 'money' then 'decimal'
                                when 'nchar' then 'string'
                                when 'ntext' then 'string'
                                when 'numeric' then 'decimal'
                                when 'nvarchar' then 'string'
                                when 'real' then 'double'
                                when 'smalldatetime' then 'DateTime'
                                when 'smallint' then 'short'
                                when 'smallmoney' then 'decimal'
                                when 'text' then 'string'
                                when 'time' then 'TimeSpan'
                                when 'timestamp' then 'DateTime'
                                when 'tinyint' then 'byte'
                                when 'uniqueidentifier' then 'Guid'
                                when 'varbinary' then 'byte[]'
                                when 'varchar' then 'string'
                                else 'UNKNOWN_' + typ.name
                        end ColumnType,
                        case
                                when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')
                                then '>'
                                else ''
                        end NullableSign2
                        , col.is_nullable
                from sys.columns col
                        join sys.types typ on
                                col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id        
                where object_id = object_id(@TableName)
        ) t,
        (
                                        SELECT c.name  AS 'ColumnName', CASE WHEN dd.pk IS NULL THEN 'false' ELSE 'true' END ISPK          
                                        FROM        sys.columns c
                                                JOIN    sys.tables  t   ON c.object_id = t.object_id    
                                                LEFT JOIN (SELECT   K.COLUMN_NAME , C.CONSTRAINT_TYPE as pk  
                                                        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS K
                                                                LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS C
                                                        ON K.TABLE_NAME = C.TABLE_NAME
                                                                AND K.CONSTRAINT_NAME = C.CONSTRAINT_NAME
                                                                AND K.CONSTRAINT_CATALOG = C.CONSTRAINT_CATALOG
                                                                AND K.CONSTRAINT_SCHEMA = C.CONSTRAINT_SCHEMA            
                                                        WHERE K.TABLE_NAME = @TableName) as dd
                                                 ON dd.COLUMN_NAME = c.name
                                         WHERE       t.name = @TableName            
                                ) pkk
        where pkk.ColumnName = t.ColumnName
        order by ColumnId

        set @Result = @Result  + '
        }
}'

        print @Result