/*
 *  Készítette:     Vanyó Tamás
 *  Leírás:         Deviza árfolyamok üzleti logika
 *  2022.02.09:     Létrehozás
 */

using System;

namespace DevizaValto
{
    /// <summary>
    /// Ősosztály a feladat kivételeihez
    /// </summary>
    public class DevizaKivetelek : Exception
    {
    }

    /// <summary>
    /// XML fájl olvasási hiba
    /// </summary>
    public class DevizaXMLNemOlvashatoKivetel : DevizaKivetelek
    {
    }

    /// <summary>
    /// Nincs dátum az XML fájlban
    /// </summary>
    public class NincsDatumKivetel : DevizaKivetelek
    {
    }

    /// <summary>
    /// HUF deviza nincs hiba
    /// </summary>
    public class HUFDevizaNincsKivetel : DevizaKivetelek
    {
    }

    /// <summary>
    /// 0 a deviza árfolyama (nullával való osztást eredményezne)
    /// </summary>
    public class NullasDevizaKivetel : DevizaKivetelek
    {
    }
}