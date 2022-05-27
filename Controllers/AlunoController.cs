using jovemProgramadorMvc.Data.Repositorio.Interfaces;
using jovemProgramadorMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAlunoRepositorio _alunorepositorio;
        private readonly IEnderecoAlunoRepositorio _EnderecoAlunorepositorio;

        public AlunoController(IConfiguration configuration, IAlunoRepositorio alunoRepositorio, IEnderecoAlunoRepositorio enderecoAlunoRepositorio)
        {
            _configuration = configuration;
            _alunorepositorio = alunoRepositorio;
            _EnderecoAlunorepositorio = enderecoAlunoRepositorio;
        }

        public IActionResult Index()
        {
            var aluno = _alunorepositorio.BuscarAlunos();
            return View(aluno);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Adicionado()
        {
            return View();
        }

        public async Task<IActionResult> BuscarEndereco(int id)
        {
            EnderecoModel enderecoModel = new();

            try
            {
                var aluno = _alunorepositorio.BuscarId(id);

                aluno.Cep = aluno.Cep.Replace("-", "");

                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + aluno.Cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });

                    if (string.IsNullOrWhiteSpace(enderecoModel.Complemento))
                    {
                        enderecoModel.Complemento = "Nenhum";
                    }

                    if (string.IsNullOrWhiteSpace(enderecoModel.Logradouro))
                    {
                        enderecoModel.Logradouro = "Cep Geral";
                    }

                    if (string.IsNullOrWhiteSpace(enderecoModel.Bairro))
                    {
                        enderecoModel.Bairro = "Cep Geral";
                    }

                    enderecoModel.IdAluno = id;

                    _EnderecoAlunorepositorio.Inserir(enderecoModel);
                }
                else
                {
                    ViewData["mensagem"] = "Erro na busca do endereço!";
                    return View("Index");
                }
            }
            catch (Exception e)
            {

            }

            return View("BuscarEndereco", enderecoModel);
        }

        [HttpPost]
        public IActionResult Inserir(AlunoModel aluno)
        {
            _alunorepositorio.Inserir(aluno);
            return RedirectToAction("Adicionado");
        }

        public IActionResult Editar(int id)
        {
            var aluno = _alunorepositorio.BuscarId(id);
            return View("Editar", aluno);
        }

        public IActionResult Atualizar(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.Atualizar(aluno);

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.Excluir(aluno.Id);

            if (retorno)
            {
                return RedirectToAction("Excluido");
            }
            else
            {
                return RedirectToAction("Erro", "Aluno");
            }

        }

        public IActionResult Excluido()
        {
            return View();
        }

        public IActionResult Erro()
        {
            return View("Erro");
        }

        public IActionResult Filtros()
        {
            return View();
        }

        public IActionResult Filtrar(AlunoModel aluno)
        {
            List<AlunoModel> alunos = new List<AlunoModel>();
            if (aluno.Idade > 0 && !string.IsNullOrWhiteSpace(aluno.Cep))
            {
                alunos = _alunorepositorio.FiltrarPorIdadeECep(aluno);
            }
            else if (aluno.Idade > 0)
            {
                alunos = _alunorepositorio.FiltroIdade(aluno.Idade, aluno.Operacao);
            }
            else if ( !string.IsNullOrWhiteSpace(aluno.Nome))
            {
                alunos = _alunorepositorio.FiltrarNome(aluno.Nome);
            }

            else 
            {
                alunos = _alunorepositorio.FiltrarCep(aluno.Cep);
            }

            return View("Index", alunos);
        }

    }
}
