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

  public dataTypeSelected$: BehaviorSubject<DataType>;

  public currentDataType: DataType;

  public fields: string[];

  public dateTimeValue: Date;

  public dateTimeValues: Date[];

  public textValue: string;

  public textValues: string[];

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
  }

  addNumberValue(){
    this.numberValues.push(this.numberValue);
    this.snackbarService.notifications$.next({
      message: "Number value added!",
      action: 'Success!',
      config: this.snackbarService.configSuccess
    });
    if(!this.isMultipleSelectEnabled){
      this.showAddButton$.next(false);
    }
  }

  addTextValue(){
    this.textValues.push(this.textValue);
    this.snackbarService.notifications$.next({
      message: "Text value added!",
      action: 'Success!',
      config: this.snackbarService.configSuccess
    });
    if(!this.isMultipleSelectEnabled){
      this.showAddButton$.next(false);
    }
  }

  addDateTimeValue(){
    this.dateTimeValues.push(this.dateTimeValue);
    this.snackbarService.notifications$.next({
      message: "Date time value added!",
      action: 'Success!',
      config: this.snackbarService.configSuccess
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
        this.fields = [...this.booleanValue.toString()]
        break;
      default:
        this.snackbarService.notifications$.next({
          message: "Unsupported field type!",
          action: 'Error!',
          config: this.snackbarService.configError
        });
        break;
    }
    this.dialogRef.close(this.fields);
  }

  showValueIsEmptyMessage(){
    this.snackbarService.notifications$.next({
      message: "Value is empty!",
      action: 'Error!',
      config: this.snackbarService.configError
    });
  }

}
