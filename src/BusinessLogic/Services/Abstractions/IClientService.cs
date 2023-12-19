namespace BusinessLogic;

public interface IClientService : IBaseService<ClientDto>
{
    Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default);
}