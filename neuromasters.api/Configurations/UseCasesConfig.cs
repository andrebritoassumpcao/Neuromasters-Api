using neuromasters.borders.UseCases.Auth;
using neuromasters.borders.UseCases.Questionnaires.Form;
using neuromasters.borders.UseCases.Questionnaires.SkillGroups;
using neuromasters.borders.UseCases.Roles;
using neuromasters.handlers.UseCases.Auth;
using neuromasters.handlers.UseCases.Questionnaires.Forms;
using neuromasters.handlers.UseCases.Questionnaires.SkillGroups;
using neuromasters.handlers.UseCases.Roles;

namespace neuromasters.api.Configurations;
public static class UseCasesConfig
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<ICreateRoleUseCase, CreateRoleUseCase>();
        services.AddScoped<IAssignRoleUseCase, AssignRoleUseCase>();
        services.AddScoped<IGetUserRoleUseCase, GetUserRoleUseCase>();
        services.AddScoped<IListRolesUseCase, ListRolesUseCase>();

        //SkillGroup
        services.AddScoped<ICreateSkillGroupUseCase, CreateSkillGroupUseCase>();
        services.AddScoped<IListSkillGroupsUseCase, ListSkillGroupsUseCase>();
        services.AddScoped<IGetSkillGroupUseCase, GetSkillGroupUseCase>();
        services.AddScoped<IUpdateSkillGroupUseCase, UpdateSkillGroupUseCase>();
        services.AddScoped<IDeleteSkillGroupUseCase, DeleteSkillGroupUseCase>();

        //Forms
        services.AddScoped<ICreateQuestionnaireUseCase, CreateQuestionnaireUseCase>();
        services.AddScoped<IListQuestionnairesUseCase, ListQuestionnairesUseCase>();
        services.AddScoped<IGetQuestionnaireUseCase, GetQuestionnaireUseCase>();
        services.AddScoped<IUpdateQuestionnaireUseCase, UpdateQuestionnaireUseCase>();
        services.AddScoped<IDeleteQuestionnaireUseCase, DeleteQuestionnaireUseCase>();

        return services;
    }
}
