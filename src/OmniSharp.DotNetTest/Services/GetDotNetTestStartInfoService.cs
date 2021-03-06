using System.Composition;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using OmniSharp.DotNetTest.Models;
using OmniSharp.Mef;
using OmniSharp.Services;

namespace OmniSharp.DotNetTest.Services
{
    [OmniSharpHandler(OmnisharpEndpoints.V2.GetDotNetTestStartInfo, LanguageNames.CSharp)]
    public class GetDotNetTestStartInfoService : RequestHandler<GetDotNetTestStartInfoRequest, GetDotNetTestStartInfoResponse>
    {
        private readonly OmniSharpWorkspace _workspace;
        private readonly DotNetCliService _dotNetCli;
        private readonly ILoggerFactory _loggerFactory;

        [ImportingConstructor]
        public GetDotNetTestStartInfoService(OmniSharpWorkspace workspace, DotNetCliService dotNetCli, ILoggerFactory loggerFactory)
        {
            _workspace = workspace;
            _dotNetCli = dotNetCli;
            _loggerFactory = loggerFactory;
        }

        public Task<GetDotNetTestStartInfoResponse> Handle(GetDotNetTestStartInfoRequest request)
        {
            var document = _workspace.GetDocument(request.FileName);

            using (var dtm = TestManager.Start(document.Project, _dotNetCli, _loggerFactory))
            {
                var response = dtm.GetTestStartInfo(request.MethodName, request.TestFrameworkName);
                return Task.FromResult(response);
            }
        }
    }
}
