# Password Hasher

[![build](https://ci.appveyor.com/api/projects/status/github/tallesl/PasswordHasher)](https://ci.appveyor.com/project/TallesL/PasswordHasher)
[![nuget package](https://badge.fury.io/nu/PasswordHasher.png)](http://badge.fury.io/nu/PasswordHasher)

A password hasher that generates a unique [salt](http://en.wikipedia.org/wiki/Salt_%28cryptography%29) for each hash and hashes using [PBKDF2](http://en.wikipedia.org/wiki/PBKDF2).

## Usage

Instantiating:

```cs
using PwdHasher;

var hasher = new PasswordHasher();
```

Hashing a password:

```cs
var hashedPassword = hasher.HashIt("my cr4zy pa$$w0rd"); // returns a HashedPassword object, which has a hash and a salt
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
