namespace RestAPI.Utils
{
    public class ImageUtils
    {
        /// <summary>
        /// Determines if the provided byte array represents a valid avatar image format.
        /// </summary>
        /// <param name="bytes">The byte array to check.</param>
        /// <param name="mimeType">The MIME type of the image if a valid format is found; otherwise, null.</param>
        /// <returns>True if the byte array represents a valid avatar image format; otherwise, false.</returns>
        public bool IsAvatarImageFormat(byte[] bytes, out string? mimeType)
        {
            mimeType = null;

            if (bytes == null || bytes.Length < 4)
                return false;

            // Check for known image formats based on magic bytes
            if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF) // JPEG
            {
                mimeType = "image/jpeg";
                return true;
            }
            if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47) // PNG
            {
                mimeType = "image/png";
                return true;
            }
            if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46) // GIF
            {
                mimeType = "image/gif";
                return true;
            }

            // No known format found
            return false;
        }
    }
}
