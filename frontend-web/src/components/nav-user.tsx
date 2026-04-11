'use client';

import {
  BellIcon,
  CreditCardIcon,
  LogOutIcon,
  MoreVerticalIcon, UserCircleIcon
} from 'lucide-react';

import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from '@/components/ui/avatar';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';
import {
  SidebarMenu, SidebarMenuButton,
  SidebarMenuItem,
  useSidebar
} from '@/components/ui/sidebar';
import { useAuth, type User } from '@/contexts/auth';
import { useNavigate } from '@tanstack/react-router';
import { useTranslation } from 'react-i18next';

export function UserPanel({ user }: { user: User | null }) {
  return (
    <>
      <Avatar className='h-8 w-8 rounded-lg'>
        <AvatarImage src={user?.photoUrl} alt={user?.email} />
        <AvatarFallback className='rounded-lg'>CN</AvatarFallback>
      </Avatar>
      <div className='grid flex-1 text-left text-sm leading-tight'>
        {!!user?.displayName ? (
          <>
            <span className='truncate font-medium'>{user.displayName}</span>
            <span className='truncate text-xs text-muted-foreground'>{user.email}</span>
          </>
        ) : <span className='truncate font-medium'>{user?.email}</span>}
      </div>
    </>
  );
}

export function NavUser() {
  const { currentUser, signOut } = useAuth();
  const { isMobile } = useSidebar();
  const navigate = useNavigate();
  const { t } = useTranslation();

  const handleSignOut = async () => {
    try {
      await signOut();
      console.log('User signed out');
      navigate({ to: '/' });
    } catch (error) {
      console.error('Error signing out:', error);
    }
  };

  if (currentUser === null) {
    return null;
  }

  return (
    <SidebarMenu>
      <SidebarMenuItem>
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <SidebarMenuButton size='lg' className='data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground'              >
              <UserPanel user={currentUser} />
              <MoreVerticalIcon className='ml-auto size-4' />
            </SidebarMenuButton>
          </DropdownMenuTrigger>
          <DropdownMenuContent className='w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg' side={isMobile ? 'bottom' : 'right'} align='end' sideOffset={4}>
            <DropdownMenuLabel className='p-0 font-normal'>
              <div className='flex items-center gap-2 px-1 py-1.5 text-left text-sm'>
                <UserPanel user={currentUser} /> {/* grascale */}
              </div>
            </DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuGroup>
              <DropdownMenuItem>
                <UserCircleIcon />
                Account
              </DropdownMenuItem>
              <DropdownMenuItem>
                <CreditCardIcon />
                Billing
              </DropdownMenuItem>
              <DropdownMenuItem>
                <BellIcon />
                Notifications
              </DropdownMenuItem>
            </DropdownMenuGroup>
            <DropdownMenuSeparator />
            <DropdownMenuItem onClick={handleSignOut}>
              <LogOutIcon />
              Log out
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </SidebarMenuItem>
    </SidebarMenu>
  );
}
