// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFinances.Application.Abstractions.Providers;
using MyFinances.IoC.Options;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MyFinances.IoC.Providers;

public sealed class JwtTokenProvider : IJwtTokenProvider {
  private readonly ILogger<JwtTokenProvider> _logger;
  private readonly JwtTokenOptions _options;
  private readonly IDateTimeProvider _timeProvider;

  public JwtTokenProvider(
    ILogger<JwtTokenProvider> logger, IDateTimeProvider timeProvider, IOptions<JwtTokenOptions> options
  ) {
    _logger = logger;
    _timeProvider = timeProvider;
    _options = options.Value;
  }

  public string GenerateAccessToken(Guid userId, string userName, string email) {
    _logger.LogInformation("Generating access token for user {UserId}", userId);

    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
      SecurityAlgorithms.HmacSha256
    );

    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
      new Claim(JwtRegisteredClaimNames.UniqueName, userName),
      new Claim(JwtRegisteredClaimNames.Email, email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var securityToken = new JwtSecurityToken(
      _options.Issuer,
      _options.Audience,
      claims,
      expires: _timeProvider.UtcNow.AddMinutes(_options.ExpirationTimeInMinutes),
      signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler()
      .WriteToken(securityToken);
  }

  public DateTime GetExpirationDate(string token) {
    _logger.LogInformation("Getting expiration date from token");

    return new JwtSecurityTokenHandler()
      .ReadJwtToken(token)
      .ValidTo;
  }
}