import OperatorEditPage from '@/app/admin/operators/edit/page'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/operators/new')({
  component: RouteComponent,
})

function RouteComponent() {
  return <OperatorEditPage operator={null} />
}
