namespace RestAPI.Utils
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
            var random = new Random();
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
