using FluentValidation;
using neuromasters.borders.Dtos.Auth;
using neuromasters.borders.Dtos.Questionnaires;
using neuromasters.borders.Dtos.Questionnaires.DefaultAswers;
using neuromasters.borders.Dtos.Questionnaires.Forms;
using neuromasters.borders.Dtos.Questionnaires.SkillGroups;
using neuromasters.borders.Dtos.Roles;
using neuromasters.handlers.Validators;
using neuromasters.handlers.Validators.Forms;

namespace neuromasters.api.Configurations;

public static class ValidatorsConfig
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddScoped<IValidator<CreateRoleRequest>, CreateRoleRequestValidator>();
        services.AddScoped<IValidator<AssignRoleRequest>, AssignRoleRequestValidator>();
        services.AddScoped<IValidator<GetUserRolesRequest>, GetUserRolesRequestValidator>();
        services.AddScoped<IValidator<GetSkillGroupRequest>, GetSkillGroupRequestValidator>();
        services.AddScoped<IValidator<CreateSkillGroupRequest>, CreateSkillGroupRequestValidator>();
        services.AddScoped<IValidator<UpdateSkillGroupRequest>, UpdateSkillGroupRequestValidator>();
        
        //Forms

        services.AddScoped<IValidator<CreateQuestionnaireRequest>, CreateQuestionnaireRequestValidator>();
        services.AddScoped<IValidator<CreateFormSectionRequest>, CreateFormSectionRequestValidator>();
        services.AddScoped<IValidator<CreateFormQuestionRequest>, CreateFormQuestionRequestValidator>();
        services.AddScoped<IValidator<GetQuestionnaireRequest>, GetQuestionnaireRequestValidator>();
        services.AddScoped<IValidator<UpdateQuestionnaireRequest>, UpdateQuestionnaireRequestValidator>();
        services.AddScoped<IValidator<UpdateFormSectionRequest>, UpdateFormSectionRequestValidator>();
        services.AddScoped<IValidator<UpdateFormQuestionRequest>, UpdateFormQuestionRequestValidator>();
        services.AddScoped<IValidator<CreateDefaultAnswerRequest>, CreateDefaultAnswerRequestValidator>();
        services.AddScoped<IValidator<ListDefaultAnswerRequest>, ListDefaultAnswerRequestValidator>();
        services.AddScoped<IValidator<GetDefaultAnswerRequest>, GetDefaultAnswerRequestValidator>();


        return services;
    }

}
