namespace RealEstate.API.Utility
{
    public static class UtilityFunctions
    {
        public static async Task<List<byte[]>> ConvertToByteArrayAsync(List<IFormFile> formFiles, ILogger logger)
        {
            var byteArrays = new List<byte[]>();
            foreach (var file in formFiles)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        byteArrays.Add(ms.ToArray());
                        logger.LogInformation($"Converted file '{file.FileName}' to byte array of size {ms.ToArray().Length} bytes");

                    }
                }
            }
            return byteArrays;
        }
        public static async Task<List<byte[]>> ConvertImagesToByteArrayAsync(List<IFormFile> imageFiles, ILogger logger)
        {
            var byteArrays = new List<byte[]>();
            foreach (var file in imageFiles)
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        byteArrays.Add(memoryStream.ToArray());
                        logger.LogInformation($"Converted file '{file.FileName}' to byte array of size {memoryStream.ToArray().Length} bytes");
                    }
                }
                else
                {
                    logger.LogWarning($"Received a file '{file.FileName}' with 0 length.");
                }
            }
            return byteArrays;
        }

    }
}
