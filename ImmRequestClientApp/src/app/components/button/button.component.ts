import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent implements OnInit {
  constructor() { }

  @Input() buttonTooltip: string;
  @Input() iconName: string;
  @Input() element: any;

  @Output() elementAction = new EventEmitter<{element:any}>();

  ngOnInit() {
  }

  elementClickedAction(element: any){
    this.elementAction.emit(element);
  }

}
