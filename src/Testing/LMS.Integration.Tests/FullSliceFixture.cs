using System.IO;
using FakeItEasy;
using LMS.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Integration.Tests
{
    public class FullSliceFixture : BaseSliceFixture
    {
        public FullSliceFixture(string env = null)
        {
            var host = A.Fake<IHostingEnvironment>();
            A.CallTo(() => host.ContentRootPath).Returns(Directory.GetCurrentDirectory());
            if (!string.IsNullOrEmpty(env))
                host.EnvironmentName = env;

            var startup = new Startup(host);
            _configuration = startup.Configuration;

            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
            _checkpoint = ConfigureCheckpoint();
        }
    }
}
