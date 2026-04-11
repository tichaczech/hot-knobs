import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_app/admin/sites/new')({
  component: RouteComponent,
})

function RouteComponent() {
  return <div>Hello "/_app/admin/operators/new"!</div>
}
