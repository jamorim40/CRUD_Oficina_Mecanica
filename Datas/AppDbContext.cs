using Mecanica.Models.Entities;
using Mecanica.Models.Entities.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Mecanica.Datas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<OrdemServico> OrdemServicos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Nome).HasMaxLength(150).IsRequired();
                entity.Property(x => x.Telefone).HasMaxLength(20);
                entity.Property(x => x.Email).HasMaxLength(150);
                entity.Property(x => x.CpfCnpj).HasMaxLength(20);
                entity.HasIndex(x => x.CpfCnpj).IsUnique();
                entity.Property(x => x.CpfCnpj).IsRequired();
            });
            //Veiculo
            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Marca).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Modelo).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Placa).HasMaxLength(8).IsRequired();
                entity.HasIndex(x => x.Placa).IsUnique();
                entity.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
            });

            //OrdemServico
            // sequência para Romaneio (valor gerado pelo banco, sequencial)
            modelBuilder.HasSequence<int>("RomaneioSequence").StartsAt(1).IncrementsBy(1);

            modelBuilder.Entity<OrdemServico>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).UseIdentityColumn();
                entity.Property(x => x.Descricao).HasMaxLength(500).IsRequired();
                entity.Property(x => x.Observacao).HasMaxLength(500);
                // Romaneio será preenchido pelo banco usando a sequência RomaneioSequence
                entity.Property(x => x.Romaneio).HasDefaultValueSql("NEXT VALUE FOR RomaneioSequence");
                entity.HasOne(x => x.Veiculo).WithMany()
                    .HasForeignKey(x => x.VeiculoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //Cargo
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).UseIdentityColumn();
                entity.Property(x => x.Nome).HasMaxLength(100).IsRequired();
                entity.HasIndex(x => x.Nome).IsUnique();
            });

            //Funcionario
            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).UseIdentityColumn();
                entity.Property(x => x.Nome).HasMaxLength(150).IsRequired();
                entity.Property(x => x.CpfCnpj).HasMaxLength(20).IsRequired();
                entity.HasIndex(x => x.CpfCnpj).IsUnique();
                entity.Property(x => x.Telefone).HasMaxLength(20).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(150);
                entity.HasOne(x => x.Cargo).WithMany().HasForeignKey(x => x.CargoId).OnDelete(DeleteBehavior.Restrict);
            });

            //Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).UseIdentityColumn();
                entity.Property(x => x.Login).HasMaxLength(50);
                entity.HasIndex(x => x.Login).IsUnique();
                entity.HasOne(x => x.Funcionario).WithOne(x => x.Usuario).HasForeignKey<Usuario>(x => x.FuncionarioId).OnDelete(DeleteBehavior.Restrict);

            });

            //EntityBase
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) 
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("DataCadastro")
                        .HasDefaultValueSql("GETDATE()")
                        .ValueGeneratedOnAdd();
                }
            }
        }
    }
}
