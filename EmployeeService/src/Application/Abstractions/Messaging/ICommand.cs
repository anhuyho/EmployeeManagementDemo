using EmployeeService.Domain.Shared;
using MediatR;

namespace EmployeeService.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand { }


public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse> { }
