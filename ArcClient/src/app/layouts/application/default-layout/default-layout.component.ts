import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NgScrollbar } from 'ngx-scrollbar';
import { IconDirective } from '@coreui/icons-angular';
import {
  ContainerComponent,
  INavData,
  ShadowOnScrollDirective,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarFooterComponent,
  SidebarHeaderComponent,
  SidebarNavComponent,
  SidebarToggleDirective,
  SidebarTogglerDirective
} from '@coreui/angular';
import { DefaultFooterComponent, DefaultHeaderComponent } from './';
import { DeviceModuleService } from '../device-module/device-module.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { DeviceEventService } from '../../shared/services/deviceEvent/device-event.service';

function isOverflown(element: HTMLElement) {
  return (
    element.scrollHeight > element.clientHeight ||
    element.scrollWidth > element.clientWidth
  );
}

@Component({
  selector: 'app-default-layout',
  standalone: true,
  imports: [
    CommonModule,
    SidebarComponent,
    SidebarHeaderComponent,
    SidebarBrandComponent,
    RouterLink,
    IconDirective,
    NgScrollbar,
    SidebarNavComponent,
    SidebarFooterComponent,
    SidebarToggleDirective,
    SidebarTogglerDirective,
    DefaultHeaderComponent,
    ShadowOnScrollDirective,
    ContainerComponent,
    RouterOutlet,
    DefaultFooterComponent
  ],
  templateUrl: './default-layout.component.html',
  styleUrl: './default-layout.component.scss'
})
export class DefaultLayoutComponent implements OnInit {
  navItems: INavData[] = [];
  loading: boolean = false;

  constructor(private deviceModuleService: DeviceModuleService, private toastr: ToastrService, private deviceEventService: DeviceEventService) {}

  ngOnInit(): void {
    this.loadNavItems();

    this.deviceEventService.deviceAdded$.subscribe(() => {
      this.loadNavItems();
    });
  }

  loadNavItems(): void {
    this.loading = true;
    this.deviceModuleService.getDeviecComponentListForSideBar().subscribe(response => {
      if (response.status === 200) {
        this.navItems = this.mapDevicesToNavItems(response.data);
      }
      this.loading = false;
    },
    (error) => {
      console.log(error);
      if (error.status === 0) {
        this.toastr.error('Server is currently offline. Please try again later.');
      } else {
        this.toastr.error(error.error.message || 'Please try again later.');
      }
      this.loading = false;
    });
  }

  onScrollbarUpdate($event: any) {
    // if ($event.verticalUsed) {
    // console.log('verticalUsed', $event.verticalUsed);
    // }
  }

  mapDevicesToNavItems(devices: any[]): INavData[] {
    const navItems: INavData[] = [
      {
        name: 'Dashboard',
        url: '/dashboard',
        iconComponent: { name: 'cil-speedometer' },
      },
      {
        name: 'DeviceModule',
        url: '/deviceManagement',
        iconComponent: { name: 'cil-storage' },
      },
      {
        name: 'Playback',
        url: '/playback',
        iconComponent: { name: 'cil-MediaPlay' },
      },
      {
        name: 'Devices',
        title: true
      }
    ];

    devices.forEach(device => {
      const deviceNavItem: INavData = {
        name: device.name,
        iconComponent: { name: 'cil-devices' },
        children: device.components.map((component: { name: any; componentId: any; }) => ({
          name: component.name,
          url: `/videoPlayer`,
          linkProps: { queryParams: { 'deviceId': `${device.id}`, 'cameraId': `${component.componentId}` } },
          iconComponent: { name: 'cil-camera' },
          attributes: { "draggable": "true" }
        }))
      };
      navItems.push(deviceNavItem);
    });

    return navItems;
  }
}
