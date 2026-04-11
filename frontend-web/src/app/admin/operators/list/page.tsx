
import { useAuth } from "@/contexts/auth";
import type { Operator } from "@/types/models";
import OperatorCard from "./card";
import PageMeta from "@/components/page-meta";
import { useTranslation } from "react-i18next";
// import { Link } from "@tanstack/react-router";
// import { buttonVariants } from "@/components/ui/button";

interface OperatorListPageProps extends React.ComponentProps<"div"> {
    operators: Array<Operator>;
}

export default function OperatorsListPage({ operators }: OperatorListPageProps) {
    const { isAdmin } = useAuth();
    const { t } = useTranslation();

    return (
        <>
            <PageMeta title={t('operators.list.page.title')} description={t('operators.list.page.description')} />
            {/* <PageBreadcrumb pageTitle="Sites" /> */}

            {/* {isAdmin && (
                <div className="flex justify-end">
                    <Link to='/admin/operators/new' className={buttonVariants({ variant: "secondary" })}>
                        {t('_common.add')}
                    </Link>
                </div>
            )} */}

            <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 xl:grid-cols-4">
                {operators.length > 0 ? (
                    operators.map(operator => (
                        <OperatorCard key={operator.id} operator={operator} />
                    ))
                ) : (
                    <p>No operators found.</p> // Handle empty list
                )}
            </div>
        </>
    )
}
