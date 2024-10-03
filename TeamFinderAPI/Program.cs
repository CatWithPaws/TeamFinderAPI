using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeamFinderAPI.Controllers.PostBody;
using TeamFinderAPI.Data;
using TeamFinderAPI.JwtAuthentication;
using TeamFinderAPI.JwtAuthentication.Endpoints;
using TeamFinderAPI.Repository;
using TeamFinderAPI.Repository.PostReposity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.





// Initialize Dependency Injection interfaces
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var jwtOptions = builder.Configuration
	.GetSection("JwtOptions")
    .Get<JwtOptions>();
    
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        //convert the string signing key to byte array
        byte[] signingKeyBytes = Encoding.UTF8
        	.GetBytes(jwtOptions.SigningKey);

        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
        opts.SaveToken = true;
    });
builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "384010096834-b2nqf1gfe13v90nfiglkqcpgd0a73deh.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });
// 👇 Configuring the Authorization Service
builder.Services.AddAuthorization();

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


builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://www.contoso.com");
        });

    options.AddPolicy("Dev",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
                //.AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
   {
       c.SwaggerEndpoint("swagger/v1/swagger.json", "My API V1");
       c.RoutePrefix = string.Empty;
   });
//}

using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<TeamFindAPIContext>();
        db.Database.EnsureCreated();
        
        //var context = scope.ServiceProvider.GetRequiredService<TeamFindAPIContext>();
        //context.Database.EnsureCreated();
    }

app.UseHttpsRedirection();
app.UseCors("Dev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

