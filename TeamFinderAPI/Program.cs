using Microsoft.EntityFrameworkCore;
using TeamFinderAPI.Data;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Repository.PostReposity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.





// Initialize Dependency Injection interfaces
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>{
        
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TeamFindAPIContext>(options => {
#if DEBUG
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#else
    var connectionString = builder.Configuration.GetConnectionString("PostgresProd");
#endif
    options.UseNpgsql(connectionString);
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

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

