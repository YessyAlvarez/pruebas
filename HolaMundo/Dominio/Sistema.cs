using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private static Sistema instancia;

        public static Sistema Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Sistema();
                }
                return instancia;
            }
        }
        private Sistema()
        {
           
        }

        public Usuario create(Usuario u)
        {
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlTransaction trn = null;
                SqlCommand cmd = new SqlCommand(@"Insert into Usuario values (@cedu, @pass, @mail, @nombre)",cnn);
                cmd.Parameters.AddWithValue("@cedu", u.Cedula);
                cmd.Parameters.AddWithValue("@pass", u.Password);
                cmd.Parameters.AddWithValue("@mail", (string.IsNullOrEmpty(u.Email) ? (object)DBNull.Value : u.Email));
                cmd.Parameters.AddWithValue("@nombre", (string.IsNullOrEmpty(u.Nombre) ? (object)DBNull.Value : u.Nombre));

                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    trn = cnn.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.ExecuteNonQuery();
                    trn.Commit();                    
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.Message);
                return null;
            }

            return u;
        }

        public Boolean updateFile(Archivo a)
        {
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlTransaction trn = null;
                SqlCommand cmd = new SqlCommand(@"Update Archivo SET descargas = @descargas WHERE id = @id", cnn);
                cmd.Parameters.AddWithValue("@id", a.Id);
                cmd.Parameters.AddWithValue("@descargas", a.Descargas);

                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    trn = cnn.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.Message);
                return false;
            }

            return true;
        }

        public Boolean deleteFile(Archivo a)
        {
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlTransaction trn = null;
                SqlCommand cmd = new SqlCommand(@"DELETE Archivo WHERE id = @id", cnn);
                cmd.Parameters.AddWithValue("@id", a.Id);

                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    trn = cnn.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.Message);
                return false;
            }

            return true;
        }

        public Archivo findFileById(Guid FileId)
        {
            Archivo retorno = null;
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlCommand cmd = new SqlCommand(@"SELECT *
                                                FROM Archivo
                                                WHERE id=@id", cnn);
                cmd.Parameters.AddWithValue("@id", FileId);

                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            retorno = archivoDesdeFila(dr);
                        }
                    }

                    //IEnumerable<Usuario> ret = retorno.OrderBy(m => m.Anho).ThenBy(m => m.Titulo);
                    DB.CerrarConexion(cnn);
                    
                }

            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.Message);
                return null;
            }
            return retorno;
        }
        public Boolean createArchivo(Archivo a)
        {
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlTransaction trn = null;
                SqlCommand cmd = new SqlCommand(@"Insert into Archivo values (@id, @creado, @nombre, @extension, @descargas)", cnn);
                cmd.Parameters.AddWithValue("@id", a.Id);
                cmd.Parameters.AddWithValue("@creado", a.Creado);
                cmd.Parameters.AddWithValue("@nombre", a.Nombre);
                cmd.Parameters.AddWithValue("@extension", a.Extension);
                cmd.Parameters.AddWithValue("@descargas", a.Descargas);

                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    trn = cnn.BeginTransaction();
                    cmd.Transaction = trn;
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.Message);
                return false;
            }

            return true;
        }

        public List<Archivo> findAllFiles()
        {
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlCommand cmd = new SqlCommand(@"SELECT *
                                                FROM Archivo", cnn);
                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Archivo> retorno = new List<Archivo>();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            retorno.Add(archivoDesdeFila(dr));
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



        public Usuario Login(Usuario u)
        {
            Usuario retorno = null;
            try
            {
                SqlConnection cnn = DB.CrearConexion();
                SqlCommand cmd = new SqlCommand(@"SELECT cedula
                                                FROM Usuario
                                                WHERE cedula=@cedu AND contrasenha = @pass", cnn);
                cmd.Parameters.AddWithValue("@cedu", u.Cedula);
                cmd.Parameters.AddWithValue("@pass", u.Password);

                using (cnn)
                {
                    DB.AbrirConexion(cnn);
                    SqlDataReader dr = cmd.ExecuteReader();                    
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            retorno= UsuarioDesdeFila(dr);
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

        public IEnumerable<Usuario> FindAllUsers()
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


        private Archivo archivoDesdeFila(IDataRecord fila)
        {
            Archivo file = new Archivo
            {
                Id = (Guid)fila["id"],
                Creado = Convert.ToDateTime(fila["creado"]),
                Nombre = fila["nombre"].ToString(),
                Extension = fila["extension"].ToString(),
                Descargas = (int)fila["descargas"]                
            };

            return file;
        }
 
        private Usuario UsuarioDesdeFila(IDataRecord fila)
        {
            Usuario user = new Usuario
            {
                Cedula = fila["cedula"] != DBNull.Value ? fila["cedula"].ToString() : "-",
               // Password = fila["contrasenha"] != DBNull.Value ? fila["contrasenha"].ToString() : "-"
               Password = "****"
            };
            return user;
        }
    }
}
