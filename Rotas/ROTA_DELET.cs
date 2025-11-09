

using controleDeFuncionarios.Dao;
using Microsoft.EntityFrameworkCore;

namespace controleDeFuncionarios.Rotas;

public static class ROTA_DELET
{
    public static void MapDeleteRoutes(this WebApplication app)
    {
        app.MapDelete("/api/cargo/{id}", async (int id, AppDbContext context) =>
          {
              var cargo = await context.Cargos.FindAsync(id);
              if (cargo == null)
              {
                  return Results.NotFound("Cargo não encontrado.");
              }

              var hasFuncionarios = await context.Funcionarios.AnyAsync(f => f.cargoId == id);
              if (hasFuncionarios)
              {
                  return Results.BadRequest("Não é possível excluir o cargo, pois há funcionários associados a ele.");
              }

              context.Cargos.Remove(cargo);
              await context.SaveChangesAsync();
              return Results.Ok("Cargo deletado com sucesso.");
          });

        app.MapDelete("/api/dadosbancarios/{id}", async (int id, AppDbContext context) =>
        {
            var dadosBancarios = await context.DadosBancarios.FindAsync(id);
            if (dadosBancarios == null)
            {
                return Results.NotFound("Dado bancário não encontrado.");
            }

            var hasFuncionarios = await context.Funcionarios.AnyAsync(f => f.dadosBancariosId == id);
            if (hasFuncionarios)
            {
                return Results.BadRequest("Não é possível excluir o dado bancário, pois há funcionários associados a ele.");
            }

            context.DadosBancarios.Remove(dadosBancarios);
            await context.SaveChangesAsync();
            return Results.Ok("Dado bancário deletado com sucesso.");
        });

        app.MapDelete("/api/endereco/{id}", async (int id, AppDbContext context) =>
        {
            var endereco = await context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return Results.NotFound("Endereço não encontrado.");
            }

            var hasFuncionarios = await context.Funcionarios.AnyAsync(f => f.enderecoID == id);
            if (hasFuncionarios)
            {
                return Results.BadRequest("Não é possível excluir o endereço, pois há funcionários associados a ele.");
            }

            context.Enderecos.Remove(endereco);
            await context.SaveChangesAsync();
            return Results.Ok("Endereço deletado com sucesso.");
        });

        app.MapDelete("/api/pessoa/{id}", async (int id, AppDbContext context) =>
        {
            var pessoa = await context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return Results.NotFound("Pessoa não encontrada.");
            }

            var hasFuncionarios = await context.Funcionarios.AnyAsync(f => f.pessoaId == id);
            if (hasFuncionarios)
            {
                return Results.BadRequest("Não é possível excluir a pessoa, pois há funcionários associados a ela.");
            }

            context.Pessoas.Remove(pessoa);
            await context.SaveChangesAsync();
            return Results.Ok("Pessoa deletada com sucesso.");
        });

        app.MapDelete("/api/setor/{id}", async (int id, AppDbContext context) =>
        {
            var setor = await context.Setores.FindAsync(id);
            if (setor == null)
            {
                return Results.NotFound("Setor não encontrado.");
            }

            var hasFuncionarios = await context.Funcionarios.AnyAsync(f => f.setorId == id);
            if (hasFuncionarios)
            {
                return Results.BadRequest("Não é possível excluir o setor, pois há funcionários associados a ele.");
            }

            context.Setores.Remove(setor);
            await context.SaveChangesAsync();
            return Results.Ok("Setor deletado com sucesso.");
        });


        app.MapDelete("/api/funcionario/{id}", async (int id, AppDbContext context) =>
        {
            var funcionario = await context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return Results.NotFound("Funcionário não encontrado.");
            }

            context.Funcionarios.Remove(funcionario);
            await context.SaveChangesAsync();
            return Results.Ok("Funcionário deletado com sucesso.");
        });
    }
}