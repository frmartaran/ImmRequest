import { Router } from '@angular/router';
import { element } from 'protractor';
import { Component, OnInit } from '@angular/core';
import { Button, Column } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { Observable, BehaviorSubject, Subject } from 'rxjs';

@Component({
  selector: 'app-dummy',
  templateUrl: './dummy.component.html',
  styleUrls: ['./dummy.component.css']
})
export class DummyComponent implements OnInit {

  private random:string;

  public buttons: Button[];

  public columns: Column[];

  public title: string;

  public dataSource: BehaviorSubject<MatTableDataSource<any>>;

  constructor(private router: Router) { }

  ngOnInit() {
    let button1: Button = {
      buttonTooltip: "button1",
      iconName: "edit",
      callback: (element) => {this.redirectElement(element)}
    }
    let button2: Button = {
      buttonTooltip: "button2",
      iconName: "delete",
      callback: (element) => {this.deleteElement(element)}
    }
    this.buttons = [button1, button2];

    let column1: Column = {
      columnClass: "column1",
      columnName: "column1",
      hasButtons: false
    }
    let column2: Column = {
      columnClass: "column2",
      columnName: "column2",
      hasButtons: true
    }
    this.columns = [column1, column2];
    this.title = "prueba";

    let element1 = {name:"element1"};
    let element2 = {name:"element2"};
    let element3 = {name:"element3"};
    let element4 = {name:"element4"};
    let element5 = {name:"element5"};

    let source = new MatTableDataSource<any>();
    source.data = [element1, element2, element3, element4, element5];
    this.dataSource = new BehaviorSubject(source);
  }

  redirectElement(element:any){
    this.router.navigate(['/home-page']);
  }

  deleteElement(element:any){
    console.log(element.name);
    console.log(this.random);
    let source = new MatTableDataSource<any>();
    this.dataSource.subscribe((dataSource) => {
      source = dataSource
    })
    let sourceData = [...source.data];
    let index = sourceData.findIndex(data => data.name == element.name);
    let arrayLength = sourceData.length;
    sourceData = [...sourceData.slice(0, index), ...sourceData.slice(index+1, arrayLength)];
    source.data = sourceData;
    this.dataSource.next(source);
  }
}
