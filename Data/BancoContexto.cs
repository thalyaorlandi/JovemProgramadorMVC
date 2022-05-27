using jovemProgramadorMvc.Data.Mapeamento;
using jovemProgramadorMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Data
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoMapeamento());
            modelBuilder.ApplyConfiguration(new EnderecoAlunoMapeamento());
        }

        public DbSet<AlunoModel> Aluno { get; set; }
        public DbSet<EnderecoModel>  Enderecos { get; set; }
    }
}
