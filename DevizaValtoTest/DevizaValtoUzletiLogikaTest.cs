/*
 *  Készítette:     Vanyó Tamás
 *  Leírás:         Devizaváltó üzeleti logika tesztelése
 *  2022.02.09:     Létrehozás
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DevizaValtoTest
{
    [TestClass]
    public class DevizaValtoUzletiLogikaTest
    {
        /// <summary>
        /// Rossz xml fájl olvasása
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DevizaValto.DevizaXMLNemOlvashatoKivetel), "Nem volt kivétel!")]
        public void RosszXmlFajlOlvasas()
        {
            DevizaValto.Deviza d = new DevizaValto.Deviza("a.a");
            string huf = d.HUF;
        }

        /// <summary>
        /// Nincs dátum az xml fájlban
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DevizaValto.NincsDatumKivetel), "Nem volt kivétel!")]
        public void NincsDatum()
        {
            File.WriteAllText(@"C:\Temp\Nincsdatum.xml", Properties.Resources.NincsDatum);
            DevizaValto.Deviza d = new DevizaValto.Deviza(@"C:\Temp\Nincsdatum.xml");
            string datum = d.Datum;
        }

        /// <summary>
        /// Nincs HUF valuta az Xml fájlban
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DevizaValto.HUFDevizaNincsKivetel), "Nem volt kivétel!")]
        public void NincsHUF()
        {
            File.WriteAllText(@"C:\Temp\Nincshuf.xml", Properties.Resources.NincsHUF);
            DevizaValto.Deviza d = new DevizaValto.Deviza(@"C:\Temp\Nincshuf.xml");
            string huf = d.HUF;
        }

        /// <summary>
        /// Jól működik-e a deviza váltás
        /// </summary>
        [TestMethod]
        public void DevizaValtas()
        {
            Assert.AreEqual(DevizaValto.Deviza.DevizaValtas(1.0d, 300.0d, 360.0d),
                (1.0d * 360.0d / 300.0d));
        }

        /// <summary>
        /// Nullás deviza váltó érték esetén kivétel kezelés
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DevizaValto.NullasDevizaKivetel), "Nem volt kivétel!")]
        public void DevizaValtasNullas()
        {
            Double d = DevizaValto.Deviza.DevizaValtas(1.0d, 0.0d, 360.0d);
        }
    }
}
