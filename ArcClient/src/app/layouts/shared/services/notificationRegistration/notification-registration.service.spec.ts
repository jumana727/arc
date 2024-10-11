import { TestBed } from '@angular/core/testing';

import { NotificationRegistrationService } from './notification-registration.service';

describe('NotificationRegistrationService', () => {
  let service: NotificationRegistrationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotificationRegistrationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
