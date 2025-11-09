using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace controleDeFuncionarios.Models
{
 public class Pessoa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public DateTime dataNascimento { get; set; }
        public string cpf { get; set; }
        public string sexo { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public int enderecoID { get; set;}

        [JsonIgnore]
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}


