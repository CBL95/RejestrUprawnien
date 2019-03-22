using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RejestrUprawnien.Models;

namespace RejestrUprawnien
{
    public class WidokController : Controller
    {
        //Drzewa Firm [obecny]
        public ActionResult Index()
        {

            RejestrEntities db = new RejestrEntities();

            List<Firma> listafirm = db.Firmas.ToList();
            WidokModel widok = new WidokModel();


            List<WidokModel> firmaWidokLista = listafirm.Select(x => new WidokModel
            {
                FirmaID = x.id,
                FirmaNazwa = x.nazwa

            }).ToList();


            return View(firmaWidokLista);
        }

        //-------------------------------------------//
        //------- Drzewo Kto --> Zasób --------------//
        //-------------------------------------------//

        //Drzewo Firma > Pracownik [obecny] 
        public ActionResult _PartPracFirm(int id)
        {


            RejestrEntities db = new RejestrEntities();

            var listapracownikow = (from t in db.Pracowniks
                                orderby t.nazwisko descending
                                    where t.id_firma.Equals(id)
                                    select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> pracownikWidokLista = listapracownikow.Select(x => new WidokModel
            {
                PracownikNazwa = x.nazwisko + " " + x.imie,
                PracownikID = x.id
            }).ToList();

           

            return PartialView("_PartPracFirm", pracownikWidokLista);

        }

        //Drzewo Firma > Pracownik > Kategoria Zasobu[obecny]
        public ActionResult _PartPracFirmZas(int id)
        {
            RejestrEntities db = new RejestrEntities();

            var listaZasobow = (from t in db.Uprawnienies
                                    where t.id_pracownik.Equals(id)
                                    select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listaZasobow.Select(x => new WidokModel
            {
                UprawnienieNazwa = x.Zasob.nazwa,
                ZasobID = x.Zasob.id
            }).ToList();

            return PartialView("_PartPracFirmZas", zasobWidokLista);
        }

        //Drzewo Firma > Pracownik > Kategoria Zasobu > Nazwa Zasobu + (Poziom)[obecny]
        public ActionResult _FirmaPracZasKonkret(int id, string nazwa)
        {

            ViewBag.nazwa = nazwa;

            RejestrEntities db = new RejestrEntities();

            var listaPracownikDoZasobu = (from t in db.Uprawnienies
                                          where t.id_zasob.Equals(id)
                                          select t).ToList();



            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listaPracownikDoZasobu.Select(x => new WidokModel
            {
                UprawnienieNazwa = x.Nazwa_zasobu.nazwa + " " + "(" + x.Poziom_uprawnien.nazwa + ")"
            }).ToList();



            return PartialView("_FirmaPracZasKonkret", zasobWidokLista);

        }





        //-------------------------------------------//
        //------- Drzewo Zasób --> Kto ------------- //
        //-------------------------------------------//    

        //Drzewo Firma > Zasób[obecny]
        public ActionResult _ZasobyFirmy(int id, string nazwa)
        {
            
            RejestrEntities db = new RejestrEntities();

            var listaZasobowFirmy = (from t in db.Zasobs
                               where t.id_firma.Equals(id)
                               select t).ToList();


            ViewBag.nazwaTest = nazwa;
            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listaZasobowFirmy.Select(x => new WidokModel
            {
                ZasobNazwa = x.nazwa,
                ZasobID = x.id,
                

            }).ToList();



            return PartialView("_ZasobyFirmy", zasobWidokLista);

        }

        //Drzewo Firma > Zasób > Konkretny[obecny]
        public ActionResult _KonkretnyZasob(int id)
        {

            RejestrEntities db = new RejestrEntities();

            var listaKonkretnychZasobow = (from t in db.Nazwa_zasobu
                                       where t.id_zasob.Equals(id)
                                       select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> uprawnienieWidokLista = listaKonkretnychZasobow.Select(x => new WidokModel
            {
                NazwaZasobNazwa = x.nazwa,
                NazwaZasobID = x.id

            }).ToList();



            return PartialView("_KonkretnyZasob", uprawnienieWidokLista);

        }

        //Drzewo Firma > Zasób > Konkretny > Kto(poziom)[obecny]
        public ActionResult _PracownikDoZasobu(int id)
        {

            RejestrEntities db = new RejestrEntities();

            var listaPracownikDoZasobu = (from t in db.Uprawnienies
                                           where t.id_nazwa_zasobu.Equals(id)
                                           select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> Lista = listaPracownikDoZasobu.Select(x => new WidokModel
            {
                PracownikNazwa = x.Pracownik.nazwisko + " " + x.Pracownik.imie + "(" + x.Poziom_uprawnien.nazwa + ")"


            }).ToList();



            return PartialView("_PracownikDoZasobu", Lista);

        }

       

    }
}
