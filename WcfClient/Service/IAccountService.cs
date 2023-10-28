using WcfClient.Models;

namespace WcfClient.Service
{
    public interface IAccountService
    {
        Task<List<AccountModels>?> GetAccounts();
        Task<AccountModels?> GetAccount(int id);
        Task<AccountModels?> Login(string email, string password);
        Task<int?> Create(AccountModels acc);
        Task<int?> Edit(AccountModels acc, int id);
        Task<int?> Delete(int id);
    }
}
