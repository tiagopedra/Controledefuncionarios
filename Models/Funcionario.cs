using System.ComponentModel.DataAnnotations.Schema;

namespace controleDeFuncionarios.Models;

public class Funcionario
{
    public int id { get; set; }
    public int pessoaId { get; set; }
    public Pessoa pessoa { get; set; }
    public int setorId { get; set; }
    public Setor setor { get; set; }
    public decimal salario { get; set; }
    public int cargoId { get; set; }
    public Cargo cargo { get; set; }
    public int dadosBancariosId { get; set; }
    public DadosBancarios dadosBancarios { get; set; }
    public int enderecoID { get; set; }
    public Endereco endereco { get; set; }

    [NotMapped]
    public ICollection<Setor> Setores { get; set; }
}