using GerenciamentoLivro.LoanReturnNotifierApp.Services;
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
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("Starting loan verification at {time}.", DateTime.Now);
        await _loanReminderService.ProcessOverdueLoans();
        _logger.LogInformation("Verification completed. Next run at {next}.", myTimer.ScheduleStatus?.Next);
    }
}