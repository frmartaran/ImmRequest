import { Component, OnInit, ViewChild } from '@angular/core';
import { ReportsService } from 'src/app/services/reports.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BehaviorSubject } from 'rxjs';
import { MatTableDataSource } from '@angular/material';
import { RequestSummary, Button, Column, ReportRequest } from 'src/app/models/models';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { HtmlHelpers } from 'src/app/helpers/html.helper';
import { ManagementComponent } from '../management/management.component';

@Component({
  selector: 'app-request-summary-report',
  templateUrl: './request-summary-report.component.html',
  styleUrls: ['./request-summary-report.component.css']
})
export class RequestSummaryReportComponent implements OnInit {

  public email: string;
  public start: Date;
  public end: Date;

  public datasource: BehaviorSubject<MatTableDataSource<RequestSummary>>;
  public columns: Column[];
  public buttons: Button[];

  constructor(private snackbarService: SnackbarService,
    private reportService: ReportsService, public datePipe: DatePipe) { }

  @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

  ngOnInit() {
    this.initializeColumns();
    let source = new MatTableDataSource<RequestSummary>();
    this.datasource = new BehaviorSubject(source);
  }

  initializeColumns() {
    this.columns = []
    let statusColumn: Column = {
      columnClass: "status",
      columnName: "Request Status",
      hasButtons: false
    }
    let countColumn: Column = {
      columnClass: "count",
      columnName: "Count",
      hasButtons: false
    }
    let requestsColums: Column = {
      columnClass: "requestNumbers",
      columnName: "Requests Numbers",
      hasButtons: false
    }
    this.columns = [statusColumn, countColumn, requestsColums];
  }

  submit(requestForm: NgForm) {
    let startString = this.datePipe.transform(this.start, "yyyy-MM-dd");
    let start = new Date(startString);

    let endString = this.datePipe.transform(this.end, "yyyy-MM-dd");
    let end = new Date(endString)
    let requestBody: ReportRequest = {
      email: this.email,
      start: start,
      end: end
    }

    this.reportService.getRequestReport(requestBody).subscribe((report) => {
      console.log(report);
      let source = new MatTableDataSource(report);
      source.paginator = this.managementeComponent.paginator;
      this.datasource.next(source);
    }, (error) => {
      this.snackbarService.notifications$.next({
        message: HtmlHelpers.getHtmlErrorMessage(error),
        action: "Error !",
        config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
      });
    })
  }

}
