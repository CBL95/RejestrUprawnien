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
            List<Firma> listafirm = db.Firmas.ToList();
            WidokModel widok = new WidokModel();



            List<WidokModel> pracownikWidokLista = listapracownikow.Select(x => new WidokModel
            { PracownikImie = x.imie,
                PracownikNazwisko = x.nazwisko,
            }).ToList();

            //List<WidokModel> firmaWidokLista = listafirm.Select(x => new WidokModel
            //{ FirmaNazwa = x.nazwa,
            //    FirmaId = x.id }).ToList();

            return View(pracownikWidokLista);
        }

           
    }
}
