using SurveyTest.API.ExceptionsHandling;
using SurveyTest.Application;
using SurveyTest.Application.Abstraction;
using SurveyTest.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddTransient<GlobalExceptionsHandlingMiddleware>()
    .AddDal(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionsHandling();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

InitializeDb();

app.Run();

void InitializeDb()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetService<IApplicationDbContext>();

        DbInitializer.Initialize(dbContext);
    }
}