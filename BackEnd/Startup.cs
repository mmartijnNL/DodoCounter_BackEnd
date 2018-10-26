using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("DodoCounter"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
            {
                options.TokenValidationParameters = new TokenValidationParameters { 
                    ValidateIssuer=true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer="mysite.com",
                    ValidAudience="mysite.com",
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersupersupersecretKey"))
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext _context)
        {
            // adding test data
            var martijn = new Models.UserModel("Martijn", "Kalteren", "martijn_kalteren@hotmail.com", "Banaan", Models.Role.Manager);
            var anouk = new Models.UserModel("Anouk", "Veer", "a@hotmail.com", "Banaan", Models.Role.Watcher);
            var admin = new Models.UserModel("Ad", "Min", "Admin", "Admin", Models.Role.Administrator);
            _context.Users.Add(martijn);
            _context.Users.Add(anouk);
            _context.Users.Add(admin);
            _context.SaveChanges();
            _context.Entries.Add(new Models.EntryModel(5, "Wezelandenpark Zwolle", DateTime.Now, martijn));
            _context.Entries.Add(new Models.EntryModel(3, "Genemuiden", new DateTime(2018, 10, 18, 9, 00, 0), martijn));
            _context.Entries.Add(new Models.EntryModel(1, "Blankenham", new DateTime(2018, 10, 19, 12, 30, 30), anouk));
            _context.SaveChanges();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //setup CORS to accept localhost port 4200
            //app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
