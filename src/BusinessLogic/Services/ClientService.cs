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

    public Task<bool> CreateAsync(ClientDto model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        return _clientRepository.CreateAsync(model.DtoToClient(), cancellationToken);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        return _clientRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<ClientDto?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(id);

        DbClient? dbclient = await _clientRepository.GetAsync(id, cancellationToken);
        return dbclient?.ClientToDto();
    }

    public async IAsyncEnumerable<ClientDto> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (DbClient clientDto in _clientRepository.GetAsync(cancellationToken))
            yield return clientDto.ClientToDto();
    }

    public Task<bool> UpdateAsync(ClientDto model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        DbClient dbclient = model.DtoToClient();
        return _clientRepository.UpdateAsync(dbclient, cancellationToken);
    }

    public Task<bool> CheckAsync(string id, CancellationToken cancellationToken = default)
        => _clientRepository.CheckAsync(id, cancellationToken);
}
