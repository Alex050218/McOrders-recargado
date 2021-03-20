using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Caja_Macdonalds
{
    public partial class Pedidos
    {
        public Pedidos()
        {
            InitializeComponent();

            // sets the order to 1 when starting
            lblNumPedido.Text = OrderNumber.ToString();
            panelSpecial.Visible = false;
            panelTacos.BringToFront();

            string Pedido = UltimoPedido;

            if (Pedido != null)
            {
                txtOrder.Text = Pedido;
                txtOrder.TextAlign = HorizontalAlignment.Left;
            }
        }

        // specials to show in the panel
        private readonly Dictionary<string, string[]> SpIngredients= new Dictionary<string, string[]>
        {

            {
                "Hamburguesa",
                new[]
                    { "Con Todo", "Queso", "Queso blanco", "Lechuga", "Tomate", "Cebolla", "Mostaza", "Catsup","Mayonesa", "Chiles toreados" }
            },

            {
                "Hotdog",
                new[]
                    { "Con Todo", "Tomate", "Pepinillos", "Mostaza", "Mayonesa", "Crema", "Queso", "Tocino", "Chiles jalapeños", "Chiles toreados" }
            }

        };

        private void Ingredients(string Producto)
        {
            Specials(Producto);

            // hamburguer has only two ingredients to select
            BtnSize2.Visible = Producto != "Hamburguesa";
            BtnSpecial.Location = Producto == "Hamburguesa"
                ? new Point(122, 33)
                : new Point(115, 54);

            // torta does not have any especial
            BtnSize2.Location = Producto == "Torta"
                ? new Point(122, 28)
                : new Point(122, 18);
            BtnSpecial.Visible = Producto != "Torta";

            panelSpecial.Visible = BtnSpecial.Visible;

            switch (Producto)
            {
                case "Tacos":

                    BtnSize1.Text = "Trompo";
                    BtnSize3.Text = "Bisteck";
                    BtnSize2.Text = "Campechana";
                    break;

                case "Hamburguesa":
                    BtnSize1.Text = "Especial";
                    BtnSize3.Text = "Doble carne";
                    break;

                case "Hotdog":
                    BtnSize1.Text = "Normal";
                    BtnSize3.Text = "Mediana";
                    BtnSize2.Text = "Jumbo";
                    break;

                case "Torta":

                    BtnSize1.Text = "De Cerdo";
                    BtnSize3.Text = "De Carne";
                    BtnSize2.Text = "Cubana";
                    break;
            }
        }

        private void Specials(string Product)
        {
            panelSpecial.Visible = Product != "Tacos";
            panelTacos.Visible = Product == "Tacos";

            switch (Product)
            {
                case "Hamburguesa":
                    AssignEspButton(Product);
                    panelSpecial.BringToFront();
                    break;

                case "Hotdog":
                    AssignEspButton(Product);
                    panelSpecial.BringToFront();
                    break;

                default:
                    panelTacos.BringToFront();
                    break;
            }
        }

        private void AssignEspButton(string Product)
        {

            // I could not figure out how to do this so I went to the bad solution

            string[] ListaIng = SpIngredients[Product];

            Esp1.Text = ListaIng[0];
            Esp2.Text = ListaIng[1];
            Esp3.Text = ListaIng[2];
            Esp4.Text = ListaIng[3];
            Esp5.Text = ListaIng[4];
            Esp6.Text = ListaIng[5];
            Esp7.Text = ListaIng[6];
            Esp8.Text = ListaIng[7];
            Esp9.Text = ListaIng[8];
            Esp10.Text = ListaIng[9];
        }
    }
}
