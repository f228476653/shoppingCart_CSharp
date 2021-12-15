
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.AggregatesModel;
#nullable disable

namespace ShoppingCart.Domain.Infrastructure
{
    public partial class ShoppingCartContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected IMediator _mediator;
        public ShoppingCartContext()
        {
        }
        public ShoppingCartContext(Func<DbContextOptions<ShoppingCartContext>> fnCreateDbContextOptions, int dbTimeout = default)
            : base(fnCreateDbContextOptions())
        {
            
        }

        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CartDto> ShoppingCart { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CartDto>(entity =>
            {
                entity.ToTable("ShoppingCart");
            });
        }

        
        public void SetMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            return await base.SaveChangesAsync(cancellationToken);
        }

/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = localhost; Database = ShoppingCart; Trusted_Connection = True;");
        }*/
    }
}
