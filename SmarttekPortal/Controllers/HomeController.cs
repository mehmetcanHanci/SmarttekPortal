﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Busines;
using Entities;

namespace SmarttekPortal.Controllers
{
    public class HomeController : Controller
    {
        private User user = new User();
        public ActionResult Index(string firmaID)
        {
            if (giris())
            {
                if (firmaID != null)
                {
                    AciklamaDurum();
                    //ViewBag.YetkiliCombo
                    Maneger mng = new Maneger();
                    if (mng.firmaKontrol(Int64.Parse(firmaID)))
                    {
                        ViewBag.FirmaList = mng.FirmaList();
                        TempData["FirmaID"] = firmaID;
                        TempData["aciklama"] = mng.firmaAciklamaList(Int64.Parse(firmaID));
                        TempData["yetkili"] = mng.firmaYetkiliList(Int64.Parse(firmaID));
                        List<SelectListItem> items = new List<SelectListItem>();
                        foreach (FirmaYetkili data in mng.firmaYetkiliList(Int64.Parse(firmaID)))
                        {
                            items.Add(new SelectListItem { Text = data.Adi + " " + data.Soyadi, Value = data.ID.ToString() });

                        }
                        ViewBag.YetkiliCombo = items;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["FirmaID"] = "0";
                    //ViewBag.FirmaList
                    Maneger mng = new Maneger();
                    ViewBag.FirmaList = mng.FirmaList();
                    AciklamaDurum();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }

        }
        [HttpPost]
        public ActionResult Index(Firma firma, FirmaAciklama firmaAciklama, FirmaYetkili firmaYetkili)
        {
            string hataMesaj = "";
            if (giris())
            {
                firma.KayitAcan = user.Adi + " " + user.Soyadi;
                firma.FirmaSehir = "varsayılan";
                firma.KayitTarih = DateTime.Now;
                firmaAciklama.KayitAcan = firma.KayitAcan;
                firmaAciklama.KayitTarih = DateTime.Now;
                Maneger mng = new Maneger();
                if (firma != null && firmaAciklama != null && firmaYetkili != null)
                {
                    AciklamaDurum();
                    int ekle = mng.firmaEkle(firma);
                    if (ekle > 0)
                    {
                        if (mng.firmaYetkiliEkle(firma,firmaYetkili) > 0)
                        {
                            if (mng.firmaAciklamaEkle(firma,firmaAciklama, firmaYetkili) > 0)
                            {
                                //hepsi eklendi mesaj
                                ViewBag.add = "Firma Eklendi.";
                            }
                            else
                            {
                                //yetkili eklenmedi
                                hataMesaj += "Firma Acıklama Eklenmedi <br>";
                            }
                        }
                        else
                        {
                            //firma Acıklama eklenmedi
                            hataMesaj += "Firma Yetkili Eklenmedi <br>";
                        }
                    }
                    else if (ekle == -1)
                    {
                        //firma Eklenmedi
                        hataMesaj += "Firma Zaten Kayıtlı <br>";
                    }
                    else
                    {
                        hataMesaj += "Firma Eklenmedi <br>";
                    }
                }
                ViewBag.addError = hataMesaj;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Giris");
            }
        }

        public ActionResult FirmaDetay(Int64 firmaID)
        {
            if (giris())
            {

            }
            else
            {
                return RedirectToAction("Index", "Giris");

            }
            return View();

        }
        public void AciklamaDurum()
        {
            //if (Int64.Parse(TempData["FirmaID"].ToString()) != 0)
            //{
            //    Maneger mng = new Maneger();
            //    List<SelectListItem> items2 = new List<SelectListItem>();
            //    foreach (FirmaYetkili data in mng.firmaYetkiliList(Int64.Parse(TempData["FirmaID"].ToString())))
            //    {
            //        items2.Add(new SelectListItem { Text = data.Adi + " " + data.Soyadi, Value = data.ID.ToString() });

            //    }
            //    ViewBag.YetkiliCombo = items2;
            //}
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Arandı", Value = "Arandı" });
            items.Add(new SelectListItem { Text = "Mail gönderildi", Value = "Mail gönderildi" });
            items.Add(new SelectListItem { Text = "Toplantı ayarlandı", Value = "Toplantı ayarlandı" });
            items.Add(new SelectListItem { Text = "Ziyaret edildi", Value = "Ziyaret edildi" });
            items.Add(new SelectListItem { Text = "Demo yapılacak", Value = "Demo yapılacak" });
            items.Add(new SelectListItem { Text = "Teklif", Value = "Teklif" });
            items.Add(new SelectListItem { Text = "Onay beklemede", Value = "Onay beklemede" });
            items.Add(new SelectListItem { Text = "Satış olumlu", Value = "Satış olumlu" });
            items.Add(new SelectListItem { Text = "Satış olumsuz", Value = "Satış olumsuz" });
            ViewBag.AciklamaDurum = items;
        }
        public bool giris()
        {
            user = Session["Login"] as User;
            if (user != null)
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