using System;
using System.IO;
using System.Windows.Forms;

namespace Caja_Macdonalds
{
    public partial class Pedidos
    {
        private string Folder
        {
            get
            {
                string Directorio = @"C:\Pedidos";

                if (!Directory.Exists(Directorio))
                {
                    Directory.CreateDirectory(Directorio);
                }

                return Directorio;
            }
        }

        private string UltimoPedido
        {
            get
            {
                string DirUltimoAgregado = Path.Combine(Folder, "UltimoAgregado.txt");

                if (File.Exists(DirUltimoAgregado))
                {
                    string DirPedido = null;
                    using (StreamReader UltimoArchivo = new StreamReader(DirUltimoAgregado))
                    {
                        DirPedido = UltimoArchivo.ReadLine();
                    }

                    if ((!File.Exists(DirPedido)) || DirPedido == null)
                    {
                        return null;
                    }

                    string Pedido = null;
                    using(StreamReader UltimoPedido = new StreamReader(DirPedido))
                    {
                        Pedido = UltimoPedido.ReadToEnd();
                    }
                    return Pedido;
                }
                return null;
            }

            set
            {
                string DirUltimoAgregado = Path.Combine(Folder, "UltimoAgregado.txt");

                if (File.Exists(DirUltimoAgregado))
                {
                    File.Delete(DirUltimoAgregado);
                }

                using (StreamWriter NuevoArchivo = new StreamWriter(DirUltimoAgregado, true))
                {
                    NuevoArchivo.Write(value);
                }
            }
        }

        public void GuardarPedido()
        {
            string NombreArchivo = "P" + OrderNumber.ToString() + DateTime.Now.ToString(" dd-MM-yyyy HH-mm-ss") + ".txt";
            string NuevoPedido = Path.Combine(Folder, NombreArchivo);

            UltimoPedido = NuevoPedido;

            StreamWriter sw = new StreamWriter(NuevoPedido);
            sw.WriteLine("McDonalds");
            sw.WriteLine("---------------------------------------------------");
            sw.WriteLine("Fecha: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            sw.WriteLine("Producto: " + Order["Producto"]);
            sw.WriteLine("Ingrediente/Tamaño: " + Order["Ingrediente"]);
            sw.WriteLine("Ordenes: " + Order["Cantidad"]);

            if (Order["Especial"] != null)
            {
                sw.WriteLine("Extra: " + Order["Especial"]);
            }

            sw.WriteLine("Precio: " + Order["Precio"] + "$");
            sw.Close();

            txtOrder.TextAlign = HorizontalAlignment.Center;
            MessageBox.Show("Ticket Generado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void VerPedido(object sender, EventArgs e)
        {
            // Un fileDialog que busque un archivo y lo ponga en la caja del ticket

            // A partir de aqui

            // No borrar esta linea
            lblTicket.Text = "Ticket seleccionado";
        }
    }
}
