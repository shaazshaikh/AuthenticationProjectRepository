using AuthenticationProject.Helpers;
using AuthenticationProject.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AuthenticationProject
{
    public class AuthenticationStartup
    {
        public IConfiguration Configuration { get; }
        public AuthenticationStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //We cannot modify name of this function
        public void ConfigureServices(IServiceCollection services)
        {
            ////uncomment to use swagger
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
            //});
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IDbConnection>((sp) => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ISignUpRepository, SignUpRepository>();
            services.AddTransient<AuthenticationHelper>();
            services.AddControllers();


        }

            //We cannot modify name of this function. It helps in  setting up http request pipeline
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ////uncomment to use swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your api v1");
            //    c.ConfigObject.DisplayRequestDuration = true;
            //    c.DocExpansion(DocExpansion.None);
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection(); // Redirects http to https
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("api", "api/[controller]");
            //});
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
