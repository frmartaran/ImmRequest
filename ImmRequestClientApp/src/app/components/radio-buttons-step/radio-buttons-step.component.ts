import { NgModel } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { RadioButton } from 'src/app/models/models';

@Component({
  selector: 'app-radio-buttons-step',
  templateUrl: './radio-buttons-step.component.html',
  styleUrls: ['./radio-buttons-step.component.css']
})
export class RadioButtonsStepComponent implements OnInit {

  @Input() hasPrevious: boolean;
  @Input() radioButtons: RadioButton[];
  @Input() bindingProp: number;

  @Output() selectButton = new EventEmitter<number>();
  @Output() bindingPropChange = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }

  onPropChange(id:number){
    this.bindingPropChange.emit(id);
    this.selectButton.emit(id);
  }

}
