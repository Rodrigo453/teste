namespace AlibiPerfeito_CRUD.Models;

public class DesculpaModel
{
    public Guid Id { get; init; } // Identificador único da desculpa
    public string DesculpaTexto { get; private set; } // Texto da desculpa (ex: "Meu cachorro comeu meu trabalho")

    // Chave estrangeira para vincular a uma Categoria
    public Guid CategoriaId { get; set; } // ID da categoria à qual a desculpa pertence
    public string CategoriaName { get; set;}
    // Construtor
    public DesculpaModel(string desculpaTexto, Guid categoriaId, string categoriaName)
    {
        Id = Guid.NewGuid();        // Gera um novo GUID para cada desculpa
        DesculpaTexto = desculpaTexto; // Define o texto da desculpa
        CategoriaId = categoriaId;  // Define a relação com uma categoria existente (por ID)
        CategoriaName = categoriaName;
    }
    
    public void ChangeName(string name)
    {
        DesculpaTexto = name;
    }
}