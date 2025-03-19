using Cinema.DataAccess;
using Cinema.WebApi.Infrastructure;
using System.Reflection;
using System.Text.Json.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "Cinema API",
                Version = "v1",
                Description = "Domi Cinema API"
            });

            var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
            c.IncludeXmlComments(xpath);
        });
        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddAutomapper();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseExceptionHandler();

        app.UseStatusCodePages();

        app.MapControllers();

        app.Run();

    }
}