// Copyright (c) Bruno Sales <me@baliestri.dev>.Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MyFinances.Core.Primitives;

namespace MyFinances.Core.Entities;

public sealed class User : BaseEntity {
  public User() { }

  public User(string username, string firstName, string lastName, string email, string password) {
    Username = username;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Password = password;
  }

  public string Username { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;

  public IEnumerable<BankAccount> BankAccounts { get; set; } = null!;
}
