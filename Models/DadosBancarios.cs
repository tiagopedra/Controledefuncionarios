using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace controleDeFuncionarios.Models;

public class DadosBancarios {
    public int id { get; set; }
    public string banco { get; set; }
    public string agencia { get; set; }
    public string conta { get; set; }
    public string tipoConta { get; set; }
    public string cpfTitular { get; set; }
    public string nomeTitular { get; set; }
    public string telefoneTitular { get; set; }
    public string emailTitular { get; set; }

    [JsonIgnore]
     public ICollection<Funcionario> Funcionarios { get; set; }
}