namespace JobScrapping.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobscrapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FailedValidationReasons",
                c => new
                    {
                        FailedValidationReasonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FailedValidationReasonId);
            
            CreateTable(
                "dbo.ScrappingDefinitionEntries",
                c => new
                    {
                        ScrappingDefinitionEntryId = c.Int(nullable: false, identity: true),
                        ScrappingSiteId = c.Int(nullable: false),
                        EntryUserId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingDefinitionEntryId)
                .ForeignKey("dbo.ScrappingSites", t => t.ScrappingSiteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.ScrappingSiteId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.ScrappingFieldDefinitions",
                c => new
                    {
                        ScrappingFieldDefinitionId = c.Int(nullable: false, identity: true),
                        ScrappingFieldId = c.Int(nullable: false),
                        DefinitionValue = c.String(),
                        ScrappingDefinitionEntry_ScrappingDefinitionEntryId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingFieldDefinitionId)
                .ForeignKey("dbo.ScrappingDefinitionEntries", t => t.ScrappingDefinitionEntry_ScrappingDefinitionEntryId)
                .ForeignKey("dbo.ScrappingFields", t => t.ScrappingFieldId, cascadeDelete: true)
                .Index(t => t.ScrappingFieldId)
                .Index(t => t.ScrappingDefinitionEntry_ScrappingDefinitionEntryId);
            
            CreateTable(
                "dbo.ScrappingFields",
                c => new
                    {
                        ScrappingFieldId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScrappingFieldId);
            
            CreateTable(
                "dbo.ScrappingSites",
                c => new
                    {
                        ScrappingSiteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ScrappingSiteId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        AmazonId = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ScrappingDefinitionValidations",
                c => new
                    {
                        ScrappingDefinitionValidationId = c.Int(nullable: false, identity: true),
                        ScrappingDefinitionEntryId = c.Int(nullable: false),
                        ValidatingUserId = c.Int(nullable: false),
                        ValidationDate = c.DateTime(nullable: false),
                        ValidatingUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingDefinitionValidationId)
                .ForeignKey("dbo.Users", t => t.ValidatingUser_UserId)
                .Index(t => t.ValidatingUser_UserId);
            
            CreateTable(
                "dbo.ScrappingFieldDefinitionValidationResults",
                c => new
                    {
                        ScrappingFieldDefinitionValidationResultId = c.Int(nullable: false, identity: true),
                        ValidationResult = c.Int(nullable: false),
                        FailedValidationReasonId = c.Int(),
                        ScrappingFieldDefinition_ScrappingFieldDefinitionId = c.Int(),
                        ScrappingDefinitionValidation_ScrappingDefinitionValidationId = c.Int(),
                    })
                .PrimaryKey(t => t.ScrappingFieldDefinitionValidationResultId)
                .ForeignKey("dbo.FailedValidationReasons", t => t.FailedValidationReasonId)
                .ForeignKey("dbo.ScrappingFieldDefinitions", t => t.ScrappingFieldDefinition_ScrappingFieldDefinitionId)
                .ForeignKey("dbo.ScrappingDefinitionValidations", t => t.ScrappingDefinitionValidation_ScrappingDefinitionValidationId)
                .Index(t => t.FailedValidationReasonId)
                .Index(t => t.ScrappingFieldDefinition_ScrappingFieldDefinitionId)
                .Index(t => t.ScrappingDefinitionValidation_ScrappingDefinitionValidationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScrappingDefinitionValidations", "ValidatingUser_UserId", "dbo.Users");
            DropForeignKey("dbo.ScrappingFieldDefinitionValidationResults", "ScrappingDefinitionValidation_ScrappingDefinitionValidationId", "dbo.ScrappingDefinitionValidations");
            DropForeignKey("dbo.ScrappingFieldDefinitionValidationResults", "ScrappingFieldDefinition_ScrappingFieldDefinitionId", "dbo.ScrappingFieldDefinitions");
            DropForeignKey("dbo.ScrappingFieldDefinitionValidationResults", "FailedValidationReasonId", "dbo.FailedValidationReasons");
            DropForeignKey("dbo.ScrappingDefinitionEntries", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ScrappingDefinitionEntries", "ScrappingSiteId", "dbo.ScrappingSites");
            DropForeignKey("dbo.ScrappingFieldDefinitions", "ScrappingFieldId", "dbo.ScrappingFields");
            DropForeignKey("dbo.ScrappingFieldDefinitions", "ScrappingDefinitionEntry_ScrappingDefinitionEntryId", "dbo.ScrappingDefinitionEntries");
            DropIndex("dbo.ScrappingFieldDefinitionValidationResults", new[] { "ScrappingDefinitionValidation_ScrappingDefinitionValidationId" });
            DropIndex("dbo.ScrappingFieldDefinitionValidationResults", new[] { "ScrappingFieldDefinition_ScrappingFieldDefinitionId" });
            DropIndex("dbo.ScrappingFieldDefinitionValidationResults", new[] { "FailedValidationReasonId" });
            DropIndex("dbo.ScrappingDefinitionValidations", new[] { "ValidatingUser_UserId" });
            DropIndex("dbo.ScrappingFieldDefinitions", new[] { "ScrappingDefinitionEntry_ScrappingDefinitionEntryId" });
            DropIndex("dbo.ScrappingFieldDefinitions", new[] { "ScrappingFieldId" });
            DropIndex("dbo.ScrappingDefinitionEntries", new[] { "User_UserId" });
            DropIndex("dbo.ScrappingDefinitionEntries", new[] { "ScrappingSiteId" });
            DropTable("dbo.ScrappingFieldDefinitionValidationResults");
            DropTable("dbo.ScrappingDefinitionValidations");
            DropTable("dbo.Users");
            DropTable("dbo.ScrappingSites");
            DropTable("dbo.ScrappingFields");
            DropTable("dbo.ScrappingFieldDefinitions");
            DropTable("dbo.ScrappingDefinitionEntries");
            DropTable("dbo.FailedValidationReasons");
        }
    }
}
