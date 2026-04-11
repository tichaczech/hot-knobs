import "@typespec/http";
import "@typespec/versioning";
import "../../../Common/Contracts/Contracts.tsp";

using TypeSpec.Http;
using TypeSpec.Rest;
using TypeSpec.Versioning;
using HotKnobs.Contracts;

@service(#{
	title: "HotKnobs: Shared.Collections",
})
@server("http://localhost:7101/v1", "Locally hosted instance")
// @server("https://shared-collections.happywater-27678224.westeurope.azurecontainerapps.io", "Development instance")

@versioned(HotKnobs.Shared.Collections.Contracts.Versions)
namespace HotKnobs.Shared.Collections.Contracts;

enum Versions {
	v1
}

// alias claimItemRead = "item.read";
// alias claimItemWrite = "item.write";
alias claimItemByTypeRead = "itemByType.read";
alias claimItemByTypeWrite = "itemByType.write";
// alias claimTypeRead = "type.read";
// alias claimTypeWrite = "type.write";

scalar ItemCode extends string;

/**
 * Represents a Collection Item.
 */
model Item extends Entity {
	/**
	 * Item code.
	 */
	code: ItemCode;

	/**
	 * Item data.
	 */
	data?: Record<unknown>;

	/**
	 * Item name.
	 */
	name: string;

	/**
	 * Item type.
	 */
	type: string;
}

// model Type extends Resource {
// 	/**
// 	 * Type display name.
// 	 */
// 	displayName: string;
// }

model ItemByType extends OmitProperties<Item, "type">{
	/**
	 * Item code.
	 */
	@visibility(Lifecycle.Read)
	code: ItemCode;
}

model ItemByTypeParameters extends ResourceParameters<Item> {
	/**
	 * Item type.
	 */
	@path()
	@visibility(Lifecycle.Create, Lifecycle.Delete, Lifecycle.Query)
	type: string;
}

model ItemByTypeCollectionParameters extends ItemByTypeParameters { }

model ItemByTypeInstanceParameters extends ResourceInstanceParameters<ItemByType> {
	...ItemByTypeCollectionParameters;

	/**
	 * Item code.
	 */
	@path()
	@visibility(Lifecycle.Create, Lifecycle.Delete, Lifecycle.Query)
	code: ItemCode;
}

/**
 * Resource list operation completed successfully.
 */
@friendlyName("ItemByTypeListResponse")
@withVisibility(Lifecycle.Read)
model ItemByTypeListResponseModel {
	...OkResponse;

	/**
	 * The list of item codes.
	 */
	@bodyRoot()
	body: ItemCode[];
}

// /**
//  * Item service.
//  */
// @route("/items")
// @tag("Item")
// interface ItemService extends ResourceOperationsWithUpdateOnly<Item, [claimItemRead], [claimItemByTypeWrite], ResourceCollectionParametersWithSearch<Item>> {}

/**
 * ItemByType service.
 */
// @route("/{type}")
@tag("Item by Type")
interface ItemByTypeService extends ResourceOperationsWithCreateOrUpdate<ItemByType, [claimItemByTypeRead], [claimItemByTypeWrite], ItemByTypeCollectionParameters, ItemByTypeInstanceParameters> { }


// /**
//  * ItemByType service.
//  */
// @route("/{type}")
// @tag("Item by Type")
// interface ItemByTypeService {
// 	/**
// 	 * Creates a new instance of the resource.
// 	 */
// 	@TypeSpec.Rest.createsOrUpdatesResource(ItemByType)
// 	@put
// 	@route("/{code}") // There is no way to specify the order of parameters in the path.
// 	@useAuth(Auth<[claimItemByTypeWrite]>)
// 	createOrUpdateByType(...ItemByTypeInstanceParameters, @bodyRoot updateRequest: ResourceCreateOrUpdateModel<ItemByType>): ResourceCreatedResponseModel<ItemByType> | ResourceUpdatedResponseModel<ItemByType> | CreateOrUpdateRequestError;

// 	/**
// 	 * Deletes an existing instance of the resource.
// 	 */
// 	@TypeSpec.Rest.deletesResource(ItemByType)
// 	@route("/{code}") // There is no way to specify the order of parameters in the path.
// 	@useAuth(Auth<[claimItemByTypeWrite]>)
// 	deleteByType(...ItemByTypeInstanceParameters): ResourceDeletedResponseModel<ItemByType> | DeleteRequestError;

// 	/**
// 	 * Gets an instance of the resource.
// 	 */
// 	@route("/{code}") // There is no way to specify the order of parameters in the path.
// 	@TypeSpec.Rest.readsResource(ItemByType)
// 	@useAuth(Auth<[claimItemByTypeRead]>)
// 	getByType(...ItemByTypeInstanceParameters): ResourceReadResponseModel<ItemByType> | NotModifiedResponse | ReadRequestError;

// 	/**
// 	 * Gets a list of instances of the resource.
// 	 */
// 	// @TypeSpec.Rest.autoRoute
// 	@route("/") // There is no way to specify the order of parameters in the path.
// 	@TypeSpec.Rest.listsResource(ItemByType)
// 	@useAuth(Auth<[claimItemByTypeRead]>)
// 	listByType(...ItemByTypeCollectionParameters): ItemByTypeListResponseModel | NotModifiedResponse | ListRequestError;
// }

// /**
//  * Type service.
//  */
// @route("/types")
// @tag("Type")
// interface TypeService extends EntityServiceWithCreateOrUpdate<Type, claimTypeRead, claimTypeWrite> { }
