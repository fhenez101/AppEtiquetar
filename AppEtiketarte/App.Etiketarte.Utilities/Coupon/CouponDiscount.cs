using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Etiketarte.Utilities.Coupon
{
    public class CouponDiscount
    {
        public string GenerateCouponCode(int maxSize, char[] key)
        {
            var data = new byte[] { };
            var result = new StringBuilder(maxSize);

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }

            data.ToList().ForEach(x => result.Append(key[x % (key.Length)]));

            return result.ToString();
        }
    }
}