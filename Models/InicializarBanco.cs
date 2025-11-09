namespace controleDeFuncionarios.Models;
using Microsoft.Extensions.DependencyInjection;
using controleDeFuncionarios.Dao;

public static class InicializarBanco
{
   public static void  PopularBancoDeDados(IServiceProvider app)
    {
        using (var scope = app.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            if (!context.Setores.Any())
            {
                context.Setores.Add(new Setor { nome = "RH", descricao = "Recursos Humanos" });
                context.Setores.Add(new Setor { nome = "TI", descricao = "Tecnologia da Informação" });
                context.Setores.Add(new Setor { nome = "Financeiro", descricao = "Setor Financeiro" });
                context.Setores.Add(new Setor { nome = "Marketing", descricao = "Marketing e Vendas" });
                context.Setores.Add(new Setor { nome = "Logística", descricao = "Logística e Transporte" });
                context.Setores.Add(new Setor { nome = "Produção", descricao = "Produção e Fabricação" });
                context.Setores.Add(new Setor { nome = "Suporte", descricao = "Suporte Técnico" });
                context.Setores.Add(new Setor { nome = "Vendas", descricao = "Vendas e Atendimento ao Cliente" });
                context.Setores.Add(new Setor { nome = "Pesquisa e Desenvolvimento", descricao = "Pesquisa e Desenvolvimento" });
                context.SaveChanges();
            }

            if (!context.Cargos.Any())
            {
                context.Cargos.Add(new Cargo { nome = "Gerente", descricao = "Gerente de Setor" });
                context.Cargos.Add(new Cargo { nome = "Analista", descricao = "Analista de Sistemas" });
                context.Cargos.Add(new Cargo { nome = "Desenvolvedor", descricao = "Desenvolvedor de Software" });
                context.Cargos.Add(new Cargo { nome = "Assistente", descricao = "Assistente Administrativo" });
                context.Cargos.Add(new Cargo { nome = "Coordenador", descricao = "Coordenador de Projetos" });
                context.SaveChanges();
            }

          if (!context.DadosBancarios.Any())
            {
                context.DadosBancarios.Add(new DadosBancarios { banco = "Itaú", agencia = "1234", conta = "56789-0", tipoConta = "Corrente", cpfTitular = "12345678901", 
                                                            nomeTitular = "João Silva", telefoneTitular = "41987654321", emailTitular = "joaoSilva@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Bradesco", agencia = "2345", conta = "67890-1", tipoConta = "Poupança", cpfTitular = "23456789012", 
                                                            nomeTitular = "Maria Oliveira", telefoneTitular = "41987654322", emailTitular = "mariaOliveira@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Santander", agencia = "3456", conta = "78901-2", tipoConta = "Corrente", cpfTitular = "34567890123", 
                                                            nomeTitular = "Carlos Santos", telefoneTitular = "41987654323", emailTitular = "carlosSantos@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Caixa", agencia = "4567", conta = "89012-3", tipoConta = "Poupança", cpfTitular = "45678901234", 
                                                            nomeTitular = "Ana Paula Souza", telefoneTitular = "41987654324", emailTitular = "anaPaula@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Banco do Brasil", agencia = "5678", conta = "90123-4", tipoConta = "Corrente", cpfTitular = "56789012345", 
                                                            nomeTitular = "Bruno Lima", telefoneTitular = "41987654325", emailTitular = "brunoLima@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Itaú", agencia = "6789", conta = "12345-5", tipoConta = "Poupança", cpfTitular = "67890123456", 
                                                            nomeTitular = "Fernanda Costa", telefoneTitular = "41987654326", emailTitular = "fernandaCosta@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Bradesco", agencia = "7890", conta = "23456-6", tipoConta = "Corrente", cpfTitular = "78901234567", 
                                                            nomeTitular = "Ricardo Alves", telefoneTitular = "41987654327", emailTitular = "ricardoAlves@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Santander", agencia = "8901", conta = "34567-7", tipoConta = "Poupança", cpfTitular = "89012345678", 
                                                            nomeTitular = "Juliana Martins", telefoneTitular = "41987654328", emailTitular = "julianaMartins@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Caixa", agencia = "9012", conta = "45678-8", tipoConta = "Corrente", cpfTitular = "90123456789", 
                                                            nomeTitular = "Paulo Henrique", telefoneTitular = "41987654329", emailTitular = "pauloHenrique@email.com" });
                context.DadosBancarios.Add(new DadosBancarios { banco = "Banco do Brasil", agencia = "0123", conta = "56789-9", tipoConta = "Poupança", cpfTitular = "01234567890", 
                                                            nomeTitular = "Camila Rocha", telefoneTitular = "41987654330", emailTitular = "camilaRocha@email.com" });
                context.SaveChanges();
            }

           if(!context.Enderecos.Any())
            {
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua XV de Novembro", Numero = "123", Complemento = "Apto 101", 
                                                    Bairro = "Centro", Estado = "PR", Cep = "80020-310"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Marechal Deodoro", Numero = "456", Complemento = "Casa 2",
                                                    Bairro = "Alto da Glória", Estado = "PR", Cep = "80030-200"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Visconde de Nácar", Numero = "789", Complemento = "Fundos",
                                                    Bairro = "São Francisco", Estado = "PR", Cep = "80510-200"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Brigadeiro Franco", Numero = "1011", Complemento = "Residencial Alfa",
                                                    Bairro = "Rebouças", Estado = "PR", Cep = "80230-220"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Itupava", Numero = "1213", Complemento = "Casa 5",
                                                    Bairro = "Alto da XV", Estado = "PR", Cep = "80045-100"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Chile", Numero = "1415", Complemento = "Apto 302",
                                                    Bairro = "Água Verde", Estado = "PR", Cep = "80240-120"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua João Negrão", Numero = "1617", Complemento = "Casa 8",
                                                    Bairro = "Centro", Estado = "PR", Cep = "80010-200"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Mateus Leme", Numero = "1819", Complemento = "Residencial Beta",
                                                    Bairro = "Bom Retiro", Estado = "PR", Cep = "80510-170"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Desembargador Westphalen", Numero = "2021", Complemento = "Casa 9",
                                                    Bairro = "Centro", Estado = "PR", Cep = "80230-110"});
                context.Enderecos.Add(new Endereco {TipoLogradouro = "Rua", Logradouro = "Rua Alferes Poli", Numero = "2223", Complemento = "Apto 404",
                                                    Bairro = "Centro", Estado = "PR", Cep = "80230-090"});
                context.SaveChanges();
            } 

            if(!context.Pessoas.Any())
            {
                context.Pessoas.Add(new Pessoa { nome = "João Silva", cpf = "12345678901", dataNascimento = new DateTime(1990, 1, 1), telefone = "41987654321",
                                sexo = "Masculino", email = "joaoSilva@email.com", enderecoID = 1});
                context.Pessoas.Add(new Pessoa { nome = "Maria Oliveira", cpf = "23456789012", dataNascimento = new DateTime(1985, 2, 2), telefone = "41987654322",
                                                sexo = "Feminino", email = "mariaOliveira@email.com", enderecoID = 2});
                context.Pessoas.Add(new Pessoa { nome = "Carlos Santos", cpf = "34567890123", dataNascimento = new DateTime(1992, 3, 3), telefone = "41987654323",
                                                sexo = "Masculino", email = "carlosSantos@email.com", enderecoID = 3});
                context.Pessoas.Add(new Pessoa { nome = "Ana Paula Souza", cpf = "45678901234", dataNascimento = new DateTime(1988, 4, 4), telefone = "41987654324",
                                                sexo = "Feminino", email = "anaPaula@email.com", enderecoID = 4});
                context.Pessoas.Add(new Pessoa { nome = "Bruno Lima", cpf = "56789012345", dataNascimento = new DateTime(1995, 5, 5), telefone = "41987654325",
                                                sexo = "Masculino", email = "brunoLima@email.com", enderecoID = 5});
                context.Pessoas.Add(new Pessoa { nome = "Fernanda Costa", cpf = "67890123456", dataNascimento = new DateTime(1991, 6, 6), telefone = "41987654326",
                                                sexo = "Feminino", email = "fernandaCosta@email.com", enderecoID = 6});
                context.Pessoas.Add(new Pessoa { nome = "Ricardo Alves", cpf = "78901234567", dataNascimento = new DateTime(1987, 7, 7), telefone = "41987654327",
                                                sexo = "Masculino", email = "ricardoAlves@email.com", enderecoID = 7});
                context.Pessoas.Add(new Pessoa { nome = "Juliana Martins", cpf = "89012345678", dataNascimento = new DateTime(1993, 8, 8), telefone = "41987654328",
                                                sexo = "Feminino", email = "julianaMartins@email.com", enderecoID = 8});
                context.Pessoas.Add(new Pessoa { nome = "Paulo Henrique", cpf = "90123456789", dataNascimento = new DateTime(1989, 9, 9), telefone = "41987654329",
                                                sexo = "Masculino", email = "pauloHenrique@email.com", enderecoID = 9});
                context.Pessoas.Add(new Pessoa { nome = "Camila Rocha", cpf = "01234567890", dataNascimento = new DateTime(1994, 10, 10), telefone = "41987654330",
                                                sexo = "Feminino", email = "camilaRocha@email.com", enderecoID = 10});
                context.SaveChanges();
            }

            if(!context.Funcionarios.Any())
            {
                context.Funcionarios.Add(new Funcionario { pessoaId = 1, setorId = 1, salario = 5000, cargoId = 1, dadosBancariosId = 1, enderecoID = 1 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 2, setorId = 2, salario = 6000, cargoId = 2, dadosBancariosId = 2, enderecoID = 2 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 3, setorId = 3, salario = 7000, cargoId = 3, dadosBancariosId = 3, enderecoID = 3 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 4, setorId = 4, salario = 8000, cargoId = 4, dadosBancariosId = 4, enderecoID = 4 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 5, setorId = 5, salario = 9000, cargoId = 5, dadosBancariosId = 5, enderecoID = 5 }); 
                context.Funcionarios.Add(new Funcionario { pessoaId = 6, setorId = 4, salario = 7000, cargoId = 4, dadosBancariosId = 6, enderecoID = 6 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 7, setorId = 3, salario = 8000, cargoId = 4, dadosBancariosId = 7, enderecoID = 7 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 8, setorId = 3, salario = 8000, cargoId = 3, dadosBancariosId = 8, enderecoID = 8 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 9, setorId = 5, salario = 8000, cargoId = 5, dadosBancariosId = 9, enderecoID = 9 });
                context.Funcionarios.Add(new Funcionario { pessoaId = 10, setorId = 1, salario = 8000, cargoId = 1, dadosBancariosId = 10, enderecoID = 10 });

                context.SaveChanges();
            }

        }
    }
}
