import { Router } from '@angular/router';
import { element } from 'protractor';
import { Component, OnInit } from '@angular/core';
import { Button, Column, Area, Topic } from 'src/app/models/models';
import { MatTableDataSource } from '@angular/material';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { ManagementService } from 'src/app/services/management.service';

@Component({
  selector: 'app-dummy',
  templateUrl: './dummy.component.html',
  styleUrls: ['./dummy.component.css']
})
export class DummyComponent implements OnInit {

  private random:string;

  public iterator:number;

  public buttons: Button[];

  public columns: Column[];

  public title: string;

  public areas: Area[];

  public dataSource: BehaviorSubject<MatTableDataSource<any>>;

  constructor(private router: Router, private managementService: ManagementService) { }

  ngOnInit() {
    this.iterator = 0;
    let button1: Button = {
      buttonTooltip: "button1",
      iconName: "add",
      callback: () => {this.addElement()}
    }
    let button2: Button = {
      buttonTooltip: "button2",
      iconName: "delete",
      callback: (element) => {this.deleteElement(element)}
    }
    this.buttons = [button1, button2];

    let column1: Column = {
      columnClass: "name1",
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

    let element1 = {name1:"element1"};
    let element2 = {name1:"element2"};
    let element3 = {name1:"element3"};
    let element4 = {name1:"element4"};
    let element5 = {name1:"element5"};

    let source = new MatTableDataSource<any>();
    source.data = [element1, element2, element3, element4, element5];
    this.dataSource = new BehaviorSubject(source);
  }

  addElement(){
    let source = new MatTableDataSource<any>();
    this.dataSource.subscribe((dataSource) => {
      source = dataSource
    })
    let sourceData = [...source.data];
    sourceData = [...sourceData, {name1:"element" + this.iterator}];
    source.data = sourceData;
    this.dataSource.next(source);
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
