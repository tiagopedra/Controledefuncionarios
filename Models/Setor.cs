using System.Text.Json.Serialization;

namespace controleDeFuncionarios.Models;

public class Setor {
    public int id { get; set; }
    public string nome { get; set; }
    public string descricao { get; set; }

    [JsonIgnore]
    public ICollection<Funcionario> Funcionarios { get; set; }

}