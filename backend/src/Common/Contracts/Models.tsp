import "@typespec/http";
import "@typespec/rest";

using TypeSpec.Http;
using TypeSpec.Rest;

// https://github.com/microsoft/typespec/issues/2964

// TODO: Add X-Sync-Checkpoint and ?syncCheckpoint parameters for list operations.

// TODO: Add Link header parsing for pagination links in list responses, e.g.
// Link: <https://api.example.com/devices?after=abc123&pageSize=100>; rel="next",
// 	<https://api.example.com/devices?before=xyz987&pageSize=100>; rel="prev"

namespace HotKnobs.Contracts;

// Responses & Errors

/**
 * Create or update operation errors.
 */
alias CreateOrUpdateRequestError = GeneralError | BadRequestResponse | ConflictResponse | PreconditionFailedResponse;

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
 * The request was rejected because one or more preconditions (e.g. If-Match) were not satisfied.
 */
model PreconditionFailedResponse is Response<412>;

/**
 * Read operation errors.
 */
alias ReadRequestError = GeneralError | NotFoundResponse | ConflictResponse;

/**
 * Update operation errors.
 */
alias UpdateRequestError = | GeneralError | BadRequestResponse | NotFoundResponse | ConflictResponse | PreconditionFailedResponse;

// Headers & Query Parameters values


// alias contentType = "application/json";

/**
 * Prefer request header value for minimal response, i.e. the server will return an empty body with only status code and headers.
 */
alias preferHeaderInstanceValueMinimal = "return=minimal";

/**
 * Prefer request header value for full resource representation response, i.e. the server will return the full resource in the response body.
 */
alias preferHeaderInstanceValueRepresentation = "return=representation";

/**
 * Prefer request header value for summary response, i.e. the server will return a list of resource summary properties (id, name, description, isActive) in the response body.
 */
alias preferHeaderCollectionValueSummary = "return=summary";

/**
 * Prefer request header values for instance responses (representation or minimal).
 */
alias preferHeaderInstanceValues = preferHeaderInstanceValueRepresentation | preferHeaderInstanceValueMinimal;

/**
 * Prefer request header values for collection responses (representation, minimal, or summary).
 */
alias preferHeaderCollectionValues = preferHeaderInstanceValueMinimal | preferHeaderCollectionValueSummary;

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
 * Represents (paging) cursor token for collection operations.
 *
 * Its value and format are opaque to clients, but it should be treated as an opaque string that clients can store and send back to the server to retrieve the next page of results. The server is responsible for generating and interpreting the token correctly.
 */
scalar CursorToken extends string; // TODO: Define the syntax and semantics for cursor values, e.g. it could be an opaque string that encodes the position in the collection, or it could be based on resource properties like "createdDate>2021-01-01T00:00:00Z".

/**
 * Represents (changelog) checkpoint token for incremental synchronization.
 *
 * Its value and format are opaque to clients, but it should be treated as an opaque string that clients can store and send back to the server to retrieve changes since that checkpoint. The server is responsible for generating and interpreting the token correctly.
 */
scalar CheckpointToken extends string;// TODO: Define the syntax and semantics for cursor values, e.g. it could be an opaque string that encodes the position in the collection, or it could be based on resource properties like "createdDate>2021-01-01T00:00:00Z".

/**
 * Represents concurrency control token.
 */
// @format("uuid")
scalar ETag extends string;

/**
 * ETag response header carrying a concurrency control token.
 */
model ETagResponseHeader {
	/**
	 * Concurrency control token for the created resource.
	 */
	@header("ETag")
	etag: ETag;
}

/**
 * Represents filter expression for collection operations.
 */
scalar FilterExpression extends string; // TODO: Define the syntax and semantics for filter parameter values, e.g. OData filter expressions or a custom syntax.

/**
 * Optional If-Match request header for optimistic concurrency.
 */
model IfMatchOptionalHeader {
	/**
	 * If mismatches the current resource value, the server will return a 412 (Precondition Failed) response.
	 */
	@header("If-Match")
	ifMatch?: ETag;
}

/**
 * Required If-Match request header for optimistic concurrency on write/delete operations.
 */
model IfMatchRequiredHeader {
	/**
	 * If mismatches the current resource value, the server will return a 412 (Precondition Failed) response.
	 */
	@header("If-Match")
	@visibility(Lifecycle.Delete, Lifecycle.Update)
	ifMatch: ETag;
}

/**
 * If-None-Match request header for conditional reads.
 */
model IfNoneMatchHeader {
	/**
	 * If set and matches the current resource value, the server will return a 304 (Not Modified) response.
	 */
	@header("If-None-Match")
	@visibility(Lifecycle.Query)
	ifNoneMatch?: ETag;
}

/**
 * Represents an order by expression for collection operations.
 */
scalar OrderByExpression extends string; // TODO: Define the syntax and semantics for order by parameter values, e.g. "name asc,createdDate desc" or "name,-createdDate", etc.

/**
 * Prefer request header for collection responses (representation, minimal, or summary).
 */
model PreferCollectionRequestHeader {
	/**
	 * If set, the server will prefer to return the specified properties in the response.
	 */
	@header("Prefer")
	@visibility(Lifecycle.Query)
	prefer?: preferHeaderCollectionValues = preferHeaderCollectionValueSummary;
}

/**
 * Preference-Applied response header for collection operations.
 */
model PreferenceAppliedCollectionResponseHeader {
	/**
	 * If set, the server confirms that the specified preference was applied in processing the request.
	 */
	@header("Preference-Applied")
	preferenceApplied?: preferHeaderCollectionValues;
}

/**
 * Preference-Applied response header for instance operations.
 */
model PreferenceAppliedInstanceResponseHeader {
	/**
	 * If set, the server confirms that the specified preference was applied in processing the request.
	 */
	@header("Preference-Applied")
	preferenceApplied?: preferHeaderInstanceValues;
}

/**
 * Prefer request header for instance responses (representation or minimal).
 */
model PreferInstanceRequestHeader {
	/**
	 * If set, the server will prefer to return the specified properties in the response.
	 */
	@header("Prefer")
	prefer?: preferHeaderInstanceValues = preferHeaderInstanceValueRepresentation;
}

/**
 * Represents resource id.
 */
// @format("uuid")
scalar ResourceId extends string;

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
	 * The preferred language for the response, e.g. "en-US". If not specified, the server will use the default language.
	 */
	@header("Accept-Language")
	acceptLanguage?: string;

	/**
	 * If set to `true`, only active resources are returned.
	 */
	@query()
	@visibility(Lifecycle.Query)
	onlyActive?: boolean = true;
}

/**
 * Represents operation parameters for the resource collection of type `TResource`.
 *
 * @template TResource The resource model.
 */
model ResourceCollectionParameters<TResource extends Resource> extends ResourceParameters<TResource> {
	...PreferCollectionRequestHeader;

	/**
	 * Filter expression for the collection.
	 */
	@query()
	@visibility(Lifecycle.Query)
	filter?: FilterExpression;

	/**
	 * If set, only resources from the specified cursor position are returned.
	 */
	@query()
	@visibility(Lifecycle.Query)
	fromCursor?: CursorToken;

	/**
	 * Maximum number of items to return in the collection.
	 */
	@query()
	@visibility(Lifecycle.Query)
	limit?: uint16;

	/**
	 * Order by expression for the collection.
	 */
	@query()
	@visibility(Lifecycle.Query)
	orderBy?: OrderByExpression;

	/**
	 * Search string. If set, collection items whose properties match the search string are returned. The syntax and semantics of the search string are defined by the server, e.g. it could be a simple keyword search that matches against multiple properties, or a more complex expression with property-specific searches, logical operators, etc.
	 */
	@query()
	@visibility(Lifecycle.Query)
	search?: string;

	/**
	 * If set, only resources created or updated since the specified checkpoint are returned.
	 */
	@query()
	@visibility(Lifecycle.Query)
	sinceCheckpoint?: CheckpointToken;
}

/**
 * Represents operation parameters for the resource instance of type `TResource`.
 *
 * @template TResource The resource model.
 */
@copyResourceKeyParameters
model ResourceInstanceParameters<TResource extends Resource> extends ResourceParameters<TResource> {
	...IfMatchRequiredHeader;
	...IfNoneMatchHeader;

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
model ResourceCreateRequestModel<TResource extends Resource> is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>>
{
	...PreferInstanceRequestHeader;

	/**
	 * The client content type for the request body, e.g. "application/json".
	 */
	@header("Accept")
	accept: string;
}

/**
 * Resource create or update operation model.
 *
 * @template TResource The resource model to create or update.
 */
@friendlyName("{name}CreateOrUpdateRequest", TResource)
@withVisibility(Lifecycle.Create, Lifecycle.Update)
model ResourceCreateOrUpdateRequestModel<TResource extends Resource> is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>> {
	...IfMatchOptionalHeader;
	...PreferInstanceRequestHeader;
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
	...ETagResponseHeader;
	...PreferenceAppliedInstanceResponseHeader;

	/**
	 * The created resource.
	 */
	@bodyRoot
	body?: TResource | { }; // TODO: Find the correct way to model optional body with full resource representation when prefer=representation, and empty body when prefer=minimal.

	// /**
	//  * The content type of the request body, e.g. "application/json".
	//  *
	//  * Note: This is optional because in case of prefer=minimal, the body will be empty and content type header may not be included. We need to find a better way to model this scenario.
	//  */
	// @header("Content-Type")
	// contentType: string;

	/**
	 * The URL of the created resource.
	 */
	@header("Location")
	location?: string;
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
 * @template TSummary The summary model in the list.
 */
@friendlyName("{name}ListResponse", TResource)
@withVisibility(Lifecycle.Read)
model ResourceListResponseModel<TResource extends Resource, TSummary extends Summary> {
	...OkResponse;
	...PreferenceAppliedCollectionResponseHeader;

	// /**
	//  * The content type of the request body, e.g. "application/json".
	//  */
	// @header("Content-Type")
	// contentType: string;

	/**
	 * The changelog checkpoint for the current state of the resource collection. This can be used by clients to perform incremental synchronization in subsequent requests.
	 */
	@header("X-Changelog-Checkpoint")
	checkpoint: CheckpointToken;

	/**
	 * The paging cursor for retrieving the next page of results. If not set, there are no more pages available.
	 */
	@header("X-Paging-Cursor")
	cursor?: CursorToken;

	/**
	 * The maximum number of items returned in the current response. This can be used by clients to adjust the page size in subsequent requests if needed.
	 */
	@header("X-Paging-Limit")
	limit?: uint16;

	/**
	 * The Link header for pagination links, e.g. next and prev links with rel attributes.
	 */
	@header("Link")
	link?: string;

	/**
	 * The list of resource summaries.
	 *
	 * Alternatively, this could be a list of resource ids, depending on the specified preference.
	 */
	@bodyRoot()
	body: ResourceId[] | TSummary[];
}

/**
 * Resource update operation completed successfully.
 *
 * @template TResource The resource model that was updated.
 */
@friendlyName("{name}ReadResponse", TResource)
@withVisibility(Lifecycle.Read)
model ResourceReadResponseModel<TResource extends Resource> {
	...ETagResponseHeader;
	...OkResponse;

	/**
	 * The resource.
	 */
	@bodyRoot()
	body: TResource;

	// /**
	//  * The content type of the request body, e.g. "application/json".
	//  *
	//  * Note: This is optional because in case of prefer=minimal, the body will be empty and content type header may not be included. We need to find a better way to model this scenario.
	//  */
	// @header("Content-Type")
	// contentType?: string;
}

/**
 * Resource update operation model.
 *
 * @template TResource The resource model to update.
 */
@friendlyName("{name}UpdateRequest", TResource)
@withVisibility(Lifecycle.Update)
model ResourceUpdateModel<TResource extends Resource> is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>> {
	...PreferInstanceRequestHeader;
}

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
	body?: TResource | { }; // TODO: Find the correct way to model optional body with full resource representation when prefer=representation, and empty body when prefer=minimal.

	// /**
	//  * The content type of the request body, e.g. "application/json".
	//  *
	//  * Note: This is optional because in case of prefer=minimal, the body will be empty and content type header may not be included. We need to find a better way to model this scenario.
	//  */
	// @header("Content-Type")
	// contentType?: string;

	...ETagResponseHeader;
	...PreferenceAppliedInstanceResponseHeader;
}

// --- Singleton Resource Models ---

/**
 * Singleton resource operation model.
 */
model SingletonResource { }

/**
 * Represents operation parameters for the singleton resource of type `TResource`.
 *
 * @template TResource The resource model.
 */
model SingletonResourceParameters<TResource extends SingletonResource> {
	...IfNoneMatchHeader;
}

/**
 * Represents operation parameters for the singleton resource of type `TResource`.
 *
 * @template TResource The resource model.
 */
model SingletonResourceInstanceParameters<TResource extends SingletonResource>
	extends SingletonResourceParameters<TResource> {
	...IfMatchRequiredHeader;
}

/**
 * Singleton resource create or update operation model.
 *
 * @template TResource The resource model to create or update.
 */
@friendlyName("{name}CreateOrUpdateRequest", TResource)
@withVisibility(Lifecycle.Create, Lifecycle.Update)
model SingletonResourceCreateOrUpdateRequestModel<TResource extends SingletonResource> is UpdateableProperties<DefaultKeyVisibility<TResource, Lifecycle.Read>> {
	...IfMatchOptionalHeader;
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
	@bodyRoot()
	body: TResource;

	...ETagResponseHeader;
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

	...ETagResponseHeader;
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

	...ETagResponseHeader;
}

model Summary {
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
