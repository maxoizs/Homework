using System.Data.Entity.Migrations;

namespace PersistXML.Migrations
{

    public partial class AddDumpXML : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DumpXmls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XmlContent = c.String(storeType: "xml"),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.DumpXmls");
        }
    }
}
