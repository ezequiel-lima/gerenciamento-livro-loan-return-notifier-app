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

## 🔍 Detalhamento das configurações

| **Chave**                          | **Descrição**                                                                 |
|------------------------------------|--------------------------------------------------------------------------------|
| `IsEncrypted`                      | Define se o conteúdo está criptografado. Deve permanecer como `false` localmente. |
| `AzureWebJobsStorage`              | String de conexão com o Azure Storage. Usa `"UseDevelopmentStorage=true"` para simular localmente. |
| `FUNCTIONS_WORKER_RUNTIME`         | Define o runtime usado na Function. Valor para .NET isolated: `"dotnet-isolated"`. |
| `LoanReminderSchedule`            | Expressão CRON para agendar a execução da função. Neste caso, a cada 1 minuto. |
| `PaginationSettings:PageSize`      | Quantidade de itens por página ao realizar paginação via API. |
| `LoanApiSettings:BaseUrl`          | URL base da API que fornece os empréstimos. |
| `LoanApiSettings:OverdueEndpoint`  | Caminho do endpoint para obter os empréstimos em atraso. |

## ✉️ Envio de e-mails com MailerSend

O envio das mensagens é realizado utilizando a plataforma [MailerSend](https://www.mailersend.com/), que fornece uma API para disparo de e-mails.

A comunicação é feita por meio de uma requisição HTTP `POST` para o endpoint: https://api.mailersend.com/v1/email

### Exemplo de email enviado 

Abaixo está um exemplo de e-mail enviado pela Azure Function, contendo os livros em atraso associados ao usuário identificado pela API. 

<img width="826" height="275" alt="image" src="https://github.com/user-attachments/assets/8be2ca42-fbb6-434b-bac2-8f0ca8e2c962" />

## 📜 Logs

Os logs abaixo demonstram o funcionamento interno da Azure Function durante a execução do job. Cada etapa do processo é registrada:

- Consulta à API de empréstimos atrasados
- Preparação e envio do e-mail via MailerSend
- Confirmação de envio (status 202 Accepted)
- Erros tratados (como limite de conta trial)

Isso permite auditoria e depuração em tempo real, facilitando o diagnóstico de falhas.

<img width="1356" height="535" alt="image" src="https://github.com/user-attachments/assets/640608bd-a568-4bbc-aed6-591c4ba58512" />

## 🔗 Repositórios Relacionados

Este projeto faz parte de um conjunto de aplicações do sistema de gerenciamento de livros.

- 📦 **Azure Function - Loan Return Notifier App**  
  Responsável por emitir notificações de atraso com base nos empréstimos registrados.  
 [Repositório](https://github.com/ezequiel-lima/gerenciamento-livro-loan-return-notifier-app)

- 🧱 **API de Empréstimos**  
  API construída em arquitetura de três camadas (.NET) que fornece os dados de empréstimos.  
  [Repositório](https://github.com/ezequiel-lima/gerenciamento-livro-tres-camadas-devio)
