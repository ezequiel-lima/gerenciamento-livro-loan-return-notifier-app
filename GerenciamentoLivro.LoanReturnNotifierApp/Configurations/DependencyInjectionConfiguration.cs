using GerenciamentoLivro.LoanReturnNotifierApp.HttpClients;
using GerenciamentoLivro.LoanReturnNotifierApp.Services;
using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static FunctionsApplicationBuilder AddDependencyInjection(this FunctionsApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ILoanHttpClient, LoanHttpClient>();
            builder.Services.AddScoped<IEmailHttpClient, EmailHttpClient>();

            builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            builder.Services.AddScoped<ILoanReminderService, LoanReminderService>();

            builder.Services.Configure<PaginationSettings>(
                builder.Configuration.GetSection("PaginationSettings"));
            builder.Services.Configure<LoanApiSettings>(
                builder.Configuration.GetSection("LoanApiSettings"));
            builder.Services.Configure<EmailApiSettings>(
                builder.Configuration.GetSection("EmailApiSettings"));

            return builder;
        }
    }
}
