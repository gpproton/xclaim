using Microsoft.AspNetCore.Mvc;
using XClaim.Common.Base;
using XClaim.Common.Dtos;

namespace XClaim.Web.Server.Modules.CompanyModule;

public class CompanyModule : IModule {
    public IServiceCollection RegisterApiModule(IServiceCollection services) {
        services.AddScoped<CompanyService>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) {
        const string name = nameof(Company);
        var url = $"{Constants.RootApi}/{name.ToLower()}";
        var group = endpoints.MapGroup(url).WithTags(name);
            
        group.MapGet("/", async (CompanyService sv) => await sv.GetAllAsync(null))
            .WithName($"GetAll{name}")
            .WithOpenApi();
            
        group.MapGet("/{id:guid}", async (Guid id, CompanyService sv) => {
            var result = await sv.GetByIdAsync(id);
            return TypedResults.Ok(result);
        }).WithName($"Get{name}ById").WithOpenApi();
            
        group.MapPost("/", async (Company value, CompanyService sv) => {
            await sv.CreateAsync(value);
            return TypedResults.Created($"{url}/{value.Id}", value);
        }).WithName($"Create{name}").WithOpenApi();
            
        group.MapPut("/", async (Company value, CompanyService sv) => {
            var result = await sv.UpdateAsync(value);
            return result is null ? Results.NotFound() : TypedResults.Ok(value);
        }).WithName($"Update{name}").WithOpenApi();
            
        group.MapDelete("/{id:guid}", async (Guid id, CompanyService sv) => {
            var item = await sv.DeleteAsync(id);
            return item is null ? Results.NotFound() : TypedResults.Ok(item);
        }).WithName($"Archive{name}").WithOpenApi();

        return group;
    }
}