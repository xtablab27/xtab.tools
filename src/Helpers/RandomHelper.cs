using System;
using System.IO;
using System.Threading;

namespace xTab.Tools.Helpers
{
    public static class RandomHelper
    {
        [ThreadStatic]
        private static Random localRandom;

        public static Random ThisThreadsRandom
        {
            get { return localRandom ?? (localRandom = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }

        public static String RandomFileName()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }

        public static String RandomString(int length, string alphabet = null)
        {
            var validAlphabet = alphabet ?? "abcdefghjklmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ1234567890";
            var randomString = String.Empty;

            for (int i = 0; i < length; i++)
                randomString += validAlphabet[ThisThreadsRandom.Next(0, validAlphabet.Length - 1)];

            return randomString;
        }
    }
}
