using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using usue_online_tests.Data;
using usue_online_tests.Report;
using usue_online_tests.Tests;

namespace usue_online_tests
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<DataContext>();

            services.AddHttpContextAccessor();
            services.AddSingleton(new TestsLoader(_environment));
            services.AddScoped<GetUserByCookie>();
            services.AddScoped<ReportMaker, ExcelReportMaker>();
            services.AddScoped<IReportDataProvider, DbDataProvider>();
            services.AddAutoMapper(ConfigMapper);

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.MaxAge = new TimeSpan(365, 0, 0, 0);
                options.ExpireTimeSpan = new TimeSpan(365, 0, 0, 0);
                options.AccessDeniedPath = "/login/noaccess";
                options.LoginPath = "/login/index";
                options.LogoutPath = "/login/loginout";
            });
        }

        private void ConfigMapper(IMapperConfigurationExpression obj)
        {
            obj.AddProfile(new MapperProfile.MapperProfile());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.Use(async (context, func) =>
            //{
            //    var endpoint = context.GetEndpoint();
            //    if (endpoint == null)
            //    {
            //        context.Response.Redirect("/");
            //        return;
            //    }

            //    await func.Invoke();
            //});

            //app.UseMiddleware<AccessProtectionMiddleware>();

            app.UseEndpoints(builder => builder.MapDefaultControllerRoute());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });

            app.UseSpa(spaBuilder =>
            {
                if (env.IsDevelopment())
                {
                    spaBuilder.UseProxyToSpaDevelopmentServer("http://localhost:5173/");
                }
            });
        }
    }

    class AccessProtectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthorizationPolicyProvider _policyProvider;
        private readonly List<Tuple<string, DateTime>> _requestTable = new();
        private readonly string[] _ipException = { "91.207.247.1", "::1" };

        public AccessProtectionMiddleware(RequestDelegate next, IAuthorizationPolicyProvider policyProvider)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _policyProvider = policyProvider ?? throw new ArgumentNullException(nameof(policyProvider));
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                await _next.Invoke(context);
                return;
            }

            if (endpoint.Metadata.Any(o => o is AntiDosAttribute))
            {
                AntiDosAttribute attribute = (AntiDosAttribute)endpoint.Metadata.First(o => o is AntiDosAttribute);
                if (context.Connection.RemoteIpAddress != null)
                {
                    string ip = context.Connection.RemoteIpAddress.ToString();

                    if (_ipException.Contains(ip))
                    {
                        await _next.Invoke(context);
                        return;
                    }

                    //string redirectUrl = "/Login?message=AntiDdos";
                    string redirectUrl = "/protection/ddos";

                    var tuple = _requestTable.FirstOrDefault(tuple => tuple.Item1 == ip);
                    if (tuple == null)
                    {
                        _requestTable.Add(Tuple.Create(ip, DateTime.Now.ToNowEkb()));
                        await _next.Invoke(context);
                        return;
                    }

                    if ((DateTime.Now.ToNowEkb() - tuple.Item2).TotalSeconds < attribute.Delay)
                    {
                        _requestTable.Remove(tuple);
                        _requestTable.Add(Tuple.Create(ip, DateTime.Now.ToNowEkb()));
                        context.Response.Redirect($"{redirectUrl}");
                        return;
                    }

                    _requestTable.Remove(tuple);
                    _requestTable.Add(Tuple.Create(ip, DateTime.Now.ToNowEkb()));
                }
            }


            await _next.Invoke(context);
        }
    }

    class AntiDosAttribute : Attribute
    {
        public int Delay { get; set; } = 5;
    }
}
