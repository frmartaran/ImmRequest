import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { BaseField, DataType } from 'src/app/models/models';
import { SelectionChange } from '@angular/cdk/collections';
import { BehaviorSubject } from 'rxjs';
import { R3FactoryDelegateType } from '@angular/compiler/src/render3/r3_factory';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-field-editor-dialog',
  templateUrl: './field-editor-dialog.component.html',
  styleUrls: ['./field-editor-dialog.component.css']
})
export class FieldEditorDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<FieldEditorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private snackbarService: SnackbarService,
    public datePipe: DatePipe) { }

  public action: string;

  public field: BaseField

  public disabled: boolean;

  public acceptsMultpleValues: boolean;

  public dataTypeSelected: BehaviorSubject<DataType>;

  public textFieldList: BehaviorSubject<string[]>;

  public start: number;

  public end: number;

  public startDate: Date;

  public endDate: Date;

  public name: string;

  ngOnInit() {
    this.textFieldList = new BehaviorSubject([]);
    this.action = this.data.action;
    this.field = this.data.field;

    if (this.field != null) {
      this.dataTypeSelected = new BehaviorSubject(this.field.dataType);
      this.ShouldDisableMultipleValues(this.field.dataType);
      this.populateValues(this.field);
    } else {
      this.dataTypeSelected = new BehaviorSubject(DataType.Number);
      this.ShouldDisableMultipleValues(DataType.Number);
    }
  }

  setDatatypeSelected(newValue) {
    this.dataTypeSelected.next(newValue);
    this.ShouldDisableMultipleValues(newValue);
  }

  private ShouldDisableMultipleValues(type: DataType) {
    if (type == 3) {
      this.disabled = true;
    }
    else {
      this.disabled = false;
    }
  }

  addToList(value: string) {
    let currentList = [];
    this.textFieldList.subscribe((values) => {
      currentList = values;
    });
    currentList.push(value);
    this.textFieldList.next(currentList);
  }

  removeFromList(value: string) {
    let currentList = [];
    this.textFieldList.subscribe((values) => {
      currentList = values;
    });
    currentList = currentList.filter(v => v != value);
    this.textFieldList.next(currentList);
  }

  populateValues(field: BaseField) {
    this.name = field.name;
    this.acceptsMultpleValues = field.multipleValues;
    switch (+field.dataType) {
      case DataType.Number:
        this.start = Number.parseInt(field.rangeValues[0]);
        this.end = Number.parseInt(field.rangeValues[1]);
        break;
      case DataType.Text:
        this.textFieldList.next(field.rangeValues);
        break;
      case DataType.DateTime:
        let start = new Date(field.rangeValues[0]).toLocaleDateString();
        this.startDate = new Date(start);
        let end = new Date(field.rangeValues[1]).toLocaleDateString();
        this.endDate = new Date(end);
        break;
      case DataType.Bool:
        break;
      default:
        this.snackbarService.notifications$.next({
          message: "Unsupported field type!",
          action: 'Error!',
          config: this.snackbarService.configError
        });
        break;
    }
  }

  submit(fieldForm: NgForm) {
    let newName = fieldForm.value.name;
    let newDataType = DataType.Number;
    this.dataTypeSelected.subscribe((dataType) => {
      newDataType = dataType;
    });
    let newMultipleValues = fieldForm.value.acceptsMultpleValues;
    let newRangeValues = [];

    switch (+newDataType) {
      case DataType.Number:
        newRangeValues = [fieldForm.value.start.toString(), fieldForm.value.end.toString()];
        break;
      case DataType.Text:
        this.textFieldList.subscribe((values) => {
          newRangeValues = values;
        });
        break;
      case DataType.DateTime:
        let start = this.datePipe.transform(fieldForm.value.startDate, "yyyy-MM-dd");
        let end = this.datePipe.transform(fieldForm.value.endDate, "yyyy-MM-dd");
        newRangeValues = [start, end];
        break;
      case DataType.Bool:
        break;
      default:
        this.snackbarService.notifications$.next({
          message: "Unsupported field type!",
          action: 'Error!',
          config: this.snackbarService.configError
        });
        break;
    }

    let newField: BaseField = {
      name: newName,
      dataType: newDataType,
      multipleValues: newMultipleValues,
      rangeValues: newRangeValues,
      parentTypeId: 0
    };

    this.field = newField;
    this.dialogRef.close(this.field);
  }

}
