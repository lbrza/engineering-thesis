//-----------------------------------------------------------------------------------
// Spongent lightweight hash function - C# implementation.
// 
// File:        Program.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: Main driver program.
// This file is part of my thesis for my Computer Engineering degree at UADE (Bachelor and Master according to wes.org).
// Thesis available in Spanish at https://repositorio.uade.edu.ar/xmlui/handle/123456789/13847
// Based on the size-optimized Spongent hashing algorithm for ATtiny45 by Wouter de Groot.
// https://github.com/weedegee/spongent-avr
// Based on the original specification by Andrey Bogdanov, Miroslav Knežević, Gregor Leander, Deniz Toz, Kerem Varıcı & Ingrid Verbauwhede.
// https://link.springer.com/chapter/10.1007/978-3-642-23951-9_21
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace spongent_cs
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Spongent Lightweight Hash Function in C#. UADE - LMB.");
            TestVector1();
        }

        public static void TestVector1()
        {
            Console.Write("Ingrese mensaje: ");
            string mess = Console.ReadLine();

            byte[] output = new byte[32];

            spongent(mess, output);
            PrintDigest(ref output);
        }

        public static void PrintDigest(ref byte[] output)
        {
            
            int i;
            for (i = 0; i < Constants.B + 1; i++)
                Console.Write("{0:X}", output[i]);
            Console.WriteLine();
            
        }
    }
}
