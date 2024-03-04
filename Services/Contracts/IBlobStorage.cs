namespace ImageCompressor.Services.Contracts
{
    public interface IBlobStorage
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
