using BuberBreakfast.Api.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSingleton<IBreakfastService, BreakfastService>();
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}