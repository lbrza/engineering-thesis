//-----------------------------------------------------------------------------------
// Spongent lightweight hash function - C# implementation.
// 
// File:        Constants.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
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
    public static class Constants
    {
        public static readonly byte[] S = new byte[16] { 0xe, 0xd, 0xb, 0x0, 0x2, 0x1, 0x4, 0xf, 0x7, 0xa, 0x8, 0x5, 0x9, 0xc, 0x3, 0x6 };

        public static readonly ushort b = 88;
        public static readonly ushort B = 88 / 8;
        public static readonly ushort n = 88;
        public static readonly byte[] state = new byte[34];
    }
}
