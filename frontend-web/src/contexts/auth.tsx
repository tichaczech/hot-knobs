import {
  type AuthProvider,
  FacebookAuthProvider,
  GoogleAuthProvider,
  createUserWithEmailAndPassword,
  onAuthStateChanged,
  sendPasswordResetEmail,
  signInWithEmailAndPassword,
  signInWithPopup,
  reauthenticateWithCredential,
  updatePassword,
  type User as FirebaseUser
} from 'firebase/auth';
import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';
import { authentication } from '../firebaseConfig';

export interface User {
  displayName: string | null | undefined;
  email: string;
  photoUrl: string | null | undefined;
}

export interface AuthContext {
  currentUser: User | null;
  isAdmin: boolean;
  isLoading: boolean;
  signInWithEmail: typeof handleSignInWithEmail;
  signInWithApple: typeof handleSignInWithApple;
  signInWithFacebook: typeof handleSignInWithFacebook;
  signInWithGoogle: typeof handleSignInWithGoogle;
  signInWithMicrosoft: typeof handleSignInWithMicrosoft;
  signOut: typeof handleSignOut;
  signUpWithEmail: typeof handleSignUpWithEmail;
  resetPassword: typeof handleResetPassword;
  updatePassword: typeof handleUpdatePassword;
}

const defaultContextValue: AuthContext = {
  currentUser: null,
  isAdmin: false,
  isLoading: true,
  signInWithEmail: handleSignInWithEmail,
  signInWithApple: handleSignInWithApple,
  signInWithFacebook: handleSignInWithFacebook,
  signInWithGoogle: handleSignInWithGoogle,
  signInWithMicrosoft: handleSignInWithMicrosoft,
  signOut: handleSignOut,
  signUpWithEmail: handleSignUpWithEmail,
  resetPassword: handleResetPassword,
  updatePassword: handleUpdatePassword,
};

const AuthContextInstance = createContext<AuthContext>(defaultContextValue);

interface AuthProviderProps {
  children: ReactNode;
}

async function handleSignInWithApple(): Promise<User> {
  throw new Error('Apple sign-in is not implemented yet');
}

async function handleSignInWithEmail(email: string, password: string): Promise<User> {
  try {
    const { user } = await signInWithEmailAndPassword(authentication, email, password);
    return { displayName: user.displayName, email: user.email!, photoUrl: user.photoURL };
  } catch (error) {
    console.error('Error signing in with email:', error);
    throw error;
  }
}

async function handleSignInWithFacebook(): Promise<User> {
  return handleSignInWithProvider(new FacebookAuthProvider());
}

async function handleSignInWithGoogle(): Promise<User> {
  return handleSignInWithProvider(new GoogleAuthProvider());
}

async function handleSignInWithMicrosoft(): Promise<User> {
  throw new Error('Microsoft sign-in is not implemented yet');
}

async function handleSignInWithProvider(provider: AuthProvider): Promise<User> {
  try {
    const { user } = await signInWithPopup(authentication, provider);
    return { displayName: user.displayName, email: user.email!, photoUrl: user.photoURL };
  } catch (error) {
    console.error(`Error signing in with ${provider.providerId}:`, error);
    throw error;
  }
}

async function handleSignOut(): Promise<void> {
  await authentication.signOut();
}

async function handleSignUpWithEmail(email: string, password: string): Promise<User> {
  const { user } = await createUserWithEmailAndPassword(authentication, email, password);
  return { displayName: user.displayName, email: user.email!, photoUrl: user.photoURL };
}

async function handleResetPassword(email: string): Promise<void> {
  await sendPasswordResetEmail(authentication, email);
}

async function handleUpdatePassword(oldPassword: string, newPassword: string): Promise<void> {
  await updatePassword(authentication.currentUser!, newPassword);
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [currentUser, setCurrentUser] = useState<User | null>(null);
  const [isAdmin, setIsAdmin] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const unsubscribe = onAuthStateChanged(authentication, async (user: FirebaseUser | null) => {
      if (user) {
        // User is signed in, get the ID token result
        try {
          const idTokenResult = await user.getIdTokenResult();
          const roleClaim = idTokenResult.claims.role as string | undefined;
          setIsAdmin(roleClaim === 'admin' || user.email === 'petr@tichy.me');
        } catch (error) {
          console.error('Error getting ID token result:', error);
          setIsAdmin(false);
        }
        setCurrentUser({
          email: user.email!,
          displayName: user.displayName,
          photoUrl: user.photoURL
        });
      } else {
        // User has signed out
        setCurrentUser(null);
        setIsAdmin(false);
      }
      setIsLoading(false);
    });

    // Cleanup subscription on unmount
    return unsubscribe;
  }, []);

  const value = { ...defaultContextValue, currentUser, isAdmin, isLoading };

  return (
    <AuthContextInstance.Provider value={value}>
      {!isLoading && children}
    </AuthContextInstance.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContextInstance);
}
