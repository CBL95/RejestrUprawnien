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

            List<Pracownik> listapracownikow = db.Pracowniks.ToList();

            WidokModel widok = new WidokModel();



            List<WidokModel> pracownikWidokLista = listapracownikow.Select(x => new WidokModel
            {
                PracownikNazwa = x.nazwisko + " " + x.imie,
                PracownikID = x.id
                
            }).ToList();


            return View(pracownikWidokLista);
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

        public ActionResult _PartPracFirm(int id, string nazwa)
        {

            ViewBag.nazwa = nazwa;

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
                UprawnienieNazwa = x.Zasob.nazwa+ "(" + x.Poziom_uprawnien.nazwa +")"
            }).ToList();



            return PartialView("_PartPracFirmZas", zasobWidokLista);

        }

        public ActionResult _PartZasFirm(int id, string nazwa)
        {

            ViewBag.nazwa = nazwa;

            RejestrEntities db = new RejestrEntities();

            var listapracownikow = (from t in db.Zasobs 
                                    where t.id_firma.Equals(id)
                                    select t).ToList();

            WidokModel widok = new WidokModel();

            List<WidokModel> zasobWidokLista = listapracownikow.Select(x => new WidokModel
            {
                ZasobNazwa = x.nazwa,
                ZasobID = x.id
            }).ToList();



            return PartialView("_PartZasFirm", zasobWidokLista);

        }

    }
}
