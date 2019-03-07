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

        public ActionResult _PartFirm()
        {
            RejestrEntities db = new RejestrEntities();
         
            List<Firma> listafirm = db.Firmas.ToList();
            WidokModel widok = new WidokModel();


            List<WidokModel> firmaWidokLista = listafirm.Select(x => new WidokModel
            {
                FirmaID = x.id,
                FirmaNazwa = x.nazwa

            }).ToList();

            return PartialView("_PartFirm",firmaWidokLista);
        }

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

        public ActionResult _PartPracFirmZas(int id, string nazwa)
        {

            ViewBag.nazwa = nazwa;

            RejestrEntities db = new RejestrEntities();

            var listaZasobow = (from t in db.Uprawnienies
                                    where t.id_pracownik.Equals(id)
                                    select t).ToList();



            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listaZasobow.Select(x => new WidokModel
            {
                UprawnienieNazwa = x.Zasob.nazwa+ " (" + x.Poziom_uprawnien.nazwa +")"
            }).ToList();



            return PartialView("_PartPracFirmZas", zasobWidokLista);

        }

        public ActionResult _PartZasFirm()
        {
            RejestrEntities db = new RejestrEntities();
            List<Firma> listafirm = db.Firmas.ToList();
            WidokModel widok = new WidokModel();
            List<WidokModel> firmaWidokLista = listafirm.Select(x => new WidokModel
            {
                FirmaID = x.id,
                FirmaNazwa = x.nazwa

            }).ToList();

            return PartialView("_PartZasFirm", firmaWidokLista);
        }

        public ActionResult _PartZasPrac(int id, string nazwa)
        {

            ViewBag.nazwa = nazwa;

            RejestrEntities db = new RejestrEntities();

            var listaUpPrac = (from t in db.Uprawnienies
                                    where t.id_zasob.Equals(id)
                                    select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listaUpPrac.Select(x => new WidokModel
            {
                PracownikNazwa = x.Pracownik.nazwisko + " " + x.Pracownik.imie + "(" + x.Poziom_uprawnien.nazwa + ")"

            }).ToList();



            return PartialView("_PartZasPrac", zasobWidokLista);

        }

        public ActionResult _ZasobyFirmy(int id)
        {
            
            RejestrEntities db = new RejestrEntities();

            var listaZasobowFirmy = (from t in db.Zasobs
                               where t.id_firma.Equals(id)
                               select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listaZasobowFirmy.Select(x => new WidokModel
            {
                ZasobNazwa = x.nazwa,
                ZasobID = x.id

            }).ToList();



            return PartialView("_ZasobyFirmy", zasobWidokLista);

        }

        public ActionResult _UprawnieniaZasobuFirmy(int id)
        {

            RejestrEntities db = new RejestrEntities();

            var listaUprawnienFirmy = (from t in db.Poziom_uprawnien
                                     where t.id_zasob.Equals(id)
                                     select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> uprawnienieWidokLista = listaUprawnienFirmy.Select(x => new WidokModel
            {
                UprawnienieNazwa = x.nazwa

            }).ToList();



            return PartialView("_UprawnieniaZasobuFirmy", uprawnienieWidokLista);

        }

    }
}
