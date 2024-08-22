using Microsoft.EntityFrameworkCore;
using TeamFinderAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TeamFindAPIContext>(options => {
#if DEBUG
    var connectionString = builder.Configuration.GetConnectionString("PostgresDev");
#else
    var connectionString = builder.Configuration.GetConnectionString("PostgresProd");
#endif
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Creating local db while delevoping 

    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<TeamFindAPIContext>();
        
        db.Database.EnsureCreated();
        //var context = scope.ServiceProvider.GetRequiredService<TeamFindAPIContext>();
        //context.Database.EnsureCreated();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
