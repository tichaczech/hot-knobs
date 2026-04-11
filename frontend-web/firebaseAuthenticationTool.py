import firebase_admin
from firebase_admin import credentials, auth

cred = credentials.Certificate("./firebase-credentials.json")
default_app = firebase_admin.initialize_app(cred)

user = auth.get_user_by_email('petr@tichy.me')
print(user.custom_claims)

# auth.set_custom_user_claims(user.uid, {'role': 'admin'})

# user = auth.get_user_by_email('petr@tichy.me')
# print(user.custom_claims)
