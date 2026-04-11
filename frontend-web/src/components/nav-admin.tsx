"use client";

import * as React from "react";

import {
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar";
import { useTranslation } from "react-i18next";
import { type NavigationItem } from "@/types/common";
import { Button } from "./ui/button";
import { PlusIcon } from "lucide-react";

export function AdministratorNavigation({ items, ...props }: { items: NavigationItem[]; } & React.ComponentPropsWithoutRef<typeof SidebarGroup>) { // TODO: Add custom type for props
  const { t } = useTranslation();

  return (
    <SidebarGroup {...props}>
      <SidebarGroupLabel>{t('_navigation.administration.title')}</SidebarGroupLabel>
      <SidebarGroupContent>
        <SidebarMenu>
          {items.map((item) => (
            <SidebarMenuItem className="flex items-center gap-2" key={item.title}>
              <SidebarMenuButton asChild>
                <a href={item.url}>
                  <item.icon />
                  <span>{item.title}</span>
                </a>
              </SidebarMenuButton>
              <Button size="icon" className="h-8 w-8 shrink-0 group-data-[collapsible=icon]:opacity-0" variant="outline">
                <PlusIcon />
                <span className="sr-only">Inbox</span>
              </Button>
            </SidebarMenuItem>
          ))}
        </SidebarMenu>
      </SidebarGroupContent>
    </SidebarGroup>
  );
}
