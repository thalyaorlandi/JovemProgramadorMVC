using jovemProgramadorMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Data.Mapeamento
{
    public class EnderecoAlunoMapeamento : IEntityTypeConfiguration<EnderecoModel>
    {
        public void Configure(EntityTypeBuilder<EnderecoModel> builder)
        {
            builder.ToTable("EnderecoAluno");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdAluno).HasColumnType("int");

            builder.Property(e => e.Logradouro).HasColumnType("varchar(200)");
            
            builder.Property(e => e.Complemento).HasColumnType("varchar(200)");
            
            builder.Property(e => e.Bairro).HasColumnType("varchar(50)");
            
            builder.Property(e => e.Localidade).HasColumnType("varchar(50)");
           
            builder.Property(e => e.Uf).HasColumnType("varchar(2)");
            
            builder.Property(e => e.Ddd).HasColumnType("varchar(3)");
        }
    }
}
