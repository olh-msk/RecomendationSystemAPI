
using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Repositories;
using RecomendatinSystemAPI.Repositories.Interfaces;
using RecomendatinSystemAPI.Services;
using RecomendatinSystemAPI.Services.Interfaces;
using RecomendationSystemAPI.Data;
using RecomendationSystemAPI.Repositories;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendatinSystemAPI
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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();

            builder.Services.AddScoped<IInterestTagRepository, InterestTagRepository>();
            builder.Services.AddScoped<IInterestTagService, InterestTagService>();

            builder.Services.AddScoped<IStudentInterestRepository, StudentInterestRepository>();
            builder.Services.AddScoped<IStudentInterestService, StudentInterestService>();

            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            builder.Services.AddScoped<IRecommendationService, RecommendationService>();

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

            // init fill DB
            DbInitializer.Seed(app);

            app.Run();
        }
    }
}
