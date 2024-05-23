using Microsoft.EntityFrameworkCore;
using QuestionBank.DataManagement;
using QuestionBank.Mapper.DomainEntityMapper;
using QuestionBank.Repository.Impl;
using QuestionBank.Repository.Interface;
using QuestionBank.Service.Impl;
using QuestionBank.Service.Interface;

namespace QuestionBank.Api
{
    public static class ServiceExtensions
    {

        public static void AddDatabaseContext(this IServiceCollection collection,string? connectionString)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            collection.AddDbContext<QuestionBankContext>(options =>
            {

                options.UseNpgsql(connectionString);
                
            });

            collection.AddScoped<DbContext, QuestionBankContext>();

            collection.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static void AddRepository(this IServiceCollection collection)
        {
            collection.AddScoped<IUserAccountRepository, UserAccountRepository>();
            collection.AddScoped<IMiscRepository,MiscRepository>();
            collection.AddScoped<IQuestionCategoryRepository,QuestionCategoryRepository>();
            collection.AddScoped<IQuestionRepository,QuestionRepository>();
            collection.AddScoped<IQuestionFeedBackRepository,QuestionFeedBackRepository>();

        }

        public static void AddBusinessLayer(this IServiceCollection collection)
        {
            collection.AddScoped<IUserAccountService, UserAccountService>();
            collection.AddScoped<IAuthService, AuthService>();
            collection.AddScoped<IQuestionCategoryService, QuestionCategoryService>();
            collection.AddScoped<ISettingService, SettingService>();
            collection.AddScoped<IQuestionService, QuestionService>();
            collection.AddScoped<IQuestionFeedBackService, QuestionFeedBackService>();
            collection.AddScoped<ITagService, TagService>();

        }


        public static void AddMapper(this IServiceCollection collection)
        {
            collection.AddAutoMapper(typeof(DomainEntityProfile).Assembly);
        }

        public static void AddInternationalization(this IServiceCollection collection) {

            collection.AddLocalization(options => options.ResourcesPath = "i18n");


        }

        public static void SetUpCors(this IServiceCollection collection)
        { 
            collection.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }


    
}
