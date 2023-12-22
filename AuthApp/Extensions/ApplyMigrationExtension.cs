using AuthApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.Extensions
{
    public static class ApplyMigrationExtension
    {
        public static void ApplyPendingMigrations(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                int pendingMigrationCount = db.Database.GetPendingMigrations().Count();
                if (pendingMigrationCount > 0)
                {
                    db.Database.Migrate();
                }
            }
        }
    }
}
