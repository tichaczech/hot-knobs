from datetime import datetime, timedelta, timezone
import json
import uuid
import re
import random
from faker import Faker

fake = Faker('cs_CZ')

from azure.storage.blob import BlobServiceClient, BlobClient
from urllib.parse import urlparse

dry_run = False

sas_token = ""
storage_url = ""
container_url = f"{storage_url}?{sas_token}"

blob_service_client = BlobServiceClient(account_url=urlparse(storage_url).scheme + "://" + urlparse(storage_url).netloc, credential=sas_token)

type_mapping = {
	0: "Clinic",
	1: "Mammography",
	2: "Pharmacy",
	3: "Laboratory"
}

contact_type_mapping = {
	1: "Phone",
	2: "Email"
}

day_of_week_mapping = {
	1: "Mo",
	2: "Tu",
	3: "We",
	4: "Th",
	5: "Fr",
	6: "Sa",
	0: "Su"
}

consents_url_mapping = {
	"GDPR1": "https://euc.cz/prakticke-informace/ochrana-osobnich-udaju/",
	"GDPR2": "https://moje.euc.cz/priloha-muj-praktik/",
	"GDPR3": "https://moje.euc.cz/zpracovani-osobnich-udaju/",
	"GDPR4": "https://www.euclekarna.cz/vraceni-zbozi-a-reklamace",
	"RFC": "https://moje.euc.cz/priloha-muj-praktik/",
	"VOP": "https://moje.euc.cz/informace-pro-zakazniky/vseobecne-podminky-uziti/",
	"CHR": "https://moje.euc.cz/informace-pro-zakazniky/reklamacni-rad/",
	"C3": "https://moje.euc.cz/informace-pro-zakazniky/reklamacni-rad/",
}

consents_type_mapping = {
	"GDPR1": "consent",
	"GDPR2": "consent",
	"GDPR3": "consent",
	"GDPR4": "consent",
	"RFC": "statement",
	"VOP": "consent",
	"CHR": "other",
	"C3": "other",
}


def transform_time_to_cest(time):
	cest_time = datetime.strptime(time, '%H:%M:%S')
	cest_time = cest_time.replace(tzinfo=timezone(timedelta(hours=2)))
	return cest_time.strftime('%H:%M:%S%z')


with open("UmbracoFacility.json", "r", encoding="utf-8") as file:
	facilities = json.load(file)
with open("UmbracoWorkplace.json", "r", encoding="utf-8") as file:
	workplaces = json.load(file)
with open("UmbracoLocality.json", "r", encoding="utf-8") as file:
	locations = json.load(file)
with open("Consents.json", "r", encoding="utf-8") as file:
	consents = json.load(file)
with open("UmbracoOnlineSpecialization.json", "r", encoding="utf-8") as file:
	onlineSpecialization = json.load(file)
with open("UmbracoSpecialization.json", "r", encoding="utf-8") as file:
	specialization = json.load(file)
with open("UmbracoOnlineExamination.json", "r", encoding="utf-8") as file:
	umbracoOnlineExamination = json.load(file)
with open("UmbracoLaboratory.json", "r", encoding="utf-8") as file:
	umbracoLaboratory = json.load(file)


def transform_workplace(workplace):
	workplace_types = ["Primary", "Outpatient", "Public"]

	attachments = workplace.get("umbracoAttachments", [])
	if not attachments:
		attachments = [{"value": f"https://euc.cz/media/3431/souhlas-z%C3%A1konn%C3%A9ho-z%C3%A1stupce-s-o%C5%A1et%C5%99en%C3%ADm-nezletil%C3%A9ho-nad-15-let.pdf"}]

	examinationTypeCodes = []

	online_specialization_code = None
	is_accepting_new_registrations = False
	if workplace.get("umbracoOnlineSpecializationId") is not None:
		online_specialization_code = str(workplace["umbracoOnlineSpecializationId"])
		wp_online_specialization = next((spec for spec in onlineSpecialization if spec["umbracoOnlineSpecializationId"] == workplace["umbracoOnlineSpecializationId"]), None)
		if wp_online_specialization is not None:
			examinationTypeCodes = [str(examinationType["umbracoOnlineExaminationId"]) for examinationType in wp_online_specialization.get("onlineExaminations", [])]
			is_accepting_new_registrations = wp_online_specialization["isCheckNewPatient"]

	if workplace.get("umbracoBuildingId") is not None:
		building_code = str(workplace["umbracoBuildingId"])
	else:
		building_code = str(workplace["umbracoDepartment"]["umbracoFacility"]["umbracoBuildingId"])

	return {
		"Code": str(workplace["umbracoWorkplaceId"]),
		"Name": workplace["name"],
		"Type": "workplace",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"openingHours": [{
				"dayOfWeek": day_of_week_mapping.get(bh.get("dayOfWeek", "")),
				"from": bh.get("from", ""),
				"to": bh.get("to", ""),
				"description": bh.get("note", "") if bh.get("note") is not None else ""
			} for bh in workplace.get("businessHours", [])],
			"openingHoursDescription": workplace.get("businessHoursDesc", "") if workplace.get("businessHoursDesc", "") is not None else "",
			"onlineSpecializationCode": online_specialization_code,
			"offlineSpecializationCodes": [str(specialization["umbracoSpecializationId"]) for specialization in workplace.get("umbracoSpecializations", [])] if workplace.get("umbracoSpecializations", []) else [
				str(specializationItem["umbracoSpecializationId"]) for specializationItem in random.sample(specialization, 3)],
			"examinationTypeCodes": examinationTypeCodes,
			"buildingCode": building_code,
			"addressDetail": fake.text(),  # todo: workplace["addressDetail"]
			"contacts": [{
				"type": contact_type_mapping.get(contact["type"], "Unknown"),
				"value": contact["value"],
				"description": contact["note"]
			} for contact in workplace.get("umbracoContacts", [])],
			"contactPerson": workplace["contactPersonDesc"],
			"attachments": [{
				"url": attachment["value"]
			} for attachment in attachments],
			"type": random.choice(workplace_types),  # todo: workplace["type"]
			"contactPersonDescription": workplace["contactPersonDesc"],
			"alerts": [],  # todo: workplace["alerts"]
			"url": workplace["externalURL"],
			"IsAcceptingNewRegistrations": is_accepting_new_registrations
		}
	}


def transform_address(building):
	if building is None:
		return {
			"streetNameAndNumber": "",
			"city": "",
			"zipCode": "",
			"gpsCoordinates": ""
		}

	addressLines = building["address"].split("\r\n")
	streetNameAndNumber = addressLines[0]
	match = re.search(r'(\d{3}\s?\d{2})', addressLines[-1])
	if match:
		zipCode = match.group(1).replace(" ", "")
		city = addressLines[-1].replace(match.group(1), "").strip()
	else:
		zipCode = ""
		city = ""

	return {
		"streetNameAndNumber": streetNameAndNumber,
		"city": city,
		"zipCode": zipCode,
		"gpsCoordinates": f"{building['latitude']}, {building['longitude']}"
	}


# find facility of type "Clinic" with the same localityId in "facilities" global array and return its code
def find_facility_parent_code(facility):
	if facility["type"] == 0:
		return ""
	else:
		for f in facilities:
			if f["type"] == 0 and f["umbracoLocalityId"] == facility["umbracoLocalityId"]:  # todo: when umbracoOtherLocalityId is set use it instead of umbracoLocalityId
				return str(f["umbracoFacilityId"])
		return ""


def find_location_code(facility):
	for f in locations:
		if f["umbracoLocalityId"] == facility["umbracoLocalityId"]:
			return str(f["umbracoLocalityId"])
	return ""


def transform_facility(facility):
	facility_type = type_mapping.get(facility["type"], "Unknown")
	department_type = ["Healthcare", "Other"]

	if facility_type == "Laboratory":# Laboratory type is processed in separate collection
		return None

	return {
		"Code": str(facility["umbracoFacilityId"]),
		"Name": facility["name"],
		"Type": "facility",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"type": facility_type,
			"parentCode": find_facility_parent_code(facility),
			"groupName": "EUC Kliniky Praha" if "Praha" in facility["name"] else "",
			"openingHours": [{
				"dayOfWeek": day_of_week_mapping.get(bh.get("dayOfWeek", "")),
				"from": bh.get("from", ""),
				"to": bh.get("to", ""),
				"description": bh.get("note", "") if bh.get("note") is not None else ""
			} for bh in facility.get("businessHours", [])],
			"openingHoursDescription": facility.get("businessHoursDesc", "") if facility.get("businessHoursDesc", "") is not None else "",
			"address": transform_address(facility["umbracoBuilding"]),
			"contacts": [{
				"type": contact_type_mapping.get(contact["type"], "Unknown"),
				"value": contact["value"],
				"description": contact["note"]
			} for contact in facility.get("contacts", [])],
			"departments": [{
				"id": str(department["umbracoDepartmentId"]),
				"name": department["name"],
				"type": random.sample(department_type, 1)[0],
				"workplaces": [
					{
						"id": transformed_workplace.get("Code"),
						"name": transformed_workplace.get("Name"),
						**transformed_workplace.get("Data", {})
					}
					for code in department.get("workplaces", [])
					for transformed_workplace in [
						transform_workplace(
							next(
								(workplace for workplace in workplaces if workplace['umbracoWorkplaceId'] == code),
								None
							)
						)
					]
				],
			} for department in facility.get("departments", [])],
			"url": "https://euc.cz" + facility["facilityUrl"],
			"locationCode": find_location_code(facility)
		}
	}

def transform_laboratory(laboratory):
	return {
		"Code": str(laboratory["umbracoLaboratoryId"]),
		"Name": laboratory["name"],
		"Type": "laboratory",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"openingHours": [{
				"dayOfWeek": day_of_week_mapping.get(bh.get("dayOfWeek", "")),
				"from": bh.get("from", ""),
				"to": bh.get("to", ""),
				"description": bh.get("note", "") if bh.get("note") is not None else ""
			} for bh in laboratory.get("businessHours", [])],
			"openingHoursDescription": laboratory.get("openingHoursDesc", "") if laboratory.get("openingHoursDesc", "") is not None else "",
			"address": transform_address(laboratory["umbracoBuilding"]),
			"contacts": [{
				"type": contact_type_mapping.get(contact["type"], "Unknown"),
				"value": contact["value"],
				"description": contact["note"]
			} for contact in laboratory.get("umbracoContacts", [])],
			"url": "https://euc.cz" + laboratory["linkUrl"] if laboratory["linkUrl"] is not None else ""
		}
	}

def transform_segment(segment):
	return {
		"Code": str(segment["segmentId"]),
		"Name": segment["name"],
		"Type": "segment",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"description": fake.text()  # segment["description"],
		}
	}


def transform_benefit(benefit):
	return {
		"Code": str(benefit["marketingInformationId"]),
		"Name": fake.company(),
		"Type": "benefit",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"subject": fake.company(),
			"label": fake.company(),
			"text": fake.text(),
			"segment": str(random.randint(1, 3)),
			"order": benefit["marketingInformationId"],
			"highlighted": random.choice([True, False])
		}
	}


def transform_consent(consent):
	return {
		"Code": str(consent["consentCode"]),
		"Name": str(consent["consentNameCz"]),
		"Type": "consent",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"textUrl": consents_url_mapping[consent["consentCode"]],
			"type": consents_type_mapping[consent["consentCode"]],
			"mandatory": random.choice([True, False])
		}
	}


def transform_building(facility):
	buildig = facility["umbracoBuilding"]

	return {
		"Code": str(buildig["umbracoBuildingId"]),
		"Name": buildig["name"],
		"Type": "building",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"address": transform_address(buildig)
		}
	}


def transform_location(location):
	return {
		"Code": str(location["umbracoLocalityId"]),
		"Name": location["name"],
		"Type": "locality",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
	}

def transform_faq(faq):
	return {
		"Code": str(faq["id"]),
		"Name": fake.sentence().rstrip('.') + '?',
		"Type": "faq",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4()),
		"Data": {
			"answer": fake.paragraph(nb_sentences=3)
		}
	}


def transform_offlineSpecialization(umbracoSpecialization):
	return {
		"Code": str(umbracoSpecialization["umbracoSpecializationId"]),
		"Name": umbracoSpecialization["name"],
		"Type": "offlineSpecialization",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4())
	}


def transform_onlineSpecialization(umbracoSpecialization):
	return {
		"Code": str(umbracoSpecialization["umbracoOnlineSpecializationId"]),
		"Name": umbracoSpecialization["name"],
		"Type": "convenientSpecialization" if umbracoSpecialization["isComfortable"] else "onlineSpecialization",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4())
	}

def transform_examinationType(umbracoOnlineExamination):
	return {
		"Code": str(umbracoOnlineExamination["umbracoOnlineExaminationId"]),
		"Name": umbracoOnlineExamination["name"],
		"Type": "examinationType",
		"IsActive": True,
		"LastModified": datetime.now(timezone.utc).isoformat(),
		"Id": str(uuid.uuid4())
	}


def import_collection(collection, collection_name, code_key, transform_fnc=lambda x: x):
	print(f"Uploading {collection_name} collection")

	if dry_run:
		dump_file = open(f"dump.{collection_name}.json", "w", encoding="utf-8")
		dump_file.write(json.dumps([transform_fnc(item) for item in collection], indent=4, ensure_ascii=False))
		dump_file.close()
		return

	existing_blobs = set([blob.name for blob in blob_service_client.get_container_client("shared-collections").list_blobs() if collection_name in blob.name])

	for item in collection:
		transformed_item = transform_fnc(item)
		if transformed_item is None:
			continue
		file_path = f"{collection_name}/{item[code_key]}"
		blob_client = blob_service_client.get_blob_client(container="shared-collections", blob=file_path)

		if blob_client.exists():
			blobContent = json.loads(blob_client.download_blob().readall())
			transformed_item["Id"] = blobContent["Id"]

		print(f"Uploading {file_path}")
		blob_client.upload_blob(json.dumps(transformed_item), overwrite=True)
		existing_blobs.discard(file_path)

	for blob_name in existing_blobs:
		blob_service_client.get_blob_client("shared-collections", blob_name).delete_blob()


import_collection(specialization, "offlinespecialization", "umbracoSpecializationId", transform_offlineSpecialization)
import_collection(onlineSpecialization, "onlinespecialization", "umbracoOnlineSpecializationId", transform_onlineSpecialization)
import_collection(onlineSpecialization, "convenientspecialization", "umbracoOnlineSpecializationId", transform_convenientSpecialization)
import_collection(facilities, "facility", "umbracoFacilityId", transform_facility)
import_collection(umbracoOnlineExamination, "examinationtype", "umbracoOnlineExaminationId", transform_examinationType)

# ## NOT NEEDED - is part of Facility composition import_collection(workplaces, "workplace", "umbracoWorkplaceId", transform_workplace)
# import_collection(facilities, "building", "umbracoBuildingId", transform_building)


segments = []
for i in range(1, 4):
	segments.append({
		"segmentId": i,
		"name": fake.company()
	})
import_collection(segments, "segment", "segmentId", transform_segment)

benefits = []
for i in range(1, 21):
	benefits.append({
		"marketingInformationId": i
	})
import_collection(benefits, "benefit", "marketingInformationId", transform_benefit)

consents.append({
	"consentId": consents.__len__() + 1,
	"internalCompanyId": 1,
	"consentNameCz": "Všeobecné obchodní podmínky",
	"consentNameEn": "Terms and conditions",
	"isActive": True,
	"extId": None,
	"validFrom": "2024-04-04T15:33:00",
	"validTo": None,
	"consentCode": "VOP"
})
consents.append({
	"consentId": consents.__len__() + 1,
	"internalCompanyId": 1,
	"consentNameCz": "Reklamační řád",
	"consentNameEn": "Claim Handling Rules",
	"isActive": True,
	"extId": None,
	"validFrom": "2024-04-04T15:33:00",
	"validTo": None,
	"consentCode": "CHR"
})
import_collection(consents, "consent", "consentCode", transform_consent)

import_collection(locations, "location", "umbracoLocalityId", transform_location)

answers = []
for i in range(1, 15):
	answers.append({
		"id": i,
	})
import_collection(answers, "faq", "id", transform_faq)

import_collection(umbracoLaboratory, "laboratory", "umbracoLaboratoryId", transform_laboratory)

print("Files uploaded successfully")
