using Microsoft.EntityFrameworkCore;
using TestProject1.Data.Interfaces;
using TestProject1.Data.Models;
using TestProject1.Infrastructure;

namespace TestProject1.Data.Reprository
{
    public class ClaimReprository : IClaimRepository
    {
        private readonly AppDbContext context;

        public ClaimReprository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Claim> Create(Claim claim)
        {
            context.Claims.Add(claim);
            await context.SaveChangesAsync();
            return claim;
        }

        public async Task<bool> Delete(int id)
        {
            var claim = await context.Claims.FindAsync(id);
            if (claim == null)
            {
                return false;
            }

            context.Claims.Remove(claim);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Claim> GetClaimById(int id)
        {
            return await context.Claims.FirstOrDefaultAsync(s=>s.Id == id);
        }

        public async Task<IEnumerable<Claim>> GetClaims()
        {
            return await context.Claims.ToListAsync();
        }

        public async Task<bool> Update(Claim claim)
        {
            context.Entry(claim).State = EntityState.Modified;
            return await context.SaveChangesAsync() > 0;
        }
    }
}
