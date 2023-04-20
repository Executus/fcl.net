using Fcl.Net.Core.Config;
using Fcl.Net.Core.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fcl.Net.Core.Models
{
    public class FclWalletDiscovery
    {
        protected readonly FetchService _fetchService;

        public FclWalletDiscovery(FetchService fetchService = null)
        {
            _fetchService = fetchService;
            Include = new List<string>();
        }

        // UI version of discovery
        // Testnet "https://fcl-discovery.onflow.org/testnet/authn"
        // Mainnet "https://fcl-discovery.onflow.org/authn"
        public Uri Wallet { get; set; }
        public FclServiceMethod WalletMethod { get; set; }
        public ICollection<string> Include { get; set; }

        // API version of discovery
        // Testnet "https://fcl-discovery.onflow.org/api/testnet/authn"
        // Mainnet "https://fcl-discovery.onflow.org/api/authn"
        public Uri Authn { get; set; }

        public async Task<List<FclService>> GetServices()
        {
            Console.WriteLine("FclWalletDiscovery GetServices");
            if (_fetchService != null)
            {
                Console.WriteLine($"found fetch service");
                var data = new Dictionary<string, object>();
                data["type"] = "authn";
                data["fclVersion"] = "1.3.2";
                data["include"] = new string[0];
                data["userAgent"] = "foooooo/bar";
                data["clientServices"] = new string[0];
                data["supportedStrategies"] = new string[2]
                {
                    "HTTP/POST",
                    "WC/RPC"
                };
                data["network"] = "testnet";

                FclService[] res = await _fetchService.FetchAndReadResponseAsync<FclService[]>(new FclService
                {
                    Endpoint = Authn.AbsoluteUri,
                    FclType = "Service",
                    FclVsn = "1.0.0",
                    Type = FclServiceType.Authn,
                    Method = FclServiceMethod.Data
                }, data);

                Console.WriteLine($"FetchAndReadResponseAsync done");
                var serviceProviders = new List<FclService>();

                foreach (FclService service in res)
                {
                    serviceProviders.Add(service);
                }

                return serviceProviders;
            }

            return new List<FclService>();
        }
    }
}
