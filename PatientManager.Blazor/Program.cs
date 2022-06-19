//Use the needed library
using PatientManager.Blazor.Data;
using PatientManager.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//Inject this program with IPatientService interface and PatientService class
builder.Services.AddTransient<IPatientService, PatientService>();

//Add Http client that will connect PatientManager.Api with this program
builder.Services.AddHttpClient<IPatientService, PatientService>(client => client.BaseAddress = new Uri("https://localhost:7260/"));


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
