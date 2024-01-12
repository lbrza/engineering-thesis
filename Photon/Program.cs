//-----------------------------------------------------------------------------------
// Photon lightweight hash function - C# implementation.
// 
// File:        Photon.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: Main driver program.
// This file is part of my thesis for my Computer Engineering degree at UADE (Bachelor and Master according to wes.org).
// Thesis available in Spanish at https://repositorio.uade.edu.ar/xmlui/handle/123456789/13847
// Based on the original code by Jian Guo, Thomas Peyrin and Axel Poschmann.
// https://link.springer.com/chapter/10.1007/978-3-642-22792-9_13
//-----------------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace photoncs
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Photon Lightweight Hash Function in C#. UADE - LMB.");
            TestVector1();
        }
        

        public static void TestVector1()
        {
            byte[] digest = new byte[Constants.DIGESTSIZE / 8];

            //Memory usage
            Process proc = Process.GetCurrentProcess();
            //Console.WriteLine("Memory usage 1: {0}", proc.PrivateMemorySize64.ToString());

            Console.Write("Ingrese mensaje: ");
            string mess = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Mensaje: " + mess);
            Console.Write("Digesto: ");
            hash(ref digest, mess, mess.Length * 8);
            printDigest(digest);

            //Console.WriteLine("Memory usage 2: {0}", proc.PrivateMemorySize64.ToString());
            Console.ReadLine();
        }

    }
}
