using Eurodruk.App.Data;
using Eurodruk.App.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<WorkshopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DatabaseInitializer>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAcceptanceService, AcceptanceService>();
builder.Services.AddScoped<IUserTicketService, UserTicketService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var databaseOptions = config
            .GetSection(DatabaseOptions.SectionName)
            .Get<DatabaseOptions>() ?? new DatabaseOptions();

        var cs = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection in configuration.");

        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

        if (databaseOptions.ApplyMigrationsOnStartup)
        {
            await initializer.MigrateAsync();
            await initializer.LogStatusAsync(logger);
        }

        if (databaseOptions.SeedOnStartup)
            await initializer.SeedAsync();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database initialization failed.");
        throw; // fail-fast
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
