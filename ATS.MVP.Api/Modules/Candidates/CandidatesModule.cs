using ATS.MVP.Api.Modules.Candidates.Requests;
using ATS.MVP.Application.Candidates.Commands;
using ATS.MVP.Application.Candidates.Queries;
using ATS.MVP.Domain.Candidates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ATS.MVP.Api.Modules.Candidates;

/// <summary>
/// Módulo (grupo de endpoints) de Candidatos
/// </summary>
internal class CandidatesModule : IModule
{
    /// <summary>
    /// Adiciona Endpoints de Candidatos ao app
    /// </summary>
    /// <param name="app"></param>
    public void AddEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/candidates");

        group.MapPost("/", async ([FromBody] CreateCandidateCommand request, IMediator mediator) =>
        {
            var response = await mediator.Send(request);

            return Results.Ok(response);
        })
            .Produces<Candidate>(StatusCodes.Status200OK);

        group.MapDelete("/{id}", async ([FromRoute] Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteCandidateByIdCommand(id));

            return Results.Accepted();
        })
        .Produces(StatusCodes.Status200OK);

        group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateCandidateRequest request, IMediator mediator) =>
        {
            await mediator.Send(new UpdateCandidateCommand(id, request.Name, request.Email, request.PhoneNumber));

            return Results.Accepted();
        })
        .Produces<Candidate>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator) =>
        {
            var response = await mediator.Send(new GetCandidateByIdQuery(id));

            return Results.Ok(response);
        })
        .Produces<Candidate>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group
            .MapGet("", async (IMediator mediator) =>
        {
            var response = await mediator.Send(new GetCandidatesQuery());

            return Results.Ok(response);
        })
        .Produces<IEnumerable<Candidate>>(StatusCodes.Status200OK);
    }
}
