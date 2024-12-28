namespace Shared.Utils
{
    public class Generator
    {
        public static string GenerateRandomString(int length)
        {
            if (length < 256)
            {
                throw new ArgumentException("Die Länge muss mindestens 256 Zeichen betragen.", nameof(length));
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder(length);

            var random = new Random();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        public static string GenerateUniqueUsername()
        { 
            return $"Miku{GenerateRandomNumber()}";
        }

        private static string GenerateRandomNumber()
        {
            var random = new Random();
            return random.Next(0, 1000000000).ToString("D10");
        }
    }
}
