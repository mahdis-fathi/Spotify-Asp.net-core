using Amazon.S3.Transfer;
using Amazon.S3;


namespace Spotify.Services
{
    public class UploadService : IUpload
    {
        private readonly TransferUtility _fileTransferUtility;

        private const string BucketName = "spotify-songs";

        public UploadService()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("c75c17bc-a808-46ab-87e2-148719a4e4fc",
              "fc497d24ba0b93cb02e245732541160ee98ce7a1bb0700d061a69308cd1a43f3");
            var config = new AmazonS3Config { ServiceURL = "https://s3.ir-tbz-sh1.arvanstorage.ir" };
            IAmazonS3 s3Client = new AmazonS3Client(awsCredentials, config);
            _fileTransferUtility = new TransferUtility(s3Client);
        }
        public string UploadObjectFromFile(IFormFile file, string keyName)
        {
            try
            {
                var request = new TransferUtilityUploadRequest()
                {
                    BucketName = BucketName,
                    Key = keyName,
                    InputStream = file.OpenReadStream(),
                    CannedACL = S3CannedACL.PublicRead
                };
                _fileTransferUtility.Upload(request);
                Console.WriteLine("Upload Completed!");

                var url = $"https://{BucketName}.s3.ir-tbz-sh1.arvanstorage.ir/{keyName}";
                return url;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            return null!;
        }
    }
}
