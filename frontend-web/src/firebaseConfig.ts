import { initializeApp } from "firebase/app";
import { connectAuthEmulator, getAuth } from "firebase/auth";
import { connectStorageEmulator, getStorage } from "firebase/storage";
import { getFirestore, connectFirestoreEmulator } from "firebase/firestore";

// TODO: Replace with your actual Firebase project configuration
const firebaseConfig = {
    apiKey: "AIzaSyD8Fn8CZYfr-ONUzq4aK7yLfoT0LbBuPik",
    appId: "1:16377007840:web:2bfadbf439799dd186ca85",
    authDomain: "mad-sprocket-demo.firebaseapp.com",
    databaseURL: "https://mad-sprocket-demo-default-rtdb.europe-west1.firebasedatabase.app",
    projectId: "mad-sprocket-demo",
    storageBucket: "mad-sprocket-demo.firebasestorage.app",
    messagingSenderId: "16377007840",
};

const app = initializeApp(firebaseConfig);

export const authentication = getAuth(app);
export const storage = getStorage(app);
export const database = getFirestore(app);

if (location.hostname === "localhost") {
    // Point to the emulator(s) running on localhost.
    connectAuthEmulator(authentication, "http://localhost:9099");
    connectStorageEmulator(storage, "localhost", 9199);
    connectFirestoreEmulator(database, 'localhost', 8080);
} 

export default app;
