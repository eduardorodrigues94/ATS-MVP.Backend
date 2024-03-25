# Projeto Destinado ao Backend - Desafio ATS da TOTVS


## Rodando a aplicação

### Pelo Docker

`docker-compose up -d`

Feito isso, dois containers docker subirão:
    - **ats-api**: aplicação **.net 8** que pode ser acessada na porta [[http://localhost:8080]]
    - **ats-database**: **mongodb** na porta [[http://localhost:27017]]

### Localmente

Requisitos
	- **sdk .net 8**
	- **Mongodb** (opcional, caso não esteja rodando o container docker)

Você pode estar instalando as dependências através do comando:

`dotnet restore .\ATS.MVP.Api\ATS.MVP.Api.csproj`

E rodando a aplicação através do comando abaixo:

`dotnet run .\ATS.MVP.Api\ATS.MVP.Api.csproj`

A aplicação será disparada em: 

https://localhost:7178

Por padrão ele pega a ConnectionString do mongodb local na porta 27017( [[http://localhost:27017]]).Caso queira estar apontando para outra conexão ou database do mongodb, você pode estar alterando o `appSettings.development` subsituindo as informações necessárias na sessão:


```json
 "MongoDBConfiguration": {
   "Database": "{database}",
   "ConnectionString": "{connectionString}"
 }
```

## Testes

Para rodar os testes unitários utilize o comando abaixo

`dotnet test`
