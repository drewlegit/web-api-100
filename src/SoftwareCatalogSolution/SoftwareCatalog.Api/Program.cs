var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Above this line is configuring the "internals" of our API Project.
var app = builder.Build(); // THE LINE IN THE SAND
// Everything after this line is configuring how the web server handles incoming requests/responses

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
// Make Some Change
app.MapControllers();
app.Run(); // a blocking infinite for loop.