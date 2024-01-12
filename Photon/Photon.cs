//-----------------------------------------------------------------------------------
// Photon lightweight hash function - C# implementation.
// 
// File:        Photon.cs
// Author:      Leandro Beraza
// Date:        2021-11-01
// Description: Main algorithm.
// This file is part of my thesis for my Master of Engineer in Information Technolgy.
//-----------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace photoncs
{
	public partial class Program
	{
		public static void hash(ref byte[] digest, string mess, int BitLen)
		{
			byte[,] state = new byte[Constants.D, Constants.D];
			byte[] padded = new byte[4];

			Init(ref state);
			int MessIndex = 0;
			byte[] str = Encoding.ASCII.GetBytes(mess);
			while (MessIndex <= (BitLen - Constants.RATE))
			{
				CompressFunction(state, str, MessIndex);
				MessIndex += Constants.RATE;
			}
			int i;
			double j;
			for (i = 0; i < (Math.Ceiling(Constants.RATE / 8.0) + 1); i++)
				padded[i] = 0;

			j = Ceiling( (Convert.ToDouble(BitLen - MessIndex)) / 8.0);
			for (i = 0; i < j; i++)
				padded[i] = Convert.ToByte(mess[(MessIndex / 8) + i]);
			padded[i] = 0x80;
			CompressFunction(state, padded, MessIndex & 0x7);
			Squeeze(state, digest);
		}


		public static void Init(ref byte[,] state)
		{
			byte[] presets = new byte[3];
			presets[0] = (Constants.DIGESTSIZE >> 2) & 0xFF;
			presets[1] = Constants.RATE & 0xFF;
			presets[2] = Constants.RATEP & 0xFF;

			WordXorByte(state, presets, 0, Constants.D * Constants.D - 24 / Constants.S, 24);
		}

		public static void WordXorByte(byte[,] state, in byte[] str, int BitOffSet, int WordOffSet, int NoOfBits)
		{
			int i = 0;
			while (i < NoOfBits)
			{
				int param1 = (WordOffSet + (i / Constants.S)) / Constants.D;
				int param2 = (WordOffSet + (i / Constants.S)) % Constants.D;
				byte tempByte1 = GetByte(str, BitOffSet + i, Min(Constants.S, NoOfBits - i));
				byte tempByte2 = (byte)(Constants.S - Min(Constants.S, NoOfBits - i));

				state[param1, param2] ^= (byte)(tempByte1 << tempByte2);
				i += Constants.S;
			}

		}

		public static byte GetByte(in byte[] str, int BitOffSet, int NoOfBits)
		{
			byte temp1 = str[BitOffSet >> 3];
			byte temp2 = (byte)(4 - (BitOffSet & 0x4));
			byte param1 = (byte)(temp1 >> temp2);
			return (byte)(param1 & Constants.WORDFILTER);
		}

		public static void CompressFunction(byte[,] state, in byte[] str, int BitOffSet)
		{
			WordXorByte(state, str, BitOffSet, 0, Constants.RATE);
			Permutation(state, Constants.ROUND);
		}

		public static void Permutation(byte[,] state, int R)
		{
			int i;
			for (i = 0; i < R; i++)
			{
				AddKey(state, i);
				SubCell(state); 
				ShiftRow(state); 
				MixColumn(state);
			}
		}

		public static void AddKey(byte[,] state, int round)
		{
			int i;
			for (i = 0; i < Constants.D; i++)
				state[i, 0] ^= Constants.RC[i, round];
		}

		public static void PrintState(byte[,] state)
		{
			int i, j;
			for (i = 0; i < Constants.D; i++)
			{
				for (j = 0; j < Constants.D; j++)
					Console.Write("{0:X2}", state[i, j]);

				Console.WriteLine();
			}
			Console.WriteLine();

		}

		public static void SubCell(byte[,] state)
		{
			int i, j;
			for (i = 0; i < Constants.D; i++)
				for (j = 0; j < Constants.D; j++)
					state[i, j] = Constants.SBOX[state[i, j]];
		}

		public static void ShiftRow(byte[,] state)
		{
			int i, j;
			byte[] tmp = new byte[Constants.D];

			for (i = 1; i < Constants.D; i++)
			{
				for (j = 0; j < Constants.D; j++)
					tmp[j] = state[i, j];
				for (j = 0; j < Constants.D; j++)
					state[i, j] = tmp[(j + i) % Constants.D];
			}
		}

		public static void MixColumn(byte[,] state)
		{
			int i, j, k;
			byte[] tmp = new byte[Constants.D];
			for (j = 0; j < Constants.D; j++)
			{
				for (i = 0; i < Constants.D; i++)
				{
					byte sum = 0;
					for (k = 0; k < Constants.D; k++)
						sum ^= FieldMult(Constants.MixColMatrix[i, k], state[k, j]);
					tmp[i] = sum;
				}
				for (i = 0; i < Constants.D; i++)
					state[i, j] = tmp[i];
			}
		}

		public static byte FieldMult(byte a, byte b)
		{
			byte x = a, ret = 0;
			int i;
			for (i = 0; i < Constants.S; i++)
			{
				if (Convert.ToBoolean((b >> i) & 1))
					ret ^= x;
				if (Convert.ToBoolean((x >> (Constants.S - 1)) & 1))
				{
					x <<= 1;
					x ^= Constants.ReductionPoly;
				}
				else
					x <<= 1;
			}
			return (byte)(ret & Constants.WORDFILTER);
		}

		public static void printDigest(byte[] digest)
		{
			int i;
			for (i = 0; i<Constants.DIGESTSIZE / 8; i++)
				Console.Write("{0:X}", digest[i]);
			Console.WriteLine();
		}

		public static void Squeeze(byte[,] state, byte[] digest)
		{
			int i = 0;
			while (true)
			{
				WordToByte(state, digest, i, Min(Constants.RATEP, Constants.DIGESTSIZE - i));
				i += Constants.RATEP;
				if (i >= Constants.DIGESTSIZE) break;
				Permutation(state, Constants.ROUND);
			}
		}

		public static void WordToByte(byte[,] state, byte[] str, int BitOffSet, int NoOfBits)
		{
			int i = 0;
			while (i < NoOfBits)
			{
				WriteByte(str, Convert.ToByte((state[i / (Constants.S * Constants.D) , (i / Constants.S) % Constants.D] & Constants.WORDFILTER) >> (Constants.S - Min(Constants.S, NoOfBits - i))), BitOffSet + i, Min(Constants.S, NoOfBits - i));
				i += Constants.S;
			}
		}

		public static void WriteByte(byte[] str, byte value, int BitOffSet, int NoOfBits)
		{
			int ByteIndex = BitOffSet >> 3;
			int BitIndex = BitOffSet & 0x7;
			byte localFilter = Convert.ToByte((((byte)1) << NoOfBits) - 1);
			value &= localFilter;
			if (BitIndex + NoOfBits <= 8)
			{
				int temp = (byte)(~(localFilter << (8 - BitIndex - NoOfBits)));
				str[ByteIndex] &= (byte)temp;
				str[ByteIndex] |= Convert.ToByte(value << (8 - BitIndex - NoOfBits));
			}
			else
			{
				uint tmp = ((((uint)str[ByteIndex]) << 8) & 0xFF00) | (((uint)str[ByteIndex + 1]) & 0xFF);
				tmp &= ~((((uint)localFilter) & 0xFF) << (16 - BitIndex - NoOfBits));
				tmp |= (((uint)(value)) & 0xFF) << (16 - BitIndex - NoOfBits);
				str[ByteIndex] = Convert.ToByte((tmp >> 8) & 0xFF);
				str[ByteIndex + 1] = Convert.ToByte(tmp & 0xFF);
			}
		}
	}
}
