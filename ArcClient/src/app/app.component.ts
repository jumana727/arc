import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from './shared/icons/icon-subset';
import { PageTitleService } from './layouts/shared/services/pageTitle/page-title.service';

@Component({
  selector: 'app-root',
  template: '<router-outlet />',
  standalone: true,
  imports: [RouterOutlet]
})

export class AppComponent implements OnInit {
  constructor(
    private router: Router,
    private pageTitleService : PageTitleService,
    private iconSetService: IconSetService
  ) {
    this.pageTitleService.setTitle(undefined);
    // iconSet singleton
    this.iconSetService.icons = { ...iconSubset };
  }

  ngOnInit(): void {
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
    });
  }

}
