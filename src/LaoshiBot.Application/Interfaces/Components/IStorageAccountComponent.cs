namespace LaoshiBot.Application.Interfaces.Components
{
    public interface IStorageAccountComponent
    {
        Task<string> UploadAudioToStorageAccount(Guid id, Stream audio);
        Task<string> UploadImageToStorageAccount(Guid id, Stream image);
    }
}
