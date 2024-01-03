using Admin.UI.Service;
using Admin.UI.Service.IService;
using Admin.UI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IUserInfoService,UserInfoService>();
ApiTypeSD.UserInfoAPIBase = builder.Configuration["ServicerUrls:UserinfoAPIBase"];
ApiTypeSD.StateAPIBase = builder.Configuration["ServicerUrls:StateAPI"];



builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IBaseService, BaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
