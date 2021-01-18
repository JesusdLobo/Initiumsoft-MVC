using Dapper;
using Initiumsoft_MVC.Models;
using Initiumsoft_MVC.Models.ViewModels;
using Initiumsoft_MVC.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Initiumsoft_MVC.Controllers
{
    public class HomeController : Controller
    {

        private InitiumsoftEntities1 db = new InitiumsoftEntities1();

        public ActionResult Index()
        {
            using (InitiumsoftEntities1 db = new InitiumsoftEntities1())
            {
                List<ViewAtencionTurnoTipoOne_Result> ColaUno = db.ViewAtencionTurnoTipoOne().ToList();
                ViewBag.ColaOne = ColaUno;
                List<ViewAtencionTurnoTipoTwo_Result> ColaDos = db.ViewAtencionTurnoTipoTwo().ToList();
                ViewBag.ColaTow = ColaDos;


            }

            return View();



        }
        [HttpPost]
        public ActionResult Index(AtencionCliente ObjComp)
        {
            try
            {
                AtencioRepo Obj = new AtencioRepo();

                
            if (Obj.AddSolicitud(ObjComp) == "1")
            {
                //ViewBag.ComplaintId = "Estimado Cliente ya Tiene un Ticket para Atencion";



                ViewBag.JavaScriptFunction = "CLienteYaCuen();";
 

            }
            else
            {
                ViewBag.ComplaintId = "Turno Asignado";

            }





            using (InitiumsoftEntities1 db = new InitiumsoftEntities1())
                {
                    List<ViewAtencionTurnoTipoOne_Result> ColaUno = db.ViewAtencionTurnoTipoOne().ToList();
                    ViewBag.ColaOne = ColaUno;
                    List<ViewAtencionTurnoTipoTwo_Result> ColaDos = db.ViewAtencionTurnoTipoTwo().ToList();
                    ViewBag.ColaTow = ColaDos;


                }

            }
            catch (Exception)
            {

                ViewBag.ComplaintId = "Error ";
            }




            ModelState.Clear();
            return View();



        }
         



         public ActionResult  Turno(int ID)
        {



            AtencionCliente model = new AtencionCliente();
            using (InitiumsoftEntities1 db = new InitiumsoftEntities1())
            {
                var oTabla = db.Atencion.Find(ID);
                model.Nombre = oTabla.Nombre;
                model.Status = (int)oTabla.Status;
                model.CI = (int)oTabla.CI;
                model.IDAtencion = oTabla.IDAtencion;

            }
            return View(model);
 
        }

        [HttpPost]
        public ActionResult TurnoAdd(AtencionCliente model)
        {

            if (ModelState.IsValid)
            {



                using (InitiumsoftEntities1 db = new InitiumsoftEntities1())
                {
                    var oTabla = db.Atencion.Find(model.IDAtencion);

                    oTabla.Status = 0;


                    db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }



                return Redirect("Index");

            }
            return View(model);
        }

    }
}