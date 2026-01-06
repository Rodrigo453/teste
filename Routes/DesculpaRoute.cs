using AlibiPerfeito_CRUD.Data;
using AlibiPerfeito_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AlibiPerfeito_CRUD.Routes;

public static class DesculpaRoute
{
    public static void DesculpaRoutes(this WebApplication app)
    {
        var route = app.MapGroup("desculpas");

        route.MapPost("/cadastrar", async (List<DesculpaRequest> req, BancoContext context) =>
        {
            var desculpas = new List<DesculpaModel>(); // Lista para armazenar as desculpas a serem salvas

            foreach (var item in req)
            {
                // Busca a categoria pela string de nome
                var categoria = await context.Categoria.FirstOrDefaultAsync(c => c.Name == item.Categoria);

                if (categoria == null)
                {
                    return Results.BadRequest(
                        $"Categoria {item.Categoria} não encontrada!"); // Retorna erro se não encontrar a categoria
                }

                // Cria uma nova desculpa associando o texto e o ID da categoria
                var desculpa = new DesculpaModel(item.DesculpaTexto, categoria.Id, categoria.Name);

                // Adiciona a desculpa à lista
                desculpas.Add(desculpa);
            }

            // Salva todas as desculpas no banco de dados
            await context.AddRangeAsync(desculpas);
            await context.SaveChangesAsync();

            return Results.Ok(desculpas); // Retorna todas as desculpas cadastradas
        }).RequireAuthorization();

        route.MapGet("buscar", async (BancoContext context) =>
        {
            var desculpas = await context.Desculpas.ToListAsync();
            return Results.Ok(desculpas);
        }).RequireAuthorization();

        route.MapGet("random", async (BancoContext context) =>
        {
            //cria var total resultado é uma consulta de numero de itens do banco .count
            var total = await context.Desculpas.CountAsync();

            if (total == 0)
            {
                return Results.NotFound("Nenhuma desculpa foi encontrada!");
            }

            // cira var random que é um numero aleatório entre 0 e o numero.count de registro do banco
            var randomIndex = new Random().Next(0, total);
            /*Cria uma var desculpa que vai no banco e pega o numero gerado faz um "skip" de tudo que vem
             antes desse numero e pega a desculpa encontrada*/
            var desculpa = await context.Desculpas.Skip(randomIndex).FirstOrDefaultAsync();

            return Results.Ok(desculpa);
        }).RequireAuthorization();

        route.MapGet("random/{categoria}", async (string categoria, BancoContext context) =>
        {
            var desculpas = await context.Desculpas.Where(d => d.CategoriaName == categoria)
                .ToListAsync();
            
            if (!desculpas.Any())
            {
                return Results.NotFound($"Nenhuma desculpa encontrada para a categoria: {categoria}");
            }
            
            var randomIndex = new Random().Next(0, desculpas.Count);
            
            var randomDesculpa = desculpas[randomIndex];

            return Results.Ok(randomDesculpa);
        }).RequireAuthorization();
        
        route.MapGet("/{categoria}", async (string categoria, BancoContext context) =>
        {
            var desculpas = await context.Desculpas.Where(d => d.CategoriaName == categoria)
                .ToListAsync();
            
            if (!desculpas.Any())
            {
                return Results.NotFound($"Nenhuma desculpa encontrada para a categoria: {categoria}");
            }

            return Results.Ok(desculpas);
        }).RequireAuthorization();
        
        route.MapPut("alterar/{id}", async (Guid id, DesculpaRequest req, BancoContext context) =>
        {
            var desculpa = await context.Desculpas.FirstOrDefaultAsync(x => x.Id == id);
            
            if (desculpa == null)
                return Results.NotFound();
            
            desculpa.ChangeName(req.DesculpaTexto); 
            await context.SaveChangesAsync();

            return Results.Ok(desculpa);
        }).RequireAuthorization();
        
        route.MapDelete("excluir/{id}", async (Guid id, BancoContext context) =>
        {
            // Busca a categoria pelo ID
            var desculpa = await context.Desculpas.FirstOrDefaultAsync(x => x.Id == id);

            // Se a categoria não existir, retorna um erro 404
            if (desculpa == null)
                return Results.NotFound();

            // Remove a categoria do banco de dados
            context.Desculpas.Remove(desculpa);
    
            // Persiste a remoção no banco de dados
            await context.SaveChangesAsync();

            // Retorna uma resposta de sucesso com a categoria removida (opcional)
            return Results.Ok($"Desculpa com ID {id} excluída com sucesso.");
        }).RequireAuthorization();

    }
}