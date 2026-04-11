import ForgotPasswordPage from '@/app/auth/forgotPassword/page'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/_auth/forgotPassword')({
  component: ForgotPasswordPage,
})
