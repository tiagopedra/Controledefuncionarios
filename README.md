 Controle de Funcionários — API REST em .NET 8

API minimalista para gestão de funcionários, cargos, setores, endereços e dados bancários. O projeto utiliza .NET 8, Entity Framework Core e banco SQLite, com organização de rotas por extensões e modelos fortemente tipados.

## Tecnologias

1. .NET 8 com Minimal APIs  
2. Entity Framework Core  
3. Provider SQLite  
4. C#

## Principais recursos

1. Cadastro e consulta de Funcionários  
2. Cadastro e consulta de Cargos e Setores  
3. Associação de Funcionários a Cargos e Setores  
4. Endereços e Dados Bancários vinculados ao Funcionário  
5. Endpoints padronizados para operações de leitura, criação e exclusão

## Endpoints

**Leitura**  
GET /api/funcionario  
GET /api/funcionario/{id}  
GET /api/funcionario/cargo/{cargoId}  
GET /api/funcionario/setor/{setorId}  
GET /api/cargo  
GET /api/cargo/{id}  
GET /api/setor  
GET /api/setor/{id}  
GET /api/pessoa  
GET /api/pessoa/{id}  
GET /api/endereco  
GET /api/endereco/{id}  
GET /api/endereco/funcionario/{id}  
GET /api/dadosbancarios  
GET /api/dadosbancarios/{id}  
GET /api/dadosbancarios/funcionario/{id}

**Criação**  
POST /api/funcionario  
POST /api/cargo  
POST /api/setor  
POST /api/pessoa  
POST /api/endereco  
POST /api/dadosbancarios

**Exclusão**  
DELETE /api/funcionario/{id}  
DELETE /api/cargo/{id}  
DELETE /api/setor/{id}  
DELETE /api/pessoa/{id}  
DELETE /api/endereco/{id}  
DELETE /api/dadosbancarios/{id}

## Modelos principais

1. Funcionario com vínculos para Cargo, Setor, Endereco e DadosBancarios  
2. Cargo com descrição  
3. Setor com nome e descrição  
4. Pessoa com dados básicos  
5. Endereco associado ao Funcionário  
6. DadosBancarios associados ao Funcionário

## Como executar

1. **Pré-requisitos**  
   .NET SDK 8 instalado

2. **Passos**  
   ```bash
   git clone <url-do-repositorio>
   cd Project---Api
   dotnet restore
   dotnet build
   dotnet run
   ```
   A aplicação inicia no Kestrel na porta padrão informada no console.

3. **Banco de dados**  
   Provider SQLite configurado em AppDbContext  
   ```bash
   Data Source=controleDeFuncionarios.db
   ```
   O arquivo é criado automaticamente no diretório do projeto.

## Estrutura

- **Program.cs** configura o pipeline e mapeia as rotas  
- **Rotas/** contém os métodos de extensão (GET, POST, DELETE)  
- **Models/** define as entidades  
- **Dao/** contém o AppDbContext  
- **Migrations/** gerencia o versionamento do schema

## Exemplos de requisição

**Criar Funcionário**

```bash
POST /api/funcionario
Content-Type: application/json

{
  "nome": "Ana Silva",
  "cargoId": 1,
  "setorId": 1,
  "endereco": {
    "logradouro": "Rua A",
    "numero": "100",
    "cidade": "Curitiba",
    "estado": "PR"
  },
  "dadosBancarios": {
    "banco": "XYZ",
    "agencia": "0001",
    "conta": "12345-6"
  }
}
```

**Consultar Funcionário por ID**

```bash
GET /api/funcionario/1
```

**Excluir Funcionário**

```bash
DELETE /api/funcionario/1
```

## Próximos passos sugeridos

1. Adicionar documentação interativa com Swagger  
2. Implementar validações com FluentValidation  
3. Criar testes de unidade e integração  
4. Padronizar respostas com objetos de resultado e códigos HTTP consistentes

---

**Créditos:** Este projeto foi idealizado e desenvolvido por **Matheus João Corrêa** e **Breno Andrade da Silva**, integrando conhecimentos práticos de programação em C# e arquitetura de software. A aplicação representa uma API REST completa criada no contexto acadêmico de Desenvolvimento de Software.
