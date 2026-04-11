import "@typespec/http";
import "@typespec/rest";
import "./Models.tsp";

using TypeSpec.Http;
using TypeSpec.Rest;

// https://github.com/microsoft/typespec/issues/2964

namespace HotKnobs.Contracts;

// interface X extends TypeSpec.Rest.Resource.ExtensionResourceOperations<{}, {}, {}> {}

// -- Resource Operation Templates --

/**
 * Resource create operation template for a resource.
 *
 * @template TResource The resource model to create.
 * @template TScope The scope required for the operation.
 * @template TError The error model.
 */
@Rest.Private.validateHasKey(TResource)
// @Rest.Private.validateIsError(TError)
interface ResourceCreate<
	TResource extends Resource,
	TScopes extends string[],
	TError = CreateRequestError
> {
	/**
	 * Creates a new instance of the resource.
	 *
	 * @template TResource The resource model to create.
	 */
	@autoRoute
	@createsResource(TResource)
	@post
	@useAuth(Auth<TScopes>)
	create(
		@bodyRoot createRequest: ResourceCreateRequestModel<TResource>,
	): ResourceCreatedResponseModel<TResource> | TError;
}

/**
 * Resource create or update operation template.
 *
 * @template TResource The resource model to create or update.
 * @template TScope The scope required for the operation.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
@Rest.Private.validateHasKey(TResourceInstanceParameters)
// @Rest.Private.validateIsError(TError)
interface ResourceCreateOrUpdate<
	TResource extends Resource,
	TScopes extends string[],
	TResourceInstanceParameters extends ResourceInstanceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TError = CreateOrUpdateRequestError
> {
	/**
	 * Creates a new or updates an existing instance of the resource.
	 *
	 * @template TResource The resource model to create or update.
	 */
	@autoRoute
	@createsOrUpdatesResource(TResource)
	@put
	@useAuth(Auth<TScopes>)
	createOrUpdate(
		...TResourceInstanceParameters,
		@bodyRoot updateRequest: ResourceCreateOrUpdateRequestModel<TResource>,
	): ResourceCreatedResponseModel<TResource> | ResourceUpdatedResponseModel<TResource> | TError;
}

/**
 * Resource delete operation template.
 *
 * @template TResource The resource model to delete.
 * @template TScope The scope required for the operation.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
@Rest.Private.validateHasKey(TResourceInstanceParameters)
// @Rest.Private.validateIsError(TError)
interface ResourceDelete<
	TResource extends Resource,
	TScopes extends string[],
	TResourceInstanceParameters extends ResourceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TError = DeleteRequestError
> {
	/**
	 * Deletes an existing instance of the resource.
	 *
	 * @template TResource The resource model to delete.
	 */
	@autoRoute
	@deletesResource(TResource)
	@useAuth(Auth<TScopes>)
	delete(
		...TResourceInstanceParameters,
	): ResourceDeletedResponseModel<TResource> | TError;
}

/**
 * Represents the resource GET operation.
 *
 * @template TResource The resource model.
 * @template TScope The scope required for the operation.
 * @template TResourceCollectionParameters The resource collection parameters.
 * @template TError The error model.

 */
// @Rest.Private.validateIsError(TError)
interface ResourceList<
	TResource extends Resource,
	TScopes extends string[],
	TResourceCollectionParameters extends ResourceParameters<TResource> = ResourceCollectionParameters<TResource>,
	TError = ListRequestError
> {
	/**
	 * Gets a list of instances of the resource.
	 *
	 * @template TResource The resource model.
	 */
	@autoRoute
	@listsResource(Resource)
	@useAuth(Auth<TScopes>)
	list(
		...TResourceCollectionParameters,
	): ResourceListResponseModel<TResource> | NotModifiedResponse | TError;
}

/**
 * Represents the resource GET operation.
 *
 * @template TResource The resource model.
 * @template TScope The scope required for the operation.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
@Rest.Private.validateHasKey(TResourceInstanceParameters)
// @Rest.Private.validateIsError(TError)
interface ResourceRead<
	TResource extends Resource,
	TScopes extends string[],
	TResourceInstanceParameters extends ResourceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TError = ReadRequestError
> {
	/**
	 * Gets an instance of the resource.
	 *
	 * @template TResource The resource model.
	 */
	@autoRoute
	@readsResource(Resource)
	@useAuth(Auth<TScopes>)
	get(
		...TResourceInstanceParameters,
	): ResourceReadResponseModel<TResource> | NotModifiedResponse | TError;
}

/**
 * Resource update operation template.
 *
 * @template TResource The resource model to update.
 * @template TScope The scope required for the operation.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
@Rest.Private.validateHasKey(TResourceInstanceParameters)
// @Rest.Private.validateIsError(TError)
interface ResourceUpdate<
	TResource extends Resource,
	TScopes extends string[],
	TResourceInstanceParameters extends ResourceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TError = UpdateRequestError
> {
	/**
	 * Updates an existing instance of the resource.
	 *
	 * @template TResource The resource model to update.
	 */
	@autoRoute
	@updatesResource(TResource)
	@useAuth(Auth<TScopes>)
	@patch(#{ implicitOptionality: false })
	update(
		...TResourceInstanceParameters,
		@bodyRoot updateRequest: ResourceUpdateModel<TResource>,
	): ResourceUpdatedResponseModel<TResource> | TError;
}

/**
 * Represents the resource collection operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TResourceCollectionParameters The resource collection parameters.
 * @template TError The error model.
 */
interface ResourceCollectionOperationsCreateAndUpdate<
	TResource extends Resource,
	TReadScopes extends string[],
	TWriteScopes extends string[],
	TResourceCollectionParameters extends ResourceParameters<TResource> = ResourceCollectionParameters<TResource>,
	TCreateError = CreateRequestError,
	TListError = ListRequestError
>
	extends ResourceCreate<TResource, TWriteScopes, TCreateError>,
		ResourceList<
			TResource,
			TReadScopes,
			TResourceCollectionParameters,
			TListError
		> {}

/**
 * Represents the resource collection operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TResourceCollectionParameters The resource collection parameters.
 * @template TError The error model.
 */
interface ResourceCollectionOperationsCreateOrUpdate<
	TResource extends Resource,
	TReadScopes extends string[],
	TResourceCollectionParameters extends ResourceParameters<TResource> = ResourceCollectionParameters<TResource>,
	TListError = ListRequestError
>
	extends ResourceList<
			TResource,
			TReadScopes,
			TResourceCollectionParameters,
			TListError
		> {}

/**
 * Represents the resource instance operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
interface ResourceInstanceOperationsCreateAndUpdate<
	TResource extends Resource,
	TReadScopes extends string[],
	TWriteScopes extends string[],
	TResourceInstanceParameters extends ResourceInstanceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TDeleteError = DeleteRequestError,
	TReadError = ReadRequestError,
	TUpdateError = UpdateRequestError
>
	extends ResourceDelete<
			TResource,
			TWriteScopes,
			TResourceInstanceParameters,
			TDeleteError
		>,
		ResourceRead<
			TResource,
			TReadScopes,
			TResourceInstanceParameters,
			TReadError
		>,
		ResourceUpdate<
			TResource,
			TWriteScopes,
			TResourceInstanceParameters,
			TUpdateError
		> {}

/**
 * Represents the resource instance operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
interface ResourceInstanceOperationsCreateOrUpdate<
	TResource extends Resource,
	TReadScopes extends string[],
	TWriteScopes extends string[],
	TResourceInstanceParameters extends ResourceInstanceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TCreateOrUpdateError = CreateOrUpdateRequestError,
	TDeleteError = DeleteRequestError,
	TReadError = ReadRequestError
>
	extends ResourceDelete<
			TResource,
			TWriteScopes,
			TResourceInstanceParameters,
			TDeleteError
		>,
		ResourceRead<
			TResource,
			TReadScopes,
			TResourceInstanceParameters,
			TReadError
		>,
		ResourceCreateOrUpdate<
			TResource,
			TWriteScopes,
			TResourceInstanceParameters,
			TCreateOrUpdateError
		> {}

/**
 * Represents the resource operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TResourceCollectionParameters The resource collection parameters.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
interface ResourceOperationsWithCreateAndUpdate<
	TResource extends Resource,
	TReadScopes extends string[],
	TWriteScopes extends string[],
	TResourceCollectionParameters extends ResourceCollectionParameters<TResource> = ResourceCollectionParameters<TResource>,
	TResourceInstanceParameters extends ResourceInstanceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TCreateError = CreateRequestError,
	TDeleteError = DeleteRequestError,
	TListError = ListRequestError,
	TReadError = ReadRequestError,
	TUpdateError = UpdateRequestError
>
	extends ResourceCollectionOperationsCreateAndUpdate<
			TResource,
			TReadScopes,
			TWriteScopes,
			TResourceCollectionParameters,
			TCreateError,
			TListError
		>,
		ResourceInstanceOperationsCreateAndUpdate<
			TResource,
			TReadScopes,
			TWriteScopes,
			TResourceInstanceParameters,
			TReadError,
			TUpdateError,
			TDeleteError
		> {}

/**
 * Represents the resource operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TResourceCollectionParameters The resource collection parameters.
 * @template TResourceInstanceParameters The resource instance parameters.
 * @template TError The error model.
 */
interface ResourceOperationsWithCreateOrUpdate<
	TResource extends Resource,
	TReadScopes extends string[],
	TWriteScopes extends string[],
	TResourceCollectionParameters extends ResourceParameters<TResource> = ResourceCollectionParameters<TResource>,
	TResourceInstanceParameters extends ResourceParameters<TResource> = ResourceInstanceParameters<TResource>,
	TCreateOrUpdateError = CreateOrUpdateRequestError,
	TDeleteError = DeleteRequestError,
	TListError = ListRequestError,
	TReadError = ReadRequestError
>
	extends ResourceInstanceOperationsCreateOrUpdate<
			TResource,
			TReadScopes,
			TWriteScopes,
			TResourceInstanceParameters,
			TCreateOrUpdateError,
			TDeleteError,
			TReadError
		>,
		ResourceCollectionOperationsCreateOrUpdate<
			TResource,
			TReadScopes,
			TResourceCollectionParameters,
			TListError = TListError
		> {}


// /**
//  * Represents the resource operations.
//  *
//  * @template TResource The resource model.
//  * @template TReadScope The scope required for reading the resource.
//  * @template TWriteScope The scope required for writing the resource.
//  * @template TResourceCollectionParameters The resource collection parameters.
//  * @template TResourceInstanceParameters The resource instance parameters.
//  * @template TError The error model.
//  */
// interface ResourceOperationsWithUpdateOnly<
// 	TResource extends Resource,
// 	TReadScopes extends string[],
// 	TWriteScopes extends string[],
// 	TResourceCollectionParameters extends ResourceCollectionParameters<TResource> = ResourceCollectionParameters<TResource>,
// 	TResourceInstanceParameters extends ResourceInstanceParameters<TResource> = ResourceInstanceParameters<TResource>,
// 	TCreateOrUpdateError = CreateOrUpdateRequestError,
// 	TDeleteError = DeleteRequestError,
// 	TListError = ListRequestError,
// 	TReadError = ReadRequestError
// >
// 	extends ResourceCollectionOperationsCreateOrUpdate<
// 			TResource,
// 			TReadScopes,
// 			TResourceCollectionParameters,
// 			TListError
// 		>,
// 		ResourceInstanceOperationsCreateAndUpdate<
// 			TResource,
// 			TReadScopes,
// 			TWriteScopes,
// 			TResourceInstanceParameters,
// 			TCreateOrUpdateError,
// 			TDeleteError,
// 			TReadError
// 		> {}

// -- Singleton Resource Operation Templates --

/**
 * Represents create or update operation template for a singleton resource.
 *
 * @template TResource The resource model to create or update.
 * @template TScope The scope required for the operation.
 * @template TError The error model.
 */
interface SingletonResourceCreateOrUpdate<
	TResource extends SingletonResource,
	TScopes extends string[],
	TError = CreateOrUpdateRequestError
> {
	/**
	 * Creates or updates the singleton resource.
	 *
	 * @template TResource The resource model to update.
	 */
	@autoRoute
	@put
	@createsOrUpdatesResource(TResource)
	@useAuth(Auth<TScopes>)
	createOrUpdate(
		@bodyRoot
		updateRequest: SingletonResourceCreateOrUpdateRequestModel<TResource>,
	): SingletonResourceCreatedResponseModel<TResource> | SingletonResourceUpdatedResponseModel<TResource> | TError;
}

/**
 * Represents read operation template for a singleton resource.
 *
 * @template TResource The resource model to read.
 * @template TScope The scope required for the operation.
 * @template TError The error model.
 */
interface SingletonResourceRead<
	TResource extends SingletonResource,
	TScopes extends string[],
	TError = ReadRequestError
> {
	/**
	 * Gets the singleton resource.
	 *
	 * @template TResource The resource model to read.
	 */
	@autoRoute
	@get
	@readsResource(TResource)
	@useAuth(Auth<TScopes>)
	get(
	): SingletonResourceReadResponseModel<TResource> | NotModifiedResponse | TError;
}

/**
 * Represents the singleton resource operations.
 *
 * @template TResource The resource model.
 * @template TReadScope The scope required for reading the resource.
 * @template TWriteScope The scope required for writing the resource.
 * @template TCreateOrUpdateError The create or update error model.
 * @template TReadError The read error model.
 */
interface SingletonResourceOperations<
	TResource extends SingletonResource,
	TReadScopes extends string[],
	TWriteScopes extends string[],
	TCreateOrUpdateError = CreateOrUpdateRequestError,
	TReadError = ReadRequestError
>
	extends SingletonResourceCreateOrUpdate<
			TResource,
			TWriteScopes,
			TCreateOrUpdateError
		>,
		SingletonResourceRead<TResource, TReadScopes, TReadError> {}
