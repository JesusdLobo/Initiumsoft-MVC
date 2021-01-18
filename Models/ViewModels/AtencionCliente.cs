using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Initiumsoft_MVC.Models.ViewModels
{
    public class AtencionCliente
    {
        public int IDAtencion { get; set; }

        public int Status { get; set; }


        [Display(Name = "C.I")]
    
        public int CI { get; set; }


        [Display(Name = "Ingrese Nombre")]
   
        public String Nombre { get; set; }
    }
}