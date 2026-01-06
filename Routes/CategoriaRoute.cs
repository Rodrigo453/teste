using AlibiPerfeito_CRUD.Data;
using AlibiPerfeito_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AlibiPerfeito_CRUD.Routes;

public static class CategoriaRoute
{
    public static void CategoriaRoutes(this WebApplication app)
    {
        var route = app.MapGroup("categoria");

        route.MapPost("/cadastrar", async (CategoriaRequest req, BancoContext context) =>
        {
            var categoria = new Categoria(req.CategoriaTexto);
            await context.AddAsync(categoria);
            await context.SaveChangesAsync();
        }).RequireAuthorization();

        route.MapGet("buscar", async (BancoContext context) =>
        {
            var categoria = await context.Categoria.ToListAsync();
            return Results.Ok(categoria);
        }).RequireAuthorization();

        route.MapPut("alterar/{id}", async (Guid id, CategoriaRequest req, BancoContext context) =>
        {
            var categoria = await context.Categoria.FirstOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
                return Results.NotFound();

            categoria.ChangeName(req.CategoriaTexto);
            await context.SaveChangesAsync();

            return Results.Ok(categoria);
        }).RequireAuthorization();
        
        route.MapDelete("excluir/{id}", async (Guid id, BancoContext context) =>
        {
            // Busca a categoria pelo ID
            var categoria = await context.Categoria.FirstOrDefaultAsync(x => x.Id == id);

            // Se a categoria não existir, retorna um erro 404
            if (categoria == null)
                return Results.NotFound();

            // Remove a categoria do banco de dados
            context.Categoria.Remove(categoria);
    
            // Persiste a remoção no banco de dados
            await context.SaveChangesAsync();

            // Retorna uma resposta de sucesso com a categoria removida (opcional)
            return Results.Ok($"Categoria com ID {id} excluída com sucesso.");
        }).RequireAuthorization();
    }
}