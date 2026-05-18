using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Configuration options for full-text search.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<FullTextSearchConfig, FullTextSearchConfigFromRaw>))]
public sealed record class FullTextSearchConfig : JsonModel
{
    /// <summary>
    /// Whether to convert each non-ASCII character in a token to its ASCII equivalent,
    /// if one exists (e.g., à -&gt; a). Defaults to `false` (i.e., no folding).
    /// </summary>
    public bool? AsciiFolding
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("ascii_folding");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("ascii_folding", value);
        }
    }

    /// <summary>
    /// The `b` document length normalization parameter for BM25. Defaults to `0.75`.
    /// </summary>
    public double? B
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("b");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("b", value);
        }
    }

    /// <summary>
    /// Whether searching is case-sensitive. Defaults to `false` (i.e. case-insensitive).
    /// </summary>
    public bool? CaseSensitive
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("case_sensitive");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("case_sensitive", value);
        }
    }

    /// <summary>
    /// The `k1` term saturation parameter for BM25. Defaults to `1.2`.
    /// </summary>
    public double? K1
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("k1");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("k1", value);
        }
    }

    /// <summary>
    /// Describes the language of a text attribute. Defaults to `english`.
    /// </summary>
    public ApiEnum<string, Language>? Language
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<ApiEnum<string, Language>>("language");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("language", value);
        }
    }

    /// <summary>
    /// Maximum length of a token in bytes. Tokens larger than this value during tokenization
    /// will be filtered out. Has to be between `1` and `254` (inclusive). Defaults
    /// to `39`.
    /// </summary>
    public long? MaxTokenLength
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("max_token_length");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("max_token_length", value);
        }
    }

    /// <summary>
    /// Removes common words from the text based on language. Defaults to `true`
    /// (i.e. remove common words).
    /// </summary>
    public bool? RemoveStopwords
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("remove_stopwords");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("remove_stopwords", value);
        }
    }

    /// <summary>
    /// Language-specific stemming for the text. Defaults to `false` (i.e., do not stem).
    /// </summary>
    public bool? Stemming
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("stemming");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("stemming", value);
        }
    }

    /// <summary>
    /// The tokenizer to use for full-text search on an attribute. Defaults to `word_v3`.
    /// </summary>
    public ApiEnum<string, Tokenizer>? Tokenizer
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<ApiEnum<string, Tokenizer>>("tokenizer");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("tokenizer", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.AsciiFolding;
        _ = this.B;
        _ = this.CaseSensitive;
        _ = this.K1;
        this.Language?.Validate();
        _ = this.MaxTokenLength;
        _ = this.RemoveStopwords;
        _ = this.Stemming;
        this.Tokenizer?.Validate();
    }

    public FullTextSearchConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public FullTextSearchConfig(FullTextSearchConfig fullTextSearchConfig)
        : base(fullTextSearchConfig) { }
#pragma warning restore CS8618

    public FullTextSearchConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    FullTextSearchConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="FullTextSearchConfigFromRaw.FromRawUnchecked"/>
    public static FullTextSearchConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class FullTextSearchConfigFromRaw : IFromRawJson<FullTextSearchConfig>
{
    /// <inheritdoc/>
    public FullTextSearchConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => FullTextSearchConfig.FromRawUnchecked(rawData);
}
