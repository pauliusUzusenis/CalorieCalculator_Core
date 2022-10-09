using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CalorieCalculatorCore.Extensions.Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach(IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.BaseType == null)
                {
                    entityType.SetTableName(entityType.DisplayName());
                }
            }
        }
    }
}
