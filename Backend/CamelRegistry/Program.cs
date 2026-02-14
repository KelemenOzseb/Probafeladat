
using Data;
using Entity.Dtos.Camel;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;

namespace CamelRegistry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration["db:conn"] ?? throw new InvalidOperationException("Connection string 'db:conn' not found.");
            builder.Services.AddDbContext<CamelRegistryDbContext>(options => options.UseSqlite(connectionString));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CamelRegistryDbContext>();
                dbContext.Database.Migrate();
            }

            // Endpoints
            // 1.  Új teve létrehozása (POST) 
            app.MapPost("/api/camels", async (CreateCamelDto dto, CamelLogic logic) =>
            {
                try
                {
                    var newCamel = await logic.CreateCamel(dto);
                    return Results.Created($"/api/camels/{newCamel.Id}", newCamel);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            // 2. Tevék listázása (GET) 
            app.MapGet("/api/camels", async (CamelLogic logic) =>
                Results.Ok(await logic.GetAllCamels()));

            // 3. Egy adott teve lekérdezése (GET /{id}) 
            app.MapGet("/api/camels/{id}", async (string id, CamelLogic logic) =>
            {
                try
                {
                    var camel = await logic.GetCamel(id);
                    return Results.Ok(camel);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });

            // 4. Teve adatainak módosítása (PUT vagy PATCH /{id}) (Esetemben PUT)
            app.MapPut("/api/camels/{id}", async (string id, UpdateCamelDto dto, CamelLogic logic) =>
            {
                try
                {
                    var updatedCamel = await logic.UpdateCamel(dto, id);
                    return Results.Ok(updatedCamel);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            // 5. Teve törlése (DELETE /{id}) 
            app.MapDelete("/api/camels/{id}", async (string id, CamelLogic logic) =>
            {
                try
                {
                    await logic.DeleteCamel(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
