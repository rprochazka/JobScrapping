namespace JobScrapping.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LookupFailedValidationReasons",
                c => new
                    {
                        FailedValidationReasonId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.FailedValidationReasonId);
            
            CreateTable(
                "dbo.ScrappingEntry",
                c => new
                    {
                        ScrappingEntryId = c.Int(nullable: false, identity: true),
                        ScrappingSiteId = c.Int(nullable: false),
                        EntryUserId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, defaultValueSql:"getdate()"),
                    })
                .PrimaryKey(t => t.ScrappingEntryId)
                .ForeignKey("dbo.LookupScrappingSites", t => t.ScrappingSiteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.EntryUserId, cascadeDelete: true)
                .Index(t => t.ScrappingSiteId)
                .Index(t => t.EntryUserId);
            
            CreateTable(
                "dbo.ScrappingFieldEntry",
                c => new
                    {
                        ScrappingFieldEntryId = c.Int(nullable: false, identity: true),
                        ScrappingFieldId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 255),
                        ScrappingEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScrappingFieldEntryId)
                .ForeignKey("dbo.ScrappingEntry", t => t.ScrappingEntryId, cascadeDelete: true)
                .ForeignKey("dbo.ScrappingFields", t => t.ScrappingFieldId)
                .Index(t => t.ScrappingFieldId)
                .Index(t => t.ScrappingEntryId);
            
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
                "dbo.LookupScrappingSites",
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
                "dbo.ScrappingValidation",
                c => new
                    {
                        ScrappingValidationId = c.Int(nullable: false, identity: true),
                        ScrappingEntryId = c.Int(nullable: false),
                        ValidatingUserId = c.Int(nullable: false),
                        ValidationDate = c.DateTime(nullable: false, defaultValueSql:"getdate()"),
                    })
                .PrimaryKey(t => t.ScrappingValidationId)
                .ForeignKey("dbo.Users", t => t.ValidatingUserId, cascadeDelete: true)
                .Index(t => t.ValidatingUserId);
            
            CreateTable(
                "dbo.ScrappingFieldEntryValidations",
                c => new
                    {
                        ScrappingFieldEntryValidationId = c.Int(nullable: false, identity: true),
                        ValidationResult = c.Int(nullable: false),
                        FailedValidationReasonId = c.Int(),
                        ScrappingFieldEntryId = c.Int(nullable: false),
                        ScrappingValidationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScrappingFieldEntryValidationId)
                .ForeignKey("dbo.LookupFailedValidationReasons", t => t.FailedValidationReasonId)
                .ForeignKey("dbo.ScrappingValidation", t => t.ScrappingValidationId)
                .ForeignKey("dbo.ScrappingFieldEntry", t => t.ScrappingFieldEntryId)
                .Index(t => t.FailedValidationReasonId)
                .Index(t => t.ScrappingFieldEntryId)
                .Index(t => t.ScrappingValidationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScrappingEntry", "EntryUserId", "dbo.Users");
            DropForeignKey("dbo.ScrappingValidation", "ValidatingUserId", "dbo.Users");
            DropForeignKey("dbo.ScrappingFieldEntryValidations", "ScrappingFieldEntryId", "dbo.ScrappingFieldEntry");
            DropForeignKey("dbo.ScrappingFieldEntryValidations", "ScrappingValidationId", "dbo.ScrappingValidation");
            DropForeignKey("dbo.ScrappingFieldEntryValidations", "FailedValidationReasonId", "dbo.LookupFailedValidationReasons");
            DropForeignKey("dbo.ScrappingEntry", "ScrappingSiteId", "dbo.LookupScrappingSites");
            DropForeignKey("dbo.ScrappingFieldEntry", "ScrappingFieldId", "dbo.ScrappingFields");
            DropForeignKey("dbo.ScrappingFieldEntry", "ScrappingEntryId", "dbo.ScrappingEntry");
            DropIndex("dbo.ScrappingFieldEntryValidations", new[] { "ScrappingValidationId" });
            DropIndex("dbo.ScrappingFieldEntryValidations", new[] { "ScrappingFieldEntryId" });
            DropIndex("dbo.ScrappingFieldEntryValidations", new[] { "FailedValidationReasonId" });
            DropIndex("dbo.ScrappingValidation", new[] { "ValidatingUserId" });
            DropIndex("dbo.ScrappingFieldEntry", new[] { "ScrappingEntryId" });
            DropIndex("dbo.ScrappingFieldEntry", new[] { "ScrappingFieldId" });
            DropIndex("dbo.ScrappingEntry", new[] { "EntryUserId" });
            DropIndex("dbo.ScrappingEntry", new[] { "ScrappingSiteId" });
            DropTable("dbo.ScrappingFieldEntryValidations");
            DropTable("dbo.ScrappingValidation");
            DropTable("dbo.Users");
            DropTable("dbo.LookupScrappingSites");
            DropTable("dbo.ScrappingFields");
            DropTable("dbo.ScrappingFieldEntry");
            DropTable("dbo.ScrappingEntry");
            DropTable("dbo.LookupFailedValidationReasons");
        }
    }
}
