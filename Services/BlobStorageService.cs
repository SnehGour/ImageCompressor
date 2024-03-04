using Azure.Storage;
using Azure.Storage.Blobs;
using ImageCompressor.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace ImageCompressor.Services
{
    public class BlobStorageService : IBlobStorage
    {
        private readonly string _storageAccount;
        private readonly string _key;
        private readonly string _blobContainerName;
        private readonly IConfiguration _configuration;

        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClinet;


        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = _configuration["Azure:BlobStorageKey"];
            _storageAccount = _configuration["Azure:StorageAccountName"];
            _blobContainerName = _configuration["Azure:ContainerName"];

            var credential = new StorageSharedKeyCredential(_storageAccount, _key);
            var blobUrl = $"https://{_storageAccount}.blob.core.windows.net";

            _blobServiceClient = new BlobServiceClient(new Uri(blobUrl),credential);
            _blobContainerClinet = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                BlobClient blobClient= _blobContainerClinet.GetBlobClient(blobName);
                await blobClient.UploadAsync(file.OpenReadStream(), true);
                return blobClient.Uri.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
