using System;
using Turbopuffer.Models;

namespace Turbopuffer.Tests.Models;

public class ClientNamespacesParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new ClientNamespacesParams
        {
            Cursor = "cursor",
            PageSize = 1,
            Prefix = "prefix",
        };

        string expectedCursor = "cursor";
        int expectedPageSize = 1;
        string expectedPrefix = "prefix";

        Assert.Equal(expectedCursor, parameters.Cursor);
        Assert.Equal(expectedPageSize, parameters.PageSize);
        Assert.Equal(expectedPrefix, parameters.Prefix);
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new ClientNamespacesParams { };

        Assert.Null(parameters.Cursor);
        Assert.False(parameters.RawQueryData.ContainsKey("cursor"));
        Assert.Null(parameters.PageSize);
        Assert.False(parameters.RawQueryData.ContainsKey("page_size"));
        Assert.Null(parameters.Prefix);
        Assert.False(parameters.RawQueryData.ContainsKey("prefix"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new ClientNamespacesParams
        {
            // Null should be interpreted as omitted for these properties
            Cursor = null,
            PageSize = null,
            Prefix = null,
        };

        Assert.Null(parameters.Cursor);
        Assert.False(parameters.RawQueryData.ContainsKey("cursor"));
        Assert.Null(parameters.PageSize);
        Assert.False(parameters.RawQueryData.ContainsKey("page_size"));
        Assert.Null(parameters.Prefix);
        Assert.False(parameters.RawQueryData.ContainsKey("prefix"));
    }

    [Fact]
    public void Url_Works()
    {
        ClientNamespacesParams parameters = new()
        {
            Cursor = "cursor",
            PageSize = 1,
            Prefix = "prefix",
        };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v1/namespaces?cursor=cursor&page_size=1&prefix=prefix"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new ClientNamespacesParams
        {
            Cursor = "cursor",
            PageSize = 1,
            Prefix = "prefix",
        };

        ClientNamespacesParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
