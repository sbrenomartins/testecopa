# Teste Copastur - Desenvolvedor

Desenvolver dois microsserviços de forma desacoplada, assíncrona,
segura, escalável e flexível.
Requisitos:

- Docker
- Fila - RabbitMQ
- Banco relacional - PostgreSQL
- Banco não relacional - MongoDB

#

Foram criados dois Microsserviços, utilizando o .NET 6:

- Copastur.Usuarios.Api
- Copastur.Auditoria.Api

Os dois serviços quandos estiverem rodando podem ser testados pelo Swagger (se estiver utilizando o Visual Studio ao dar o Start já abrirá um navegador com a página do Swagger, se estiver utilizando o .NET CLI será necessário acessar a rota sugerida mais "/swagger").

O fluxo dos dois microsserviços funciona assim: Ao inserir, atualizar ou deletar algum usuário no projeto Copastur.Usuarios.Api será produzida uma mensagem e postada em uma fila no RabbitMQ, após isso o processo de salvar no banco Postgres continuará, já o serviço Copastur.Auditoria.Api foi criado uma classe service que implementa um BackgroundService, injetado como um HostedService para rodar assim que iniciar a aplicação e continuar rodando em background "ouvindo" as entradas na fila do RabbitMQ, sempre que uma nova mensagem é postada, ele a recupera, trata e salva a informação de auditoria no MongoDB.

Os passos para iniciar os projetos e tudo necessário para rodar estão logo abaixo:

## Passos para rodar o projeto

- Clonar este repositório
- Na raiz do projeto, utilizar o comando abaixo para iniciar os containers do PostgreSql, MongoDb e RabbitMQ:

```bash
  docker-compose up -d
```

### Copastur.Usuarios.Api

- Acessar a pasta do projeto Copastur.Usuarios.Api/src/Api e utilizar o comando:

-- Caso esteja utilizando o .NET CLI

```bash
  dotnet ef database update
```

-- Caso esteja utilizando o Visual Studio

```bash
  Update-Database
```

- Após rodar as migrations, para iniciar o projeto (ainda na pasta src/Api), utilize o comando abaixo caso esteja utilizando o .NET CLI:

```bash
  dotnet run
```

- Caso esteja utilizando o Visual Studio basta apertar o botão "Start" ou apertar F5.

### Copastur.Auditoria.Api

- Se estiver utilizando o .NET CLI, acesse a pasta Copastur.Auditoria.Api/src/Api e utilize o comando para rodar o projeto:

```bash
  dotnet run
```

- Se estiver utilizando o Visual Studio basta apertar o botão "Start" ou apertar F5.
