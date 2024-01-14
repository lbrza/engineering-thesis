//-----------------------------------------------------------------------------------
// Quark lightweight hash function - C# implementation.
// 
// File:        Program.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: Main driver program.
// This file is part of my thesis for my Computer Engineering degree at UADE (Bachelor and Master according to wes.org).
// Thesis available in Spanish at https://repositorio.uade.edu.ar/xmlui/handle/123456789/13847
// Based on the original specification by Jean-Philippe Aumasson, Luca Henzen, Willi Meier & María Naya-Plasencia.
// https://link.springer.com/chapter/10.1007/978-3-642-15031-9_1
//-----------------------------------------------------------------------------------
using System;

namespace quark_cs
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Quark Lightweight Hash Function in C#");
            TestVector1();
        }

        public static void TestVector1()
        {
            byte[] digest = new byte[Constants.MAXDIGEST];

            string mess = "";
            Console.WriteLine(mess);
            Quark(ref digest, mess, (ulong)mess.Length);
            printDigest(ref digest);
        }
      
        public static void printDigest(ref byte[] digest)
        {
            int i;
            for (i = 0; i < Constants.MAXDIGEST / 8; i++)
                Console.Write("{0:X}", digest[i]);
            Console.WriteLine();
        }
    }
}
