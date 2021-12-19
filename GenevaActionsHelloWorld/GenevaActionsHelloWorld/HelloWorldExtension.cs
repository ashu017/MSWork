using Microsoft.WindowsAzure.Wapd.Acis.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldExtension
{
    class HelloWorldManagementExtension : AcisServiceManagementExtension
    {
        // A convenience in this sample for general use
        static public string ServiceNameConst = "HelloWorld987";

        #region Required for Extension infrastructure
        /// <summary>
        /// Extension name
        /// Uniquely identifies the Extension
        /// </summary>
        public override string ServiceName { get => ServiceNameConst; }

        /// <summary>
        /// Extension Version
        /// </summary>
        public override string ExtensionVersion { get { return "1.0"; } }

        /// <summary>
        /// Always called when extension is loaded, allows dynamic creation of
        ///  endpoints and general initialization
        /// </summary>
        /// <returns></returns>
        public override bool OnLoad()
        {
            Logger.LogVerbose(string.Format("Just loaded {0}", ServiceName));
            return true;
        }

        /// <summary>
        /// Each endpoint that is created is initialized and then this method is called
        /// providing that endpoint.  Unlike our other extension points, you can't extend
        /// an endpoint - it is populated only from your configuration file.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public override bool OnEndpointCreate(IAcisSMEEndpoint endpoint)
        {
            // Report on the core information for the extension
            Logger.LogVerbose(string.Format("Extension {0} creating endpoint {1}", endpoint.ContainingExtension.ServiceName, endpoint.Name));
            Logger.LogVerbose(string.Format(".. claims required are {0}", string.Join("|", endpoint.ClaimsRequired.Select(claim => claim.Name))));
            Logger.LogVerbose(string.Format(".. operations provided are {0}", string.Join("|", endpoint.Operations.Select(op => op.ToString()))));

            // Report on the configuration contained in the endpoint - the Geneva Actions infrastructure doesn't rely on any of this
            //  configuration it's purely for the extension's use
            Logger.LogVerbose(string.Format(".. configuration defines environment as {0}", endpoint.Configuration.GetConfigurationValue("env")));

            return true;
        }

        #endregion
    }
}
