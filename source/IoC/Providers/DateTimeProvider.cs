// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Logging;
using MyFinances.Application.Abstractions.Providers;

namespace MyFinances.IoC.Providers;

public sealed class DateTimeProvider : IDateTimeProvider {
  private readonly ILogger<DateTimeProvider> _logger;

  public DateTimeProvider(ILogger<DateTimeProvider> logger)
    => _logger = logger;

  public DateTime UtcNow {
    get {
      _logger.LogInformation("Getting current UTC time");

      return DateTime.UtcNow;
    }
  }
}