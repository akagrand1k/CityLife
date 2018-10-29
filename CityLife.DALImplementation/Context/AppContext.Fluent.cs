using CityLife.Entities.Models.News;
using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.DALImplementation.Context
{
   public partial class AppContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureNewstagsEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        private void ConfigureNewstagsEntity(DbModelBuilder builder)
        {

            builder.Entity<Article>()
                .HasMany<ApplicationReferences>(x => x.Tags)
                .WithMany(x => x.CityNews)
                .Map(e =>
                {
                    e.MapLeftKey("ArticleId");
                    e.MapRightKey("TagId");
                    e.ToTable("NewsTags");
                });
        }
    }
}
