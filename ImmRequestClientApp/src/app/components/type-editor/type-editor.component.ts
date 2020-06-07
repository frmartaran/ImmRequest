import { Component, OnInit } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField, Button, DataType, Column } from 'src/app/models/models';
import { NgForm } from '@angular/forms';
import { MatTableDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-type-editor',
  templateUrl: './type-editor.component.html',
  styleUrls: ['./type-editor.component.css']
})

export class TypeEditorComponent implements OnInit {

  constructor(private snackBarService: SnackbarService) { }

  public action: string;

  public id: number;

  public name: string;

  public fields: BaseField[];

  public buttons: Button[];

  public columns: Column[];

  public datasource: BehaviorSubject<MatTableDataSource<BaseField>>;
  
  ngOnInit() {
    let field: BaseField = {
      name: "Test 1",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field2: BaseField = {
      name: "Test 2",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field3: BaseField = {
      name: "Test 3",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field4: BaseField = {
      name: "Test 4",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field5: BaseField = {
      name: "Test 5",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field6: BaseField = {
      name: "Test 6",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field7: BaseField = {
      name: "Test 7",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field8: BaseField = {
      name: "Test 8",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field9: BaseField = {
      name: "Test 9",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    let field10: BaseField = {
      name: "Test 10",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    this.fields = [field, field2, field3, field4, field5, field6, field7, field8, field9, field10];
    let source = new MatTableDataSource<BaseField>();
    source.data = this.fields;
    this.datasource = new BehaviorSubject(source);

    this.initializeButtons();
    this.initializeColumns();
  }


  Send(typeEditorForm: NgForm){

  }

  initializeColumns(){
    this.columns = []
    let headerName: Column = {
      columnClass: "Field",
      columnName: "Fields",
      hasButtons: false
    }
    let headerButtons: Column = {
      columnClass: "buttons",
      columnName: "Actions",
      hasButtons: true
    }
    this.columns.push(headerName);
    this.columns.push(headerButtons);
  }

  initializeButtons(){
    let editButton: Button = {
      buttonTooltip: "edit",
      iconName: "edit",
      callback: (element) => {this.openFieldDetails(element)}
    }
    let deleteButton: Button = {
      buttonTooltip: "delete",
      iconName: "delete",
      callback: (element) => {this.deleteField(element)}
    }
    this.buttons = [editButton, deleteButton];
  }

  deleteField(field: BaseField){
    
    this.snackBarService.notifications$.next({
      message: "Delete button click",
      action: 'Success!',
      config: this.snackBarService.configSuccess
    });
  }

  openFieldDetails(field: BaseField){

    this.snackBarService.notifications$.next({
      message: "Edit button click",
      action: 'Success!',
      config: this.snackBarService.configSuccess
    });
  }

}
