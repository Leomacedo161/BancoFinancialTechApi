---

# FullTechApiDesafio

## Descrição

O projeto **FullTechApi** é uma API desenvolvida em .NET 6.0 que oferece serviços bancários, incluindo cadastro de contas, geração de extratos e realização de transferências. A API utiliza Entity Framework Core para acesso ao banco de dados PostgreSQL.

## Tecnologias Utilizadas

- **.NET 6.0**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker**
- **FluentValidation**
- **Newtonsoft.Json**
- **Swashbuckle (Swagger)**

## Estrutura do Projeto

- **Controllers**: Contém os controladores da API.
- **Models**: Contém os modelos de dados.
- **Services**: Contém os serviços que implementam a lógica de negócios.
- **Data**: Contém o contexto do banco de dados.
- **Interface**: Contém as interfaces dos serviços.

## Configuração do Ambiente

### Pré-requisitos

- **Docker**
- **Postgresql**
- **.NET 6.0 SDK**

### Configuração do Docker

O projeto utiliza Docker para facilitar a configuração do ambiente. O arquivo `docker-compose.yml` define os serviços necessários:

- **PostgreSQL**: Banco de dados.
- **FullTechApiDesafio**: Serviço da API.

Para iniciar os serviços, execute:

```sh
docker-compose up
```

### Configuração do Banco de Dados

É recomendado criar um banco com as informações
_Host=localhost;Database=postgres;Username=postgres;Password=admin_

O banco de dados PostgreSQL então, fica configurado com as seguintes credenciais:

- **Usuário**: postgres
- **Senha**: admin
- **Banco de Dados**: postgres

Os dados são persistidos no diretório `./data`.

### Executando a Aplicação

Para executar a aplicação localmente, siga os passos abaixo:

1. Restaure as dependências:

    ```sh
    dotnet restore
    ```

2. Compile o projeto:

    ```sh
    dotnet build
    ```

 3. Rodar as migrações para:

    ```sh
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```   

4. Execute a aplicação:

    ```sh
    dotnet run
    ```

A aplicação estará disponível em [http://localhost:5000](http://localhost:5000).

## Testes

Os testes unitários estão localizados na pasta `Tests`. Para executar os testes, utilize o comando:

```sh
dotnet test
```

## Endpoints

A documentação dos endpoints da API pode ser acessada via Swagger.

