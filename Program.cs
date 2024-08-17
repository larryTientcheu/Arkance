using Arkance.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register the DbManager service
builder.Services.AddDbContext<ArkanceTestContext>();

// Configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
