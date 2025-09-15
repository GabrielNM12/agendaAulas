# 📅 Agenda Aulas API

API desenvolvida em **.NET 8** para gerenciamento de aulas, alunos e agendamentos.

---

## 🚀 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [PostgreSQL](https://www.postgresql.org/) rodando localmente  

---

## ⚙️ Configuração

1. Clone o repositório:
   ```
   git clone https://github.com/seu-usuario/agendaAulas.git
   cd agendaAulas
   ```

2. Restaure as dependências:

   ```
   dotnet restore
   ```

3. Aplique as migrations para criar o banco:

   ```
   dotnet ef database update
   ```

   ⚠️ Observação: em alguns ambientes, pode ser necessário rodar o comando duas vezes, na primeira pode ocorrer erro por causa da inicialização do banco.

4. Rode a aplicação:

   ```
   dotnet run
   ```

---

## 📖 Endpoints

Após iniciar, acesse o Swagger para explorar os endpoints:

* 👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🛠 Tecnologias

* .NET 8
* Entity Framework Core
* PostgreSQL
* Swagger / OpenAPI

---

## 📌 Observações

* Connection string configurada em `appsettings.json`.
* Usuário padrão do Postgres: `postgres`
* Senha padrão: `123`
* Banco: `agenda`
