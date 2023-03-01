# RestWithASP-NET5Udemy-My-Version

Uma  API ``.Net 5.0``. Que possuir Swagger, Sistema de login com tokens JWT, Serilog, DOCKER, entre outros, implementados.

# Como executar

- Realize o download do .Net 5 SDK e Visual Studio/Code.
- Vá para a pasta RestWithASPNETUdemy e rode esse seguinte comando:
> dotnet run 

Para roda o projeto pela linha de comando.


## Execução Independente
1. Você vai precisar de uma instância do MsSQL rodando, com as migrations propriamente inicializadas.
	- Você pode simplesmente rodar o banco de dados no docker. Para isso, você precisa alterar a connection string para "Server=db;DataBase=rest_with_asp_net_udemy;Uid=root;Pwd=docker;SslMode=none" e rodar o comando a seguir: ``docker-compose up -d db``. Fazendo isso, a aplicação estará apta para se conectar ao container do servidor de banco de dados.
download das imagens em docker:

link da imagem da Aplicação em docker: https://hub.docker.com/repository/docker/artarthur87333/rest-with-asp-net-udemy/general

link da imagem do banco de dados MySql em docker: https://hub.docker.com/repository/docker/artarthur87333/rest-with-asp-net-udemy-db/general

## Autenticação
Neste projeto, algumas rotas precisam de autenticação/autorização. Para isso, você terá que utilizar a rota ``api/auth/v1/signin`` para obter o JWT.
Por padrão, você tem um usuário disponívei para login:
- Usuário normal: 
	- userName: leandro
	- password: "admin123"

Depois disso, você pode passar o JWT clicando no cadeado (se estiver usando swagger) ou via o cabeçalho `Authorization` se tiver realizando uma requisição http.

# Este projeto contém:
- Integração com o Banco de Dados MySql
- Versionamento de Endpoints
- Arquitetura em Camadas
- Migrations com Envolve
- Generic Repository
- Value Object (VO)
- Custom Serialization
- Content Negociation
- HATEOAS
- Swagger (OpenAPI)
- CORS (Cross-origin resource sharing)
- Autenticação com tokens JWT
- Query Params e Paged Search (ou paginação)
- Trabalhando com Arquivos
- Consumindo a API com ReactJS
- SwaggerUI
- EntityFramework
- Injeção de Dependência

# Estrutura do Projeto

1. Business: a camada responsável por tratar das regras de negocios da Api fazendo chamadas diretamente para a camada Repository ou converte os dados do banco para VO (Value object)
2. ci: pasta responsável com guarda um script chamado init_database.sh cujo o proposito desse script é exercutar cada migration e dataseed da camada db que é rodado caso você subar o projeto em sua máquina local com docker.
3. Configurations: a camada que contém uma class chamada TokenConfiguration que esse class contém os atributos de configuração do token JWT.
4. Controllers: a camada que vai receber as requisições em Http do client.
5. Data : a camada que tabalha com o VO (value object) da aplicação.
6. db: a camada que guarda outras duas pastas dataset e migrations. Essas duas pastas contém as queries que seram exercutadas no banco de dados pela ferramenta de migrations [Envolve](https://evolve-db.netlify.app/).
7. Hypermedia: é a camada que contém toda estrutura do hateoas da aplicação.
8. Model:  é a camada que contém as entidades da aplicação que o ORM EntityFramework usar para mapear os objetos para o banco de dados da aplicação.
9. Repository: é a camada que mexer diretamente com o banco de dados.
10. Services: é a camada que Gerar os tokens JWT e outras coisas relacionadas também. 
11. UploadDir: camada que persistir/salvar as fotos e arquivos recebidos pela rota ``api/file/v1/``

# Adotando ao seu projeto
1. Altere o dockerfile e docker-compose.yml de acordo com os novos nomes de csproj/pasta.
2. Dê uma estrela a este repositório!

# Migrations
Para rodar as migrations do projeto foi usado a ferramenta de migrations [Envolve](https://evolve-db.netlify.app/).
1. Vá para o arquivo Startup.cs e descomente o método MigrateDatabase que se encontra atualmente na linha 107 desse arquivo e rode o projeto.

# Caso tenha gostado deste repositório, dê uma estrela!
Se este template foi útil para você ou se você aprendeu algo, por favor dê uma estrela! :star:

