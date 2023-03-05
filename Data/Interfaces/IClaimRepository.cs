using Amazon.Auth.AccessControlPolicy;
using TestProject1.Data.Models;

namespace TestProject1.Data.Interfaces
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetClaims();
        Task<Claim> GetClaimById(int id);
        Task<Claim> Create(Claim claim);
        Task<bool> Update(Claim claim);
        Task<bool> Delete(int id);
    }
}
