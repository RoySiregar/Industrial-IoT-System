using Microsoft.EntityFrameworkCore;
using ProductionReportAPI.Data;
// 1. [BARU] Tambahkan namespace Services agar 'RobotService' dikenali
using ProductionReportAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);





// 2. [BARU] Daftarkan RobotService di sini!
//    Kita pakai 'AddScoped' karena Service ini menggunakan Database (DbContext).


builder.Services.AddScoped<HistoricalDataService>();

builder.Services.AddScoped<CategoryDetailService>();

// Menambahkan IMemoryCache ke DI container
builder.Services.AddMemoryCache();

// Tambahkan CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // Mengizinkan semua asal (origin)
                  .AllowAnyMethod()  // Mengizinkan semua metode (GET, POST, dll)
                  .AllowAnyHeader(); // Mengizinkan semua header
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductionReportAPI V1");
        c.RoutePrefix = "Api";
    });
}

// app.UseHttpsRedirection(); // Matikan dulu sesuai request Anda

app.UseRouting();

// Aktifkan CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();