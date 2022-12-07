// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Primitives;

namespace MyFinances.Core.Entities;

public sealed class UserEntity : BaseEntity {
  public UserEntity() { }

  public UserEntity(string userName, string firstName, string lastName, string email, string password) {
    UserName = userName;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password;
  }

  public string UserName { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;

  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenExpiration { get; set; }
}
