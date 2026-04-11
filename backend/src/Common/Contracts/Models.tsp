import "@typespec/http";
import "@typespec/rest";

using TypeSpec.Http;
using TypeSpec.Rest;

// https://github.com/microsoft/typespec/issues/2964

namespace HotKnobs.Contracts;

// Responses & Errors

/**
 * Create or update operation errors.
 */
alias CreateOrUpdateRequestError = GeneralError | BadRequestResponse | ConflictResponse; // TODO: ??? | PreconditionFailedResponse;

/**
 * Create operation errors.
 */
alias CreateRequestError = GeneralError | BadRequestResponse | ConflictResponse;

/**
 * Delete operation errors.
 */
alias DeleteRequestError = GeneralError | NotFoundResponse | PreconditionFailedResponse;

/**
 * General errors.
 */
alias GeneralError = UnauthorizedResponse | ForbiddenResponse | InternalServerErrorResponse;

/**
 * The server failed to process the request due to an unforeseen condition.
 */
model InternalServerErrorResponse is Response<500>;

/**
 * List operation errors.
 */
alias ListRequestError = GeneralError;

/**
 * The access to the target resource has been denied.
 */
model PreconditionFailedResponse is Response<412>;

/**
 * Read operation errors.
 */
alias ReadRequestError = GeneralError | NotFoundResponse | ConflictResponse;

/**
 * Update operation errors.
 */
alias UpdateRequestError =
	| GeneralError
	| BadRequestResponse
	| NotFoundResponse
	| ConflictResponse
	| PreconditionFailedResponse;

// --- General Models ---

/**
 * Represents the default authentication mode, which is OAuth2.
 *
 * @template TScopes The scopes required for the operation.
 */
model Auth<TScopes extends string[]>
	is OAuth2Auth<
		[
			{
				type: OAuth2FlowType.implicit;
				authorizationUrl: "https://api.example.com/oauth2/authorize";
				refreshUrl: "https://api.example.com/oauth2/refresh";
			}
		],
		TScopes
	>;

/**
 * Represents resource id.
 */
// @format("uuid")
scalar ResourceId extends string;

/**
 * Represents concurrency control token.
 */
// @format("uuid")
scalar ETag extends string;

/**
 * Resource operation model.
 */
model Resource {
	/**
	 * Resource id.
	 */
	@key()
	@visibility(Lifecycle.Read)
	id: ResourceId;

	/**
	 * Soft delete flag.
	 */
	@visibility(Lifecycle.Read)
	isActive: boolean;
}

/**
 * Represents operation parameters for the resource of type `TResource`.
 *
 * @template TResource The resource model.
 */
model ResourceParameters<TResource extends Resource> {
	/**
	 * If set and matches the current resource value, the server will return a 304 (Not Modified) response.
	 */
	@visibility(Lifecycle.Query)
	@header("If-None-Match")
	ifNoneMatch?: ETag;

	/**
	 * If set to `true`, only active resources are returned.
	 */
	@visibility(Lifecycle.Query)
	@query()
	onlyActive?: boolean = true;
}

/**
 * Represents operation parameters for the resource collection of type `TResource`.
 *
 * @template TResource The resource model.
 */
model ResourceCollectionParameters<TResource extends Resource>
	extends ResourceParameters<TResource> {}


/**
 * Profile collection parameters with Search.
 *
 * @template TResource The resource model.
 */
model ResourceCollectionParametersWithSearch<TResource extends Resource>
	extends ResourceCollectionParameters<TResource> {
	/**
	 * Search string.
	 */
	@query()
	@visibility(Lifecycle.Query)
	search?: string;
}

/**
 * Represents operation parameters for the resource instance of type `TResource`.
 *
 * @template TResource The resource model.
 */
@copyResourceKeyParameters
model ResourceInstanceParameters<TResource extends Resource>
	extends ResourceParameters<TResource> {
	/**
	 * If mismatches the current resource value, the server will return a 412 (Precondition Failed) response.
	 */
	@visibility(Lifecycle.Delete, Lifecycle.Update)
	@header("If-Match")
	ifMatch: ETag;

	/**
	 * Resource id.
	 */
	@key()
	@query()
	id: ResourceId;
}

/**
 * Resource create operation model.
 *
 * @template TResource The resource model to create.
 */
@friendlyName("{name}CreateRequest", TResource)
@withVisibility(Lifecycle.Create)
model ResourceCreateRequestModel<TResource extends Resource>
	is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>>;

/**
 * Resource create or update operation model.
 *
 * @template TResource The resource model to create or update.
 */
@friendlyName("{name}CreateOrUpdateRequest", TResource)
@withVisibility(Lifecycle.Create, Lifecycle.Update)
model ResourceCreateOrUpdateRequestModel<TResource extends Resource>
	is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>>
{
	/**
	 * If mismatches the current resource value, the server will return a 412 (Precondition Failed) response.
	 */
	@header("If-Match")
	ifMatch?: ETag;
}

/**
 * Resource create operation completed successfully.
 *
 * @template TResource The resource model that was created.
 */
@friendlyName("{name}CreatedResponse", TResource)
@withVisibility(Lifecycle.Read)
model ResourceCreatedResponseModel<TResource extends Resource> {
	...CreatedResponse;

	/**
	 * The created resource.
	 */
	@bodyRoot
	body: TResource;

	/**
	 * Concurrency control token for the created resource.
	 */
	@header("ETag")
	etag: ETag;
}

/**
 * Resource delete operation completed successfully.
 *
 * @template TResource The resource model to delete.
 */
model ResourceDeletedResponseModel<TResource extends Resource> {
	...NoContentResponse;
}

/**
 * Resource list operation completed successfully.
 *
 * @template TResource The resource model in the list.
 */
@friendlyName("{name}ListResponse", TResource)
@withVisibility(Lifecycle.Read)
model ResourceListResponseModel<TResource extends Resource> {
	...OkResponse;

	/**
	 * The list of resource ids.
	 */
	@bodyRoot()
	body: ResourceId[];
}

/**
 * Resource update operation completed successfully.
 *
 * @template TResource The resource model that was updated.
 */
@friendlyName("{name}ReadResponse", TResource)
@withVisibility(Lifecycle.Read)
model ResourceReadResponseModel<TResource extends Resource> {
	...OkResponse;

	/**
	 * The resource.
	 */
	@bodyRoot()
	body: TResource;

	/**
	 * Concurrency control token for the resource.
	 */
	@header("ETag")
	etag: ETag;
}

/**
 * Resource update operation model.
 *
 * @template TResource The resource model to update.
 */
@friendlyName("{name}UpdateRequest", TResource)
@withVisibility(Lifecycle.Update)
model ResourceUpdateModel<TResource extends Resource>
	is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>>;

/**
 * Resource update operation completed successfully.
 *
 * @template TResource The resource model that was updated.
 */
@friendlyName("{name}UpdatedResponse", TResource)
@withVisibility(Lifecycle.Read)
model ResourceUpdatedResponseModel<TResource extends Resource> {
	...OkResponse;

	/**
	 * The updated resource.
	 */
	@bodyRoot()
	body: TResource;

	/**
	 * Concurrency control token for the updated resource.
	 */
	@header("ETag")
	etag: ETag;
}

// --- Singleton Resource Models ---

/**
 * Singleton resource operation model.
 */
model SingletonResource {}

/**
 * Represents operation parameters for the singleton resource of type `TResource`.
 *
 * @template TResource The resource model.
 */
model SingletonResourceParameters<TResource extends SingletonResource> {
	/**
	 * If set and matches the current resource value, the server will return a 304 (Not Modified) response.
	 */
	@visibility(Lifecycle.Query)
	@header("If-None-Match")
	ifNoneMatch?: ETag;
}

/**
 * Represents operation parameters for the singleton resource of type `TResource`.
 *
 * @template TResource The resource model.
 */
model SingletonResourceInstanceParameters<TResource extends SingletonResource>
	extends SingletonResourceParameters<TResource> {
	/**
	 * If mismatches the current resource value, the server will return a 412 (Precondition Failed) response.
	 */
	@visibility(Lifecycle.Delete, Lifecycle.Update)
	@header("If-Match")
	ifMatch: ETag;
}

/**
 * Singleton resource create or update operation model.
 *
 * @template TResource The resource model to create or update.
 */
@friendlyName("{name}CreateOrUpdateRequest", TResource)
@withVisibility(Lifecycle.Create, Lifecycle.Update)
model SingletonResourceCreateOrUpdateRequestModel<TResource extends SingletonResource>
	is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>>
{
	/**
	 * If mismatches the current resource value, the server will return a 412 (Precondition Failed) response.
	 */
	@header("If-Match")
	ifMatch?: ETag;
}

/**
 * Singleton resource create operation completed successfully.
 *
 * @template TResource The resource model that was created.
 */
@friendlyName("{name}CreatedResponse", TResource)
@withVisibility(Lifecycle.Read)
model SingletonResourceCreatedResponseModel<TResource extends SingletonResource> {
	...CreatedResponse;

	/**
	 * The created resource.
	 */
	@bodyRoot
	body: TResource;

	/**
	 * Concurrency control token for the created resource.
	 */
	@header("ETag")
	etag: ETag;
}

/**
 * Singleton resource update operation completed successfully.
 *
 * @template TResource The resource model that was updated.
 */
@friendlyName("{name}ReadResponse", TResource)
@withVisibility(Lifecycle.Read)
model SingletonResourceReadResponseModel<TResource extends SingletonResource> {
	...OkResponse;

	/**
	 * The resource.
	 */
	@bodyRoot()
	body: TResource;

	/**
	 * Concurrency control token for the resource.
	 */
	@header("ETag")
	etag: ETag;
}

/**
 * Singleton resource update operation completed successfully.
 *
 * @template TResource The resource model that was updated.
 */
@friendlyName("{name}UpdatedResponse", TResource)
@withVisibility(Lifecycle.Read)
model SingletonResourceUpdatedResponseModel<TResource extends SingletonResource> {
	...OkResponse;

	/**
	 * The updated resource.
	 */
	@bodyRoot()
	body: TResource;

	/**
	 * Concurrency control token for the updated resource.
	 */
	@header("ETag")
	etag: ETag;
}

// --- HotKnobs Models ---

/**
 * Represents a HotKnobs entity.
 */
model Entity extends Resource {
	/**
	 * The name of the entity.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create, Lifecycle.Update)
	name: string;

	/**
	 * The description of the entity.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create, Lifecycle.Update)
	description?: string;
}
