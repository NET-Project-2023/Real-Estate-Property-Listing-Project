namespace RealEstate.API.Utility
{
    public static class UtilityFunctions
    {
        public static async Task<List<byte[]>> ConvertToByteArrayAsync(List<IFormFile> formFiles)
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
                    }
                }
            }
            return byteArrays;
        }

    }
}
