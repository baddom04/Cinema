namespace Cinema.Web.Utility
{
    public static class ImageExtension
    {
        public static string ToImgSrc(this byte[] img)
        {
            var base64 = Convert.ToBase64String(img);
            return $"data:image/jpg;base64,{base64}";
        }
    }
}
