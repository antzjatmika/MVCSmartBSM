using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using MVCSmartAPI01.Models;
using Newtonsoft.Json;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MVCSmartAPI01.Logging
{
    public abstract class MessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var corrId = Guid.NewGuid();
            var requestMethod = request.Method.Method.ToString();
            var requestUri = request.RequestUri.ToString();

            string strUserName = string.Empty;
            if (request.GetOwinContext() != null)
            {
                strUserName = request.GetOwinContext().Request.User.Identity.Name;
            }
            //var requestMessage = await request.Content.ReadAsByteArrayAsync();
            //await LogMessageAsync(corrId, requestUri, ipAddress, "Request", requestMethod, request.Headers.ToString(), requestMessage, string.Empty);

            var response = await base.SendAsync(request, cancellationToken);

            //var responseMessage = await response.Content.ReadAsByteArrayAsync();
            //await LogMessageAsync(corrId, requestUri, ipAddress, "Response", requestMethod, response.Headers.ToString(), responseMessage, ((int)response.StatusCode).ToString() + "-" + response.ReasonPhrase);

            return response;
        }

        protected abstract Task LogMessageAsync(Guid CorrelationId, string APIUrl, string ClientIPAddress, string RequestResponse, string HttpMethod, string HttpHeaders, byte[] HttpMessage, string HttpStatusCode);

    }
    public class MessageLoggingHandler : MessageHandler
    {
        protected override async Task LogMessageAsync(Guid CorrelationId, string APIUrl, string ClientIPAddress, string RequestResponse, string HttpMethod, string HttpHeaders, byte[] HttpMessage, string HttpStatusCode)
        {
            // Create logger here

            //Do your logging here
        }
    }
}