namespace FSS.Data
{
    using FSS.Data.Shared.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Reflection;

    internal class EntityIndexesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            var deletableEntityTypes = modelBuilder.Model
                    .GetEntityTypes()
                    .Where(e => e.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(e.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IDeletableEntity.IsDeleted));
            }
        }        
    }
}
