
using System;
using ElasticsearchTestApp.Services;
using Minio;

namespace ElasticsearchTestApp
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

            builder.Services.AddSingleton(_ =>
            {
                return new MinioClient()
                    .WithEndpoint("localhost:9000")
                    .WithCredentials("minio_access_key", "minio_secret_key")
                    .WithSSL(false)
                    .Build();
            });

            builder.Services.AddScoped<FileStorageService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
