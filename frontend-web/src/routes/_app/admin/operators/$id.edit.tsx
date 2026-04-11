import OperatorEditPage from '@/app/admin/operators/edit/page'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/operators/$id/edit')({
  component: RouteComponent,
})

function RouteComponent() {
  return <OperatorEditPage operatorId={null} />
}
