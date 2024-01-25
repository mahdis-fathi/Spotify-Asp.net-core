namespace Spotify.Services
{
    public interface IUpload
    {
        string UploadObjectFromFile(IFormFile file, string keyName);
    }
}
