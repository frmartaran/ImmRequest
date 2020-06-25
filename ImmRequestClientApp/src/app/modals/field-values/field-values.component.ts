import { Component, OnInit, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DataType } from 'src/app/models/models';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-field-values',
  templateUrl: './field-values.component.html',
  styleUrls: ['./field-values.component.css']
})
export class FieldValuesComponent implements OnInit {

  public numberValue: number;

  public isMultipleSelectEnabled: Boolean;

  public showAddButton$: BehaviorSubject<Boolean>;

  public numberValues: number[];

  public numberValuesSubject$: BehaviorSubject<number[]>;

  public dataTypeSelected$: BehaviorSubject<DataType>;

  public currentDataType: DataType;

  public fields: string[];

  public dateTimeValue: Date;

  public dateTimeValues: Date[];

  public dateTimeValuesSubject$: BehaviorSubject<Date[]>;

  public textValue: string;

  public textValues: string[];

  public textValuesSubject$: BehaviorSubject<string[]>;

  public booleanValue: Boolean

  constructor(public dialogRef: MatDialogRef<FieldValuesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private snackbarService: SnackbarService,
    public datePipe: DatePipe) { }

  ngOnInit() {
    this.textValue = '';
    this.numberValue = 0;
    this.booleanValue = false;
    this.dateTimeValue = new Date();
    this.currentDataType = this.data.dataType;
    this.isMultipleSelectEnabled = this.data.isMultipleSelectEnabled;
    this.numberValues = [];
    this.textValues = [];
    this.dateTimeValues = [];
    this.dataTypeSelected$ = new BehaviorSubject(this.currentDataType);
    this.showAddButton$ = new BehaviorSubject(true);
    this.numberValuesSubject$ = new BehaviorSubject([]);
    this.dateTimeValuesSubject$ = new BehaviorSubject([]);
    this.textValuesSubject$ = new BehaviorSubject([]);
  }

  addNumberValue(){
    this.numberValues.push(this.numberValue);
    this.numberValuesSubject$.next(this.numberValues);
    this.snackbarService.notifications$.next({
      message: "Number value added!",
      action: 'Success!',
      config: Object.assign({}, {duration:3000}, this.snackbarService.configSuccess)
    });
    if(!this.isMultipleSelectEnabled){
      this.showAddButton$.next(false);
    }
  }

  addTextValue(){
    this.textValues.push(this.textValue);
    this.textValuesSubject$.next(this.textValues);
    this.snackbarService.notifications$.next({
      message: "Text value added!",
      action: 'Success!',
      config: Object.assign({}, {duration:3000}, this.snackbarService.configSuccess)
    });
    if(!this.isMultipleSelectEnabled){
      this.showAddButton$.next(false);
    }
  }

  addDateTimeValue(){
    this.dateTimeValues.push(this.dateTimeValue);
    this.dateTimeValuesSubject$.next(this.dateTimeValues);
    this.snackbarService.notifications$.next({
      message: "Date time value added!",
      action: 'Success!',
      config: Object.assign({}, {duration:3000}, this.snackbarService.configSuccess)
    });
    if(!this.isMultipleSelectEnabled){
      this.showAddButton$.next(false);
    }
  }

  setFieldValue(){
    let dataTypeSelected = 0;
    this.dataTypeSelected$.subscribe(
      (result) => dataTypeSelected = result
    );
    switch (dataTypeSelected) {
      case DataType.Number:
        if(this.numberValues.length == 0){
          this.showValueIsEmptyMessage();
        }else{
          this.fields = this.numberValues.map(String)
        }
        break;
      case DataType.Text:
        if(this.textValues.length == 0){
          this.showValueIsEmptyMessage();
        }else{
          this.fields = [...this.textValues]
        }
        break;
      case DataType.DateTime:
        if(this.dateTimeValues.length == 0){
          this.showValueIsEmptyMessage();
        }else{
          this.fields = this.dateTimeValues.map(dtv => this.datePipe.transform(dtv, "yyyy-MM-dd"))
        }
        break;
      case DataType.Bool:
        this.fields = [this.booleanValue.toString()]
        break;
      default:
        this.snackbarService.notifications$.next({
          message: "Unsupported field type!",
          action: 'Error!',
          config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
        });
        break;
    }
    this.dialogRef.close(this.fields);
  }

  showValueIsEmptyMessage(){
    this.snackbarService.notifications$.next({
      message: "Value is empty!",
      action: 'Error!',
      config: Object.assign({}, {duration:3000}, this.snackbarService.configError)
    });
  }

  removeFromNumberValues(index: number){
    let arrayLength = this.numberValues.length;
    this.numberValues = [...this.numberValues.slice(0, index), ...this.numberValues.slice(index+1, arrayLength)];
    this.numberValuesSubject$.next(this.numberValues);
  }

  removeFromTextValues(index: number){
    let arrayLength = this.textValues.length;
    this.textValues = [...this.textValues.slice(0, index), ...this.textValues.slice(index+1, arrayLength)];
    this.textValuesSubject$.next(this.textValues);
  }

  removeFromDateValues(index: number){
    let arrayLength = this.dateTimeValues.length;
    this.dateTimeValues = [...this.dateTimeValues.slice(0, index), ...this.dateTimeValues.slice(index+1, arrayLength)];
    this.dateTimeValuesSubject$.next(this.dateTimeValues);
  }

}
