﻿using jovemProgramadorMvc.Data.Repositorio.Interfaces;
using jovemProgramadorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Data.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly BancoContexto _bancoContexto;

        public AlunoRepositorio(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
        }

        public AlunoModel Inserir(AlunoModel aluno)
        {
            _bancoContexto.Aluno.Add(aluno);
            _bancoContexto.SaveChanges();
            return aluno;
        }

        public List<AlunoModel> BuscarAlunos()
        {
            return _bancoContexto.Aluno.ToList();
        }

        public AlunoModel BuscarId(int id)
        {
            return _bancoContexto.Aluno.FirstOrDefault(x => x.Id == id);
        }

        public bool Atualizar(AlunoModel aluno)
        {
            AlunoModel alunoDb = BuscarId(aluno.Id);

            if (alunoDb == null)
                return false;

            alunoDb.Nome = aluno.Nome;
            alunoDb.Idade = aluno.Idade;
            alunoDb.Contato = aluno.Contato;
            alunoDb.Email = aluno.Email;
            alunoDb.Cep = aluno.Cep;

            _bancoContexto.Aluno.Update(alunoDb);
            _bancoContexto.SaveChanges();

            return true;
        }

        public bool Excluir(int id)
        {
            AlunoModel aluno = BuscarId(id);

            if (aluno == null)
                return false;

            _bancoContexto.Aluno.Remove(aluno);
            _bancoContexto.SaveChanges();

            return true;
        }

        public List<AlunoModel> FiltroIdade(int idade, string operacao)
        {
            switch (operacao)
            {
                case ">":
                    return _bancoContexto.Aluno.Where(x => x.Idade > idade).ToList();
                case "<":
                    return _bancoContexto.Aluno.Where(x => x.Idade < idade).ToList();
                default:
                    return _bancoContexto.Aluno.Where(x => x.Idade == idade).ToList();
            }
            
        }

        public List<AlunoModel> FiltrarPorIdadeECep(AlunoModel aluno)
        {
            return _bancoContexto.Aluno.Where(x => x.Idade == aluno.Idade && x.Cep == aluno.Cep).ToList();
        }

        public List<AlunoModel> FiltrarCep(string cep)
        {
            return _bancoContexto.Aluno.Where(x =>  x.Cep == cep).ToList();
        }

        public List<AlunoModel> FiltrarNome(string nome)
        {
           return _bancoContexto.Aluno.Where(x => x.Nome == nome).ToList(); 
        }
    }
}
