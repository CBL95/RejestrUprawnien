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
                PracownikNazwa = x.nazwisko + " " + x.imie
                
            }).ToList();


            return View(pracownikWidokLista);
        }


        public ActionResult Partial1()
        {
            RejestrEntities db = new RejestrEntities();
         
            List<Firma> listafirm = db.Firmas.ToList();
            WidokModel widok = new WidokModel();


            List<WidokModel> firmaWidokLista = listafirm.Select(x => new WidokModel
            {
                FirmaNazwa = x.nazwa

            }).ToList();

            return PartialView("Partial1",firmaWidokLista);
        }



        public ActionResult Partial2()
        {
            RejestrEntities db = new RejestrEntities();

            List<Poziom_uprawnien> listauprawnien = db.Poziom_uprawnien.ToList();
            WidokModel widok = new WidokModel();


            List<WidokModel> uprawnienieWidokLista = listauprawnien.Select(x => new WidokModel
            {
                UprawnienieNazwa = x.nazwa

            }).ToList();

            return PartialView("Partial2", uprawnienieWidokLista);
        }

        public ActionResult Partial3()
        {
            RejestrEntities db = new RejestrEntities();

            List<Zasob> listauprawnien = db.Zasobs.ToList();
            WidokModel widok = new WidokModel();


            List<WidokModel> zasobWidokLista = listauprawnien.Select(x => new WidokModel
            {
                ZasobNazwa = x.nazwa

            }).ToList();

            return PartialView("Partial3", zasobWidokLista);
        }



    }
}
