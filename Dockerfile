# Etapa base para o SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Diretório de trabalho no container
WORKDIR /src

# Copiar o arquivo de projeto primeiro para aproveitar o cache
COPY ["AlibiPerfeito-CRUD.csproj", "./"]

# Restaurar dependências do projeto
RUN dotnet restore "AlibiPerfeito-CRUD.csproj" --source "https://api.nuget.org/v3/index.json"

# Copiar o restante do código para o container
COPY . .

# Construir o projeto
RUN dotnet build "AlibiPerfeito-CRUD.csproj" -c Release -o /app/build

# Publicar o projeto para uma pasta definitiva
RUN dotnet publish "AlibiPerfeito-CRUD.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa base para o runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Diretório de trabalho para a aplicação
WORKDIR /app

# Copiar os arquivos publicados da etapa anterior
COPY --from=build /app/publish .

# Configurar a variável de ambiente para desenvolvimento
ENV ASPNETCORE_ENVIRONMENT=Development

# Configurar o comando de inicialização
ENTRYPOINT ["dotnet", "AlibiPerfeito-CRUD.dll"]
