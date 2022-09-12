﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fcl.Net.Core.Models;
using Fcl.Net.Core.Service.Strategy;
using Flow.Net.Sdk.Core.Exceptions;
using Newtonsoft.Json;

namespace Fcl.Net.Blazor.Strategy
{
    public class JsStrategy : IStrategy
    {
        private readonly FclJsObjRef _fclJsObjRef;

        public JsStrategy(FclJsObjRef fclJsObjRef)
        {
            _fclJsObjRef = fclJsObjRef;
        }

        public async Task<FclAuthResponse> ExecuteAsync(FclService service, FclServiceConfig? config = null, object? data = null, HttpMethod? httpMethod = null)
        {
            try
            {
                var response = await _fclJsObjRef.OpenLocalViewAwaitResponse(service, config, data).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<FclAuthResponse>(response);
            }
            catch (Exception ex)
            {
                throw new FlowException("Frame error.", ex);
            }
        }
    }
}
