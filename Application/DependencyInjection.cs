using Application.Behaviors;
using Application.Commands.AuthorCommands;
using Application.Commands.BookCommands;
using Application.Validators.AuthorValidators;
using Application.Validators.BookValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(assembly));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IValidator<UpdateBookCommand>, UpdateBookCommandValidator>();
            services.AddTransient<IValidator<AddBookCommand>, AddBookCommandValidator>();
            services.AddTransient<IValidator<AddAuthorCommand>, AddAuthorCommandValidator>();
            services.AddTransient<IValidator<UpdateAuthorCommand>, UpdateAuthorCommandValidator>();

            return services;
        }
    }
}
