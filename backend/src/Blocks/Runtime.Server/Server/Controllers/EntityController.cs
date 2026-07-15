using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using Fand.Runtime.Mapping;

using Mediator;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.Filters;

using thc.HotKnobs.Commands;
using thc.HotKnobs.Contracts;
using thc.HotKnobs.Models;
using thc.HotKnobs.Queries;

namespace thc.HotKnobs.Runtime.Server.Controllers;

/// <summary>
/// Entity controller with base operations.
/// </summary>
public abstract class EntityController<TEntity, TResponse, TDeleteCommand, TGetByIdQuery, TListQuery> : ControllerBase, IResourceOperation<TResponse>
	where TEntity : Entity
	where TResponse : class, IResourceResponse
	where TDeleteCommand : IEntityDeleteCommand, new()
	where TGetByIdQuery : IEntityGetByIdQuery<TEntity>, new()
	where TListQuery : IEntityListQuery<TEntity>, new()
{
	/// <summary>
	/// AutoMapper.
	/// </summary>
	protected readonly IMapper Mapper;

	/// <summary>
	/// Mediator.
	/// </summary>
	protected readonly IMediator Mediator;

	protected EntityController(ILogger<EntityController<TEntity, TResponse, TDeleteCommand, TGetByIdQuery, TListQuery>> logger, IMapper mapper, IMediator mediator)
	{
		Mapper = mapper;
		Mediator = mediator;
	}

	/// <inheritdoc />
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[SwaggerResponseHeader(StatusCodes.Status204NoContent, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async ValueTask Delete([FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		var cmd = new TDeleteCommand { Id = id, ETag = etag };
		_ = await Mediator.Send(cmd, cancellationToken);

		Response.StatusCode = StatusCodes.Status204NoContent;
	}

	/// <inheritdoc />
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async ValueTask<TResponse> Get([FromRoute] string id, [FromHeader(Name = "If-None-Match")] string? etag = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		var qry = new TGetByIdQuery { Id = id, OnlyActive = onlyActive };
		var entity = await Mediator.Send(qry, cancellationToken);

		return Mapper.Map<TResponse>(entity);
	}

	/// <inheritdoc />
	public virtual async IAsyncEnumerable<dynamic> List([FromQuery] string? columns = default, [FromQuery] string? filter = null, [FromQuery] bool onlyActive = true, [FromQuery] string? orderBy = null, [FromQuery] string? paginationToken = null, [FromQuery] string? search = null, [FromQuery] string? synchronizationToken = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var qry = new TListQuery
		{
			// Filter = filter is not null ? System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), false, filter) : null,
			OnlyActive = onlyActive,
			OrderBy = orderBy?.Split(',').Select(x =>
			{
				var parts = x.Trim().Split(' ');
				var propertyName = parts[0];
				var direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? OrderDirection.Descending : OrderDirection.Ascending;

				var parameter = Expression.Parameter(typeof(TEntity), "x");
				var property = Expression.Property(parameter, propertyName);
				var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);

				return Tuple.Create(lambda, direction);
			}),
			PaginationToken = paginationToken,
			Search = search,
			Selector = x => x.Id,
			SynchronizationToken = synchronizationToken
		};

		var result = await Mediator.Send(qry, cancellationToken);

		await foreach (var id in result.Items.WithCancellation(cancellationToken))
			yield return id;
	}
}

/// <summary>
/// Entity controller with both Create and Update.
/// </summary>
public abstract class EntityControllerWithCreateAndUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateRequest, TUpdateRequest, TCreateCommand, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery> : EntityControllerWithUpdateOnly<TEntity, TUpdateModel, TResponse, TUpdateRequest, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery>, IResourceOperationsWithCreateAndUpdate<TResponse, TCreateRequest, TUpdateRequest>
	where TEntity : Entity
	where TCreateModel : class, new()
	where TUpdateModel : class, new()
	where TResponse : class, IResourceResponse
	where TCreateRequest : class, IResourceCreateRequest
	where TUpdateRequest : class, IResourceUpdateRequest
	where TCreateCommand : IEntityCreateCommand<TCreateModel, TEntity>, new()
	where TDeleteCommand : IEntityDeleteCommand, new()
	where TUpdateCommand : IEntityUpdateCommand<TUpdateModel, TEntity>, new()
	where TGetByIdQuery : IEntityGetByIdQuery<TEntity>, new()
	where TListQuery : IEntityListQuery<TEntity>, new()
{
	protected EntityControllerWithCreateAndUpdate(ILogger<EntityControllerWithCreateAndUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateRequest, TUpdateRequest, TCreateCommand, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery>> logger, IMapper mapper, IMediator mediator) : base(logger, mapper, mediator)
	{
	}

	/// <inheritdoc />
	[ProducesResponseType(StatusCodes.Status201Created)]
	[SwaggerResponseHeader(StatusCodes.Status201Created, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async ValueTask<TResponse> Create([FromBody] TCreateRequest request, CancellationToken cancellationToken = default)
	{
		var model = Mapper.Map<TCreateModel>(request);
		var cmd = new TCreateCommand { CreateModel = model };

		var entity = await Mediator.Send(cmd, cancellationToken);

		Response.StatusCode = StatusCodes.Status201Created;
		return Mapper.Map<TResponse>(entity);
	}
}

/// <summary>
/// Entity controller with Create or Update.
/// </summary>
public abstract class EntityControllerWithCreateOrUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateOrUpdateRequest, TCreateCommand, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery> : EntityController<TEntity, TResponse, TDeleteCommand, TGetByIdQuery, TListQuery>, IResourceOperationsWithCreateOrUpdate<TResponse, TCreateOrUpdateRequest>
	where TEntity : Entity
	where TCreateModel : class, new()
	where TUpdateModel : class, new()
	where TResponse : class, IResourceResponse
	where TCreateOrUpdateRequest : class, IResourceCreateOrUpdateRequest
	where TCreateCommand : IEntityCreateCommand<TCreateModel, TEntity>, new()
	where TDeleteCommand : IEntityDeleteCommand, new()
	where TUpdateCommand : IEntityUpdateCommand<TUpdateModel, TEntity>, new()
	where TGetByIdQuery : IEntityGetByIdQuery<TEntity>, new()
	where TListQuery : IEntityListQuery<TEntity>, new()
{
	protected EntityControllerWithCreateOrUpdate(ILogger<EntityControllerWithCreateOrUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateOrUpdateRequest, TCreateCommand, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery>> logger, IMapper mapper, IMediator mediator) : base(logger, mapper, mediator)
	{
	}


	/// <inheritdoc />
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	[SwaggerResponseHeader(StatusCodes.Status201Created, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async ValueTask<TResponse> CreateOrUpdate([FromBody] TCreateOrUpdateRequest request, [FromRoute] string? id = default, [FromHeader(Name = "If-Match")] string? etag = default, CancellationToken cancellationToken = default)
	{
		TEntity? entity;

		try
		{
			var updateModel = Mapper.Map<TUpdateModel>(request);
			var cmd = new TUpdateCommand { Id = id, ETag = etag, UpdateModel = updateModel };

			entity = await Mediator.Send(cmd, cancellationToken);
		}
		catch (EntityNotFoundException)
		{
			var createModel = Mapper.Map<TCreateModel>(request);
			var cmd = new TCreateCommand { Id = id, CreateModel = createModel };

			entity = await Mediator.Send(cmd, cancellationToken);

			Response.StatusCode = StatusCodes.Status201Created;
		}

		return Mapper.Map<TResponse>(entity);
	}
}

/// <summary>
/// Entity controller with only Update.
/// </summary>
public abstract class EntityControllerWithUpdateOnly<TEntity, TUpdateModel, TResponse, TUpdateRequest, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery> : EntityController<TEntity, TResponse, TDeleteCommand, TGetByIdQuery, TListQuery>, IResourceOperationsWithUpdateOnly<TResponse, TUpdateRequest>
	where TEntity : Entity
	where TUpdateModel : class, new()
	where TResponse : class, IResourceResponse
	where TUpdateRequest : class, IResourceUpdateRequest
	where TDeleteCommand : IEntityDeleteCommand, new()
	where TUpdateCommand : IEntityUpdateCommand<TUpdateModel, TEntity>, new()
	where TGetByIdQuery : IEntityGetByIdQuery<TEntity>, new()
	where TListQuery : IEntityListQuery<TEntity>, new()
{
	protected EntityControllerWithUpdateOnly(ILogger<EntityControllerWithUpdateOnly<TEntity, TUpdateModel, TResponse, TUpdateRequest, TDeleteCommand, TUpdateCommand, TGetByIdQuery, TListQuery>> logger, IMapper mapper, IMediator mediator) : base(logger, mapper, mediator)
	{
	}

	/// <inheritdoc />
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async ValueTask<TResponse> Update([FromBody] TUpdateRequest request, [FromRoute] string id, [FromHeader(Name = "If-Match")] string etag, CancellationToken cancellationToken = default)
	{
		var updateModel = Mapper.Map<TUpdateModel>(request);
		var cmd = new TUpdateCommand { Id = id, ETag = etag, UpdateModel = updateModel };

		var entity = await Mediator.Send(cmd, cancellationToken);

		return Mapper.Map<TResponse>(entity);
	}
}
