using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Tests.Core;

public class ClientOptionsTest : TestBase
{
    [Fact]
    public void BaseUrlTemplateVariablesSubstitute()
    {
        ClientOptions opts = new() { Region = "gcp-us-central1" };
        Assert.Equal("https://gcp-us-central1.turbopuffer.com", opts.BaseUrl);
    }
}
