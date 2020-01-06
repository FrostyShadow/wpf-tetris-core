using System;
using System.Security.Cryptography;
using System.Threading;

namespace WpfTetrisLib.Providers
{
    public static class RandomProvider
    {
        private static ThreadLocal<Random> RandomWrapper { get; } = new ThreadLocal<Random>(() =>
        {
            var @byte = new byte[sizeof(int)];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(@byte);
            }

            var seed = BitConverter.ToInt32(@byte, 0);
            return new Random(seed);
        });

        public static Random ThreadRandom => RandomProvider.RandomWrapper.Value;
    }
}