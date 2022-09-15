using System.Reflection;
using Microsoft.OpenApi.Models;
using ViewAdAPI.BAL;
using ViewAdAPI.BAL.Interface;
using ViewAdAPI.DAL;
using ViewAdAPI.DAL.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Users
builder.Services.AddSingleton<IUsersDAL, UsersDAL>();
builder.Services.AddSingleton<IUsersBAL, UsersBAL>();
//Tokens
builder.Services.AddSingleton<ITokensDAL, TokensDAL>();
builder.Services.AddSingleton<ITokensBAL, TokensBAL>();
//Transactions
builder.Services.AddSingleton<ITransactionsDAL, TransactionsDAL>();
builder.Services.AddSingleton<ITransactionsBAL, TransactionsBAL>();
//Withdrawaldata
builder.Services.AddSingleton<IWithdrawaldataDAL, WithdrawaldataDAL>();
builder.Services.AddSingleton<IWithdrawaldataBAL, WithdrawaldataBAL>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "api-key",
        Description = "API key"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "APIKey" }
            },
            new string[] {}
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
