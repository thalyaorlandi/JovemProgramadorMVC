using jovemProgramadorMvc.Data.Repositorio.Interfaces;
using jovemProgramadorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Data.Repositorio
{
    public class EnderecoAlunoRepositorio : IEnderecoAlunoRepositorio
    {
        private readonly BancoContexto _bancoContexto;

        public EnderecoAlunoRepositorio(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
        }

        public EnderecoModel Inserir(EnderecoModel Endereco)
        {
            _bancoContexto.Enderecos.Add(Endereco);
            _bancoContexto.SaveChanges();
            return Endereco;
        }
    }
}
