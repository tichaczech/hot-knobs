import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/riders/$id/view')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_app/admin/riders/$id/view"!</div>
}
