﻿using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Task_Details.Queries
{
    public sealed record GetTaskByIdQuery(Guid Id) : Query<TaskDetailsResponseDto>;

    public sealed class GetTaskByIdQueryHandler : QueryHandler<GetTaskByIdQuery, TaskDetailsResponseDto>
    {
        private readonly IRepository<TaskDetails> _taskDetailsRepository;
        public GetTaskByIdQueryHandler(IMapper mapper, IRepository<TaskDetails> taskDetailsRepository) : base(mapper)
        {
            _taskDetailsRepository = taskDetailsRepository;
        }

        protected async override Task<TaskDetailsResponseDto> HandleAsync(GetTaskByIdQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var taskDetails = _taskDetailsRepository
            .GetAll(false).Include(a => a.TaskStatus).Include(a => a.Project).FirstOrDefault(t => t.Id == request.Id);
            if (taskDetails != null)
            {
                Guard.Against.NotFound(taskDetails);
                return (Mapper.Map<TaskDetailsResponseDto>(taskDetails));
            }
            throw new ArgumentNullException(nameof(request));
        }
    }
}
