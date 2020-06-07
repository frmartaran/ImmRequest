import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, PageEvent } from '@angular/material';
import { Button, Column } from 'src/app/models/models';

@Component({
  selector: 'app-management',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.css']
})
export class ManagementComponent implements OnInit {

  constructor() { }

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  @Input() buttons: Button[];
  @Input() columns: Column[];
  @Input() title: string;
  @Input() pageSize: number;
  @Input() pageSizeOptions: number[];
  @Input() dataSource: MatTableDataSource<any>

  public displayedColumns: string[];

  public resultsLength: number;

  private allData: any[];

  ngOnInit() {
    this.displayedColumns = this.columns.map(column => column.columnClass);
    this.resultsLength = this.dataSource.data.length;
    this.allData = this.dataSource.data;
    this.TrimDataToPageSize(0);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  pageEvent(event: PageEvent){
    let offset = event.pageIndex * event.pageSize;
    this.TrimDataToPageSize(offset);
  }

  private TrimDataToPageSize(offset: number) {
    this.dataSource.data = this.allData.slice(offset).slice(0, this.pageSize);
  }
}