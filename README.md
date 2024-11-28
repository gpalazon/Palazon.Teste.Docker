
# Palazon.Teste.API

Palazon.Teste.API é uma API desenvolvida para gerenciar tarefas e projetos. A aplicação utiliza Docker Compose para orquestração de serviços e SQL Server como banco de dados.

## Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:
- Docker Desktop
- .NET SDK 8.0 (opcional, se precisar rodar localmente sem Docker)
- Ferramenta cliente SQL (opcional): Azure Data Studio ou SQL Server Management Studio (SSMS)

## Instruções para Execução

1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-repositorio/Palazon.Teste.API.git
   cd Palazon.Teste.API
   ```

2. Certifique-se de que o Docker Desktop está em execução.

3. Construa e inicie os serviços com Docker Compose:
   ```bash
   docker-compose up --build
   ```

4. Acesse a API:
   - **HTTP**: [http://localhost:8080/swagger](http://localhost:8080/swagger)
   - **HTTPS**: [https://localhost:8081/swagger](https://localhost:8081/swagger)

5. (Opcional) Acesse o banco de dados:
   - Servidor: `localhost,1433`
   - Usuário: `sa`
   - Senha: `Palazon@2024`


## Instruções para Popular Tabelas Necessárias

Para que a aplicação funcione corretamente, é necessário popular as tabelas `Users` e `Profiles` com dados iniciais. Siga os passos abaixo:

1. Certifique-se de que o banco de dados está em execução.
2. Execute o seguinte comando para aplicar o script de inicialização:
   ```bash
   docker exec -i sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Palazon@2024 -d TestDb -i ./scripts/init-data.sql
   ```
3. O script criará as tabelas e incluirá os seguintes dados:
   - Perfis:
     - **Gestor**
     - **Usuario**
   - Usuários:
     - **admin@teste.com**
     - **gustavo.palazon@gmail.com**

4. Para verificar os dados no banco, use um cliente SQL e execute:
   ```sql
   SELECT * FROM Users;
   SELECT * FROM Profiles;
   ```

## Testes

- Para rodar os testes unitários:
  ```bash
  dotnet test
  ```
- Certifique-se de que o banco de dados está em execução antes de rodar os testes que dependem de dados.

---

## Estrutura do Projeto

A estrutura básica do projeto é a seguinte:

```
Palazon.Teste.Docker/
├── docker-compose.yml
├── Palazon.Teste.Docker.API/
│   ├── Controllers/
│   ├── Program.cs
│   └── Startup.cs
├── Palazon.Teste.Docker.Application/
│   ├── DTO/
│   ├── Interfaces/
│   └── Services/
├── Palazon.Teste.Docker.Domain/
│   ├── Interfaces/
│   └── Models/
├── Palazon.Teste.Docker.Infraestructure/
│   ├── AppDbContext/
│   ├── Migrations/
│   └── Repositories/
├── Palazon.Teste.Docker.UnityTest/
│   └── Test Cases
└── README.md
```

## Observações

- A pasta **`scripts`** contém o arquivo `init-data.sql`, que é necessário para popular as tabelas iniciais.
- Certifique-se de que o volume do SQL Server está configurado corretamente no Docker Compose.

- Para rodar a migrations, mude o apontamento na connectionstring de sqlserver,1344 para localhost,1344 (arquivo appSettings.json) e marque o projeto Palazon.Teste.Docker.API como projeto de inicialização. Depois retorne as configurações iniciais para a execução do projeto.

dotnet ef database update --project ./Palazon.Teste.Docker.Infraestructure --startup-project ./Palazon.Teste.Docker.API


---

## **Explicação Geral da Arquitetura**

A arquitetura do projeto segue os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**, com foco em separação de responsabilidades e testabilidade. As camadas principais são:

1. **API**: Camada de entrada (interface) que expõe os endpoints REST para os consumidores da aplicação.
2. **Application**: Responsável pela lógica de aplicação, validações e regras de negócios específicas da API.
3. **Domain**: Contém as entidades e regras de negócios essenciais, abstraídas para serem reutilizáveis e independentes da infraestrutura.
4. **Infrastructure**: Lida com a persistência de dados, implementações de repositórios e comunicação com o banco de dados.
5. **Tests**: Conjunto de testes para validar a funcionalidade e as regras de negócios.

Essa estrutura promove a escalabilidade e facilita a manutenção ao longo do tempo.

---

## **Breve Descrição de Cada Projeto**

### **1. Palazon.Teste.Docker.API**
**Responsabilidade**: Camada de entrada da aplicação.
- Contém os **Controllers**, que são responsáveis por lidar com as requisições HTTP e delegar tarefas para as camadas inferiores.
- Configurações do projeto, como injeção de dependências e Swagger, são feitas no `Startup.cs` ou `Program.cs`.

**Principais diretórios:**
- **Controllers/**: Define os endpoints e gerencia as requisições HTTP.

---

### **2. Palazon.Teste.Docker.Application**
**Responsabilidade**: Regras de negócios específicas da API e integração com o domínio.
- Define **DTOs** (Data Transfer Objects) usados para transferir dados entre as camadas.
- Implementa **Services** que encapsulam a lógica de aplicação.
- Contém **Interfaces** para abstrair dependências.

**Principais diretórios:**
- **DTO/**: Classes usadas para transportar dados entre a API e outras camadas.
- **Interfaces/**: Define contratos para os serviços.
- **Services/**: Implementa a lógica da aplicação e interage com a camada de domínio e infraestrutura.

---

### **3. Palazon.Teste.Docker.Domain**
**Responsabilidade**: Núcleo do domínio, contendo as regras de negócio fundamentais e independentes.
- Define as **Models**, que são representações das entidades de negócio.
- Contém **Interfaces** que descrevem contratos genéricos para repositórios e outras dependências.

**Principais diretórios:**
- **Models/**: Entidades centrais como `User`, `Profile`, etc.
- **Interfaces/**: Contratos que a infraestrutura deve implementar, como `IRepository`.

---

### **4. Palazon.Teste.Docker.Infraestructure**
**Responsabilidade**: Implementação dos repositórios e gerenciamento da persistência de dados.
- Utiliza o **Entity Framework Core** para interagir com o banco de dados.
- Contém a configuração do **AppDbContext**, que define o mapeamento das entidades.
- Implementa repositórios para acessar e manipular os dados.

**Principais diretórios:**
- **AppDbContext/**: Define a configuração do banco de dados e mapeamento das entidades.
- **Migrations/**: Scripts de migração gerados pelo Entity Framework para gerenciar mudanças no esquema do banco.
- **Repositories/**: Implementações concretas para manipular dados, como `UserRepository`.

---

### **5. Palazon.Teste.Docker.Tests**
**Responsabilidade**: Garantir a funcionalidade do sistema por meio de testes automatizados.
- Contém testes unitários e de integração para validar as regras de negócios e a interação entre as camadas.
- Utiliza o framework como **XUnit** para escrever os testes e a biblioteca **Moq** para a criação de dependências simuladas (mocks).

---

### **6. docker-compose.yml**
**Responsabilidade**: Gerencia os serviços do Docker.
- Configura o ambiente de desenvolvimento, incluindo o contêiner da API e o SQL Server.
- Define os volumes e variáveis de ambiente necessários para o projeto.

---

## **Vantagens da Arquitetura**

- **Manutenibilidade**: Cada camada tem responsabilidades claras, facilitando a modificação e o teste de partes específicas.
- **Escalabilidade**: A separação de camadas permite adicionar novas funcionalidades ou adaptar tecnologias sem grandes impactos.
- **Testabilidade**: A estrutura modular facilita a criação de testes para validar a funcionalidade de cada parte.

---



### Fase 2: Refinamento

Para a segunda fase, escreva no arquivo **README.md** em uma sessão dedicada, o que você perguntaria para o *PO* visando o refinamento para futuras implementações ou melhorias.

1. Há regras de permissionamento para os usuários, por ex: Um usuário pode ver tarefas de outros usuários? Se sim qual a regra.

2. Há alguma regra de validação para os campos (Projeto e Tarefas)?

3. Não há necessidade de ter uma listagem de tarefas com filtro por status?

---

### Fase 3: Final

Na terceira fase, escreva no arquivo **README.md** em uma sessão dedicada o que você melhoraria no projeto, identificando possíveis pontos de melhoria, implementação de padrões, visão do projeto sobre arquitetura/cloud, etc.

1. Adicionar validações no modelo / DTO
2. Incluir regras de permissionamento e validações nas tarefas de inclusão/alteração dos projetos e tarefas.
3. Incluir o status e prioridade nas pesquisas / listagens de tarefas
4. Incluir a data de início da atividade nas tarefas
5. Notificações por e-mail:
   a - Nota tarefa criada
   b - Tarefa atrasada
   c - Aviso de tarefa proximo da data limite
   d - Atividade a mais de x dias atrasada
6. Passar o usuario atual através dos Cabeçalhos HTTP para maior segurança e controle (obrigatório o envio do Usuário no Cabeçalho HTTP), Ex: App-User-Id: 123
*Código na Controller: [FromHeader(Name = "App-User-Id")] int userId
7. Criar novos Relatórios: 
   a - Tarefas Criadas
   b - Tarefas Pendentes 
   c - Projetos com tarefas atrasadas
8. Atualizar o Docker Compose para Executar o Script de inicialização do BD automaticamente
9. Relatórios de erros / logs na aplicação