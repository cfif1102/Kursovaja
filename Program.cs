using Kursovaja;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

string connection = Configuration.GetConnectionString("SqlServerConnection");

var Configuration = builder.Configuration;
var services = builder.Services;

services.AddDbContext<StudentsContext>(options => options.UseSqlServer(connection));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.Run();
