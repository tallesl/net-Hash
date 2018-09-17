# Hash

[![][build-img]][build]
[![][nuget-img]][nuget]

A password hasher that generates a unique [salt] for each hash and hashes using [PBKDF2].

[build]:     https://ci.appveyor.com/project/TallesL/net-hash
[build-img]: https://ci.appveyor.com/api/projects/status/github/tallesl/net-hash?svg=true
[nuget]:     https://www.nuget.org/packages/Hash
[nuget-img]: https://badge.fury.io/nu/Hash.svg
[salt]:      http://en.wikipedia.org/wiki/Salt_%28cryptography%29
[PBKDF2]:    http://en.wikipedia.org/wiki/PBKDF2

## Usage

```cs
var plainPassword = "my cr4zy pa$$w0rd";

// gives a HashedPassword object
// which has Hash and Salt properties ready to be stored somewhere
var hash = HashedPassword.New(plainPassword);

// checking the plain text password against the hashed one
if (hash.Check(plainPassword))
{
    // the given password matches the given hash
}
else
{
    // wrong password
}
```