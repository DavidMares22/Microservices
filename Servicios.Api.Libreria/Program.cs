
using Servicios.Api.Libreria.Core;
using Servicios.Api.Libreria.Core.ContextMongoDB;
using Servicios.Api.Libreria.Repository;

namespace Servicios.Api.Libreria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

      
            builder.Services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = builder.Configuration.GetSection("MongoDb:ConnectionString").Value;

                    options.Database = builder.Configuration.GetSection("MongoDb:Database").Value;
                }
            );

            builder.Services.AddSingleton<MongoSettings>();
            builder.Services.AddTransient<IAutorContext, AutorContext>();
            builder.Services.AddTransient<IAutorRepository, AutorRepository>();
            builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));


            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
