// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace MyFinances.IoC.Options;

public sealed class JwtTokenOptions {
  public const string SECTION_NAME = "JwtToken";

  public string Secret { get; set; } = null!;
  public string Issuer { get; set; } = null!;
  public string Audience { get; set; } = null!;
  public int ExpirationTimeInMinutes { get; set; }
}