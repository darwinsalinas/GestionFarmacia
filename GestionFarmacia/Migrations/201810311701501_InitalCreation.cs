namespace GestionFarmacia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Presentacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        PresentacionId = c.Int(nullable: false),
                        Precio = c.Single(nullable: false),
                        Existencia = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Presentacions", t => t.PresentacionId, cascadeDelete: true)
                .Index(t => t.PresentacionId);
            
            CreateTable(
                "dbo.VentaDetalles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Single(nullable: false),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicamentoes", t => t.MedicamentoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.MedicamentoId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VentaDetalles", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.VentaDetalles", "MedicamentoId", "dbo.Medicamentoes");
            DropForeignKey("dbo.Medicamentoes", "PresentacionId", "dbo.Presentacions");
            DropIndex("dbo.VentaDetalles", new[] { "VentaId" });
            DropIndex("dbo.VentaDetalles", new[] { "MedicamentoId" });
            DropIndex("dbo.Medicamentoes", new[] { "PresentacionId" });
            DropTable("dbo.Ventas");
            DropTable("dbo.VentaDetalles");
            DropTable("dbo.Medicamentoes");
            DropTable("dbo.Presentacions");
        }
    }
}
