namespace AlibiPerfeito_CRUD.Models;

public class Categoria
{
    public Guid Id { get; init; } // Identificador único da categoria
    public string Name { get; private set; } // Nome da categoria (ex: Trabalho)

    // Relação com Desculpas: Uma Categoria pode ter várias Desculpas
    //public ICollection<DesculpaModel> Desculpas { get; set; } = new List<DesculpaModel>();

    // Construtor
    public Categoria(string name)
    {
        Id = Guid.NewGuid(); // Gera um novo GUID para cada categoria
        Name = name;         // Define o nome
    }

    public void ChangeName(string name)
    {
        Name = name;
    }
}