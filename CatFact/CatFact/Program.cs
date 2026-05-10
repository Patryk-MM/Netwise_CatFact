using CatFact;
using CatFact.Services;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();

services.AddScoped<MainMenu>();
services.AddScoped<CatFactService>();
services.AddHttpClient<CatFactService>(client => {
	client.BaseAddress = new Uri(@"https://catfact.ninja/fact");
});

IServiceProvider serviceProvider = services.BuildServiceProvider();

var mainMenu = serviceProvider.GetRequiredService<MainMenu>();

await mainMenu.RunAsync();