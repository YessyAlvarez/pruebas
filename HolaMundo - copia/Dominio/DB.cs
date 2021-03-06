﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Dominio
{
    internal class DB
    {
        private static string cadenaConexion =
            ConfigurationManager.
            ConnectionStrings["cnnHolaMundo"].ConnectionString;

        internal static SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        internal static bool AbrirConexion(SqlConnection cn)
        {
            try
            {
                if (cn.State == System.Data.ConnectionState.Closed)
                {
                    cn.Open();
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.Fail(ex.ErrorCode + " " + ex.Message);
                return false;
            }
        }

        internal static bool CerrarConexion(SqlConnection cn)
        {
            try
            {
                if (cn.State != System.Data.ConnectionState.Closed)
                {
                    cn.Close();
                    cn.Dispose();
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.Fail(ex.ErrorCode + " " + ex.Message);
                return false;
            }
        }

        public static string MensajeExcepcion(SqlException ex)
        {
            string mensaje = ex.Message;
            if (ex.Number == 2627 || ex.Number == 2601)
            {
                mensaje = " No se pueden registrar duplicados. " + mensaje;
            }
            return mensaje;
        }


    }
}
