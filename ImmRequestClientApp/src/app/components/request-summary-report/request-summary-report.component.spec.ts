import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestSummaryReportComponent } from './request-summary-report.component';

describe('RequestSummaryReportComponent', () => {
  let component: RequestSummaryReportComponent;
  let fixture: ComponentFixture<RequestSummaryReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestSummaryReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestSummaryReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
