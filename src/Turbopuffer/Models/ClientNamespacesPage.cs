using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models;

/// <summary>
/// A single page from the paginated endpoint that <see cref="ITurbopufferClient.Namespaces(ClientNamespacesParams, CancellationToken)"/> queries.
/// </summary>
public sealed class ClientNamespacesPage(
    ITurbopufferClientWithRawResponse service,
    ClientNamespacesParams parameters,
    ClientNamespacesPageResponse response
) : IPage<NamespaceSummary>
{
    /// <inheritdoc/>
    public IReadOnlyList<NamespaceSummary> Items
    {
        get { return response.Namespaces ?? []; }
    }

    /// <inheritdoc/>
    public bool HasNext()
    {
        try
        {
            return this.Items.Count > 0 && response.NextCursor != null;
        }
        catch (TurbopufferInvalidDataException)
        {
            // If accessing the response data to determine if there's a next page failed, then just
            // assume there's no next page.
            return false;
        }
    }

    /// <inheritdoc/>
    async Task<IPage<NamespaceSummary>> IPage<NamespaceSummary>.Next(
        CancellationToken cancellationToken
    ) => await this.Next(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc cref="IPage{T}.Next"/>
    public async Task<ClientNamespacesPage> Next(CancellationToken cancellationToken = default)
    {
        var nextCursor =
            response.NextCursor ?? throw new InvalidOperationException("Cannot request next page");
        using var nextResponse = await service
            .Namespaces1(parameters with { Cursor = nextCursor }, cancellationToken)
            .ConfigureAwait(false);
        return await nextResponse.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public void Validate()
    {
        response.Validate();
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(JsonSerializer.SerializeToElement(this.Items)),
            ModelBase.ToStringSerializerOptions
        );

    public override bool Equals(object? obj)
    {
        if (obj is not ClientNamespacesPage other)
        {
            return false;
        }

        return Enumerable.SequenceEqual(this.Items, other.Items);
    }

    public override int GetHashCode() => 0;
}
