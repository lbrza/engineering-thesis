//-----------------------------------------------------------------------------------
// Quark lightweight hash function - C# implementation.
// 
// File:        Constants.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: File containing constants.
// This file is part of my thesis for my Computer Engineering degree at UADE (Bachelor and Master according to wes.org).
// Thesis available in Spanish at https://repositorio.uade.edu.ar/xmlui/handle/123456789/13847
// Based on the original specification by Jean-Philippe Aumasson, Luca Henzen, Willi Meier & María Naya-Plasencia.
// https://link.springer.com/chapter/10.1007/978-3-642-15031-9_1
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace quark_cs
{
    public static class Constants
    {
        public const int MAXDIGEST = 48;

        public const int RATE = 1;
        public const int WIDTH = 17;
        public static readonly byte[] IV = new byte[] { 0xd8,0xda,0xca,0x44,0x41,0x4a,0x09,0x97,
                                                        0x19,0xc8,0x0a,0xa3,0xaf,0x06,0x56,0x44,0xdb };

        public const int ROUNDS_U = 4 * 136;
        public const int N_LEN_U = 68;
        public const int L_LEN_U = 10;

        public const int DIGEST = WIDTH;
    }
}
