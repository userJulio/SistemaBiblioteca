using LibraryRent.Repositories.Implementation;
using LibraryRent.Repositories.Interface;
using LibraryRent.Services.Implementation;
using LibraryRent.Services.Interface;
using LibraryRent.Services.Profiles;
using LibrayRent.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//CORS Configuration
var corsConfiguration = "LibraryCors";
builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader().WithExposedHeaders(new string[] { "TotalRegistros" });
        policy.AllowAnyMethod();
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configruacion context and conection string
builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CustomerProfile>();
    config.AddProfile<BookProfile>();
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService,OrderService>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsConfiguration);
app.UseAuthorization();


app.MapControllers();

app.Run();
