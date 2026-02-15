
using Data;
using Data.Repository;
using Entity.Dtos.Camel;
using Entity.Models;
using Logic.Helper;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;

namespace CamelRegistry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Database connection
            var connectionString = builder.Configuration["db:conn"] ?? throw new InvalidOperationException("Connection string 'db:conn' not found.");
            builder.Services.AddDbContext<CamelRegistryDbContext>(options => options.UseSqlite(connectionString));
            // Add services to the container.
            builder.Services.AddTransient(typeof(Repository<>));
            builder.Services.AddTransient<DtoProvider>();
            builder.Services.AddTransient<CamelLogic>();
            //builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS policy
            var frontendUrl = builder.Configuration["settings:frontend"] ?? "http://localhost:4200";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CamelCorsPolicy", policy =>
                {
                    policy.WithOrigins(frontendUrl)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Database
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CamelRegistryDbContext>();
                dbContext.Database.Migrate();
            }

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi(); 
                app.UseSwagger(); 
                app.UseSwaggerUI();
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
            })
            .WithName("CreateCamel")
            .WithOpenApi(x => {
                x.Summary = "Létrehoz egy tevét";
                x.Description = "Létrehoz egy tevét, aminek legalább a nevét meg kell adni. A púpok száma 1 és 2 között lehet csak. " +
                "Az etetés dátuma értelemszerûen nem lehet a jõvõben";
                return x;
            })
            .Produces<Camel>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // 2. Tevék listázása (GET) 
            app.MapGet("/api/camels", async (CamelLogic logic) =>
                Results.Ok(await logic.GetAllCamels()))
                .WithName("GetCamels")
                .WithOpenApi(x => {
                    x.Summary = "Lekéri az összes tevének az összes adatát.";
                    x.Description = "Visszaadja az összes tevének az összes adatát.";
                    return x;
                })
                .Produces<Camel>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

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
            })
            .WithName("GetCamelById") 
            .WithOpenApi(x => {
            x.Summary = "Lekér egy tevét azonosító alapján";
            x.Description = "Visszaadja a teve összes adatát, vagy 404-et, ha nem létezik.";
            return x;
            })
            .Produces<Camel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

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
            })
            .WithName("UpdateCamelById")
            .WithOpenApi(x => {
                x.Summary = "Frissiti egy teve adatait azonosító alapján";
                x.Description = "Frissiti egy teve adatait azonosító alapján, vagy 404-es hibát dob, ha nem létezik. " +
                "Az adatokat validálja, hibás adat esetén 400-as hibát dob. (A púpok száma 1 és 2 között lehet csak. " + 
                "Az etetés dátuma értelemszerûen nem lehet a jõvõben)";
                return x;
            })
            .Produces<Camel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

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
            })
            .WithName("DeleteCamelById")
            .WithOpenApi(x => {
                x.Summary = "Töröl egy tevét azonosító alapján";
                x.Description = "Törli az adott tevét, vagy 404-et dob vissza, ha nem létezik.";
                return x;
            })
            .Produces<Camel>(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors("CamelCorsPolicy");
            //app.UseAuthorization();


            //app.MapControllers();

            app.Run();
        }
    }
}
