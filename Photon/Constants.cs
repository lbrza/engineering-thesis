//-----------------------------------------------------------------------------------
// Photon lightweight hash function - C# implementation.
// 
// File:        Constants.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: File containing constants.
// This file is part of my thesis for my Master of Engineer in Information Technolgy.
// Thesis available in Spanish at https://repositorio.uade.edu.ar/xmlui/handle/123456789/13847
// Based on the original code by Jian Guo, Thomas Peyrin and Axel Poschmann.
// https://link.springer.com/chapter/10.1007/978-3-642-22792-9_13
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace photoncs
{
    public static class Constants
    {
        public const int ROUND = 12;

        //_PHOTON80_
        public const int S = 4;
        public const int D = 5;
        public const int RATE = 20;
        public const int RATEP = 16;
        public const int DIGESTSIZE = 80;
        public static readonly byte[,] RC = new byte[Constants.D, 12] {
                { 1, 3, 7, 14, 13, 11, 6, 12, 9, 2, 5, 10 },
                { 0, 2, 6, 15, 12, 10, 7, 13, 8, 3, 4, 11 },
                { 2, 0, 4, 13, 14, 8, 5, 15, 10, 1, 6, 9 },
                { 7, 5, 1, 8, 11, 13, 0, 10, 15, 4, 3, 12 },
                { 5, 7, 3, 10, 9, 15, 2, 8, 13, 6, 1, 14 }

        };
        public const byte WORDFILTER = ((byte)1 << (byte)Constants.S) - (byte)1;

        public static readonly byte[] SBOX = new byte[16] { 12, 5, 6, 11, 9, 0, 10, 13, 3, 14, 15, 8, 4, 7, 1, 2 };

        public static readonly byte[,] MixColMatrix = new byte[Constants.D, Constants.D] {
                { 1, 2, 9, 9, 2 },
                { 2, 5, 3, 8, 13 },
                { 13, 11, 10, 12, 1 },
                { 1, 15, 2, 3, 14 },
                { 14, 14, 8, 5, 12 }
        };

        public const byte ReductionPoly = 0x3;
    }
}
