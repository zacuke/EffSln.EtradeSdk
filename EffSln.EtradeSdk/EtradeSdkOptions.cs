using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffSln.EtradeSdk;
public class EtradeSdkOptions
{
    public string Key { get; set; }
    public string Secret { get; set; }
    /// <summary>
    /// Defaults to oob
    /// </summary>
    public string Oauth_callback { get; set; } = "oob";
    public bool UseSandbox { get; set; } = true;
    public void Validate()
    {
        if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Secret) || string.IsNullOrEmpty(Oauth_callback))
        {
            throw new ArgumentException("Key, Secret, and Oauth_callback must be provided for the SDK.");
        }
    }
}