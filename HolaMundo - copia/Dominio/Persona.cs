using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Persona
    {
        int IdPersona { get; set; }
        String Nombre { get; set; }

        public IEnumerable<Persona> FindAll()
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
                    List<Persona> retorno = new List<Persona>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            retorno.Add(PersonaDesdeFila(dr));
                        }
                    }
                    
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

        private Persona PersonaDesdeFila(IDataRecord fila)
        {
            return new Persona
            {
                IdPersona = 1,
                Nombre = "asd"
            };
        }

    }
}
