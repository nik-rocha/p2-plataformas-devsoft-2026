using Microsoft.EntityFrameworkCore;
using APIP2Minardi.Models;

namespace APIP2Minardi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Setor> Setores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<PrescricaoGeral> PrescricoesGerais { get; set; }
        public DbSet<PrescricaoMedicamento> PrescricoesMedicamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                => options.UseSqlite("Data Source=hospital.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setor>().HasKey(s => s.Id);
            modelBuilder.Entity<Paciente>().HasKey(p => p.Id);
            modelBuilder.Entity<Medico>().HasKey(p => p.Id);
            modelBuilder.Entity<Medicamento>().HasKey(p => p.Id);
            modelBuilder.Entity<PrescricaoGeral>().HasKey(p => p.Id);
            modelBuilder.Entity<PrescricaoMedicamento>().HasKey(p => p.Id);

            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Setor)
                .WithMany(s => s.Pacientes)
                .HasForeignKey(p => p.SETOR_id);

            modelBuilder.Entity<PrescricaoGeral>()
                .HasOne(p => p.Medico)
                .WithMany(m => m.PrescricaosGerais)
                .HasForeignKey(p => p.MEDICO_id);

            modelBuilder.Entity<PrescricaoGeral>()
                .HasOne(p => p.Paciente)
                .WithMany(pac => pac.PrescricoesGerais)
                .HasForeignKey(p => p.PACIENTE_id);

            modelBuilder.Entity<PrescricaoMedicamento>()
                .HasOne(pm => pm.PrescricaoGeral)
                .WithMany(p => p.PrecricaoMedicamentos)
                .HasForeignKey(pm => pm.PRESCRICAO_id);

            modelBuilder.Entity<PrescricaoMedicamento>()
                .HasOne(pm => pm.Medicamentos)
                .WithMany(m => m.PrecricaoMedicamentos)
                .HasForeignKey(pm => pm.MEDICAMENTO_id);
        }
    }
}