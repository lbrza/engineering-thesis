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
using System.Text;

namespace quark_cs
{
	public partial class Program
	{
		public struct HashState
		{
			public int pos;
			public uint[] x;
		}

		public static void Quark(ref byte[] output, string input, ulong inlen)
		{
			HashState state = new HashState
			{
				x = new uint[Constants.WIDTH * 8]
			};
			Init(ref state);
			Update(ref state, input, inlen);
			Final(ref state, ref output);
		}

		

		public static void Init(ref HashState state)
		{
			int i;

			for (i = 0; i < 8 * Constants.WIDTH; ++i)
				state.x[i] = (uint)((Constants.IV[i / 8] >> (7 - (i % 8))) & 1);

			state.pos = 0;
		}

		public static void Update(ref HashState state, string input, ulong databytelen)
		{
			int i;

			byte[] bstr = Encoding.ASCII.GetBytes(input);
			int j = 0;

			while (databytelen > 0)
			{
				byte u = (j >= input.Length) ? (byte)0 : bstr[j];

				for (i = 8 * state.pos; i < 8 * state.pos + 8; ++i)
				{
					state.x[
							(8 * (Constants.WIDTH - Constants.RATE)) + i
						   ] ^= Convert.ToUInt32((u >> (i % 8)) & 1);
				}

				j++;
        
				databytelen -= 1;
				state.pos += 1;

				if (state.pos == Constants.RATE)
				{
					Permute(ref state.x);
					state.pos = 0;
				}

			}
			Console.WriteLine("Update done.");

		}

		public static void Permute(ref uint[] x)
		{
			Permute_u(ref x);
		}

		private static void Permute_u(ref uint[] x)
		{
			uint[] xx, yy, lfsr;
			uint h;
			int i;

			xx   = new uint[(Constants.N_LEN_U + Constants.ROUNDS_U)];
			yy   = new uint[(Constants.N_LEN_U + Constants.ROUNDS_U)];
			lfsr = new uint[(Constants.L_LEN_U + Constants.ROUNDS_U)];

			for (i = 0; i < Constants.N_LEN_U; ++i)
			{
				xx[i] = x[i];
				yy[i] = x[i + Constants.N_LEN_U];
			}

			for (i = 0; i < Constants.L_LEN_U; ++i)
				lfsr[i] = 0xFFFFFFFF;

			for (i = 0; i < Constants.ROUNDS_U; ++i)
			{
				xx[Constants.N_LEN_U + i] = xx[i] ^ yy[i];
				xx[Constants.N_LEN_U + i] ^= xx[i + 9] ^ xx[i + 14] ^ xx[i + 21] ^ xx[i + 28] ^
					xx[i + 33] ^ xx[i + 37] ^ xx[i + 45] ^ xx[i + 52] ^ xx[i + 55] ^ xx[i + 50] ^
					(xx[i + 59] & xx[i + 55]) ^ (xx[i + 37] & xx[i + 33]) ^ (xx[i + 15] & xx[i + 9]) ^
					(xx[i + 55] & xx[i + 52] & xx[i + 45]) ^ (xx[i + 33] & xx[i + 28] & xx[i + 21]) ^
					(xx[i + 59] & xx[i + 45] & xx[i + 28] & xx[i + 9]) ^
					(xx[i + 55] & xx[i + 52] & xx[i + 37] & xx[i + 33]) ^
					(xx[i + 59] & xx[i + 55] & xx[i + 21] & xx[i + 15]) ^
					(xx[i + 59] & xx[i + 55] & xx[i + 52] & xx[i + 45] & xx[i + 37]) ^
					(xx[i + 33] & xx[i + 28] & xx[i + 21] & xx[i + 15] & xx[i + 9]) ^
					(xx[i + 52] & xx[i + 45] & xx[i + 37] & xx[i + 33] & xx[i + 28] & xx[i + 21]);

				yy[Constants.N_LEN_U + i] = yy[i];
				yy[Constants.N_LEN_U + i] ^= yy[i + 7] ^ yy[i + 16] ^ yy[i + 20] ^ yy[i + 30] ^
					yy[i + 35] ^ yy[i + 37] ^ yy[i + 42] ^ yy[i + 51] ^ yy[i + 54] ^ yy[i + 49] ^
					(yy[i + 58] & yy[i + 54]) ^ (yy[i + 37] & yy[i + 35]) ^ (yy[i + 15] & yy[i + 7]) ^
					(yy[i + 54] & yy[i + 51] & yy[i + 42]) ^ (yy[i + 35] & yy[i + 30] & yy[i + 20]) ^
					(yy[i + 58] & yy[i + 42] & yy[i + 30] & yy[i + 7]) ^
					(yy[i + 54] & yy[i + 51] & yy[i + 37] & yy[i + 35]) ^
					(yy[i + 58] & yy[i + 54] & yy[i + 20] & yy[i + 15]) ^
					(yy[i + 58] & yy[i + 54] & yy[i + 51] & yy[i + 42] & yy[i + 37]) ^
					(yy[i + 35] & yy[i + 30] & yy[i + 20] & yy[i + 15] & yy[i + 7]) ^
					(yy[i + 51] & yy[i + 42] & yy[i + 37] & yy[i + 35] & yy[i + 30] & yy[i + 20]);

				lfsr[Constants.L_LEN_U + i] = lfsr[i];
				lfsr[Constants.L_LEN_U + i] ^= lfsr[i + 3];

				h = xx[i + 25] ^ yy[i + 59] ^ (yy[i + 3] & xx[i + 55]) ^ (xx[i + 46] & xx[i + 55]) ^ (xx[i + 55] & yy[i + 59]) ^
					(yy[i + 3] & xx[i + 25] & xx[i + 46]) ^ (yy[i + 3] & xx[i + 46] & xx[i + 55]) ^ (yy[i + 3] & xx[i + 46] & yy[i + 59]) ^
					(xx[i + 25] & xx[i + 46] & yy[i + 59] & lfsr[i]) ^ (xx[i + 25] & lfsr[i]);
				h ^= xx[i + 1] ^ yy[i + 2] ^ xx[i + 4] ^ yy[i + 10] ^ xx[i + 31] ^ yy[i + 43] ^ xx[i + 56] ^ lfsr[i];

				xx[Constants.N_LEN_U + i] ^= h;
				yy[Constants.N_LEN_U + i] ^= h;
			}

			for (i = 0; i < Constants.N_LEN_U; ++i)
			{
				x[i] = xx[Constants.ROUNDS_U + i];
				x[i + Constants.N_LEN_U] = yy[Constants.ROUNDS_U + i];
			}
		}

		private static void Final(ref HashState state, ref byte[] output)
		{
			int i;
			int outbytes = 0;
			byte u;

			state.x[8 * (Constants.WIDTH - Constants.RATE) + state.pos * 8] ^= 1;

			Permute(ref state.x);

			for (i = 0; i < Constants.DIGEST; ++i)
				output[i] = 0;

			while (outbytes < Constants.DIGEST)
			{
				for (i = 0; i < 8; ++i)
				{
					u = Convert.ToByte(state.x[8 * (Constants.WIDTH - Constants.RATE) + i + 8 * (outbytes % Constants.RATE)] & 1);
					output[outbytes] ^= (byte)(u << (7 - i));
				}

				outbytes += 1;
				if (outbytes == Constants.DIGEST)
					break;

				if ((outbytes % Constants.RATE) == 0)
				{
					Permute(ref state.x);
				}
			}
		}
	}
}
