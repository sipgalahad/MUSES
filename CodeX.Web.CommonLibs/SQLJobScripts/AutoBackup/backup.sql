DECLARE @Database VARCHAR(100) = 'VIDA',
			@Path VARCHAR(MAX) = N'D:\',
			@Filename VARCHAR(MAX) = 'BU_VIDA_'
DECLARE @Date VARCHAR(10) = DATENAME(DW,GETDATE()),
		@Time VARCHAR(10) = SUBSTRING(REPLACE(CONVERT(VARCHAR(8),GETDATE(),114),':','_'),1,5),
		@Ext VARCHAR(4) = '.bak'
DECLARE @Name VARCHAR(MAX) = @Database + '-Full Database Backup'
DECLARE @Disk VARCHAR(MAX) = @Path + @Filename + @Date + '_' + @Time + @Ext
BACKUP DATABASE @Database TO  DISK =  @Disk WITH NOFORMAT, INIT,  NAME = @Name, SKIP, NOREWIND, NOUNLOAD,  STATS = 1
