using FluentValidation;
using KickTime.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace KickTime.API.Configuration;

public static class ValidationResponseFactory
{
    public static IActionResult Create(
    ActionContext context)
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x =>
                string.IsNullOrWhiteSpace(x.ErrorMessage)
                    ? "Validation failed."
                    : x.ErrorMessage)
            .ToList();

        var response = ApiResponse<object>.Fail(
            "Validation failed.",
            errors);

        return new BadRequestObjectResult(response);
    }
}