using AbissalSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddDbContext<AbissalSystemDbContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
