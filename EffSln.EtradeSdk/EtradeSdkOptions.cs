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

    public void Validate()
    {
        if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Secret))
        {
            throw new ArgumentException("Key and Secret must be provided for the SDK.");
        }
    }
}