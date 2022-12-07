// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace MyFinances.Application.Abstractions.Providers;

public interface IJwtTokenProvider {
  string GenerateAccessToken(string userId, string userName, string email);
}
