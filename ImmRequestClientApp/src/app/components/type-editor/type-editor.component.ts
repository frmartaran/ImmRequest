import { Component, OnInit, Input } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField, Button, DataType, Column, TopicType } from 'src/app/models/models';
import { NgForm } from '@angular/forms';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { FieldEditorDialogComponent } from 'src/app/modals/field-editor-dialog/field-editor-dialog.component';
import { TypeService } from 'src/app/services/type.service';
import { HtmlHelpers } from 'src/app/helpers/html.helper';

@Component({
  selector: 'app-type-editor',
  templateUrl: './type-editor.component.html',
  styleUrls: ['./type-editor.component.css']
})

export class TypeEditorComponent implements OnInit {

  constructor(public dialog: MatDialog,
    private snackBarService: SnackbarService,
    private typeService: TypeService) { }

  public action: string;

  public name: string;

  public buttons: Button[];

  public columns: Column[];

  public allFields: BehaviorSubject<BaseField[]>;

  public datasource: BehaviorSubject<MatTableDataSource<BaseField>>;

  @Input() type: TopicType;

  @Input() parentTopicId: number;

  ngOnInit() {
    this.action = "Create"
    if(this.type == null){
      let emptyType: TopicType = {
        name: "",
        Fields: []
      };
      this.type = emptyType;
    }
    this.parentTopicId = 1;
    let source = new MatTableDataSource<BaseField>();
    source.data = this.type.Fields;
    this.datasource = new BehaviorSubject(source);
    this.allFields = new BehaviorSubject(this.type.Fields);

    this.initializeButtons();
    this.initializeColumns();
  }


  Send(typeEditorForm: NgForm) {
    this.type.name = typeEditorForm.value.name;
    let fields = [];
    this.allFields.subscribe((currentFields) => {
      fields = currentFields
    });
    this.type.Fields = fields;
    if (this.action == "Create") {
      this.Create();
    }else{
      this.Edit();
    }
  }

  private Create() {
    this.typeService.createType(1, this.type)
      .subscribe((res) => {
        this.snackBarService.notifications$.next({
          message: res,
          action: 'Success!',
          config: this.snackBarService.configSuccess
        });
      }, (error) => {
        this.snackBarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackBarService.configError
        });
      });
  }

  private Edit() {
    this.typeService.updateType(this.type)
      .subscribe((res) => {
        this.snackBarService.notifications$.next({
          message: res,
          action: 'Success!',
          config: this.snackBarService.configSuccess
        });
      }, (error) => {
        this.snackBarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: 'Error!',
          config: this.snackBarService.configError
        });
      });
  }

  initializeColumns() {
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

  initializeButtons() {
    let editButton: Button = {
      buttonTooltip: "edit",
      iconName: "edit",
      callback: (element) => { this.editField(element) }
    }
    let deleteButton: Button = {
      buttonTooltip: "delete",
      iconName: "delete",
      callback: (element) => { this.deleteField(element) }
    }
    this.buttons = [editButton, deleteButton];
  }

  deleteField(field: BaseField) {
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

  editField(field: BaseField) {
    let action = "Edit";
    let editDialog = this.dialog.open(FieldEditorDialogComponent, {
      data: {
        action: action,
        field: field
      },
      autoFocus: false
    });
    editDialog.afterClosed().subscribe((res) => {
      if (res) {
        let newField = res as BaseField;
        let currentFields = [];
        this.allFields.subscribe((fields) => {
          currentFields = fields;
        });
        let index = currentFields.findIndex(f => f.name == field.name);
        currentFields[index] = newField;
        this.allFields.next(currentFields);

        let currentDisplay = new MatTableDataSource<BaseField>();
        this.datasource.subscribe((display) => {
          currentDisplay = display
        });
        let displayIndex = currentDisplay.data.findIndex(f => f.name == field.name);
        currentDisplay.data[displayIndex] = newField;
        this.datasource.next(currentDisplay);
      }
    });
  }

  createField() {
    let action = "Create"
    let createDialog = this.dialog.open(FieldEditorDialogComponent, {
      data: {
        action: action,
      },
      autoFocus: false
    });
    createDialog.afterClosed().subscribe((res) => {
      if (res) {
        let newField = res as BaseField;
        let currentFields = [];
        this.allFields.subscribe((fields) => {
          currentFields = fields;
        });
        currentFields.push(newField);
        this.allFields.next(currentFields);
      }
    });
  }

}
