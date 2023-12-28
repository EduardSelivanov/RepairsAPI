
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepairsAPI.DBContexts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repairs.API.Repositories.Token;
using Repairs.API.DBContexts;
using Repairs.API.Mappings;
using Repairs.API.Repositories.RepairRepo;

namespace RepairsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IRepairRepository, RepairRepository>();

            builder.Services.AddDbContext<AuthDBContext>(opt=>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("RepAuthConnectionString")));

            builder.Services.AddDbContext<RepairDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("RepairConnectionString")));

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Repar")
                .AddEntityFrameworkStores<AuthDBContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequiredUniqueChars = 0;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
            opt.TokenValidationParameters=new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience=true,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}