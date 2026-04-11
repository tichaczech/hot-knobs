import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/operators/$id/view')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_app/_admin/operators/$id/view"!</div>
}
