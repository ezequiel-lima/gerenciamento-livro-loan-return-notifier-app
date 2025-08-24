using GerenciamentoLivro.LoanReturnNotifierApp.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GerenciamentoLivro.LoanReturnNotifierApp.Functions;

public class NotifyLoanReturnFunction
{
    private readonly ILogger _logger;
    private readonly ILoanReminderService _loanReminderService;

    public NotifyLoanReturnFunction(ILoggerFactory loggerFactory, ILoanReminderService loanReminderService)
    {
        _logger = loggerFactory.CreateLogger<NotifyLoanReturnFunction>();
        _loanReminderService = loanReminderService;
    }

    [Function("NotifyLoanReturnFunction")]
    public async Task Run([TimerTrigger("%LoanReminderSchedule%")] TimerInfo myTimer)
    {
        _logger.LogInformation("Starting loan verification at {time}.", DateTime.Now);
        await _loanReminderService.ProcessOverdueLoans();
        _logger.LogInformation("Verification completed. Next run at {next}.", myTimer.ScheduleStatus?.Next);
    }
}