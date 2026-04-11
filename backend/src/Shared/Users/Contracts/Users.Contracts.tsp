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
alias claimDeviceWrite = "users.device.write";
alias claimProfileRead = "users.profile.read";
alias claimProfileWrite = "users.profile.write";

/**
 * Represents a (User) Device.
 */
model Device extends Entity {
	/**
	 * The device Apple Push Notification service Token.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	apnsToken?: string;

	/**
	 * The device Firebase Cloud Messaging Token.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	fcmToken: string;

	/**
	 * The device platform.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	platform: string;

	/**
	 * The ID of the profile the device belongs to.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Create)
	profileId: ResourceId;

	/**
	 * The date and time when the device was last active.
	 */
	@visibility(Lifecycle.Read, Lifecycle.Query)
	lastActiveAt: offsetDateTime;
}

model MyDevice extends OmitProperties<Device, "profileId"> {}

model MyDeviceRegisterRequest extends OmitProperties<Device, "description" | "id" | "isActive" | "lastActiveAt" | "profileId"> {}

model MyProfile extends OmitProperties<Profile, "id" | "isActive"> {}

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

/**
 * Device service.
 */
@route("/devices")
@tag("Device")
interface DeviceService extends ResourceOperationsWithUpdateOnly<Device, [claimDeviceRead], [claimDeviceWrite], ResourceCollectionParametersWithSearch<Device>> {}

@route("/my/devices")
@tag("My Devices")
interface MyDevicesService extends ResourceOperationsWithUpdateOnly<MyDevice, [], [], ResourceCollectionParametersWithSearch<MyDevice>> {
	/**
	 * Registers a new device or updates an existing one.
	 */
	@createsOrUpdatesResource(MyDevice)
	@put()
	// @route("/register")
	@useAuth(Auth<[claimDeviceWrite]>)
	registerDevice(@bodyRoot registerRequest: MyDeviceRegisterRequest): ResourceCreatedResponseModel<MyDevice> | ResourceUpdatedResponseModel<MyDevice> | CreateOrUpdateRequestError;
}

/**
 * My Profile service.
 */
@route("/my/profile")
@tag("My Profile")
interface MyProfileService extends SingletonResourceOperations<MyProfile, [], []> {}

/**
 * Profile service.
 */
@route("/profiles")
@tag("Profile")
interface ProfileService extends ResourceOperationsWithUpdateOnly<Profile, [claimProfileRead], [claimProfileWrite], ResourceCollectionParametersWithSearch<Profile>> {}
