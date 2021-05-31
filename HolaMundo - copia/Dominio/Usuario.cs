using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string Password { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }


        



        public IEnumerable<Usuario> FindAll()
        {
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlCommand cmd = new SqlCommand(@"SELECT *
                                                FROM Usuario", cnn);
                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Usuario> retorno = new List<Usuario>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            retorno.Add(UsuarioDesdeFila(dr));
                        }
                    }

                    //IEnumerable<Usuario> ret = retorno.OrderBy(m => m.Anho).ThenBy(m => m.Titulo);
                    DB.CerrarConexion(cnn);
                    return retorno;
                }

            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.Message);
                return null;
            }
        }




        private Usuario UsuarioDesdeFila(IDataRecord fila)
        {     
            return new Usuario
            {                    
                Cedula = fila["cedula"] != DBNull.Value ? fila["cedula"].ToString() : "-",
                Password = fila["contrasenha"] != DBNull.Value ? fila["contrasenha"].ToString() : "-"
            };  
        }


    }
}
