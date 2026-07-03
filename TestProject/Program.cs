using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Interfaces.Repositories;
using TestProject.Application.Interfaces.Services;
using TestProject.Application.Services;
using TestProject.Domain.Entities;
using TestProject.Infrastructure.Persistence;
using TestProject.Infrastructure.Repositories;
using TestProject;
using TestProject.Interfaces.Services;
using TestProject.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// var services = new ServiceCollection();

// services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(
//         @"Server=(localdb)\MSSQLLocalDB;
//           Database=StudentManagementDb;
//           Trusted_Connection=True;
//           MultipleActiveResultSets=true"));

// services.AddScoped<IStudentRepository, StudentRepository>();
// services.AddScoped<ISubjectRepository, SubjectRepository>();
// services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

// services.AddScoped<IStudentService, StudentService>();
// services.AddScoped<ISubjectService, SubjectService>();
// services.AddScoped<IEnrollmentService, EnrollmentService>();

// var provider = services.BuildServiceProvider();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDbContext<ApplicationDbContext>(
options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString(
                "DefaultConnection")));




builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AngularPolicy",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Security

builder.Services
    .AddAuthentication(
        JwtBearerDefaults
            .AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]))
            };
    });

builder.Services.AddAuthorization();

// Swagger configuration with JWT security for .NET 10
builder.Services.AddSwaggerGen(options =>
{
    options.AddJwtBearerSecurity();
});

// builder.Services.AddSwaggerGen(options =>
// {
//     options.AddSecurityDefinition(
//         "Bearer",
//         new global::Microsoft.OpenApi.Models.OpenApiSecurityScheme
//         {
//             Name = "Authorization",
//             Type = global::Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//             Scheme = "bearer",
//             BearerFormat = "JWT",
//             In = global::Microsoft.OpenApi.Models.ParameterLocation.Header,
//             Description = "Enter: Bearer {your JWT token}"
//         });
//
//     options.AddSecurityRequirement(
//         new global::Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//         {
//             {
//                 new global::Microsoft.OpenApi.Models.OpenApiSecurityScheme
//                 {
//                     Reference = new global::Microsoft.OpenApi.Models.OpenApiReference
//                     {
//                         Type = global::Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                         Id = "Bearer"
//                     }
//                 },
//                 Array.Empty<string>()
//             }
//         });
// });

// builder.Services
// .AddAuthentication(
// JwtBearerDefaults
// .AuthenticationScheme)
// .AddJwtBearer(...);

// builder.Services
//     .AddAuthorization();

//pipeline

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("AngularPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// using var scope = provider.CreateScope();

// var studentService =
//     scope.ServiceProvider.GetRequiredService<IStudentService>();

// var subjectService =
//     scope.ServiceProvider.GetRequiredService<ISubjectService>();

// var enrollmentService =
//     scope.ServiceProvider.GetRequiredService<IEnrollmentService>();

// await studentService.AddStudentAsync(new Student
// {
//     StudentId = "ST001",
//     Name = "John",
//     Age = 20,
//     DateOfBirth = new DateTime(2005, 1, 1),
//     Address = "Colombo"
// });

// await subjectService.AddSubjectAsync(new Subject
// {
//     SubjectId = "SUB001",
//     SubjectName = "Mathematics"
// });

// await enrollmentService.AssignStudentToSubjectAsync(1, 1);

// Console.WriteLine("Data inserted successfully.");






// using Microsoft.Extensions.DependencyInjection;
// using TestProject.Interfaces;
// using TestProject.Services;
// using TestProject.Repositories;

// var services = new ServiceCollection();

// // Repositories
// services.AddSingleton<IStudentRepository, StudentRepository>();
// services.AddSingleton<ISubjectRepository, SubjectRepository>();
// services.AddSingleton<IEnrollmentRepository, EnrollmentRepository>();

// // Services
// services.AddSingleton<IStudentService, StudentService>();
// services.AddSingleton<ISubjectService, SubjectService>();
// services.AddSingleton<IEnrollmentService, EnrollmentService>();

// var serviceProvider = services.BuildServiceProvider();

// // Resolve services
// var studentService = serviceProvider.GetRequiredService<IStudentService>();
// var subjectService = serviceProvider.GetRequiredService<ISubjectService>();
// var enrollmentService = serviceProvider.GetRequiredService<IEnrollmentService>();


// Console.WriteLine("Adding New Student");

// // Add Students
// studentService.AddStudent(new Student
// {
//     Id = 1,
//     Name = "John",
//     Age = 20,
//     DateOfBirth = new DateTime(2004, 1, 1),
//     Address = "Colombo"
// });

// studentService.AddStudent(new Student
// {
//     Id = 2,
//     Name = "Jane",
//     Age = 21,
//     DateOfBirth = new DateTime(2003, 5, 15),
//     Address = "Kandy"
// });

// // Add Subjects
// subjectService.AddSubject(new Subject
// {
//     Id = 101,
//     Name = "Mathematics"
// });

// subjectService.AddSubject(new Subject
// {
//     Id = 102,
//     Name = "Science"
// });

// // Assign Students
// enrollmentService.AssignStudentToSubject(1, 101);
// enrollmentService.AssignStudentToSubject(2, 102);

// Console.WriteLine("Students, subjects and enrollments added successfully.");