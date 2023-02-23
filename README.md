
# Teste Copastur - Desenvolvedor

Desenvolver dois microsserviços de forma desacoplada, assíncrona,
segura, escalável e flexível.
Requisitos:
- Docker
- Fila
- Banco relacional
- Banco não relacional
#
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