using Microsoft.WindowsAzure.Wapd.Acis.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldExtension
{
    /// <summary>
    /// Each Extension contains one more operation groups.
    /// This enables extension owners to group (categorize) operations into meaningful groups for ease of management.
    /// </summary>
    class HelloWorldOperationGroup : AcisSMEOperationGroup
    {
        private const string operationGroupNameConst = "Operations group one";

        /// <summary>
        /// Name of this operation group. Displayed in Jarvis UI prominently
        /// </summary>
        public override string Name => operationGroupNameConst;
    }
}