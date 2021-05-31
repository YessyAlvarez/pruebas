<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EjemploRedimensionFotos.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    Imagen agregada:
                    <br />
                    <asp:Image ID="imgPreview" Width="200" ImageUrl="https://i.pinimg.com/564x/11/fb/f6/11fbf67c17d18f070d68a4c3cee32565.jpg" runat="server" />
                    <br />
                    <br />
                    Archivo:
                    <asp:FileUpload ID="uplImg" accept=".jpg" runat="server"  CssClass="form-control"/>
                    <br />
                    <br />
                    Titulo de imagen:
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnSubir" runat="server" Text="Adjuntar Imagen" CssClass="btn btn-success" OnClick="btnSubir_Click"/>
                </div>
            </div>


<%--            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
                <ItemTemplate>

                </ItemTemplate>
            </div>--%>

        </div>
    </form>
</body>
</html>
