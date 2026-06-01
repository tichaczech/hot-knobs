using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using thc.HotKnobs.Model;
using thc.HotKnobs.Model.Entities;
using thc.HotKnobs.UseCases;

using Swashbuckle.AspNetCore.Filters;
using thc.HotKnobs.Contracts;
using Microsoft.OpenApi;

namespace thc.HotKnobs.Runtime.Server.Controllers;

/// <summary>
/// Entity controller with base operations.
/// </summary>
public abstract class EntityController<TEntity, TCreateModel, TUpdateModel, TResponse, TUseCases> : ControllerBase, IResourceOperation<TResponse>
	where TEntity : Entity
	where TCreateModel : class, IEntityCreateModel
	where TUpdateModel : class, IEntityUpdateModel
	where TResponse : class, IResourceResponse
	where TUseCases : IEntityUseCases<TEntity, TCreateModel, TUpdateModel>
{
	/// <summary>
	/// AutoMapper.
	/// </summary>
	protected readonly IMapper Mapper;

	/// <summary>
	/// Entity service.
	/// </summary>
	protected readonly TUseCases UseCases;

	protected EntityController(ILogger<EntityController<TEntity, TCreateModel, TUpdateModel, TResponse, TUseCases>> logger, IMapper mapper, TUseCases useCases)
	{
		Mapper = mapper;
		UseCases = useCases;
	}

	/// <inheritdoc />
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[SwaggerResponseHeader(StatusCodes.Status204NoContent, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		Response.StatusCode = StatusCodes.Status204NoContent;
		return UseCases.DeleteAsync(id, cancellationToken);
	}

	/// <inheritdoc />
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async Task<TResponse> GetAsync(string id, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		var entity = await UseCases.GetAsync(id, onlyActive, cancellationToken);

		return Mapper.Map<TResponse>(entity);
	}

	/// <inheritdoc />
	public virtual IAsyncEnumerable<string> ListAsync([FromHeader(Name = "If-Modified-Since")] DateTimeOffset? modifiedSince = default, [FromQuery] bool onlyActive = true, CancellationToken cancellationToken = default)
	{
		return UseCases.ListAsync(modifiedSince, onlyActive, cancellationToken);
	}
}

/// <summary>
/// Entity controller with both Create and Update.
/// </summary>
public abstract class EntityControllerWithCreateAndUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateRequest, TUpdateRequest, TUseCases> : EntityControllerWithUpdateOnly<TEntity, TCreateModel, TUpdateModel, TResponse, TUpdateRequest, TUseCases>, IResourceOperationsWithCreateAndUpdate<TResponse, TCreateRequest, TUpdateRequest>
	where TEntity : Entity
	where TCreateModel : class, IEntityCreateModel
	where TUpdateModel : class, IEntityUpdateModel
	where TResponse : class, IResourceResponse
	where TCreateRequest : class, IResourceCreateRequest
	where TUpdateRequest : class, IResourceUpdateRequest
	where TUseCases : IEntityUseCases<TEntity, TCreateModel, TUpdateModel>
{
	protected EntityControllerWithCreateAndUpdate(ILogger<EntityControllerWithCreateAndUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateRequest, TUpdateRequest, TUseCases>> logger, IMapper mapper, TUseCases useCases) : base(logger, mapper, useCases)
	{
	}

	/// <inheritdoc />
	[ProducesResponseType(StatusCodes.Status201Created)]
	[SwaggerResponseHeader(StatusCodes.Status201Created, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async Task<TResponse> CreateAsync(TCreateRequest request, CancellationToken cancellationToken = default)
	{
		var model = Mapper.Map<TCreateModel>(request);
		var entity = await UseCases.CreateAsync(default, model, cancellationToken);

		Response.StatusCode = StatusCodes.Status201Created;
		return Mapper.Map<TResponse>(entity);
	}
}

/// <summary>
/// Entity controller with Create or Update.
/// </summary>
public abstract class EntityControllerWithCreateOrUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateOrUpdateRequest, TUseCases> : EntityController<TEntity, TCreateModel, TUpdateModel, TResponse, TUseCases>, IResourceOperationsWithCreateOrUpdate<TResponse, TCreateOrUpdateRequest>
	where TEntity : Entity
	where TCreateModel : class, IEntityCreateModel
	where TUpdateModel : class, IEntityUpdateModel
	where TResponse : class, IResourceResponse
	where TCreateOrUpdateRequest : class, IResourceCreateOrUpdateRequest
	where TUseCases : IEntityUseCases<TEntity, TCreateModel, TUpdateModel>
{
	protected EntityControllerWithCreateOrUpdate(ILogger<EntityControllerWithCreateOrUpdate<TEntity, TCreateModel, TUpdateModel, TResponse, TCreateOrUpdateRequest, TUseCases>> logger, IMapper mapper, TUseCases useCases) : base(logger, mapper, useCases)
	{
	}

	/// <inheritdoc />
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	[SwaggerResponseHeader(StatusCodes.Status201Created, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async Task<TResponse> CreateOrUpdateAsync(string id, TCreateOrUpdateRequest request, CancellationToken cancellationToken = default)
	{
		TEntity? entity;

		try
		{
			entity = await UseCases.GetAsync(id, cancellationToken: cancellationToken);

			var model = Mapper.Map<TUpdateModel>(request);
			entity = await UseCases.UpdateAsync(id, model, cancellationToken);
		}
		catch (EntityNotFoundException)
		{
			var model = Mapper.Map<TCreateModel>(request);
			entity = await UseCases.CreateAsync(id, model, cancellationToken);

			Response.StatusCode = StatusCodes.Status201Created;
		}

		return Mapper.Map<TResponse>(entity);
	}
}

/// <summary>
/// Entity controller with only Update.
/// </summary>
public abstract class EntityControllerWithUpdateOnly<TEntity, TCreateModel, TUpdateModel, TResponse, TUpdateRequest, TUseCases> : EntityController<TEntity, TCreateModel, TUpdateModel, TResponse, TUseCases>, IResourceOperationsWithUpdateOnly<TResponse, TUpdateRequest>
	where TEntity : Entity
	where TCreateModel : class, IEntityCreateModel
	where TUpdateModel : class, IEntityUpdateModel
	where TResponse : class, IResourceResponse
	where TUpdateRequest : class, IResourceUpdateRequest
	where TUseCases : IEntityUseCases<TEntity, TCreateModel, TUpdateModel>
{
	protected EntityControllerWithUpdateOnly(ILogger<EntityControllerWithUpdateOnly<TEntity, TCreateModel, TUpdateModel, TResponse, TUpdateRequest, TUseCases>> logger, IMapper mapper, TUseCases useCases) : base(logger, mapper, useCases)
	{
	}

	/// <inheritdoc />
	[SwaggerResponseHeader(StatusCodes.Status200OK, "ETag", JsonSchemaType.String, "ETag of the resource")]
	public virtual async Task<TResponse> UpdateAsync(string id, TUpdateRequest request, CancellationToken cancellationToken = default)
	{
		var model = Mapper.Map<TUpdateModel>(request);
		var entity = await UseCases.UpdateAsync(id, model, cancellationToken);

		return Mapper.Map<TResponse>(entity);
	}
}
