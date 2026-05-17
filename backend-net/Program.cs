using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using POS.API.Data;
using POS.API.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

[CompilerGenerated]
internal class Program
{
	private static void _003CMain_003E_0024(string[] args)
	{
		_003C_003Ec__DisplayClass0_0 CS_0024_003C_003E8__locals0 = new _003C_003Ec__DisplayClass0_0();
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		CS_0024_003C_003E8__locals0.builder = WebApplication.CreateBuilder(args);
		CS_0024_003C_003E8__locals0.builder.get_Services().AddDbContext<AppDbContext>((Action<DbContextOptionsBuilder>?)delegate(DbContextOptionsBuilder options)
		{
			options.UseNpgsql(ConfigurationExtensions.GetConnectionString((IConfiguration)(object)CS_0024_003C_003E8__locals0.builder.get_Configuration(), "DefaultConnection"));
		}, (ServiceLifetime)1, (ServiceLifetime)1);
		AuthenticationServiceCollectionExtensions.AddAuthentication(CS_0024_003C_003E8__locals0.builder.get_Services(), "Bearer").AddJwtBearer(delegate(JwtBearerOptions options)
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = CS_0024_003C_003E8__locals0.builder.get_Configuration().get_Item("Jwt:Issuer"),
				ValidAudience = CS_0024_003C_003E8__locals0.builder.get_Configuration().get_Item("Jwt:Audience"),
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.get_UTF8().GetBytes(CS_0024_003C_003E8__locals0.builder.get_Configuration().get_Item("Jwt:Key")))
			};
		});
		PolicyServiceCollectionExtensions.AddAuthorization(CS_0024_003C_003E8__locals0.builder.get_Services());
		CS_0024_003C_003E8__locals0.builder.get_Services().AddSwaggerGen(delegate(SwaggerGenOptions c)
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "POS Ferreteria API",
				Version = "v1"
			});
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "bearer"
			});
			OpenApiSecurityRequirement openApiSecurityRequirement = new OpenApiSecurityRequirement();
			((Dictionary<OpenApiSecurityScheme, System.Collections.Generic.IList<string>>)openApiSecurityRequirement).Add(new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			}, (System.Collections.Generic.IList<string>)System.Array.Empty<string>());
			c.AddSecurityRequirement(openApiSecurityRequirement);
		});
		CorsServiceCollectionExtensions.AddCors(CS_0024_003C_003E8__locals0.builder.get_Services(), (Action<CorsOptions>)delegate(CorsOptions options)
		{
			options.AddDefaultPolicy((Action<CorsPolicyBuilder>)delegate(CorsPolicyBuilder policy)
			{
				policy.WithOrigins(new string[7] { "http://localhost", "http://localhost:80", "http://localhost:5000", "http://localhost:5001", "http://localhost:8080", "http://127.0.0.1", "http://127.0.0.1:8080" }).AllowAnyMethod().AllowAnyHeader();
			});
		});
		ServiceCollectionServiceExtensions.AddScoped<AuthService>(CS_0024_003C_003E8__locals0.builder.get_Services());
		ServiceCollectionServiceExtensions.AddScoped<ProductService>(CS_0024_003C_003E8__locals0.builder.get_Services());
		ServiceCollectionServiceExtensions.AddScoped<SaleService>(CS_0024_003C_003E8__locals0.builder.get_Services());
		ServiceCollectionServiceExtensions.AddScoped<ReportService>(CS_0024_003C_003E8__locals0.builder.get_Services());
		ServiceCollectionServiceExtensions.AddScoped<CashRegisterService>(CS_0024_003C_003E8__locals0.builder.get_Services());
		ServiceCollectionServiceExtensions.AddScoped<FacturaService>(CS_0024_003C_003E8__locals0.builder.get_Services());
		MvcServiceCollectionExtensions.AddControllers(CS_0024_003C_003E8__locals0.builder.get_Services());
		WebApplication val = CS_0024_003C_003E8__locals0.builder.Build();
		ExceptionHandlerExtensions.UseExceptionHandler((IApplicationBuilder)(object)val, (Action<IApplicationBuilder>)delegate(IApplicationBuilder exceptionHandlerApp)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			object obj = _003C_003Ec._003C_003E9__0_6;
			if (obj == null)
			{
				RequestDelegate val2 = delegate(HttpContext context)
				{
					//IL_0002: Unknown result type (might be due to invalid IL or missing references)
					//IL_0007: Unknown result type (might be due to invalid IL or missing references)
					_003C_003Ec._003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed _003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed = default(_003C_003Ec._003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed);
					_003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
					_003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed.context = context;
					_003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed._003C_003E1__state = -1;
					((AsyncTaskMethodBuilder)(ref _003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed._003C_003Et__builder)).Start<_003C_003Ec._003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed>(ref _003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed);
					return ((AsyncTaskMethodBuilder)(ref _003C_003C_003CMain_003E_0024_003Eb__0_6_003Ed._003C_003Et__builder)).get_Task();
				};
				obj = (object)val2;
				_003C_003Ec._003C_003E9__0_6 = val2;
			}
			RunExtensions.Run(exceptionHandlerApp, (RequestDelegate)obj);
		});
		CorsMiddlewareExtensions.UseCors((IApplicationBuilder)(object)val);
		AuthAppBuilderExtensions.UseAuthentication((IApplicationBuilder)(object)val);
		AuthorizationAppBuilderExtensions.UseAuthorization((IApplicationBuilder)(object)val);
		((IApplicationBuilder)(object)val).UseSwagger();
		((IApplicationBuilder)(object)val).UseSwaggerUI();
		ControllerEndpointRouteBuilderExtensions.MapControllers((IEndpointRouteBuilder)(object)val);
		val.get_Urls().Add("http://0.0.0.0:5000");
		val.Run((string)null);
	}
}
