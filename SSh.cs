using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ferma
{
    public static class SSh
    {
        public static bool SShAlg(BigInteger n, int k)
        {

            for(int i = 0; i < k; i++)
            {
                //Get Random 2<a<n-1
                Random rnd = new Random();
                byte[] bytarr = new byte[n.ToByteArray().LongLength];
                BigInteger a;
                do
                {
                    rnd.NextBytes(bytarr);
                    a = new BigInteger(bytarr);
                }
                while (a < 2 || a >= n - 2);

                if (GCD(a, n) > 1)
                {
                    return false; //составное
                }

                BigInteger modPow = MyModPow(a, (n - 1) / 2, n);
                //+n на случай, если Jacobi вернет -1
                BigInteger jacobiMod = (Jacobi(a, n)+n)%n;
                if (modPow != jacobiMod)
                    return false;
            }
            return true;



            return false;
        }

        /// <summary>
        /// Getting Greatest Common Divisior for BigInt 
        /// </summary>
        /// <param name="a">First BigInt number</param>
        /// <param name="b">Second BigInt number</param>
        /// <returns>Greatest Common Divisior</returns>
        static BigInteger GCD(BigInteger a, BigInteger b)
        {
            BigInteger Remainder;

            while (b != 0)
            {
                Remainder = a % b;
                a = b;
                b = Remainder;
            }

            return a;
        }


        /// <summary>
        /// Getting Jacobi symbol (a/b)
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>(a/b) Jacobi</returns>
        public static BigInteger Jacobi(BigInteger a, BigInteger b)  
        {
            if (GCD(a, b) != 1)
                return 0;
            BigInteger r = 1;
            if (a < 0)
            {
                a = -a;
                if (b % 4 == 3)
                    r = -r;
            }
            while (a != 0)
            {
                BigInteger t = 0;
                while (a % 2 == 0)
                {
                    t++;
                    a = a / 2;
                }
                if (t % 2 == 1)
                {
                    if (b % 8 == 3 || b % 8 == 5)
                        r = -r;
                }
                if (a % 4 == 3 && b % 4 == 3)
                    r = -r;
                BigInteger c = a;
                a = b % c;
                b = c;

            }
            return r;
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
