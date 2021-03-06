using OmniSharp.Mef;
using OmniSharp.Models;

namespace OmniSharp.DotNetTest.Models
{
    [OmniSharpEndpoint(OmnisharpEndpoints.V2.RunDotNetTest, typeof(RunDotNetTestRequest), typeof(RunDotNetTestResponse))]
    public class RunDotNetTestRequest : Request
    {
        public string MethodName { get; set; }

        public string TestFrameworkName { get; set; }
    }
}
