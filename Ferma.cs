using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;


namespace Ferma
{
    public static class Fermas
    {
        public static BigInteger result;
        public static bool flag=true;
        public static bool FermaAlg(BigInteger a, int times)
        {
            if (a == 2||a==3 || a == 5)
            {
                
                return true;
            }
            if (a % 2==0|| a % 3 == 0|| a % 5 == 0)
            {
                if (a == 561 || a == 1105 || a == 1729)
                {
                    return true;
                }
                return false;
            }
            
            /*1.Берется рандомное число<a
            2.Проверяется(a, p) == 1
            3.Проверяется нужное уравнение
            4.Повторяется times раз*/


            BigInteger p=a;

            for (int i = 0; i < times; i++)
            {
                do
                { 
                        p = getRandom(times, 2,a-1);
                    
                }
                while (BigInteger.GreatestCommonDivisor(a, p) != 1);

                if (BigInteger.ModPow(p,a - 1, a) != 1)
                {
                    return false;
                }
               
            }
            return true;
           


        }



        //minimum
        public static BigInteger minimum(int length)
        {
            BitArray b = new BitArray(length);
            b.SetAll(false);
            b.Set(length - 1, true);

            byte[] m = b.ToBytes().ToArray();
            BigInteger res = new BigInteger(m.Concat(new byte[] { 0 }).ToArray());
            return res;
        }


        //maximum
        public static BigInteger maximum(int length)
        {
            BitArray b = new BitArray(length);
            b.SetAll(true);
            byte[] m = b.ToBytes().ToArray();
            BigInteger res = new BigInteger(m.Concat(new byte[] { 0 }).ToArray());
            return res;
        }


        //Рандом от 0 до числа
        public static BigInteger getRandom(int length, BigInteger min, BigInteger max)
        {
            if (length == 0)
            {

            }
            BigInteger num;
            Random random = new Random();
            BitArray bitarr = new BitArray(length);
            byte[] bytearr = ToBytes(bitarr).ToArray();
         
            

            do
            {
                random.NextBytes(bytearr);
                
                num = new BigInteger(bytearr.Concat(new byte[] { 0 }).ToArray());
                
            } while (num > max||num < min);
            return num;

        }

        public static IEnumerable<byte> ToBytes(this BitArray bits, bool MSB = false)
        {
            int bitCount = 7;
            int outByte = 0;

            foreach (bool bitValue in bits)
            {
                if (bitValue)
                    outByte |= MSB ? 1 << bitCount : 1 << (7 - bitCount);
                if (bitCount == 0)
                {
                    yield return (byte)outByte;
                    bitCount = 8;
                    outByte = 0;
                }
                bitCount--;
            }
            // Последний частично декодированный байт
            if (bitCount < 7)
                yield return (byte)outByte;
        }

        public static BigInteger MyModPow(BigInteger Number, BigInteger Pow, BigInteger Mod)
        {
            BigInteger Result = 1;
            BigInteger Bit = Number % Mod;

            while (Pow > 0)
            {
                if ((Pow & 1) == 1)
                {
                    Result *= Bit;
                    Result %= Mod;
                }
                Bit *= Bit;
                Bit %= Mod;
                //битовый сдвиг, вправо - целочисленное деление на 2, влево - умножение на 2
                Pow >>= 1;
            }
            return Result;
        }




    }
}
