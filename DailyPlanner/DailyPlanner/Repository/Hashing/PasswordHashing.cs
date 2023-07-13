using XSystem.Security.Cryptography;

namespace DailyPlanner.Repository.Hashing
{
    public static class PasswordHashing
    {
        public static string HashPassword(string password)
        {
            var sha = new SHA256Managed();

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha.ComputeHash(bytes);

            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);

            return hash;
        }
    }
}
