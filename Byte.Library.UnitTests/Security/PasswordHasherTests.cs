using System;
using System.Security.Cryptography;
using Byte.Library.Security;
using Xunit;

namespace Byte.Library.UnitTests.Security
{
    public class PasswordHasherTests
    {
        [Fact]
        public void Different_salt_produces_different_hash()
        {
            var salt1 = PasswordHasher.GenerateSalt();
            var salt2 = PasswordHasher.GenerateSalt();

            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var passwordHasher = new PasswordHasher(sha256);

                string hash1 = passwordHasher.ComputeHash("foobar", salt1);
                string hash2 = passwordHasher.ComputeHash("foobar", salt2);

                Assert.NotEqual(hash1, hash2);
            }
        }

        [Fact]
        public void Empty_salt_throws_ArgumentException()
        {
            var salt = new byte[0];

            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var passwordHasher = new PasswordHasher(sha256);

                Assert.Throws(
                    typeof(ArgumentException), 
                    () =>
                    {
                        passwordHasher.ComputeHash("foobar", salt);
                    });
            }
        }

        [Fact]
        public void ComputerHash_produces_expected_hash()
        {
            var salt = new byte[1] { 1 };

            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var passwordHasher = new PasswordHasher(sha256);

                string hash = passwordHasher.ComputeHash("foobar", salt);

                Assert.Equal("F0-39-42-EC-A4-82-7C-93-93-1F-EE-97-F1-17-47-9E-F4-74-C9-AA-A4-49-65-5D-DF-FB-48-88-6B-DE-58-AD", hash);
            }
        }
    }
}
