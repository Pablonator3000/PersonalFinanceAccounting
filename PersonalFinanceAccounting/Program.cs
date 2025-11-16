using Microsoft.EntityFrameworkCore;
using PersonalFinanceAccounting;
using PersonalFinanceAccounting.Application;
using PersonalFinanceAccounting.Infrastructure.Data;

var options = new DbContextOptionsBuilder<PfaDbContext>()
    .UseSqlite("Data Source=financial.db")
    .Options;

using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

await using var context = new PfaDbContext(options);

await context.Database.MigrateAsync(cancellationToken);

var reportService = new ReportService(context);
var menu = new Menu(reportService);

await menu.StartAsync(cancellationToken);

Console.WriteLine("Завершено.");