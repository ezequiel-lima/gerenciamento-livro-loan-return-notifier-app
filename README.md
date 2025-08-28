# GerenciamentoLivro.LoanReturnNotifierApp

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
