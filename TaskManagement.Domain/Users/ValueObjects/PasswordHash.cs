using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using TaskManagement.Domain.Users.Policies;
namespace TaskManagement.Domain.Users.ValueObjects
{
   public sealed class PasswordHash
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public string Value { get; }

        private PasswordHash(string value )=>Value = value;

        public static PasswordHash FromPlainText(string password, IPasswordPolicy policy)
        {
            if (!policy.isSatisfiedBy(password))
                throw new ArgumentException("Password Does meet policy requirements.");

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2
                        (password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
            var Payload = Convert.ToBase64String(salt)+ "."+Convert.ToBase64String(hash);
            return new PasswordHash(Payload);
        }

        public bool Verify(string password)
        {
            var parts = password.Split('.');
            if (parts.Length != 2) return false;
            var salt = Convert.FromBase64String(parts[0]);
            var expected = Convert.FromBase64String(parts[1]);
            var actual = Rfc2898DeriveBytes.Pbkdf2
                        (password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }

        public override string ToString() => Value;

    }
}
