using GerenciamentoLivro.LoanReturnNotifierApp.Configurations;
using GerenciamentoLivro.LoanReturnNotifierApp.HttpClients;
using GerenciamentoLivro.LoanReturnNotifierApp.Services;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ILoanHttpClient, LoanHttpClient>();
builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<ILoanReminderService, LoanReminderService>();

builder.Services.Configure<PaginationSettings>(
    builder.Configuration.GetSection("PaginationSettings"));

builder.Build().Run();
