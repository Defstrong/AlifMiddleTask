using DataAccess;
using System.Runtime.CompilerServices;

namespace BusinessLogic;

public sealed class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        ArgumentNullException.ThrowIfNull(clientRepository);

        _clientRepository = clientRepository;
    }

    public Task<bool> CreateAsync(ClientDto model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        return _clientRepository.CreateAsync(model.DtoToClient(), token);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _clientRepository.DeleteAsync(id, token);
    }

    public async Task<ClientDto?> GetAsync(string id, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        DbClient? dbclient = await _clientRepository.GetAsync(id, token);
        return dbclient?.ClientToDto();
    }

    public async IAsyncEnumerable<ClientDto> GetAsync([EnumeratorCancellation] CancellationToken token = default)
    {
        await foreach (DbClient clientDto in _clientRepository.GetAsync(token))
            yield return clientDto.ClientToDto();
    }

    public Task<bool> UpdateAsync(ClientDto model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        DbClient dbclient = model.DtoToClient();
        return _clientRepository.UpdateAsync(dbclient, token);
    }
}