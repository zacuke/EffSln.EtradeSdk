//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace EffSln.EtradeSdk.Authorization;

//public abstract class BaseClient
//{
//    protected virtual ValueTask PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
//    {
//        return ValueTask.CompletedTask;
//    }

//    protected virtual ValueTask PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder url, CancellationToken cancellationToken)
//    {
//        return ValueTask.CompletedTask;
//    }

//    protected virtual ValueTask ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
//    {
//        return ValueTask.CompletedTask;
//    }
//}
