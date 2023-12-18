using DataAccess;

namespace BusinessLogic;

/// <summary>
///     Represents a class for convert ClientDto to DbClient and vice versa.
/// </summary>
public static class ClientExtensions
{
    public static ClientDto ClientToDto(this DbClient dbClient)
        => new()
        {
            Id = dbClient.Id,
            Name = dbClient.Name,
            Digest = dbClient.Digest,
            WalletId = dbClient.WalletId,
            Wallet = dbClient.Wallet?.WalletToDto()
        };

    public static DbClient DtoToClient(this ClientDto clientDto)
        => new()
        {
            Id = clientDto.Id,
            Name = clientDto.Name,
            Digest = clientDto.Digest,
            WalletId = clientDto.WalletId,
            Wallet = clientDto.Wallet?.DtoToWallet()
        };
}