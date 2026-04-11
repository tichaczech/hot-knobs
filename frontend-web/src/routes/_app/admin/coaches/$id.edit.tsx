import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/coaches/$id/edit')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_app/admin/coaches/$id/edit"!</div>
}
