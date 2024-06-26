﻿using FluentValidation;
using MediatR.Pipeline;

namespace ATS.MVP.Application.Commom.Pipelines
{
    public class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {

        private readonly IValidator<TRequest> _validator;

        public ValidationPreProcessor(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
        }
    }
}
