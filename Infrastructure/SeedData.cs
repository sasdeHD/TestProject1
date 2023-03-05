using Microsoft.EntityFrameworkCore;
using TestProject1.Data.Models;

namespace TestProject1.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Claims.Any())
                {
                    return;
                }
                for (int i = 0; i < 10; i++)
                {
                    var testData = new Claim
                    {
                        Name = "Test" + i,
                        StatementDate = DateTime.Now,
                        Message = "Test_message" + i,
                        Email = "Mail"+i+"@mail.ru",
                        AdditionalData = new Dictionary<string, object>()
                        {
                            { "key_0_" + i.ToString(), "Test_0_" + i },
                            { "key_0_" + (i+1).ToString(), "Test_1_" + i },
                            { "key_0_" + (i+2).ToString(), "Test_2_" + i },
                        }
                    };
                    context.Add(testData);
                }
                context.SaveChanges();
            }
        }

    }
}
