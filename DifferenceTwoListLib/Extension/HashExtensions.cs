namespace System
{
    using System.Security.Cryptography;
    using System.Text;

    public static class HashExtensions
    {
        /// <summary>
        /// Gibt den Hash eines String als MD5-Hash zurück
        /// </summary>
        /// <param name="this">String von der MD5 Hash ermittelt werden soll</param>
        /// <param name="isUpperOrLower">True, MD5-Hash wird in Großbuchstanben zurückgegeben</param>
        /// <returns>MD5-Hash</returns>
        public static string ToMD5(this string @this, bool isUpperOrLower = false)
        {
            byte[] bytes = (new MD5CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(@this));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                if (isUpperOrLower == false)
                {
                    sb.Append(b.ToString("x2").ToLower());
                }
                else
                {
                    sb.Append(b.ToString("x2").ToUpper());
                }
            }

            return sb.ToString();
        }
    }
}
