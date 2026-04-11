import { useAuth } from "@/contexts/auth";

export default function Page() {
    const { currentUser } = useAuth();
}
