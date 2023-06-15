using Microsoft.EntityFrameworkCore;
using RedisNet6.Cache;
using RedisNet6.WebAPI.Models;
using RedisNet6.WebAPI.Repositories;
using RedisNet6.WebAPI.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductRepository>(sp =>
    {
        var appDbContext=sp.GetRequiredService<AppDbContext>(); 
        var productRepository=new ProductRepository(appDbContext);
        var redisService=sp.GetRequiredService<RedisService>();

        return new ProductRepositoryWithCacheDecorator(productRepository, redisService);

    });

    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseInMemoryDatabase("myDatabase");
    });

    builder.Services.AddSingleton<RedisService>(sp =>
    {
        return new RedisService(builder.Configuration["CacheOptions:Url"]);
    });

    builder.Services.AddSingleton<IDatabase>(sp =>
    {
        RedisService redisService = sp.GetRequiredService<RedisService>();
        return redisService.GetDatabase(0);

    });

    builder.Services.AddScoped<IProductService, ProductService>();
}

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}