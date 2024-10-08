USE [Ecommerce_Db]
GO
/****** Object:  StoredProcedure [dbo].[SP_Create_Category]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Create_Category]
(
 @CategoryName nvarchar(max),
 @ParentCategoryId nvarchar(max),
 @CategoryDescription nvarchar(max),
 @ImageUrl nvarchar(max),
 @SerialNumber int,
 @IsActive nvarchar(1),
 @CreatedBy nvarchar(max)
)
AS
Declare @strErrorMessage varchar(100)  
Declare @CompletionCode varchar(10)


SET @CompletionCode = 'C01_001'
SET @strErrorMessage = ''

BEGIN TRY  
 begin transaction     
   BEGIN
		 
		Declare @ErrorCode varchar(10) = '00';
		--EXEC SP_Validation_Pages @Id=0,@PageContent=@PageContent,@SubTitle = @SubTitle,@PageName = @PageName,@MenuId=0, @ErrorCode = @ErrorCode OUTPUT;
		
		if(@ErrorCode='00')
			begin
			
   				Declare @CategoryId int
				set @CategoryId=(convert(varchar(max),(SELECT count(*) FROM tblVendors with (nolock))+1))
				
				Insert into tblCategories(CategoryName,CategoryDescription,ParentCategoryId,ImageUrl,SerialNumber,IsActive,IsDeleted,CreatedBy,CreatedAt)
								VALUES(@CategoryName,@CategoryDescription,@ParentCategoryId,@ImageUrl,@SerialNumber,@IsActive,0,@CreatedBy,dbo.GetDateLocal())
				
				select @strErrorMessage = isnull( CompletionDescription, @CompletionCode) from tblTransactionCompletionCodes where CompletionCode	= @CompletionCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
			end 
		else
			begin
				select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  

			end
   END   
 Commit Transaction  
END TRY  
  
BEGIN CATCH  
 SET @CompletionCode = 'EE00'
 if @@TRANCOUNT > 0  
 ROLLBACK TRAN  
		DECLARE @message NVARCHAR(MAX)  
        DECLARE @Estate INT  
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()  
        --RAISERROR (@message, 11, @Estate)  
		SELECT @CompletionCode as CompletionCode, @message as strErrorMessage
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Create_Vendor]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Create_Vendor]
(
 @VendorName nvarchar(max),
 @VendorTypeId nvarchar(max),
 @Description nvarchar(max),
 @MobileNumber nvarchar(max),
 @SerialNumber int,
 @IsActive nvarchar(1),
 @CreatedBy nvarchar(max)
)
AS
Declare @strErrorMessage varchar(100)  
Declare @CompletionCode varchar(10)


SET @CompletionCode = 'C01_001'
SET @strErrorMessage = ''

BEGIN TRY  
 begin transaction     
   BEGIN
		 
		Declare @ErrorCode varchar(10) = '00';
		--EXEC SP_Validation_Pages @Id=0,@PageContent=@PageContent,@SubTitle = @SubTitle,@PageName = @PageName,@MenuId=0, @ErrorCode = @ErrorCode OUTPUT;
		
		if(@ErrorCode='00')
			begin
			
   				Declare @VendorId int
				set @VendorId=(convert(varchar(max),(SELECT count(*) FROM tblVendors with (nolock))+1))
				
				Insert into tblVendors(VendorName,VendorTypeId,Description,MobileNumber,SerialNumber,IsActive,IsDeleted,CreatedBy,CreatedAt)
								VALUES(@VendorName,@VendorTypeId,@Description,@MobileNumber,@SerialNumber,@IsActive,0,@CreatedBy,dbo.GetDateLocal())
				
				select @strErrorMessage = isnull( CompletionDescription, @CompletionCode) from tblTransactionCompletionCodes where CompletionCode	= @CompletionCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
			end 
		else
			begin
				select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  

			end
   END   
 Commit Transaction  
END TRY  
  
BEGIN CATCH  
 SET @CompletionCode = 'EE00'
 if @@TRANCOUNT > 0  
 ROLLBACK TRAN  
		DECLARE @message NVARCHAR(MAX)  
        DECLARE @Estate INT  
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()  
        --RAISERROR (@message, 11, @Estate)  
		SELECT @CompletionCode as CompletionCode, @message as strErrorMessage
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Create_VendorType]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Create_VendorType]
(
 @VendorTypeName nvarchar(max),
 @SerialNumber int,
 @IsActive nvarchar(1),
 @CreatedBy nvarchar(max)
)
AS
Declare @strErrorMessage varchar(100)  
Declare @CompletionCode varchar(10)


SET @CompletionCode = 'C01_001'
SET @strErrorMessage = ''

BEGIN TRY  
 begin transaction     
   BEGIN
		 
		Declare @ErrorCode varchar(10) = '00';
		--EXEC SP_Validation_Pages @Id=0,@PageContent=@PageContent,@SubTitle = @SubTitle,@PageName = @PageName,@MenuId=0, @ErrorCode = @ErrorCode OUTPUT;
		
		if(@ErrorCode='00')
			begin
			
   				Declare @VendorTypeId int
				set @VendorTypeId=(convert(varchar(max),(SELECT count(*) FROM tblVendors with (nolock))+1))
				
				Insert into tblVendorTypes(TypeName,SerialNumber,IsActive,IsDeleted,CreatedBy,CreatedAt)
								VALUES(@VendorTypeName,@SerialNumber,@IsActive,0,@CreatedBy,dbo.GetDateLocal())
				
				select @strErrorMessage = isnull( CompletionDescription, @CompletionCode) from tblTransactionCompletionCodes where CompletionCode	= @CompletionCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
			end 
		else
			begin
				select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  

			end
   END   
 Commit Transaction  
END TRY  
  
BEGIN CATCH  
 SET @CompletionCode = 'EE00'
 if @@TRANCOUNT > 0  
 ROLLBACK TRAN  
		DECLARE @message NVARCHAR(MAX)  
        DECLARE @Estate INT  
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()  
        --RAISERROR (@message, 11, @Estate)  
		SELECT @CompletionCode as CompletionCode, @message as strErrorMessage
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetAllCategories]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_GetAllCategories]
(
  @QueryExtension varchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN

		if(@QueryExtension='')
			begin
			SELECT * FROM tblCategories cat
						  order by cat.SerialNumber
				
			end
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetAllVendors]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_GetAllVendors]
(
  @QueryExtension varchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN

		if(@QueryExtension='')
			begin
			SELECT vt.TypeName as VendorTypeName,vt.Id as VendorTypeId,v.Id,v.VendorName,v.MobileNumber,
			v.IsActive,v.CreatedAt,v.CreatedBy,v.UpdatedAt,v.UpdatedBy,v.Description,v.SerialNumber FROM tblVendors v
						 left join tblVendorTypes vt on vt.Id = v.VendorTypeId order by v.SerialNumber
				
			end
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetAllVendorTypes]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_Select_GetAllVendorTypes]
(
	
	@QueryExtension varchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN

		if(@QueryExtension='')
			begin
			SELECT * FROM tblVendorTypes 
					  	 order by SerialNumber
				
			end
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetCategoryById]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_GetCategoryById]
(
  @Id varchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN

		if(@Id !='')
			begin
			SELECT * FROM tblCategories cat where cat.Id = @Id
						  order by cat.SerialNumber
				
			end
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetCategoryByParentId]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_GetCategoryByParentId]
(
  @ParentId varchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN

		if(@ParentId !='')
			begin
			SELECT * FROM tblCategories cat where cat.ParentCategoryId = @ParentId
						  order by cat.SerialNumber
				
			end
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetVendorById]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_GetVendorById]
(
  @Id nvarchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN
			SELECT v.Id,v.VendorName,v.MobileNumber,
			v.IsActive,v.CreatedAt,v.CreatedBy,v.UpdatedAt,v.UpdatedBy,v.Description,v.SerialNumber,vt.TypeName as VendorTypeName,vt.Id as VendorTypeId FROM tblVendors v
						 right join tblVendorTypes vt on vt.Id = v.VendorTypeId where v.Id = @Id order by v.SerialNumber
				
			
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Select_GetVendorTypeById]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Select_GetVendorTypeById]
(
   @Id nvarchar(max)
)
as 


BEGIN TRY    
 begin transaction    
   SET  NOCOUNT ON   
   BEGIN
			SELECT * FROM tblVendorTypes c where c.Id = @Id
					  	 order by SerialNumber
				
			
		--else
		--	begin
		--		Declare @sql varchar(max)				
		--		set @sql=' select * from tblCategory where ' + @Field +@Value
		--		exec(@sql)
		--	end	 
		
	
END     
 Commit Transaction
   SET  NOCOUNT OFF    
END TRY    
    
BEGIN CATCH    
 if @@TRANCOUNT > 0    
 ROLLBACK TRAN    
 DECLARE @message NVARCHAR(MAX)    
        DECLARE @Estate INT    
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()    
        RAISERROR (@message, 11, @Estate)    
    
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Category]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Update_Category]
(
 @Id int ,
 @CategoryName nvarchar(max),
 @ParentCategoryId nvarchar(max),
 @CategoryDescription nvarchar(max),
 @ImageUrl nvarchar(max),
 @SerialNumber int,
 @IsActive nvarchar(1),
 @CreatedBy nvarchar(max),
 @CreatedAt nvarchar(max),
 @UpdatedBy nvarchar(max)

)
AS
Declare @strErrorMessage varchar(100)  
Declare @CompletionCode varchar(10)


SET @CompletionCode = 'C01_001'
SET @strErrorMessage = ''

BEGIN TRY  
 begin transaction     
   BEGIN
 		Declare @ErrorCode varchar(10)
		--EXEC SP_Validation_Pages @Id=@Id,@MenuId=@MenuId,@PageName=@PageName,@PageContent=@PageContent,@SubTitle=@SubTitle, @ErrorCode = @ErrorCode OUTPUT;
		set @ErrorCode='00'
		if(@ErrorCode='00')
			begin
					begin
						update tblCategories set CategoryName=@CategoryName,ParentCategoryId = @ParentCategoryId,
						 CategoryDescription = @CategoryDescription,ImageUrl = @ImageUrl
						,SerialNumber = @SerialNumber,IsActive = @IsActive
						,CreatedAt = @CreatedAt,CreatedBy = @CreatedBy
						,UpdatedBy=@UpdatedBy,UpdatedAt=dbo.GetDateLocal() where Id=@Id
						
						select @strErrorMessage = isnull( CompletionDescription, @CompletionCode) from tblTransactionCompletionCodes where CompletionCode	= @CompletionCode   and Languagecode=dbo.GetLanguageId(0)
						select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
					end
				--else
				--	begin
				--		set @ErrorCode='E01_002'

				--		select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				--		select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
				--	end

			end 
		else
			begin
				select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
			end
   END   
 Commit Transaction  
END TRY  
  
BEGIN CATCH  
 SET @CompletionCode = 'EE00'
 if @@TRANCOUNT > 0  
 ROLLBACK TRAN  
		DECLARE @message NVARCHAR(MAX)  
        DECLARE @Estate INT  
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()  
        --RAISERROR (@message, 11, @Estate)  
		SELECT @CompletionCode as CompletionCode, @message as strErrorMessage
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Vendor]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROC [dbo].[SP_Update_Vendor]
(  
 @Id int,
 @VendorName nvarchar(max),
 @MobileNumber nvarchar(max),
 @Description nvarchar(max),
 @VendorTypeId int,
 @SerialNumber int,
 @IsActive nvarchar(1),
 @CreatedBy nvarchar(max),
 @CreatedAt nvarchar(max),
 @UpdatedBy nvarchar(max)
)
AS

Declare @strErrorMessage varchar(100)  
Declare @CompletionCode varchar(10)


SET @CompletionCode = 'C01_001'
SET @strErrorMessage = ''

BEGIN TRY  
 begin transaction     
   BEGIN
 		Declare @ErrorCode varchar(10)
		--EXEC SP_Validation_Pages @Id=@Id,@MenuId=@MenuId,@PageName=@PageName,@PageContent=@PageContent,@SubTitle=@SubTitle, @ErrorCode = @ErrorCode OUTPUT;
		set @ErrorCode='00'
		if(@ErrorCode='00')
			begin
					begin
						update tblVendors set VendorName=@VendorName,VendorTypeId = @VendorTypeId,
						MobileNumber = @MobileNumber,Description=@Description
						,SerialNumber = @SerialNumber,IsActive = @IsActive
						,CreatedAt = @CreatedAt,CreatedBy = @CreatedBy
						,UpdatedBy=@UpdatedBy,UpdatedAt=dbo.GetDateLocal() where Id=@Id
						
						select @strErrorMessage = isnull( CompletionDescription, @CompletionCode) from tblTransactionCompletionCodes where CompletionCode	= @CompletionCode   and Languagecode=dbo.GetLanguageId(0)
						select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
					end
				--else
				--	begin
				--		set @ErrorCode='E01_002'

				--		select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				--		select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
				--	end

			end 
		else
			begin
				select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
			end
   END   
 Commit Transaction  
END TRY  
  
BEGIN CATCH  
 SET @CompletionCode = 'EE00'
 if @@TRANCOUNT > 0  
 ROLLBACK TRAN  
		DECLARE @message NVARCHAR(MAX)  
        DECLARE @Estate INT  
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()  
        --RAISERROR (@message, 11, @Estate)  
		SELECT @CompletionCode as CompletionCode, @message as strErrorMessage
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_VendorType]    Script Date: 10/3/2024 6:02:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROC [dbo].[SP_Update_VendorType]
(  
 @Id int,
 @TypeName nvarchar(max),
 @SerialNumber int,
 @IsActive nvarchar(1),
 @CreatedBy nvarchar(max),
 @CreatedAt nvarchar(max),
 @UpdatedBy nvarchar(max)
)
AS

Declare @strErrorMessage varchar(100)  
Declare @CompletionCode varchar(10)


SET @CompletionCode = 'C01_001'
SET @strErrorMessage = ''

BEGIN TRY  
 begin transaction     
   BEGIN
 		Declare @ErrorCode varchar(10)
		--EXEC SP_Validation_Pages @Id=@Id,@MenuId=@MenuId,@PageName=@PageName,@PageContent=@PageContent,@SubTitle=@SubTitle, @ErrorCode = @ErrorCode OUTPUT;
		set @ErrorCode='00'
		if(@ErrorCode='00')
			begin
					begin
						update tblVendorTypes set TypeName=@TypeName
						,SerialNumber = @SerialNumber,IsActive = @IsActive
						,CreatedAt = @CreatedAt,CreatedBy = @CreatedBy
						,UpdatedBy=@UpdatedBy,UpdatedAt=dbo.GetDateLocal() where Id=@Id
						
						select @strErrorMessage = isnull( CompletionDescription, @CompletionCode) from tblTransactionCompletionCodes where CompletionCode	= @CompletionCode   and Languagecode=dbo.GetLanguageId(0)
						select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
					end
				--else
				--	begin
				--		set @ErrorCode='E01_002'

				--		select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				--		select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
				--	end

			end 
		else
			begin
				select @strErrorMessage = isnull( ErrorDescription, @ErrorCode) from tbl_ErrorCodes where ErrorCode	= @ErrorCode   and Languagecode=dbo.GetLanguageId(0)
				select @ErrorCode as ErrorCode, @strErrorMessage as strErrorMessage  ----From GUI if CompletionCode !='00' ,show strErrorMessage from GUI  
			end
   END   
 Commit Transaction  
END TRY  
  
BEGIN CATCH  
 SET @CompletionCode = 'EE00'
 if @@TRANCOUNT > 0  
 ROLLBACK TRAN  
		DECLARE @message NVARCHAR(MAX)  
        DECLARE @Estate INT  
        SELECT @message = ERROR_MESSAGE(), @Estate = ERROR_STATE()  
        --RAISERROR (@message, 11, @Estate)  
		SELECT @CompletionCode as CompletionCode, @message as strErrorMessage
END CATCH
GO
