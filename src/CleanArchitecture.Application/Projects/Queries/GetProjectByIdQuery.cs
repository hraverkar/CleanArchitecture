﻿using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Projects.Entities;

namespace CleanArchitecture.Application.Projects.Queries
{
    public sealed record GetProjectByIdQuery(Guid Id) : Query<ProjectResponseDto>;
    public sealed class GetProjectByIdQueryHandler : QueryHandler<GetProjectByIdQuery, ProjectResponseDto>
    {
        private readonly IRepository<Project> _repository;
        public GetProjectByIdQueryHandler(IMapper mapper, IRepository<Project> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected async override Task<ProjectResponseDto> HandleAsync(GetProjectByIdQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var projectDetails = _repository.GetAll(false).FirstOrDefault(i => i.Id == request.Id && !i.IsDeleted);
            if (projectDetails != null)
            {
                projectDetails = Guard.Against.NotFound(projectDetails);
                return Mapper.Map<ProjectResponseDto>(projectDetails);

            }
            throw new ArgumentNullException(nameof(request));
        }
    }
}
