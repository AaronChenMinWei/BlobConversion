namespace BlobConversion.Models
{
    public class Blob
    {
        public Blob(string _base64String, int _sizeInByte)
        {
            Base64String = _base64String;
            SizeInByte = _sizeInByte;
        }

        public string Base64String { get; set; }
        public int SizeInByte { get; set; }
    }
}
