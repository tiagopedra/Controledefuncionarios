using Microsoft.EntityFrameworkCore;
using controleDeFuncionarios.Models;
using controleDeFuncionarios.Dao;

namespace controleDeFuncionarios.Rotas;

public static class ROTA_GET
{

    public static void MapGetRoutes(this WebApplication app)
    {
        app.MapGet("/api/cargo", async (AppDbContext context) =>
        {
            var cargos = await context.Cargos.ToListAsync();
            if (cargos == null || cargos.Count == 0)
            {
                return Results.NotFound("Nenhum cargo encontrado.");
            }
            return Results.Ok(cargos);
        });

        app.MapGet("/api/cargo/{id}", async (int id, AppDbContext context) =>
        {
            var cargo = await context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return Results.NotFound("Cargo não encontrado.");
            }
            return Results.Ok(cargo);
        });

        app.MapGet("/api/dadosbancarios", async (AppDbContext context) =>
        {
            var dadosBancarios = await context.DadosBancarios.ToListAsync();
            if (dadosBancarios == null || dadosBancarios.Count == 0)
            {
                return Results.NotFound("Nenhum dado bancário encontrado.");
            }
            return Results.Ok(dadosBancarios);
        });

        app.MapGet("/api/dadosbancarios/{id}", async (int id, AppDbContext context) =>
        {
            var dadosBancarios = await context.DadosBancarios.FindAsync(id);
            if (dadosBancarios == null)
            {
                return Results.NotFound("Dado bancário não encontrado.");
            }
            return Results.Ok(dadosBancarios);
        });

        app.MapGet("/api/dadosbancarios/funcionario/{id}", async (int id, AppDbContext context) =>
        {
            var funcionario = await context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return Results.NotFound("Funcionário não encontrado.");
            }
            var dadosBancarios = await context.DadosBancarios.FindAsync(funcionario.dadosBancariosId);
            if (dadosBancarios == null)
            {
                return Results.NotFound("Dados bancários não encontrados para este funcionário.");
            }
            return Results.Ok(dadosBancarios);
        });

        app.MapGet("/api/endereco", async (AppDbContext context) =>
        {
            var enderecos = await context.Enderecos.ToListAsync();
            if (enderecos == null || enderecos.Count == 0)
            {
                return Results.NotFound("Nenhum endereço encontrado.");
            }
            return Results.Ok(enderecos);
        });

        app.MapGet("/api/endereco/{id}", async (int id, AppDbContext context) =>
        {
            var endereco = await context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return Results.NotFound("Endereço não encontrado.");
            }
            return Results.Ok(endereco);
        });

        app.MapGet("/api/endereco/funcionario/{id}", async (int id, AppDbContext context) =>
         {
             var funcionario = await context.Funcionarios.FindAsync(id);
             if (funcionario == null)
             {
                 return Results.NotFound("Funcionário não encontrado.");
             }
             var endereco = await context.Enderecos.FindAsync(funcionario.enderecoID);
             if (endereco == null)
             {
                 return Results.NotFound("Endereço não encontrado para este funcionário.");
             }
             return Results.Ok(endereco);
         });

        app.MapGet("/api/pessoa", async (AppDbContext context) =>
        {
            var pessoas = await context.Pessoas.ToListAsync();
            if (pessoas == null || pessoas.Count == 0)
            {
                return Results.NotFound("Nenhuma pessoa encontrada.");
            }
            return Results.Ok(pessoas);
        });

        app.MapGet("/api/pessoa/{id}", async (int id, AppDbContext context) =>
        {
            var pessoa = await context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return Results.NotFound("Pessoa não encontrada.");
            }
            return Results.Ok(pessoa);
        });

        app.MapGet("/api/setor", async (AppDbContext context) =>
        {
            var setores = await context.Setores.ToListAsync();
            if (setores == null || setores.Count == 0)
            {
                return Results.NotFound("Nenhum setor encontrado.");
            }
            return Results.Ok(setores);
        });

        app.MapGet("/api/setor/{id}", async (int id, AppDbContext context) =>
        {
            var setor = await context.Setores.FindAsync(id);
            if (setor == null)
            {
                return Results.NotFound("Setor não encontrado.");
            }
            return Results.Ok(setor);
        });

        app.MapGet("/api/setor/funcionario/{id}", async (int id, AppDbContext context) =>
        {
            var funcionario = await context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return Results.NotFound("Funcionário não encontrado.");
            }
            var setor = await context.Setores.FindAsync(funcionario.setorId);
            if (setor == null)
            {
                return Results.NotFound("Setor não encontrado para este funcionário.");
            }
            return Results.Ok(setor);
        });

        app.MapGet("/api/funcionario", async (AppDbContext context) =>
        {
            var funcionarios = await context.Funcionarios
                .Include(f => f.pessoa)
                .Include(f => f.setor)
                .Include(f => f.cargo)
                .Include(f => f.dadosBancarios)
                .Include(f => f.endereco)
                .ToListAsync();
            if (funcionarios == null || funcionarios.Count == 0)
            {
                return Results.NotFound("Nenhum funcionário encontrado.");
            }
            return Results.Ok(funcionarios);
        });

        app.MapGet("/api/funcionario/{id}", async (int id, AppDbContext context) =>
        {
            var funcionario = await context.Funcionarios
                .Include(f => f.pessoa)
                .Include(f => f.setor)
                .Include(f => f.cargo)
                .Include(f => f.dadosBancarios)
                .Include(f => f.endereco)
                .FirstOrDefaultAsync(f => f.id == id);
            if (funcionario == null)
            {
                return Results.NotFound("Funcionário não encontrado.");
            }
            return Results.Ok(funcionario);
        });

        app.MapGet("/api/funcionario/setor/{setorId}", async (int setorId, AppDbContext context) =>
        {
            var funcionarios = await context.Funcionarios
                .Include(f => f.pessoa)
                .Include(f => f.setor)
                .Include(f => f.cargo)
                .Include(f => f.dadosBancarios)
                .Include(f => f.endereco)
                .Where(f => f.setorId == setorId)
                .ToListAsync();
            if (funcionarios == null || funcionarios.Count == 0)
            {
                return Results.NotFound("Nenhum funcionário encontrado para este setor.");
            }
            return Results.Ok(funcionarios);
        });

        app.MapGet("/api/funcionario/cargo/{cargoId}", async (int cargoId, AppDbContext context) =>
        {
            var funcionarios = await context.Funcionarios
                .Include(f => f.pessoa)
                .Include(f => f.setor)
                .Include(f => f.cargo)
                .Include(f => f.dadosBancarios)
                .Include(f => f.endereco)
                .Where(f => f.cargoId == cargoId)
                .ToListAsync();
            if (funcionarios == null || funcionarios.Count == 0)
            {
                return Results.NotFound("Nenhum funcionário encontrado para este cargo.");
            }
            return Results.Ok(funcionarios);
        });

    }
}   
