# Azure Function do Sistema de Gerenciamento de Livros

Esta Azure Function tem como objetivo **emitir mensagens para usuários com empréstimos em atraso**, com base na quantidade de **dias de atraso identificados** via API.

[![API](https://img.shields.io/badge/🔗API-blue)](https://github.com/ezequiel-lima/gerenciamento-livro-tres-camadas-devio)
[![Azure Function](https://img.shields.io/badge/Azure_Function-%2300BCF2?logo=azure-functions&logoColor=white)](https://github.com/ezequiel-lima/gerenciamento-livro-loan-return-notifier-app)

## 📁 `local.settings.json`

Este arquivo define as configurações locais para execução da Azure Function em ambiente de desenvolvimento.

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "LoanReminderSchedule": "0 */1 * * * *",
    "PaginationSettings:PageSize": "100",
    "LoanApiSettings:BaseUrl": "https://localhost:7140",
    "LoanApiSettings:OverdueEndpoint": "/api/Emprestimos/atrasados"
  }
}
```

### 🔍 Detalhamento das configurações

| **Chave**                          | **Descrição**                                                                 |
|------------------------------------|--------------------------------------------------------------------------------|
| `IsEncrypted`                      | Define se o conteúdo está criptografado. Deve permanecer como `false` localmente. |
| `AzureWebJobsStorage`              | String de conexão com o Azure Storage. Usa `"UseDevelopmentStorage=true"` para simular localmente. |
| `FUNCTIONS_WORKER_RUNTIME`         | Define o runtime usado na Function. Valor para .NET isolated: `"dotnet-isolated"`. |
| `LoanReminderSchedule`            | Expressão CRON para agendar a execução da função. Neste caso, a cada 1 minuto. |
| `PaginationSettings:PageSize`      | Quantidade de itens por página ao realizar paginação via API. |
| `LoanApiSettings:BaseUrl`          | URL base da API que fornece os empréstimos. |
| `LoanApiSettings:OverdueEndpoint`  | Caminho do endpoint para obter os empréstimos em atraso. |

## 🔗 Repositórios Relacionados

Este projeto faz parte de um conjunto de aplicações do sistema de gerenciamento de livros.

- 📦 **Azure Function - Loan Return Notifier App**  
  Responsável por emitir notificações de atraso com base nos empréstimos registrados.  
 [Repositório](https://github.com/ezequiel-lima/gerenciamento-livro-loan-return-notifier-app)

- 🧱 **API de Empréstimos**  
  API construída em arquitetura de três camadas (.NET) que fornece os dados de empréstimos.  
  [Repositório](https://github.com/ezequiel-lima/gerenciamento-livro-tres-camadas-devio)
