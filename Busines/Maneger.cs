using DataAcces.EntityFramework;
using Entities;
using Entities.Messasages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Busines
{
    public class Maneger
    {
        Repository<User> repo_User = new Repository<User>();
        Repository<Firma> repo_Firma = new Repository<Firma>();
        Repository<FirmaAciklama> repo_FirmaAciklama = new Repository<FirmaAciklama>();
        Repository<FirmaYetkili> repo_FirmaYetkili = new Repository<FirmaYetkili>();

        public BusinesLayerResult<User> Login(string mail, string sifre)
        {

            BusinesLayerResult<User> layerResult = new BusinesLayerResult<User>();
            layerResult.result = repo_User.Find(x => x.Mail == mail && x.Sifre == sifre);
            if (layerResult.result == null)
            {
                layerResult.addError(ErrosMesasagesCode.KullaniciBulunamadi, "Kullanıcı Bulunamadı!..");
            }
            return layerResult;
        }
        public int firmaEkle(Firma data)
        {
            BusinesLayerResult<Firma> layerResult = new BusinesLayerResult<Firma>();
            layerResult.result = repo_Firma.Find(x => x.FirmaAdi == data.FirmaAdi);
            if (layerResult.result == null)
            {
                return repo_Firma.Insert(data);
            }
            else
            {
                return -1;
            }

        }
        public int firmaAciklamaEkle(Firma firma, FirmaAciklama data, FirmaYetkili yetkili)
        {
            BusinesLayerResult<FirmaYetkili> layerResult = new BusinesLayerResult<FirmaYetkili>();
            layerResult.result = repo_FirmaYetkili.Find(x => x.Adi == yetkili.Adi);
            BusinesLayerResult<Firma> layerResult2 = new BusinesLayerResult<Firma>();
            layerResult2.result = repo_Firma.Find(x => x.FirmaAdi == firma.FirmaAdi);
            if (layerResult.result != null && layerResult2.result != null)
            {
                data.Yetkili = layerResult.result.ID;
                data.firmaID = layerResult2.result.ID;
                return repo_FirmaAciklama.Insert(data);
            }
            else
            {
                return -1;
            }

        }
        public int firmaYetkiliEkle(Firma firma, FirmaYetkili data)
        {
            BusinesLayerResult<Firma> layerResult2 = new BusinesLayerResult<Firma>();
            layerResult2.result = repo_Firma.Find(x => x.FirmaAdi == firma.FirmaAdi);
            if (layerResult2.result != null)
            {
                data.firmaID = layerResult2.result.ID;
                return repo_FirmaYetkili.Insert(data);
            }
            else
            {
                return -1;
            }

        }
        public List<Firma> FirmaList()
        {
            return repo_Firma.List();
        }
        public List<FirmaAciklama> firmaAciklamaList(Int64 firmaID)
        {
            return repo_FirmaAciklama.List(x => x.firmaID == firmaID);
        }
        public List<FirmaYetkili> firmaYetkiliList(Int64 firmaID)
        {
            return repo_FirmaYetkili.List(x => x.firmaID == firmaID);
        }
        public bool firmaKontrol(Int64 firmaID)
        {
            BusinesLayerResult<Firma> layerResult = new BusinesLayerResult<Firma>();
            layerResult.result = repo_Firma.Find(x => x.ID == firmaID);
            if (layerResult.result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
