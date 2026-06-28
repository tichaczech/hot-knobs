import "@typespec/http";
import "../../../Common/Contracts/Contracts.tsp";

using TypeSpec.Http;
using mojeEUC.Contracts;

@service({
	title: "mojeEUC Native: Shared.Configuration",
	version: "1.0.0",
})
@server("http://localhost:7202", "Locally hosted instance")
@server("https://shared-configuration.happywater-27678224.westeurope.azurecontainerapps.io", "Development instance")
namespace mojeEUC.Shared.Configuration.Contracts;

alias claimItemRead = "item.read";
alias claimItemWrite = "item.write";
alias claimItemByTypeRead = "itemByType.read";
alias claimItemByTypeWrite = "typeByItem.write";
alias claimTypeRead = "type.read";
alias claimTypeWrite = "type.write";

scalar ItemName extends string;

model Item extends Resource {
	/**
	 * Item code.
	 */
	name: ItemName;

	/**
	 * Item data.
	 */
	data?: Record<unknown>;

	/**
	 * Item type.
	 */
	type: string;
}

model Type extends Resource {
	/**
	 * Type display name.
	 */
	displayName: string;
}

model ItemByType extends OmitProperties<Item, "type">{
	/**
	 * Item name.
	 */
	@visibility("read")
	name: ItemName;
}

model ItemByTypeParameters extends ResourceParameters<Item> {
	/**
	 * Item type.
	 */
	@path()
	@visibility("create", "delete", "query")
	type: string;
}

model ItemByTypeCollectionParameters extends ItemByTypeParameters { }

model ItemByTypeInstanceParameters extends ItemByTypeCollectionParameters {
	/**
	 * Item name.
	 */
	@path()
	@visibility("create", "delete", "query")
	name: ItemName;
}

/**
 * Resource list operation completed successfully.
 */
@friendlyName("ItemListResponse")
@withVisibility("read")
model ItemByTypeListResponseModel {
	...OkResponse;

	/**
	 * The list of item codes.
	 */
	@bodyRoot()
	body: ItemName[];
}

/**
 * Item service.
 */
@route("/items")
@tag("Item")
interface ItemService extends EntityServiceWithCreateOrUpdate<Item, claimItemRead, claimItemWrite> { }

/**
 * ItemByType service.
 */
@route("/{type}")
@tag("Item by Type")
interface ItemByTypeService {
	/**
	 * Creates a new instance of the resource.
	 */
	@TypeSpec.Rest.createsOrUpdatesResource(ItemByType)
	@put
	@route("/{name}") // There is no way to specify the order of parameters in the path.
	@useAuth(Auth<[claimItemByTypeWrite]>)
	createOrUpdateByType(...ItemByTypeInstanceParameters, @bodyRoot updateRequest: ResourceCreateOrUpdateModel<ItemByType>): ResourceCreatedResponseModel<ItemByType> | ResourceUpdatedResponseModel<ItemByType> | CreateOrUpdateRequestError;

	/**
	 * Deletes an existing instance of the resource.
	 */
	@TypeSpec.Rest.deletesResource(ItemByType)
	@route("/{name}") // There is no way to specify the order of parameters in the path.
	@useAuth(Auth<[claimItemByTypeWrite]>)
	deleteByType(...ItemByTypeInstanceParameters): ResourceDeletedResponseModel<ItemByType> | DeleteRequestError;

	/**
	 * Gets an instance of the resource.
	 */
	@route("/{name}") // There is no way to specify the order of parameters in the path.
	@TypeSpec.Rest.readsResource(ItemByType)
	@useAuth(Auth<[claimItemByTypeRead]>)
	getByType(...ItemByTypeInstanceParameters): ResourceReadResponseModel<ItemByType> | NotModifiedResponse | ReadRequestError;

	/**
	 * Gets a list of instances of the resource.
	 */
	// @TypeSpec.Rest.autoRoute
	@route("/") // There is no way to specify the order of parameters in the path.
	@TypeSpec.Rest.listsResource(ItemByType)
	@useAuth(Auth<[claimItemByTypeRead]>)
	listByType(...ItemByTypeCollectionParameters): ItemByTypeListResponseModel | NotModifiedResponse | ListRequestError;
}

/**
 * Type service.
 */
@route("/types")
@tag("Type")
interface TypeService extends EntityServiceWithCreateOrUpdate<Type, claimTypeRead, claimTypeWrite> { }
