using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace EjemploRedimensionFotos
{
    public partial class Index : System.Web.UI.Page
    {

        string cadenaConexion = "Data Source=DESKTOP-39925CU\\SQLEXPRESS;Initial Catalog = PruebaFotos;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {

            //Obtener datos de la imagen
            int imgSize = uplImg.PostedFile.ContentLength;
            byte[] imgOriginal = new byte[imgSize];
            uplImg.PostedFile.InputStream.Read(imgOriginal, 0, imgSize);
            Bitmap imgOriginalBinaria = new Bitmap(uplImg.PostedFile.InputStream);



            //Crear una imagen Thumbnail

            System.Drawing.Image imtThumbnail;
            int TamanioThumbnail = 200;
            imtThumbnail = RedimencionarImagen(imgOriginalBinaria, TamanioThumbnail);
            byte[] bImagenThumbnail = new byte[TamanioThumbnail];

            ImageConverter Convertidor = new ImageConverter();
            bImagenThumbnail = (byte[])Convertidor.ConvertTo(imtThumbnail, typeof(byte[]));





            //Insertar en la base de datos

            SqlConnection conexionSql = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Imagenes(Imagen,Titulo) VALUES (@imagen,@titulo)";
            cmd.Parameters.Add("@imagen", SqlDbType.Image).Value = bImagenThumbnail;
            cmd.Parameters.Add("@titulo", SqlDbType.Text).Value = txtTitulo.Text;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSql;
            conexionSql.Open();
            cmd.ExecuteNonQuery();



            //Preview
            string imgDataURL64 = "data:image/jpg;base64, " + Convert.ToBase64String(bImagenThumbnail);
            imgPreview.ImageUrl = imgDataURL64;

        }


        public System.Drawing.Image RedimencionarImagen(System.Drawing.Image ImagenOriginal, int Alto)
        {
            var Radio = (double)Alto / ImagenOriginal.Height;
            var NuevoAncho = (int)(ImagenOriginal.Width * Radio);
            var NuevoAlto = (int)(ImagenOriginal.Height * Radio);
            var NuevaImagenRedimencionada = new Bitmap(NuevoAncho, NuevoAlto);
            var g = Graphics.FromImage(NuevaImagenRedimencionada);
            g.DrawImage(ImagenOriginal, 0, 0, NuevoAncho, NuevoAlto);

            return NuevaImagenRedimencionada;
        }

 
    }
}