import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeSummaryReportComponent } from './type-summary-report.component';

describe('TypeSummaryReportComponent', () => {
  let component: TypeSummaryReportComponent;
  let fixture: ComponentFixture<TypeSummaryReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypeSummaryReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeSummaryReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
