using jovemProgramadorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Data.Repositorio.Interfaces
{
    public interface IEnderecoAlunoRepositorio
    {
        EnderecoModel Inserir(EnderecoModel Endereco);
    }
}
