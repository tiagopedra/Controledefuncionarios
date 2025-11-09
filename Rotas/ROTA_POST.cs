using Microsoft.EntityFrameworkCore;
using controleDeFuncionarios.Models;
using controleDeFuncionarios.Dao;

namespace controleDeFuncionarios.Rotas;

public static class ROTA_POST
{
    public static void MapPostRoutes(this WebApplication app)
    {
        
        app.MapPost("/api/cargo", async (Cargo cargo, AppDbContext context) =>
        {
            if (string.IsNullOrWhiteSpace(cargo.descricao))
                cargo.descricao = "Sem descrição";
            context.Cargos.Add(cargo);
            await context.SaveChangesAsync();
            return Results.Created($"/api/cargo/{cargo.id}", cargo);
        });

        
        app.MapPost("/api/dadosbancarios", async (DadosBancarios dados, AppDbContext context) =>
        {
            context.DadosBancarios.Add(dados);
            await context.SaveChangesAsync();
            return Results.Created($"/api/dadosbancarios/{dados.id}", dados);
        });

        
        app.MapPost("/api/endereco", async (Endereco endereco, AppDbContext context) =>
        {
            context.Enderecos.Add(endereco);
            await context.SaveChangesAsync();
            return Results.Created($"/api/endereco/{endereco.Id}", endereco);
        });

        
        app.MapPost("/api/pessoa", async (Pessoa pessoa, AppDbContext context) =>
        {
            context.Pessoas.Add(pessoa);
            await context.SaveChangesAsync();
            return Results.Created($"/api/pessoa/{pessoa.id}", pessoa);
        });

        
        app.MapPost("/api/setor", async (Setor setor, AppDbContext context) =>
        {
            try
            {
                if (string.IsNullOrWhiteSpace(setor.descricao)) {
    if (!string.IsNullOrWhiteSpace(setor.nome))
        setor.descricao = setor.nome;
    else
        setor.descricao = "Sem descrição";
}
                Console.WriteLine($"[LOG] Recebido para criar setor: nome={setor?.nome}, id={setor?.id}");
                context.Setores.Add(setor);
                await context.SaveChangesAsync();
                Console.WriteLine($"[LOG] Setor criado com sucesso: id={setor.id}, nome={setor.nome}");
                return Results.Created($"/api/setor/{setor.id}", setor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Erro ao criar setor: {ex.Message}\n{ex.StackTrace}");
                var inner = ex.InnerException;
                int depth = 1;
                while (inner != null)
                {
                    Console.WriteLine($"[ERRO][INNER {depth}] {inner.Message}\n{inner.StackTrace}");
                    inner = inner.InnerException;
                    depth++;
                }
                return Results.Problem($"Erro ao criar setor: {ex.Message} - Veja console para detalhes das inner exceptions.");
            }
        });

        
        app.MapPost("/api/funcionario", async (Funcionario funcionario, AppDbContext context) =>
        {
            
            var cargo = await context.Cargos.FindAsync(funcionario.cargoId);
            if (cargo == null)
                return Results.BadRequest($"Cargo com id {funcionario.cargoId} não existe.");

            var setor = await context.Setores.FindAsync(funcionario.setorId);
            if (setor == null)
                return Results.BadRequest($"Setor com id {funcionario.setorId} não existe.");

            var dadosBancarios = await context.DadosBancarios.FindAsync(funcionario.dadosBancariosId);
            if (dadosBancarios == null)
                return Results.BadRequest($"DadosBancarios com id {funcionario.dadosBancariosId} não existe.");

            var endereco = await context.Enderecos.FindAsync(funcionario.enderecoID);
            if (endereco == null)
                return Results.BadRequest($"Endereco com id {funcionario.enderecoID} não existe.");

            
            if (funcionario.pessoa != null)
            {
                
                var pessoaExistente = await context.Pessoas.FirstOrDefaultAsync(p => p.cpf == funcionario.pessoa.cpf);
                if (pessoaExistente != null)
                {
                    funcionario.pessoaId = pessoaExistente.id;
                    funcionario.pessoa = pessoaExistente;
                }
                else
                {
                    
                    var novaPessoa = funcionario.pessoa;
                    context.Pessoas.Add(novaPessoa);
                    await context.SaveChangesAsync();
                    funcionario.pessoaId = novaPessoa.id;
                    funcionario.pessoa = novaPessoa;
                }
            }
            else
            {
                
                if (funcionario.pessoaId <= 0)
                    return Results.BadRequest("Pessoa ou pessoaId é obrigatório.");
            }

            
            funcionario.cargo = null;
            funcionario.setor = null;
            funcionario.dadosBancarios = null;
            funcionario.endereco = null;

            context.Funcionarios.Add(funcionario);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao salvar funcionário: {ex.Message}");
            }
            
            var funcionarioCompleto = await context.Funcionarios
                .Include(f => f.pessoa)
                .Include(f => f.setor)
                .Include(f => f.cargo)
                .Include(f => f.dadosBancarios)
                .Include(f => f.endereco)
                .FirstOrDefaultAsync(f => f.id == funcionario.id);
            return Results.Created($"/api/funcionario/{funcionario.id}", funcionarioCompleto);
        });

        
        app.MapPut("/api/cargo/{id}", async (int id, Cargo cargoAtualizado, AppDbContext context) =>
        {
            var cargoExistente = await context.Cargos.FindAsync(id);
            if (cargoExistente == null)
                return Results.NotFound("Cargo não encontrado.");

            cargoExistente.nome = cargoAtualizado.nome;
            await context.SaveChangesAsync();
            return Results.Ok(cargoExistente);
        });

        
        app.MapPut("/api/setor/{id}", async (int id, Setor setorAtualizado, AppDbContext context) => 
        {
            var setorExistente = await context.Setores.FindAsync(id);
            if (setorExistente == null)
                return Results.NotFound("Setor não encontrado.");

            setorExistente.nome = setorAtualizado.nome;
            await context.SaveChangesAsync();
            return Results.Ok(setorExistente);
        });

        
        app.MapPut("/api/funcionario/{id}", async (int id, Funcionario funcionario, AppDbContext context) =>
        {
            var funcionarioExistente = await context.Funcionarios.FindAsync(id);
            if (funcionarioExistente == null)
                return Results.NotFound($"Funcionário com id {id} não encontrado.");

           
            var cargo = await context.Cargos.FindAsync(funcionario.cargoId);
            if (cargo == null)
                return Results.BadRequest($"Cargo com id {funcionario.cargoId} não existe.");

            var setor = await context.Setores.FindAsync(funcionario.setorId);
            if (setor == null)
                return Results.BadRequest($"Setor com id {funcionario.setorId} não existe.");

            var dadosBancarios = await context.DadosBancarios.FindAsync(funcionario.dadosBancariosId);
            if (dadosBancarios == null)
                return Results.BadRequest($"DadosBancarios com id {funcionario.dadosBancariosId} não existe.");

            var endereco = await context.Enderecos.FindAsync(funcionario.enderecoID);
            if (endereco == null)
                return Results.BadRequest($"Endereco com id {funcionario.enderecoID} não existe.");

            
            if (funcionario.pessoa != null)
            {
                var pessoaExistente = await context.Pessoas.FirstOrDefaultAsync(p => p.cpf == funcionario.pessoa.cpf);
                if (pessoaExistente != null)
                {
                    
                    pessoaExistente.nome = funcionario.pessoa.nome;
                    pessoaExistente.sexo = funcionario.pessoa.sexo;
                    pessoaExistente.telefone = funcionario.pessoa.telefone;
                    pessoaExistente.email = funcionario.pessoa.email;
                    pessoaExistente.dataNascimento = funcionario.pessoa.dataNascimento;
                    funcionarioExistente.pessoaId = pessoaExistente.id;
                }
                else
                {
                    var novaPessoa = funcionario.pessoa;
                    context.Pessoas.Add(novaPessoa);
                    await context.SaveChangesAsync();
                    funcionarioExistente.pessoaId = novaPessoa.id;
                }
            }
            else
            {
                if (funcionario.pessoaId <= 0)
                    return Results.BadRequest("Pessoa ou pessoaId é obrigatório.");
                funcionarioExistente.pessoaId = funcionario.pessoaId;
            }

            
            funcionarioExistente.cargoId = funcionario.cargoId;
            funcionarioExistente.setorId = funcionario.setorId;
            funcionarioExistente.dadosBancariosId = funcionario.dadosBancariosId;
            funcionarioExistente.enderecoID = funcionario.enderecoID;
            funcionarioExistente.salario = funcionario.salario;


            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Results.Problem($"Erro ao atualizar funcionário: {ex.Message}");
            }
            
            var funcionarioCompleto = await context.Funcionarios
                .Include(f => f.pessoa)
                .Include(f => f.setor)
                .Include(f => f.cargo)
                .Include(f => f.dadosBancarios)
                .Include(f => f.endereco)
                .FirstOrDefaultAsync(f => f.id == id);
            return Results.Ok(funcionarioCompleto);
        });
    }
}
