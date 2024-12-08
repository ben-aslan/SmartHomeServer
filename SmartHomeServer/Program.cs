using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Core.DependencyResolvers;
using Core.Utilities.Security.Encryption;
using Core.Utilities;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Jwt;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using SmartHomeServer.Controllers;
using SmartHomeServer.TelegramUtils.BotConfiguration;
using SmartHomeServer.TelegramUtils.BotConfiguration.Abstract;
using SmartHomeServer.TelegramUtils.DependencyResolvers.Abstract;
using SmartHomeServer.TelegramUtils.DependencyResolvers;
using SmartHomeServer.TelegramUtils.Handle.Abstract;
using SmartHomeServer.TelegramUtils.Handle;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBotConfiguration, BotConfiguration>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

builder.Services.AddSingleton<IDependencyResolver, AutofacDR>();

builder.Services.AddScoped<IHandle, UpdateHandle>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(b =>
{
    b.RegisterModule(new AutofacDR(builder.Environment, builder.Configuration));
});

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

builder.Services.AddHostedMqttServer(ob =>
{
    ob.WithDefaultEndpoint();
    ob.WithDefaultEndpointPort(1883);
    ob.WithConnectionBacklog(100);
}).AddMqttConnectionHandler().AddConnections();

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseKestrel(o =>
    {
        o.ListenAnyIP(1883, l => l.UseMqtt());
        o.ListenAnyIP(5025);
        o.ListenAnyIP(5026, l => l.UseHttps());
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapConnectionHandler<MQTTController>("/mqtt",
        httpConnectionDispatcherOptions => httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector = protocolList => protocolList.FirstOrDefault() ?? string.Empty);

app.MapMqtt("/mqtt");

app.UseMqttServer(server => { });

app.Run();
