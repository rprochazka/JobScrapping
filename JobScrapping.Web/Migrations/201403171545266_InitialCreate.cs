namespace JobScrapping.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScrappingSite",
                c => new
                    {
                        ScrappingSiteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ScrappingSiteId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        AmazonId = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ScrappingField",
                c => new
                    {
                        ScrappingFieldId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScrappingFieldId);
            
            CreateTable(
                "dbo.FailedValidationReason",
                c => new
                    {
                        FailedValidationReasonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FailedValidationReasonId);
            
            CreateTable(
                "dbo.ScrappingDefinitionEntry",
                c => new
                    {
                        ScrappingDefinitionEntryId = c.Int(nullable: false, identity: true),
                        ScrappingSiteId = c.Int(nullable: false),
                        EntryUserId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingDefinitionEntryId)
                .ForeignKey("dbo.ScrappingSite", t => t.ScrappingSiteId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.ScrappingSiteId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.ScrappingFieldDefinition",
                c => new
                    {
                        ScrappingFieldDefinitionId = c.Int(nullable: false, identity: true),
                        ScrappingFieldId = c.Int(nullable: false),
                        DefinitionValue = c.Int(nullable: false),
                        ScrappingDefinitionEntry_ScrappingDefinitionEntryId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingFieldDefinitionId)
                .ForeignKey("dbo.ScrappingDefinitionEntry", t => t.ScrappingDefinitionEntry_ScrappingDefinitionEntryId)
                .Index(t => t.ScrappingDefinitionEntry_ScrappingDefinitionEntryId);
            
            CreateTable(
                "dbo.ScrappingDefinitionValidation",
                c => new
                    {
                        ScrappingDefinitionValidationId = c.Int(nullable: false, identity: true),
                        ScrappingDefinitionEntryId = c.Int(nullable: false),
                        ValidatingUserId = c.Int(nullable: false),
                        ValidationDate = c.DateTime(nullable: false),
                        ValidatingUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingDefinitionValidationId)
                .ForeignKey("dbo.User", t => t.ValidatingUser_UserId)
                .Index(t => t.ValidatingUser_UserId);
            
            CreateTable(
                "dbo.ScrappingFieldDefinitionValidationResult",
                c => new
                    {
                        ScrappingFieldDefinitionId = c.Int(nullable: false, identity: true),
                        ValidationResult = c.Int(nullable: false),
                        FailedValidationReasonId = c.Int(),
                        ScrappingFieldDefinition_ScrappingFieldDefinitionId = c.Int(),
                        ScrappingDefinitionValidation_ScrappingDefinitionValidationId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingFieldDefinitionId)
                .ForeignKey("dbo.ScrappingFieldDefinition", t => t.ScrappingFieldDefinition_ScrappingFieldDefinitionId)
                .ForeignKey("dbo.FailedValidationReason", t => t.FailedValidationReasonId)
                .ForeignKey("dbo.ScrappingDefinitionValidation", t => t.ScrappingDefinitionValidation_ScrappingDefinitionValidationId)
                .Index(t => t.ScrappingFieldDefinition_ScrappingFieldDefinitionId)
                .Index(t => t.FailedValidationReasonId)
                .Index(t => t.ScrappingDefinitionValidation_ScrappingDefinitionValidationId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ScrappingFieldDefinitionValidationResult", new[] { "ScrappingDefinitionValidation_ScrappingDefinitionValidationId" });
            DropIndex("dbo.ScrappingFieldDefinitionValidationResult", new[] { "FailedValidationReasonId" });
            DropIndex("dbo.ScrappingFieldDefinitionValidationResult", new[] { "ScrappingFieldDefinition_ScrappingFieldDefinitionId" });
            DropIndex("dbo.ScrappingDefinitionValidation", new[] { "ValidatingUser_UserId" });
            DropIndex("dbo.ScrappingFieldDefinition", new[] { "ScrappingDefinitionEntry_ScrappingDefinitionEntryId" });
            DropIndex("dbo.ScrappingDefinitionEntry", new[] { "User_UserId" });
            DropIndex("dbo.ScrappingDefinitionEntry", new[] { "ScrappingSiteId" });
            DropForeignKey("dbo.ScrappingFieldDefinitionValidationResult", "ScrappingDefinitionValidation_ScrappingDefinitionValidationId", "dbo.ScrappingDefinitionValidation");
            DropForeignKey("dbo.ScrappingFieldDefinitionValidationResult", "FailedValidationReasonId", "dbo.FailedValidationReason");
            DropForeignKey("dbo.ScrappingFieldDefinitionValidationResult", "ScrappingFieldDefinition_ScrappingFieldDefinitionId", "dbo.ScrappingFieldDefinition");
            DropForeignKey("dbo.ScrappingDefinitionValidation", "ValidatingUser_UserId", "dbo.User");
            DropForeignKey("dbo.ScrappingFieldDefinition", "ScrappingDefinitionEntry_ScrappingDefinitionEntryId", "dbo.ScrappingDefinitionEntry");
            DropForeignKey("dbo.ScrappingDefinitionEntry", "User_UserId", "dbo.User");
            DropForeignKey("dbo.ScrappingDefinitionEntry", "ScrappingSiteId", "dbo.ScrappingSite");
            DropTable("dbo.ScrappingFieldDefinitionValidationResult");
            DropTable("dbo.ScrappingDefinitionValidation");
            DropTable("dbo.ScrappingFieldDefinition");
            DropTable("dbo.ScrappingDefinitionEntry");
            DropTable("dbo.FailedValidationReason");
            DropTable("dbo.ScrappingField");
            DropTable("dbo.User");
            DropTable("dbo.ScrappingSite");
        }
    }
}
