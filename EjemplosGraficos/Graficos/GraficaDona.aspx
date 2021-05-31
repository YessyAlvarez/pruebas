<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraficaDona.aspx.cs" Inherits="Graficos.GraficaDona" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
      google.charts.load('current', {'packages':['corechart']});
      google.charts.setOnLoadCallback(drawChart);

      function drawChart() {

        var data = google.visualization.arrayToDataTable(<%=obtenerDatos()%>);

        var options = {
          title: 'Estadisticas de lenguajes'
          };


          var chart = new google.visualization.PieChart(document.getElementById('piechart'));

            //Eventos sobre la gráfica

          google.visualization.events.addListener(chart, 'select', function () {

              var elementoSeleccionado = chart.getSelection()[0];
              if (elementoSeleccionado) {
                  var valorSeleccionado = data.getValue(elementoSeleccionado.row, 2);

                  alert(valorSeleccionado);
              }

          });



        chart.draw(data, options);
      }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="piechart" style="width: 900px; height: 500px;"></div>
        </div>
    </form>
</body>
</html>
