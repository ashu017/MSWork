using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Wapd.Acis.Contracts;
using System.Threading;

namespace HelloWorldExtension
{
    /// <summary>
    /// Class name is important for Extension operations.
    /// ClassName after truncating Operation word in the end, is treated as operation ID.
    /// for example - for below GreetingsOperation the operation ID will be Greetings
    ///
    /// Also we need to implement a method with same name, which works as main execute method for this operation.
    /// There is a way to overwrite and implement some other method as main execute, Refer References in section for details.
    /// for example in this class we have Greetings() method as main execute method.
    /// </summary>
    class GreetingsOperation : AcisSMEOperation
    {
        /// <summary>
        /// Name of the operation. This is prominently visible from Jarvis. One search an operation by name.
        /// </summary>
        public override string OperationName { get => "Greetings"; }

        /// <summary>
        /// Each operation belongs to an operation group. This is how we associate an operation with operation group.
        /// </summary>
        public override IAcisSMEOperationGroup OperationGroup { get => new HelloWorldOperationGroup(); }

        /// <summary>
        /// These are the claims required to run this operation.
        /// As this is a test operation everybody who has PlatformServiceViewer claim can run this operation. That is everybody inside @microsoft.com
        /// </summary>
        public override IEnumerable<AcisUserClaim> ClaimsRequired
        {
            get
            {
                return new[]
                {
                    AcisSMESecurityGroup.PlatformServiceViewer
               };
            }
        }
        /// <summary>
        /// This is main execute method for the operation.
        /// Name of the method is same as class name after truncating Operation in the end.
        /// for example this class name is GreetingsOperation and thus method name is Greetings()
        /// </summary>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        /// <param name="updater"></param>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public IAcisSMEOperationResponse Greetings(IAcisServiceManagementExtension extension = null, IAcisSMEOperationProgressUpdater updater = null, IAcisSMEEndpoint endpoint = null)
        {
            String returnMessage = "Hello World ";

            extension.Logger.LogVerbose("Executing operation MyOperation() against endpoint " + endpoint.Name);
            updater.WriteLine("About to start operation");

            Thread.Sleep(3000);
            updater.WriteLine("Three seconds into the operation execution");

            Thread.Sleep(6000);
            updater.WriteLine("Finished operation");
            return AcisSMEOperationResponseExtensions.StandardSuccessResponse(returnMessage);
        }

        /// <summary>
        /// Data Access Levels provide information to compliance about what sort of data your operation is interacting with.
        /// By default, all Data Access Levels are set to DataAccessLevel.None. The framework enforces that at least one of them
        /// must be set to a higher level (DataAccessLevel.ReadOnly or DataAccessLevel.ReadWrite).Additionally, there is claim/data-access-level
        /// verification: if you indicate your operation interacts with customer data, your claims cannot be* PlatformService* claims.
        /// If you indicate that your operation is a read-write operation, your claims cannot be* Viewer claims.
        /// </summary>
        public override DataAccessLevel SystemMetadata
        {
            get
            {
                return DataAccessLevel.ReadOnly;
            }
        }

        /// <summary>
        /// This controls all input parameters to an operation. For current operation we do not have any input parameters.
        /// Look at examples mentioned in the end of this tutorial to see how different parameters can be used.
        /// </summary>
        public override IEnumerable<IAcisSMEParameterRef> Parameters
        {
            get { return new IAcisSMEParameterRef[0]; }
        }
    }
}
