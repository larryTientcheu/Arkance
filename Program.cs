using Arkance.Interface;
using Arkance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register the DbManager service
builder.Services.AddDbContext<ArkanceTestContext>();

// Configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Arkance School API", Version = "v1" });
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ArkanceTestContext>();

    try
    { // Apply migrations
        db.Database.Migrate();
    }
    catch (Exception)
    {
        // Do nothing, migration has not been performed
        //Console.WriteLine(ex.Message);
    }

    try
    {
        DbSeeder.Seed(db);
    }
    catch (Exception)
    {
        Console.WriteLine("Did not seed the database");
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder =>
    builder.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader());
}

// Redirect root to Swagger UI
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
