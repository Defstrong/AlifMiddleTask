using DataAccess;

namespace BusinessLogic;

public static class ClientExtensions
{
    public static ClientDto ClientToDto(this DbClient dbClient)
        => new()
        {
            Id = dbClient.Id,
            Name = dbClient.Name,
            WalletId = dbClient.WalletId,
            Wallet = dbClient.Wallet?.WalletToDto()
        };

    public static DbClient DtoToClient(this ClientDto clientDto)
        => new()
        {
            Id = clientDto.Id,
            Name = clientDto.Name,
            WalletId = clientDto.WalletId,
            Wallet = clientDto.Wallet?.DtoToWallet()
        };
}