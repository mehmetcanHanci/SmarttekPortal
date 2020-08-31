namespace DataAcces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Firma1",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FirmaAdi = c.String(),
                        KayitAcan = c.String(),
                        KayitTarih = c.DateTime(nullable: false),
                        FirmaTelefon = c.String(),
                        FirmaWebSite = c.String(),
                        FirmaAdres = c.String(),
                        FirmaSehir = c.String(),
                        FirmaMail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.firmaAciklama1",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        firmaID = c.Long(nullable: false),
                        Aciklama = c.String(),
                        KayitAcan = c.String(),
                        KayitTarih = c.DateTime(nullable: false),
                        Yetkili = c.Long(nullable: false),
                        Durum = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FirmaYetkili1",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Adi = c.String(),
                        Soyadi = c.String(),
                        Unvan = c.String(),
                        firmaID = c.Long(nullable: false),
                        Telefon = c.String(),
                        Mail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FirmaYetkili1");
            DropTable("dbo.firmaAciklama1");
            DropTable("dbo.Firma1");
        }
    }
}
