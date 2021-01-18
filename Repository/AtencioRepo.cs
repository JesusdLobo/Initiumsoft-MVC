using Dapper;
using Initiumsoft_MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Initiumsoft_MVC.Repository
{
    public class AtencioRepo
    {

        SqlConnection con;
        //Se crea la Coneccion desde el Web.Config
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["InitiumsoftConnectionString"].ToString();
            con = new SqlConnection(constr);
        }
        //Agrega a la SP con un Parametor Output
        public string AddSolicitud(AtencionCliente Obj)
        {
            DynamicParameters ObjParm = new DynamicParameters();
            ObjParm.Add("@CI", Obj.CI);
            ObjParm.Add("@Nombre", Obj.Nombre);
            ObjParm.Add("@IDAtencion", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            ObjParm.Add("@Ret", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);   

            connection();
            con.Open();
            con.Execute("[CreateAtencionTurno]", ObjParm, commandType: CommandType.StoredProcedure);
             

            //Retorna el Valor del Output
            var IDAtencion = ObjParm.Get<string>("@IDAtencion");

            int Valuereturn = ObjParm.Get<int>("@Ret");  

            con.Close();
            return    Convert.ToInt32(Valuereturn).ToString(); 

        }




        public string Updatedolicitud(AtencionCliente ObjTiw)
        {
            DynamicParameters ObjParm = new DynamicParameters();
            ObjParm.Add("@IDAtencion", ObjTiw.IDAtencion);            
            ObjParm.Add("@Ret", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            connection();
            con.Open();
            con.Execute("[UpdateAtencionTurno]", ObjParm, commandType: CommandType.StoredProcedure);


       

            int Valuereturn = ObjParm.Get<int>("@Ret");

            con.Close();
            return Convert.ToInt32(Valuereturn).ToString();

        }
    }
}