using System.Text.Json.Serialization;
using Home_6.API.Filters;
using Serilog;

namespace Home_6.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddControllers(opts =>
        {
            opts.Filters.Add<LogFilter>();       
            opts.Filters.Add<AuthFilter>();      
            opts.Filters.Add<ExceptionFilter>();
        }).AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((ctx, lc) =>
        {
            lc.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Service", ctx.Configuration["Logging:ApplicationName"])
                .WriteTo.Console(outputTemplate:"{Timestamp:HH:mm:ss} [{Level:u3}] [{Service}] {Message:lj}{NewLine}{Exception}");
        });
        
        builder.Services.AddWebApi(builder.Configuration);
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
            app.UseSwagger();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseSerilogRequestLogging();
        
        app.MapControllers();
        app.Run();
    }
}