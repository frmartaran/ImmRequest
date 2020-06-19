import { Component, OnInit, ViewChild } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { ReportsService } from 'src/app/services/reports.service';
import { ManagementComponent } from '../management/management.component';
import { DatePipe } from '@angular/common';
import { Column, Button, TypeSummary, ReportRequest } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { NgForm } from '@angular/forms';
import { HtmlHelpers } from 'src/app/helpers/html.helper';

@Component({
  selector: 'app-type-summary-report',
  templateUrl: './type-summary-report.component.html',
  styleUrls: ['./type-summary-report.component.css']
})
export class TypeSummaryReportComponent implements OnInit {

  public start: Date;
  public end: Date;

  public columns: Column[];
  public buttons: Button[];

  public datasource: BehaviorSubject<MatTableDataSource<TypeSummary>>;

  constructor(private snackbarService: SnackbarService,
    private reportsService: ReportsService,
    public datePipe: DatePipe) { }

  @ViewChild(ManagementComponent, { static: true }) managementeComponent: ManagementComponent;

  ngOnInit() {
    let source = new MatTableDataSource<TypeSummary>();
    this.datasource = new BehaviorSubject(source);
    this.initializeColumns();
  }

  initializeColumns() {
    this.columns = []
    let typeColumn: Column = {
      columnClass: "name",
      columnName: "Type",
      hasButtons: false
    }
    let countColumn: Column = {
      columnClass: "count",
      columnName: "Count",
      hasButtons: false
    }
    let createdDateColumn: Column = {
      columnClass: "typeCreatedAt",
      columnName: "Created Date",
      hasButtons: false
    }
    this.columns = [typeColumn, countColumn, createdDateColumn];
  }

  submit(reportForm: NgForm) {
    let startString = this.datePipe.transform(this.start, "yyyy-MM-dd");
    let start = new Date(startString);

    let endString = this.datePipe.transform(this.end, "yyyy-MM-dd");
    let end = new Date(endString);

    let requestBody: ReportRequest = {
      email: "",
      start: start,
      end: end
    }

    this.reportsService.getTypeReport(requestBody).subscribe(
      (report) => {
        let source = new MatTableDataSource(report);
        source.paginator = this.managementeComponent.paginator;
        this.datasource.next(source);
      },
      (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: "Error !",
          config: this.snackbarService.configError
        });
      });

  }

}
