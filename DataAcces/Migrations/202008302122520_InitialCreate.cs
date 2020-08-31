namespace DataAcces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.firma_bilgileri",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        satis_tarih = c.String(),
                        referans = c.String(),
                        ilgili_kisi = c.String(),
                        ilgili_kisi_soy = c.String(),
                        cinsiyet = c.String(),
                        kisi_tel = c.String(),
                        kisi_adres = c.String(),
                        kisi_posizyon = c.String(),
                        kisi_mail = c.String(),
                        konu = c.String(),
                        saha_sorumlu = c.String(),
                        arvato_sorumlusu = c.String(),
                        net_teklif = c.String(),
                        firma_adi = c.String(),
                        firma_cari_unvan = c.String(),
                        firma_telefon = c.String(),
                        mail = c.String(),
                        il = c.String(),
                        ilce = c.String(),
                        adres = c.String(),
                        vergi_dairesi = c.String(),
                        vergi_no = c.String(),
                        bayisi = c.String(),
                        Kayit_tarih = c.String(),
                        tarih_ara = c.String(),
                        ozel_fatura_no = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.temas_bilgi",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        siparis_numara = c.String(maxLength: 150),
                        acma_nedeni = c.String(maxLength: 500),
                        temas_olusturan = c.String(maxLength: 500),
                        firma_adi = c.String(maxLength: 500),
                        temas_numara = c.String(maxLength: 500),
                        yetkili_kisi = c.String(maxLength: 500),
                        firma_telefon = c.String(maxLength: 500),
                        adres = c.String(maxLength: 500),
                        vergi_dairesi = c.String(maxLength: 500),
                        vergi_no = c.String(maxLength: 500),
                        simdiki_durum = c.String(maxLength: 500),
                        temas_tarih = c.String(maxLength: 500),
                        temas_asama = c.String(maxLength: 500),
                        tarih_guncelle = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.temas_bilgi");
            DropTable("dbo.firma_bilgileri");
        }
    }
}
