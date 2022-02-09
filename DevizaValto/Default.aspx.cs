/*
 *  Készítette:     Vanyó Tamás
 *  Leírás:         Deviza árfolyamok felhasználói felület
 *  2022.02.08:     Létrehozás
 *  2022.02.09:     Kivételkezelés beépítése
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevizaValto
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Lap betöltése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                Alaphelyzet();
        }

        /// <summary>
        /// Alaphelyzetbe állítja a kontrollokat
        /// </summary>
        private void Alaphelyzet()
        {
            try
            {
                lblHiba.Text = "";
                Deviza deviza = new Deviza(Properties.Resources.DevizaXml);
                ddlValutak.DataSource = deviza.Devizak;
                ddlValutak.DataTextField = "Key";
                ddlValutak.DataValueField = "Value";
                ddlValutak.DataBind();
                lblDatum.Text = Convert.ToDateTime(deviza.Datum).ToShortDateString();
                hifHUF.Value = deviza.HUF;
                txbValtando.Focus();

            }
            catch (DevizaXMLNemOlvashatoKivetel)
            {
                Hiba(true, Properties.Resources.HibaDevizaXMLNemOlvashato);
            }
            catch (HUFDevizaNincsKivetel)
            {
                Hiba(true, Properties.Resources.HibaHUFDevizaNincs);
            }
            catch (NincsDatumKivetel)
            {
                Hiba(true, Properties.Resources.HibaNincsDatum);
            }
        }

        /// <summary>
        /// Devizaváltás gomb eseménykezelő
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnValt_Click(object sender, EventArgs e)
        {
            try
            {
                lblHiba.Text = "";
                // filléreket nem írom ki
                txbForint.Text = String.Format("{0:N0}",
                    Deviza.DevizaValtas(
                        Convert.ToDouble(txbValtando.Text),
                        Convert.ToDouble(ddlValutak.SelectedItem.Value, System.Globalization.CultureInfo.InvariantCulture),
                        Convert.ToDouble(hifHUF.Value, System.Globalization.CultureInfo.InvariantCulture)));
            }
            catch (NullasDevizaKivetel)
            {
                Hiba(false, Properties.Resources.HibaNullasDeviza);
            }

        }

        /// <summary>
        /// Hibakezelés
        /// </summary>
        /// <param name="kritikus">Kritikus-e a hiba. Ha igen, akkor letilt minden kontrollt.</param>
        /// <param name="uzenet">Kiírandó üzenet szövege</param>
        private void Hiba(bool kritikus, string uzenet)
        {
            lblHiba.Text = uzenet;
            if (kritikus)
            {
                txbValtando.Enabled = false;
                txbForint.Enabled = false;
                ddlValutak.Enabled = false;
                btnValt.Enabled = false;
            }
        }
    }
}