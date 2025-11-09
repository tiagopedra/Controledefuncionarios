using System.Text.Json.Serialization;

namespace controleDeFuncionarios.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; } 

        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        
        [JsonIgnore]
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
