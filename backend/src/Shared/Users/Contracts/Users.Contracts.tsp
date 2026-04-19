import "@typespec/http";
import "@typespec/versioning";
import "../../../Common/Contracts/Contracts.tsp";

using TypeSpec.Http;
using TypeSpec.Rest;
using TypeSpec.Versioning;
using HotKnobs.Contracts;

@service(#{
	title: "HotKnobs: Shared.Users",
})
@server("http://localhost:7101/v1", "Locally hosted instance")
// @server("https://shared-users.happywater-27678224.westeurope.azurecontainerapps.io", "Development instance")

@versioned(HotKnobs.Shared.Users.Contracts.Versions)
namespace HotKnobs.Shared.Users.Contracts;

enum Versions {
	v1
}

alias claimDeviceRead = "users.device.read";
alias claimDeviceTokenRead = "users.device.token.read";
alias claimDeviceWrite = "users.device.write";
alias claimProfileRead = "users.profile.read";
alias claimProfileWrite = "users.profile.write";

alias platform = "ios" | "android";

/**
 * Represents a (User) Device.
 */
model Device extends Entity {
	/**
	 * Unique identifier for the device, e.g. UUID generated and stored in device secure storage.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	deviceId: string;

	/**
	 * The device model name, e.g. "iPhone 12".
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	modelName: string;

	/**
	 * The date and time when the device was last active.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Query)
	lastActiveAt: offsetDateTime;

	/**
	 * The device platform.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	platform: platform;

	/**
	 * The ID of the profile the device belongs to.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	profileId: ResourceId;

	/**
	 * The device operating system, e.g. "iOS 14.4".
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	systemName: string;
}

model DeviceRegisterRequest extends OmitProperties<Device, "description" | "id" | "isActive" | "lastActiveAt" | "profileId"> {
	/**
	 * The device token for Apple Push Notification Service or Firebase Cloud Messaging.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	token?: string;
}

model DeviceSummary extends Summary {
	...OmitProperties<Device, "deviceId" | "modelName" | "platform" | "systemName">;
}

/**
 * Represents a (User) Profile.
 */
model Profile extends Entity {
	/**
	 * The user's email address.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create, Lifecycle.Update)
	email: string;

	/**
	 * The user's phone number.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create, Lifecycle.Update)
	phoneNumber?: string;

	/**
	 * The user's photo URL.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create, Lifecycle.Update)
	photoUrl?: string;
}

model ProfileSummary extends Summary {
	...OmitProperties<Profile, "email" | "phoneNumber">;
}

// model ProfileCollectionParameters extends ResourceCollectionParameters<Profile> { }

model TokenParameters {
	/**
	 * Device id.
	 */
	@key()
	@path()
	id: ResourceId;
}

model TokenResponse {
	/**
	 * The device token for Apple Push Notification service or Firebase Cloud Messaging.
	 */
	@visibility(Lifecycle.Read)
	value?: string;

	/**
	 * The date and time when the Apple Push Notification service or Firebase Cloud Messaging token was last updated.
	 */
	@visibility(Lifecycle.Read)
	updatedAt?: offsetDateTime;
}

/**
 * Device service.
 */
@route("/devices")
@tag("Device")
interface DeviceService extends ResourceOperationsWithUpdateOnly<Device, DeviceSummary, [claimDeviceRead], [claimDeviceWrite]> {
	/**
	 * Registers a new device or updates an existing one for the currently authenticated user.
	 */
	@createsOrUpdatesResource(Device)
	@put()
	// @route("/register")
	@useAuth(Auth<[claimDeviceWrite]>)
	registerDevice(@bodyRoot registerRequest: DeviceRegisterRequest): SingletonResourceCreatedResponseModel<Device> | SingletonResourceUpdatedResponseModel<Device> | CreateOrUpdateRequestError;

	/**
	 * Gets the device (FCM/APNS) token for the currently authenticated user.
	 */
	@get()
	@route("/{id}/token")
	@useAuth(Auth<[claimDeviceTokenRead]>)
	getToken(...TokenParameters): TokenResponse | ReadRequestError;
}

/**
 * Profile service.
 */
@route("/profiles")
@tag("Profile")
interface ProfileService extends ResourceOperationsWithUpdateOnly<Profile, ProfileSummary, [claimProfileRead], [claimProfileWrite]> {
	/**
	 * Gets the profile of the currently authenticated user.
	 */
	@get()
	@readsResource(Profile)
	@route("/my")
	@useAuth(Auth<[claimProfileRead]>)
	getMy(): SingletonResourceReadResponseModel<Profile> | ReadRequestError;

	/**
	 * Creates or updates the profile of the currently authenticated user.
	 */
	@put()
	@createsOrUpdatesResource(Profile)
	@route("/my")
	@useAuth(Auth<[claimProfileWrite]>)
	updateMy(@bodyRoot profile: SingletonResourceCreateOrUpdateRequestModel<Profile>): SingletonResourceCreatedResponseModel<Profile> | SingletonResourceUpdatedResponseModel<Profile> | CreateOrUpdateRequestError;
}
