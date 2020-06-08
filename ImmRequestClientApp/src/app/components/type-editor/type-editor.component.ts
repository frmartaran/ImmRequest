import { Component, OnInit } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField, Button, DataType, Column } from 'src/app/models/models';
import { NgForm } from '@angular/forms';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { FieldEditorDialogComponent } from 'src/app/modals/field-editor-dialog/field-editor-dialog.component';

@Component({
  selector: 'app-type-editor',
  templateUrl: './type-editor.component.html',
  styleUrls: ['./type-editor.component.css']
})

export class TypeEditorComponent implements OnInit {

  constructor(public dialog: MatDialog, private snackBarService: SnackbarService) { }

  public action: string;

  public id: number;

  public name: string;

  public buttons: Button[];

  public columns: Column[];

  public allFields: BehaviorSubject<BaseField[]>;

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
    let fields = [field, field2, field3, field4, field5, field6, field7, field8, field9, field10];
    let source = new MatTableDataSource<BaseField>();
    source.data = fields;
    this.datasource = new BehaviorSubject(source);
    this.allFields = new BehaviorSubject(fields);

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
      callback: (element) => {this.editField(element)}
    }
    let deleteButton: Button = {
      buttonTooltip: "delete",
      iconName: "delete",
      callback: (element) => {this.deleteField(element)}
    }
    this.buttons = [editButton, deleteButton];
  }

  deleteField(field: BaseField){
    this.updateTableSource(field);
    let updatedFields = [];
    this.allFields.subscribe((fields) => {
      updatedFields = fields
    });
    updatedFields = updatedFields.filter(f => f.name != field.name);
    this.allFields.next(updatedFields);
  }

  private updateTableSource(field: BaseField) {
    let source = new MatTableDataSource<any>();
    this.datasource.subscribe((dataSource) => {
      source = dataSource;
    });
    let sourceData = [...source.data];
    let index = sourceData.findIndex(data => data.name == field.name);
    let arrayLength = sourceData.length;
    sourceData = [...sourceData.slice(0, index), ...sourceData.slice(index + 1, arrayLength)];
    source.data = sourceData;
    this.datasource.next(source);
  }

  editField(field: BaseField){
    let action = "Edit";
    let editDialog = this.dialog.open(FieldEditorDialogComponent, {
      data: {
        action: action,
        field: field
      }
    });
    editDialog.afterClosed().subscribe((res) => {
      if(res){
        console.log("closed edit dialog with OK");
      }
      console.log("clicked cancel");
    });
  }

  createField(){
    let action = "Create"
    let createDialog = this.dialog.open(FieldEditorDialogComponent, {
      data: {
        action: action,
      }
    });
    createDialog.afterClosed().subscribe((res) => {
      if(res){
        console.log("closed create dialog with OK");
      }
      console.log("clicked cancel");
    });
  }

}
