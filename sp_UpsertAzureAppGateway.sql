CREATE PROCEDURE sp_UpsertAzureAppGateway   
    @location NVARCHAR(128),
    @resourceId NVARCHAR(1024)
AS           
BEGIN    
    IF NOT EXISTS (SELECT TOP 1 [id] FROM [dbo].[entity_azure_app_gateway] WHERE [resource_id] = @resourceId)
    BEGIN
        INSERT INTO [dbo].[entity_azure_app_gateway]
        ([id]
        ,[location]
        ,[resource_id]
        ,[sku_name]
        ,[api_version]
        ,[state]
        ,[owner_operation_id]
        ,[owner_operation_type]
        ,[previous_owner_operation_id]
        ,[previous_owner_operation_type]
        ,[create_time]
        ,[entity_version]
        ,[is_provisioned]
        ,[error_message])
        VALUES
        (NEWID()
        ,@location
        ,@resourceId
        ,'WAF_v2'
        ,'2021-01-01'
        ,'Ready'
        ,NULL
        ,NULL
        ,NULL
        ,NULL
        ,GETDATE()
        ,0
        ,1
        ,NULL)
    END
END