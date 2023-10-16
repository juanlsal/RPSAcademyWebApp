using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Data;
using RPSAcademy.Factories;
using RPSAcademy.Models;
using RPSAcademy.Repository;
using RPSAcademy.Services;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//Services 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IFileService, FileService>();

//Repositories
builder.Services.AddTransient<IPlayRepository, PlayRepository>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();

//Factories
builder.Services.AddScoped<IUserCreatedSubjectsFactory, UserCreatedSubjectsFactory>();
builder.Services.AddScoped<IUserInfoFactory, UserInfoFactory>();
builder.Services.AddScoped<IDefaultSubjectsFactory, DefaultSubjectsFactory>();
builder.Services.AddScoped<IDefaultQuestionFactory, DefaultQuestionFactory>();
builder.Services.AddScoped<IUserCreatedQuestionsFactory, UserCreatedQuestionsFactory>();

//Patterns
builder.Services.AddControllersWithViews();

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedDefaultData(scope.ServiceProvider);
}*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();