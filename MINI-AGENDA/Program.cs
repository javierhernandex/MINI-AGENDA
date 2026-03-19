using MINI_AGENDA;
using MINI_AGENDA.Models.Exceptions;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);



var app = builder.Build();

startup.Configure(app, app.Environment);



app.UseMiddleware<ExceptionMiddleware>();

app.Run();
