﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jovemProgramadorMvc.Models
{
    public class ProfessorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Contato { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
    }
}
