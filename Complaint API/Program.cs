using Complaint_API.Contexts;
using Complaint_API.Repository.Contracts;
using Complaint_API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Complaint_API.Handlers;
using System.Net;
using Complaint_API.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure MyContext to SqlServer
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));

// Configure CORS
builder.Services.AddCors(options =>
                        options.AddDefaultPolicy(policy => {
                            policy.AllowAnyOrigin();
                            policy.AllowAnyHeader();
                            policy.AllowAnyMethod();
                        }));

// Configure Dependency Injection for Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IResolutionRepository, ResolutionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStatusCodePages(async context => {
    var response = context.HttpContext.Response;
    ResultFormat format = new ResultFormat { Data = 1 };

    if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
    {
        format.ChangeStatus(StatusCodes.Status401Unauthorized, HttpStatusCode.Unauthorized.ToString(), "Please login first!");
        await response.WriteAsJsonAsync(format);
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
    {
        format.ChangeStatus(StatusCodes.Status404NotFound, HttpStatusCode.NotFound.ToString(), "Data not found");
        await response.WriteAsJsonAsync(format);
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
    {
        format.ChangeStatus(StatusCodes.Status403Forbidden, HttpStatusCode.Forbidden.ToString(), "You are forbidden to use this feature!");
        await response.WriteAsJsonAsync(format);
    }
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
