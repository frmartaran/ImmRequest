import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';
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
  @Input() dataSource: MatTableDataSource<any>
  @Input() pageSize: number;

  public displayedColumns: string[];

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.displayedColumns = this.columns.map(column => column.columnClass);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue;
  }
}