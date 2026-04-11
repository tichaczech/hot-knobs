import { database } from '@/firebaseConfig';
import type { Operator } from '@/types/models';
import { type UseQueryOptions } from '@tanstack/react-query';
import { collection, doc, getDocs, getDoc } from "firebase/firestore";

const queryName: string = 'operators';
const operatorsCollection = collection(database, queryName);

export const getOperator = async (id: string): Promise<Operator | undefined> => {
    const snapshot = await getDoc(doc(operatorsCollection, id));
    if (snapshot.exists()) {
        return snapshot.data() as Operator;
    } else {
        return undefined;
    }
};

export const getOperators = async (): Promise<Operator[]> => {
    try {
        const snapshot = await getDocs(operatorsCollection);

        if (snapshot.empty) {
            return [];
        }

        const operators: Operator[] = [];
        snapshot.forEach((doc) => {
            const operator = doc.data() as Operator;
            operator.id = doc.id; // Ensure the operator has an ID
            operators.push(operator);
        });

        // Sort operators by name
        operators.sort((a, b) => a.name.localeCompare(b.name));

        return operators;
    } catch (error) {
        console.error("Error fetching operators:", error);
        throw error; // Re-throw the error to be handled by the caller

    }
};

export const getOperatorQueryOptions = (id: string): UseQueryOptions<Operator | undefined> => {
    return {
        queryKey: [queryName, id],
        queryFn: () => getOperator(id),
        staleTime: 1000 * 60
    };
};

export const listOperatorsQueryOptions = (): UseQueryOptions<Operator[]> => {
    return {
        queryKey: [queryName],
        queryFn: getOperators,
        staleTime: 1000 * 60
    };
};
