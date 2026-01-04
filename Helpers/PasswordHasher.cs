using System;
using System.Security.Cryptography;

namespace Project5LMS.Helpers
{
    public static class PasswordHasher
    {
        // Number of iterations for PBKDF2 - higher is more secure but slower
        private const int Iterations = 10000;
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 32; // 256 bits

        /// <summary>
        /// Hashes a password using PBKDF2 with a random salt
        /// </summary>
        /// <param name="password">The plain text password to hash</param>
        /// <returns>A hashed password string in format: iterations:salt:hash</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            byte[] hash = HashPasswordWithSalt(password, salt, Iterations);

            // Combine iterations, salt, and hash into a single string
            return $"{Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verifies a password against a stored hash
        /// </summary>
        /// <param name="password">The plain text password to verify</param>
        /// <param name="storedHash">The stored hash in format: iterations:salt:hash</param>
        /// <returns>True if password matches, false otherwise</returns>
        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
                return false;

            try
            {
                // Parse the stored hash
                string[] parts = storedHash.Split(':');
                if (parts.Length != 3)
                {
                    // Legacy support: if hash doesn't have the format, try direct comparison
                    // This allows backward compatibility with existing plain text or simple hashes
                    return storedHash.Equals(password, StringComparison.Ordinal);
                }

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] hash = Convert.FromBase64String(parts[2]);

                // Hash the provided password with the same salt and iterations
                byte[] computedHash = HashPasswordWithSalt(password, salt, iterations);

                // Compare the hashes
                return SlowEquals(hash, computedHash);
            }
            catch
            {
                // If parsing fails, try direct comparison for backward compatibility
                return storedHash.Equals(password, StringComparison.Ordinal);
            }
        }

        /// <summary>
        /// Hashes a password with a given salt using PBKDF2
        /// </summary>
        private static byte[] HashPasswordWithSalt(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Compares two byte arrays in constant time to prevent timing attacks
        /// </summary>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }
    }
}
