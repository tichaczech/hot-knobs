import OperatorsListPage from '@/app/admin/operators/list/page';
import { PendingScreen } from '@/components/pending-screen';
import { listOperatorsQueryOptions } from '@/queries/operators'
import { useQuery } from '@tanstack/react-query';
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/operators/')({
  component: RouteComponent,
  loader: async ({ context }) => {
    await context.queryClient.prefetchQuery(listOperatorsQueryOptions());
  },
  pendingComponent: () => (
    <PendingScreen />
  )
})

function RouteComponent() {
  const { data } = useQuery(listOperatorsQueryOptions());

  return <OperatorsListPage operators={data || []} />
}
