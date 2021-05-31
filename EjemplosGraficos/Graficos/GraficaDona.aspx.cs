using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Graficos
{
    public partial class GraficaDona : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected string obtenerDatos(){


            SqlConnection conexionSQL = new SqlConnection("Data Source=LAPTOP-DCCP04KE;Initial Catalog = PruebaGrafica;Integrated Security=True");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Estadistica";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();

            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            conexionSQL.Close();


            string strDatos;
            strDatos = "[['Popularidad','Lenguaje','Descripción'],";

            foreach(DataRow dr in Datos.Rows)
            {
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1] + ",'" + dr[2] + "'";
                strDatos = strDatos + "],";
            }

            strDatos = strDatos + "]";

            return strDatos;

        }
    }
}