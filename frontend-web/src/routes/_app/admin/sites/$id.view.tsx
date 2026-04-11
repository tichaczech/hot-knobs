import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/sites/$id/view')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_app/_admin/sites/$id/view"!</div>
}
