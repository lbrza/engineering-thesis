//-----------------------------------------------------------------------------------
// Spongent lightweight hash function - C# implementation.
// 
// File:        Spongent.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: Main algorithm.
// This file is part of my thesis for my Computer Engineering degree at UADE (Bachelor and Master according to wes.org).
// Thesis available in Spanish at https://repositorio.uade.edu.ar/xmlui/handle/123456789/13847
// Based on the original code by Andrey Bogdanov, Miroslav Knežević, Gregor Leander, Deniz Toz, Kerem Varıcı & Ingrid Verbauwhede.
// https://link.springer.com/chapter/10.1007/978-3-642-23951-9_21
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace spongent_cs
{
    public partial class Program
    {
        public static byte nextValueForLfsr(byte lfsr1)
        {
            uint lfsr = Convert.ToUInt16(lfsr1);
            uint value = unchecked((lfsr << 1)) | (
                                    (
                                        unchecked((lfsr >> 1)) ^ unchecked((lfsr >> 2)) ^ unchecked((lfsr >> 3)) ^ unchecked((lfsr >> 7))
                                    ) & 1
                                 );
            return Convert.ToByte(value & 0xFF);
            
        }

        public static byte reverse(byte b)
        {
            b = Convert.ToByte((b & 0xF0) >> 4 | (b & 0x0F) << 4);
            b = Convert.ToByte((b & 0xCC) >> 2 | (b & 0x33) << 2);
            return Convert.ToByte((b & 0xAA) >> 1 | (b & 0x55) << 1);
        }

        public static void pLayer()
        {
            byte[] tmp = new byte[11];

            for (uint idx = 0; idx < (Constants.b - 1); ++idx)
            {
                byte bit = Convert.ToByte((Constants.state[idx / 8] >> (byte)(idx % 8)) & 0x1);

                if (bit != 0)
                {
                    uint dest = Convert.ToUInt16(((long)idx * Constants.b / 4) % (Constants.b - 1));
                    tmp[dest / 8] |= Convert.ToByte(1 << (int)dest % 8);
                }
            }

            tmp[Constants.B - 1] |= Convert.ToByte(Constants.state[Constants.B - 1] & 0x80);

            for (int i = 0; i < Constants.B; i++)
            {
                Constants.state[i] = tmp[i];
            }
        }

        public static void permute()
        {
            byte lfsr = 0x9e;
            do
            {
                Constants.state[0] ^= lfsr;
                Constants.state[Constants.B - 1] ^= reverse(lfsr);

                lfsr = nextValueForLfsr(lfsr);

                for (uint idx = 0; idx < Constants.B; ++idx)
                    Constants.state[idx] = Convert.ToByte(Constants.S[Constants.state[idx] >> 4] << 4 | Constants.S[Constants.state[idx] & 0xF]);

                pLayer();
            } while (lfsr != 0xFF);
        }

        public static void spongent(string mess, byte[] output)
        {
            uint idx = 0;
            byte[] input = Encoding.ASCII.GetBytes(mess);

            if (input[idx] != 0)
            {

                while ((idx+1) >= input.Length ? false : true)
                {
                    Constants.state[0] ^= input[idx];
                    Constants.state[1] ^= input[idx + 1];

                    permute();
                    idx += 2;
                }
            }

            if (idx > input.Length ? false : true)
            {
                Constants.state[0] ^= 0x80;
            }
            else
            {
                Constants.state[0] ^= input[idx];
                Constants.state[1] ^= 0x80;
            }
            permute();

            for (uint idx2 = 0; idx2 < Constants.n / 8; idx2 += 2)
            {
                output[idx2] = Constants.state[0];
                output[idx2 + 1] = Constants.state[1];

                permute();
            }
        }
    }     
}
