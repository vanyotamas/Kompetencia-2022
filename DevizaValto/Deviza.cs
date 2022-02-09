/*
 *  Készítette:     Vanyó Tamás
 *  Leírás:         Deviza árfolyamok üzleti logika
 *  2022.02.08:     Létrehozás
 *  2022.02.09:     Kivételkezelés beépítése
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DevizaValto
{
    /// <summary>
    /// Üzleti logikai réteg
    /// Deviza osztály a devizák tárolására és a számoláshoz
    /// </summary>
    public class Deviza
    {
        public Deviza(string fajlnev)
        {
            xmlfajlnev = fajlnev;
        }

        /// <summary>
        /// Beolvadandó fájl neve
        /// </summary>
        private string xmlfajlnev;

        /// <summary>
        /// currency és rate tárolására való tömb
        /// </summary>
        private SortedDictionary<string, string> devizak;
        public SortedDictionary<string, string> Devizak
        {
            get 
            {
                if (devizak == null)
                    Beolvas();
                return devizak;
            }
        }

        /// <summary>
        /// xml fájl belső napi dátuma
        /// </summary>
        private string datum;
        public string Datum
        {
            get
            {
                if (datum == null)
                    Beolvas();
                return datum;
            }
        }

        /// <summary>
        /// EUR/HUF árfolyam változója
        /// </summary>
        private string huf;
        public string HUF
        {
            get
            {
                if (huf == null)
                    Beolvas();
                return huf;
            }
        }

        /// <summary>
        /// Xml fájl beolvasása, aktuális dátum és a devizanemek feltöltése
        /// </summary>
        private void Beolvas()
        {
            devizak = new SortedDictionary<string, string>();
            XDocument xml = new XDocument();
            try
            {
                xml = XDocument.Load(xmlfajlnev);
            }
            catch
            {
                // ha bármi okból nem olvasható az xml fájl, kivétel dobása
                throw new DevizaXMLNemOlvashatoKivetel();
            }

            foreach (XElement node in xml.Descendants())
            {
                // csak a Cube node-ok kellenek
                if (node.Name.LocalName == "Cube")
                {
                    if (node.HasAttributes)
                    {
                        // napi dátum kiolvasása
                        if (node.Attribute("time") != null)
                            datum = node.Attribute("time").Value;

                        // árfolyam kiolvasása
                        // nem túl elengáns, hogy a Cube node kétféle is lehet :-(
                        if (node.Attribute("currency") != null)
                        {
                            Devizak.Add(node.Attribute("currency").Value, node.Attribute("rate").Value);
                        }
                    }
                }
            }

            try
            {
                // a magyar deviza értékének eltárolása
                huf = Devizak.First(x => x.Key == "HUF").Value;

                // magyar deviza törlése, mert nincs értelme a HUF -> HUF konverziónak
                Devizak.Remove("HUF");
            }
            catch
            {
                // ha nincs HUF, kivétel dobása
                throw new HUFDevizaNincsKivetel();
            }

            // mivel a valuták Euró alapúak, a feladatben pedig forintra kell konvertálni
            // ezért hozzádásra kerül az euró valuta az EUR -> HUF konverzióhoz 
            Devizak.Add("EUR", "1.0");

            // nincs dátum, valószínűleg hibás az xml fájl
            if (datum == null)
                throw new NincsDatumKivetel();
        }

        /// <summary>
        /// Kiszámolja a megadott deviza forint értékét
        /// </summary>
        /// <param name="valtando">Váltandó összeg</param>
        /// <param name="valutaarfolyam">Valuta árfolyama</param>
        /// <param name="eurhufarfolyam">Euró/forint árfolyama</param>
        /// <returns>Kiszámolt érték. Ha nullával osztott, akkor saját kivételt dob.</returns>
        public static Double DevizaValtas(Double valtando, Double valutaarfolyam, Double eurhufarfolyam)
        {
            Double dv = 0;
                dv = valtando * eurhufarfolyam / valutaarfolyam;
            if (Double.IsInfinity(dv))
                throw new NullasDevizaKivetel();
            return dv;
        }
    }
}