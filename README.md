# TechFluency

API do projeto Techfluency desenvolvida em ASP.NET Core. O sistema é modular e bem estruturado, utilizando boas práticas como separação de responsabilidades com camadas de Controllers, Services, Repository e Models.

## 🌐 Deploy

O projeto está disponível online em:
🔗 [https://techfluency.onrender.com](https://techfluency.onrender.com)

## 🚀 Como rodar localmente

### Pré-requisitos

Certifique-se de ter instalado em sua máquina:

* [.NET SDK 8.0](https://dotnet.microsoft.com/download)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) ou editor de sua preferência
* (Opcional) [Postman](https://www.postman.com/) para testar as requisições

### Passos para iniciar

1. **Clone o repositório ou extraia o arquivo ZIP:**

   ```bash
   git clone https://github.com/techfluency-project/techfluency-api
   ```

   Ou, se estiver com o ZIP, apenas extraia em seu diretório de preferência.

2. **Acesse a pasta do projeto:**

   ```bash
   cd TechFluency
   ```

3. **Rode a aplicação:**

   Via terminal:

   ```bash
   dotnet run
   ```

   Ou abra o projeto no Visual Studio e pressione `F5` para rodar com o IIS Express ou o botão .

4. A API estará disponível por padrão em:

   ```
   https://localhost:7290
   http://localhost:5092
   ```

## 🛠 Estrutura do Projeto

* `Controllers/` – Endpoints da API
* `Services/` – Regras de negócio
* `Repository/` – Acesso a dados
* `Models/DTOs/Enums` – Tipos e estruturas utilizadas
* `Context/` – Configuração do banco de dados (MongoDB)
* `Helpers/Constants` – Classes auxiliares

