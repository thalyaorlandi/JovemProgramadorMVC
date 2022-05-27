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
    public class ProfessorController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProfessorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Adicionado()
        {
            return View();
        }
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            EnderecoModel enderecoModel = new();

            try
            {
                cep = cep.Replace("-", "");

                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

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
    }
}
