"use client";

import { AdministratorNavigation } from "@/components/nav-admin";
// import { NavDocuments } from "@/components/nav-documents";
import { MainNavigation } from "@/components/nav-main";
import { NavUser } from "@/components/nav-user";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar";
import type { NavigationItem } from "@/types/common";
import { t } from "i18next";
import {
  ArrowUpCircleIcon, LandmarkIcon,
  BarChartIcon, FolderIcon, LayoutDashboardIcon,
  ListIcon, UsersIcon,
  MapPinned,
  ShieldUserIcon
} from "lucide-react";
import * as React from "react";

const mainNavigation: NavigationItem[] = [
  {
    title: "Dashboard",
    url: "/",
    icon: LayoutDashboardIcon,
  },
  {
    title: "Lifecycle",
    url: "/signIn",
    icon: ListIcon,
  },
  {
    title: "Analytics",
    url: "#",
    icon: BarChartIcon,
  },
  {
    title: "Projects",
    url: "#",
    icon: FolderIcon,
  },
  {
    title: "Team",
    url: "#",
    icon: UsersIcon,
  },
];

const administrationNavigation: NavigationItem[] = [
  {
    title: t("_navigation.administration.items.operators"),
    url: "/admin/operators",
    icon: LandmarkIcon,
  },
  {
    title: t("_navigation.administration.items.sites"),
    url: "/admin/sites",
    icon: MapPinned,
  },
  {
    title: t("_navigation.administration.items.coaches"),
    url: "/admin/coaches",
    icon: ShieldUserIcon,
  },
  {
    title: t("_navigation.administration.items.riders"),
    url: "/admin/riders",
    icon: UsersIcon,
  },
];

// const navSecondary: NavigationItem = [
//   {
//     title: "Settings",
//     url: "#",
//     icon: SettingsIcon,
//   },
//   {
//     title: "Get Help",
//     url: "#",
//     icon: HelpCircleIcon,
//   },
//   {
//     title: "Search",
//     url: "#",
//     icon: SearchIcon,
//   },
// ];

// const navClouds: NavigationItem[] = [
//   {
//     title: "Capture",
//     icon: CameraIcon,
//     isActive: true,
//     url: "#",
//     items: [
//       {
//         title: "Active Proposals",
//         url: "#",
//       },
//       {
//         title: "Archived",
//         url: "#",
//       },
//     ],
//   },
//   {
//     title: "Proposal",
//     icon: FileTextIcon,
//     url: "#",
//     items: [
//       {
//         title: "Active Proposals",
//         url: "#",
//       },
//       {
//         title: "Archived",
//         url: "#",
//       },
//     ],
//   },
//   {
//     title: "Prompts",
//     icon: FileCodeIcon,
//     url: "#",
//     items: [
//       {
//         title: "Active Proposals",
//         url: "#",
//       },
//       {
//         title: "Archived",
//         url: "#",
//       },
//     ],
//   },
// ];

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  return (
    <Sidebar collapsible="offcanvas" {...props}>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton asChild className="data-[slot=sidebar-menu-button]:!p-1.5"            >
              <a href="#">
                <ArrowUpCircleIcon className="h-5 w-5" />
                <span className="text-base font-semibold">Hot Knobs</span>
              </a>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <MainNavigation items={mainNavigation} />
        {/* <NavDocuments items={data.documents} /> */}
        <AdministratorNavigation items={administrationNavigation} className="mt-auto" />
      </SidebarContent>
      <SidebarFooter>
        <NavUser />
      </SidebarFooter>
    </Sidebar>
  );
}
