
using Scalar.AspNetCore;

namespace ValueJetImport
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.AllowAnyOrigin() // Allow specific origin
                          .AllowAnyHeader(); // Allow credentials (e.g., cookies, authentication headers)
                });
            });

            // Register HttpClientFactory
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("File Processor")
                        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios)
                        .WithDownloadButton(true)
                        .WithModels(true)
                        .WithSidebar(true)
                        .WithOpenApiRoutePattern("/swagger/v1/swagger.json"); // Add Swagger JSON specification
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowSpecificOrigins");


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
