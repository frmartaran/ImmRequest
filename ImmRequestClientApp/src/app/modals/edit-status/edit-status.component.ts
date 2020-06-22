import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FieldValuesComponent } from '../field-values/field-values.component';

@Component({
  selector: 'app-edit-status',
  templateUrl: './edit-status.component.html',
  styleUrls: ['./edit-status.component.css']
})
export class EditStatusComponent implements OnInit {

  public textValue: string;

  constructor(public dialogRef: MatDialogRef<FieldValuesComponent>) { }

  ngOnInit() {
  }

  setStatusValue(){
    this.dialogRef.close(this.textValue);
  }

}
