using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Caja_Macdonalds
{
    public partial class Pedidos : Form
    {
        private int OrderNumber = 1;

        private string BotonSeleccionado;

        private readonly Dictionary<string, int> PreciosProductos = new Dictionary<string, int>()
        {
            { "Hamburguesa", 40 },
            { "Hotdog", 20 },
            { "Torta", 20 },
            { "Tacos", 30 }
        };

        private readonly Dictionary<string, int> PreciosExtras = new Dictionary<string, int>()
        {
            { "Especial", 15 },
            { "Size1", 10 },
            { "Size2", 15 },
            { "Size3", 20 }
        };

        private readonly Dictionary<string, string> Order = new Dictionary<string, string>()
        {
            { "Producto", null },
            { "Ingrediente", null },
            { "Especial", null },
            { "Cantidad", "0"  },
            { "Precio", null }
        };

        private void SelecProducto(object sender, EventArgs e)
        {

            // sets the selected product into handable data
            RadioButton SelButton = (RadioButton)sender;
            string SelectedBtn = SelButton.Text;

            Order["Producto"] = SelectedBtn;
            Order["Especial"] = null;
            Order["Cantidad"] = "0";

            // Show avaible ingredients
            Ingredients(SelectedBtn);
            LimpiarSelecciones();

            panelSize.Enabled = true;
            PanelEsp.Enabled = false;

            BtnSpecial.Enabled = false;
            BtnOrder.Enabled = false;
        }

        private void LimpiarSelecciones()
        {
            foreach (Control control in panelSize.Controls)
            {
                if (control is RadioButton btn)
                {
                    btn.Checked = false;
                }
            }

            CajaCantidad.Text = "";
            BtnSpecial.Checked = false;
        }

        private void SelecIngrediente(object sender, EventArgs e)
        {
            // Sets the ingredients to handable data
            RadioButton SelButton = (RadioButton)sender;
            string SelIngredient = SelButton.Text;

            EspCheese.Text = SelIngredient == "Campechana"
                ? "Sin queso"
                : "Con queso";

            Order["Ingrediente"] = SelIngredient;
            Order["Especial"] = null;
            BotonSeleccionado = SelButton.Name.Substring(3);

            BtnSpecial.Enabled = true;
            BtnOrder.Enabled = Order["Cantidad"] != "0";

            // unselect the special button if you change the ingredient of the product
            BtnSpecial.Checked = SelIngredient != Order["Ingrediente"];
        }

        private void BtnEspecial_CheckedChanged(object sender, EventArgs e)
        {
            // deletes the special if the special button is not checked
            if (BtnSpecial.Checked)
            {
                PanelEsp.Enabled = true;
                BtnOrder.Enabled = false;
                BtnOrder.Enabled = Order["Especial"] != null;
            }
            else
            {
                PanelEsp.Enabled = false;
                BtnOrder.Enabled = Order["Cantidad"] != "0";
                Order["Especial"] = null;
            }

            LimpiarPanelesEspeciales();
        }

        private void LimpiarPanelesEspeciales()
        {
            foreach (Control control in panelSpecial.Controls)
            {
                if (control is RadioButton btn)
                {
                    btn.Checked = false;
                }
            }

            foreach (Control control in panelTacos.Controls)
            {
                if (control is RadioButton btn)
                {
                    btn.Checked = false;
                }
            }
        }

        private void SelecEspecial(object sender, EventArgs e)
        {
            // sets the special to order by the costumer
            RadioButton BtnSel = (RadioButton)sender;
            string SelSpecial = BtnSel.Text;

            Order["Especial"] = SelSpecial;
            BtnOrder.Enabled = Order["Cantidad"] != "0" && Order["Especial"] != null;
        }

        // order button
        private void BtnPedido_Click(object sender, EventArgs e)
        {
            int PrecioTotal = AsignarPrecios();
            Order["Precio"] = PrecioTotal.ToString();

            // if there is an ordered special
            string ExtraText = Order["Especial"] == null
                ? ""
                : Environment.NewLine +
                  "----------------------------------------" +
                        Environment.NewLine +

                    "Especial: " + Order["Especial"];

            // shows the last order
            txtOrder.Text =
                "Orden Numero: " + OrderNumber.ToString() +

                    Environment.NewLine +
                "----------------------------------------" +
                    Environment.NewLine +

                "Producto: " + Order["Producto"] +

                        Environment.NewLine +
                "----------------------------------------" +
                        Environment.NewLine +

                "Tamaño/Ingrediente: " + Order["Ingrediente"] +

                        Environment.NewLine +
                "----------------------------------------" +
                        Environment.NewLine +

                $"Cantidad: {Order["Cantidad"]}, Precio: {Order["Precio"]}$"
                + ExtraText;

            OrderNumber += 1;
            lblNumPedido.Text = $"{OrderNumber}";
            GuardarPedido();
        }

        private int AsignarPrecios()
        {
            int PrecioExtraTam = PreciosExtras[BotonSeleccionado];
            int PrecioEspecial = Order["Especial"] != null ? PreciosExtras["Especial"] : 0;

            string ProdSeleccionado = Order["Producto"];
            int PrecioProducto = PreciosProductos[ProdSeleccionado];

            int CantidadComida = Convert.ToInt32(Order["Cantidad"]);
            int PrecioNeto = PrecioProducto + PrecioEspecial + PrecioExtraTam;

            int PrecioFinal = CantidadComida * PrecioNeto;
            return PrecioFinal;
        }

        private void CajaCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Order["Cantidad"] = CajaCantidad.Text;
            BtnOrder.Enabled = CajaCantidad.Text != "0" && Order["Ingrediente"] != null;
        }

        private void CajaCantidad_TextChanged(object sender, EventArgs e)
        {
            string ValorIntroducido = CajaCantidad.Text;

            if (!Regex.IsMatch(ValorIntroducido, @"^\d{1,3}$"))
            {
                CajaCantidad.Text = "";
                Order["Cantidad"] = "0";
                BtnOrder.Enabled = false;
            } else
            {
                BtnOrder.Enabled = BtnSpecial.Checked
                    ? ValorIntroducido != "0" && Order["Especial"] != null
                    : ValorIntroducido != "0" && Order["Ingrediente"] != null;

                Order["Cantidad"] = ValorIntroducido == "0"
                    ? "0"
                    : ValorIntroducido;
            }
        }
    }
}
