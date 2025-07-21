# CineZtar

CineZtar é uma aplicação web criada em **ASP.NET Core MVC** (target **.NET 5.0**) que demonstra o fluxo de compra de ingressos de cinema.
O projeto inclui cadastro de filmes, gêneros, idiomas, usuários e integração com o **MercadoPago** para efetuar pagamentos.

## Funcionalidades
- Cadastro e manutenção de filmes com imagens
- Gerenciamento de gêneros e idiomas
- Administração de usuários e tipos de usuário
- Processamento de compras e integração com MercadoPago

## Requisitos
- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- Servidor **SQL Server** para o banco de dados `DB_Ingressos2`
- Token de acesso do MercadoPago (defina a variável de ambiente `MP_ACCESS_TOKEN`)

## Como executar
1. Clone este repositório e instale as dependências:
   ```bash
   git clone https://github.com/mathgarcia1/CineZtar
   cd CineZtar
   dotnet restore
   ```
2. Ajuste a string de conexão em `Repositorio/Models/DB_Ingressos2Context.cs` caso necessário.
3. Aplique as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update --project Repositorio/Repositorio.csproj
   ```
4. Inicie a aplicação:
   ```bash
   dotnet run --project Cine/Cine.csproj
   ```
   O site ficará disponível em `https://localhost:5001`.

## Estrutura do repositório
- `Cine` &mdash; projeto web ASP.NET Core MVC com controllers, views e arquivos estáticos
- `Repositorio` &mdash; biblioteca com entidades do Entity Framework Core e repositórios de dados

## Licença
Distribuído sob a [licença MIT](LICENSE).
