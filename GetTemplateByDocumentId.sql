USE [ZadDocument]
GO

/****** Object:  StoredProcedure [dbo].[GetTemplateByDocumentId]    Script Date: 2/21/2022 4:32:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Script for SelectTopNRows command from SSMS  ******/

CREATE PROCEDURE [dbo].[GetTemplateByDocumentId]
	@DocumentId	INT
AS
BEGIN
	SELECT [Id]
		   ,[FlowId]
		   ,[FlowName]
		   ,[DivisionId]
           ,[DivisionName]
		   ,[DepartmentId]
		   ,[DepartmentName]
		   ,[UserId]
		   ,[UserName]
		   ,[LevelId]
		   ,[LevelName]
		   ,[ApproveId]
		   ,[ApproveName]
		   ,[Description]
		   ,[AltDescription]
		   ,[Active]
		   ,[CreatedId]
		   ,[CreatedTime]
		   ,[CreatedName]
		   ,[UpdatedId]
		   ,[UpdatedTime]
		   ,[UpdatedName]
		   FROM [vwTemplate]
	 WHERE [FlowId] = (select FlowId from vwDocument where id = @DocumentId)
END


GO


