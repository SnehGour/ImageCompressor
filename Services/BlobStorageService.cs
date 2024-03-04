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
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            var credential = new StorageSharedKeyCredential(_storageAccount,key)
            string connectionString = _configuration["Azure:BlobStorageConnection"];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                //string connectionString = _configuration["Azure:BlobStorageConnection"];
                
                
                _containerName = _configuration.GetConnectionString("simagecompressor");
                string blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
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
