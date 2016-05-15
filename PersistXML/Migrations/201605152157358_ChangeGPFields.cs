using System.Data.Entity.Migrations;

namespace PersistXML.Migrations
{
    public partial class ChangeGPFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GPDetails",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(),
                        Initials = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.PatientInterviews",
                c => new
                    {
                        TransactionId = c.String(nullable: false, maxLength: 128),
                        TransactionTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PasNumber = c.String(),
                        Forenames = c.String(),
                        Surname = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        SexCode = c.String(),
                        HomeTelephoneNumber = c.String(),
                        NextOfKinId = c.Int(nullable: false),
                        GpCode = c.String(maxLength: 128),
                        PatientInterview_TransactionId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GPDetails", t => t.GpCode)
                .ForeignKey("dbo.NextOfKins", t => t.NextOfKinId, cascadeDelete: true)
                .ForeignKey("dbo.PatientInterviews", t => t.PatientInterview_TransactionId)
                .Index(t => t.NextOfKinId)
                .Index(t => t.GpCode)
                .Index(t => t.PatientInterview_TransactionId);
            
            CreateTable(
                "dbo.NextOfKins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RelationshipCode = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        AddressLine4 = c.String(),
                        Postcode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "PatientInterview_TransactionId", "dbo.PatientInterviews");
            DropForeignKey("dbo.Patients", "NextOfKinId", "dbo.NextOfKins");
            DropForeignKey("dbo.Patients", "GpCode", "dbo.GPDetails");
            DropIndex("dbo.Patients", new[] { "PatientInterview_TransactionId" });
            DropIndex("dbo.Patients", new[] { "GpCode" });
            DropIndex("dbo.Patients", new[] { "NextOfKinId" });
            DropTable("dbo.NextOfKins");
            DropTable("dbo.Patients");
            DropTable("dbo.PatientInterviews");
            DropTable("dbo.GPDetails");
        }
    }
}
