using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffSln.EtradeSdk;

public static class OAuthHelper
{
    private static Random secureRand = new Random(); // You might want to use a more secure RNG, e.g., RNGCryptoServiceProvider.

 
    public static string ComputeNonce()
    {
        // Generate a long value similar to Java's nextLong
        long generatedNo = (long)(secureRand.NextDouble() * long.MaxValue);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(generatedNo.ToString()));
 
    }

    public static string ComputeTimestamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
 
    }
}

 
