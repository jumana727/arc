import { Component, OnInit } from '@angular/core';
import { NgFor } from '@angular/common';
import {
  BorderDirective,
  CardBodyComponent,
  CardComponent,
  CardImgDirective,
  CardLinkDirective,
  CardTextDirective,
  CardTitleDirective,
  ColComponent,
  GutterDirective,
  RowComponent,
  TextColorDirective,
  NavLinkDirective,
} from '@coreui/angular';
import { RouterModule } from '@angular/router';
import { LogData, LoggingService } from '../../../shared/services/logging/logging.service';
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    RowComponent,
    GutterDirective,
    NgFor,
    ColComponent,
    TextColorDirective,
    CardComponent,
    CardImgDirective,
    CardBodyComponent,
    CardTitleDirective,
    CardTextDirective,
    BorderDirective,
    CardLinkDirective,
    RouterModule,
    NavLinkDirective,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  cards = new Array(4).fill({ body: '' });
  logData: LogData = {
    timestamp: new Date().toISOString(),
    level: 'info',
    message: 'User Successfully came to dashboard Page',
    servicename: 'dashboard componenet',
    additionalInfo: {
      user: 'abhi',
      location: 'india',
    },
  };

  constructor(
    private loggingService: LoggingService,
  ) { }
  ngOnInit(): void {
    this.cards[0] = {
      title: 'Device Module',
      img: 'assets/images/modules/deviceModule.jpg',
      body: 'Manage Devices, Camera, Storage, Recording',
      route: '/deviceManagement'
    };
    this.cards[1] = {
      title: 'Playback',
      img: 'assets/images/modules/playback.jpg',
      body: 'play recorded stream',
      route: '/playback'
    };
    this.cards[2] = {
      title: 'General Setting',
      img: 'assets/images/modules/GeneralSetting.jpg',
      body: 'desc',
      route: '/generalSetting'
    };
    this.cards[3] = {
      title: 'Vehicle Management',
      img: 'assets/images/modules/vehicleManagement.jpg',
      body: 'desc',
      route: '/vehicleManagement'
    };
    this.cards[4] = {
      title: 'System Monitor',
      img: 'assets/images/modules/systemMonitorModule.jpg',
      body: 'desc',
      route: '/systemMonitor'
    };
  }

  onCardClick(card: any): void {

    const logDataCopy: LogData = { ...this.logData };

    // Update the message for the clicked card
    logDataCopy.message = "User Clicked on " + card.title;
  
    // Send the log
    this.loggingService.sendLogEntry(logDataCopy).subscribe({
      next: (response) => {
        console.log('Log entry sent:', response);
      },
      error: (error) => {
        console.error('Error sending log entry:', error);
      },
    });
  
    console.log('Card clicked:', card);

  }
}
