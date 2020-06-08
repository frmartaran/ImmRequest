import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { BaseField, DataType } from 'src/app/models/models';
import { SelectionChange } from '@angular/cdk/collections';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-field-editor-dialog',
  templateUrl: './field-editor-dialog.component.html',
  styleUrls: ['./field-editor-dialog.component.css']
})
export class FieldEditorDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<FieldEditorDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  public action: string;

  public field: BaseField

  public disabled: boolean;

  public dataTypeSelected: BehaviorSubject<DataType>;

  public textFieldList: BehaviorSubject<string[]>;

  ngOnInit() {
    this.dataTypeSelected = new BehaviorSubject(DataType.Number);
    this.textFieldList = new BehaviorSubject([]);
    this.action = this.data.action;
    this.field = this.data.field;
    this.disabled = false;
    let holi: BaseField = {
      name: "Test 1",
      dataType: DataType.Number,
      rangeValues: [],
      range: null
    }
    this.field = holi;
  }

  setDatatypeSelected(newValue) {
    this.dataTypeSelected.next(newValue);
    if (newValue == 3) {
      this.disabled = true;
    }
    else {
      this.disabled = false;
    }
  }

  addToList(value: string){
    let currentList = [];
    this.textFieldList.subscribe((values) => {
      currentList = values;
    });
    currentList.push(value);
    this.textFieldList.next(currentList);
  }

  removeFromList(value: string){
    let currentList = [];
    this.textFieldList.subscribe((values) => {
      currentList = values;
    });
    currentList = currentList.filter(v => v != value);
    this.textFieldList.next(currentList);
  }

}
