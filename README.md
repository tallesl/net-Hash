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

Instantiating:

```cs
using HashLibrary;

var hasher = new Hasher();
```

Hashing a password:

```cs
var hashedPassword = hasher.HashPassword("my cr4zy pa$$w0rd"); // returns a HashedPassword object, which has a hash and a salt
                                                               // a new salt is generated for each hash
```

Checking a password:

```cs
if (hasher.Check(somePassword, hashedPassword))
{
    // the given password matches the given hash
}
else
{
    // wrong password
}
```