using Microsoft.AddFeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration.AddAzureAppConfiguration(
    options => {
        options.Connect("Endpoint=https://applicationconfig100000.azconfig.io;Id=4J03;Secret=3E4PNY9QIzLslklC7DkRx2AMiTQpkFAQ9LChaqcU96PQTqBMHObgJQQJ99BCACYeBjFxysoCAAACAZAC4LxF");
        options.UseFeatureFlags();
    });


builder.Services.AddFeatureManagement();

//.AddJsonFile("appsettings.json")
//    .AddEnvironmentVariables();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
