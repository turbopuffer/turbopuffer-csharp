using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceRecallResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
            GroundTruth =
            [
                new()
                {
                    NearestNeighbors =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                    QueryVector = [0],
                },
            ],
        };

        double expectedAvgAnnCount = 0;
        double expectedAvgExhaustiveCount = 0;
        double expectedAvgRecall = 0;
        List<GroundTruth> expectedGroundTruth =
        [
            new()
            {
                NearestNeighbors =
                [
                    new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                ],
                QueryVector = [0],
            },
        ];

        Assert.Equal(expectedAvgAnnCount, model.AvgAnnCount);
        Assert.Equal(expectedAvgExhaustiveCount, model.AvgExhaustiveCount);
        Assert.Equal(expectedAvgRecall, model.AvgRecall);
        Assert.NotNull(model.GroundTruth);
        Assert.Equal(expectedGroundTruth.Count, model.GroundTruth.Count);
        for (int i = 0; i < expectedGroundTruth.Count; i++)
        {
            Assert.Equal(expectedGroundTruth[i], model.GroundTruth[i]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
            GroundTruth =
            [
                new()
                {
                    NearestNeighbors =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                    QueryVector = [0],
                },
            ],
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceRecallResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
            GroundTruth =
            [
                new()
                {
                    NearestNeighbors =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                    QueryVector = [0],
                },
            ],
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceRecallResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        double expectedAvgAnnCount = 0;
        double expectedAvgExhaustiveCount = 0;
        double expectedAvgRecall = 0;
        List<GroundTruth> expectedGroundTruth =
        [
            new()
            {
                NearestNeighbors =
                [
                    new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                ],
                QueryVector = [0],
            },
        ];

        Assert.Equal(expectedAvgAnnCount, deserialized.AvgAnnCount);
        Assert.Equal(expectedAvgExhaustiveCount, deserialized.AvgExhaustiveCount);
        Assert.Equal(expectedAvgRecall, deserialized.AvgRecall);
        Assert.NotNull(deserialized.GroundTruth);
        Assert.Equal(expectedGroundTruth.Count, deserialized.GroundTruth.Count);
        for (int i = 0; i < expectedGroundTruth.Count; i++)
        {
            Assert.Equal(expectedGroundTruth[i], deserialized.GroundTruth[i]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
            GroundTruth =
            [
                new()
                {
                    NearestNeighbors =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                    QueryVector = [0],
                },
            ],
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
        };

        Assert.Null(model.GroundTruth);
        Assert.False(model.RawData.ContainsKey("ground_truth"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,

            // Null should be interpreted as omitted for these properties
            GroundTruth = null,
        };

        Assert.Null(model.GroundTruth);
        Assert.False(model.RawData.ContainsKey("ground_truth"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,

            // Null should be interpreted as omitted for these properties
            GroundTruth = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceRecallResponse
        {
            AvgAnnCount = 0,
            AvgExhaustiveCount = 0,
            AvgRecall = 0,
            GroundTruth =
            [
                new()
                {
                    NearestNeighbors =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                    QueryVector = [0],
                },
            ],
        };

        NamespaceRecallResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class GroundTruthTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new GroundTruth
        {
            NearestNeighbors =
            [
                new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
            ],
            QueryVector = [0],
        };

        List<Row> expectedNearestNeighbors =
        [
            new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
        ];
        List<double> expectedQueryVector = [0];

        Assert.Equal(expectedNearestNeighbors.Count, model.NearestNeighbors.Count);
        for (int i = 0; i < expectedNearestNeighbors.Count; i++)
        {
            Assert.Equal(expectedNearestNeighbors[i], model.NearestNeighbors[i]);
        }
        Assert.Equal(expectedQueryVector.Count, model.QueryVector.Count);
        for (int i = 0; i < expectedQueryVector.Count; i++)
        {
            Assert.Equal(expectedQueryVector[i], model.QueryVector[i]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new GroundTruth
        {
            NearestNeighbors =
            [
                new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
            ],
            QueryVector = [0],
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<GroundTruth>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new GroundTruth
        {
            NearestNeighbors =
            [
                new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
            ],
            QueryVector = [0],
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<GroundTruth>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        List<Row> expectedNearestNeighbors =
        [
            new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
        ];
        List<double> expectedQueryVector = [0];

        Assert.Equal(expectedNearestNeighbors.Count, deserialized.NearestNeighbors.Count);
        for (int i = 0; i < expectedNearestNeighbors.Count; i++)
        {
            Assert.Equal(expectedNearestNeighbors[i], deserialized.NearestNeighbors[i]);
        }
        Assert.Equal(expectedQueryVector.Count, deserialized.QueryVector.Count);
        for (int i = 0; i < expectedQueryVector.Count; i++)
        {
            Assert.Equal(expectedQueryVector[i], deserialized.QueryVector[i]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new GroundTruth
        {
            NearestNeighbors =
            [
                new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
            ],
            QueryVector = [0],
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new GroundTruth
        {
            NearestNeighbors =
            [
                new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
            ],
            QueryVector = [0],
        };

        GroundTruth copied = new(model);

        Assert.Equal(model, copied);
    }
}
