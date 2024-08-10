using Apollo.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); // Protect all pages by default
    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Login"); // Allow anonymous access to the Login page
    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Register"); // Allow anonymous access to the Register page
    options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/Logout"); // Allow anonymous access to the Logout page
});

builder.Services.AddGeminiService();
builder.Services.AddData(builder.Configuration);
builder.Services.AddIdentity();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
