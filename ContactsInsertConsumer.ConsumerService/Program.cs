using ContactsInsertConsumer.ConsumerService;
using FIAP.TechChallenge.ContactsInsertConsumer.ConsumerService.Events;
using FIAP.TechChallenge.ContactsInsertConsumer.ConsumerService.IoC;
using MassTransit;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.Dev.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                         .AddJsonFile("appsettings.json", false, true)
                                                         .AddJsonFile($"appsettings.Dev.json", true, true)
                                                         .AddEnvironmentVariables()
                                                         .Build();

builder.Services.AddDependencyResolver(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddHostedService<Worker>();

var queue = configuration.GetSection("MassTransit:QueueName").Value ?? string.Empty;
var server = configuration.GetSection("MassTransit:Server").Value ?? string.Empty;
var user = configuration.GetSection("MassTransit:User").Value ?? string.Empty;
var password = configuration.GetSection("MassTransit:Password").Value ?? string.Empty;

builder.Services.AddMassTransit(x =>
{  
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(server, "/", h =>
        {
            h.Username(user);
            h.Password(password);
        });

        cfg.ReceiveEndpoint(queue, e =>
        {
            e.ConfigureConsumer<ContactInsertConsumer>(context);
            //e.Consumer<ContactInsertConsumer>();
        });
       
        cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<ContactInsertConsumer>();
});
            

var host = builder.Build();
host.Run();
