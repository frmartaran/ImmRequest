import { Component, OnInit, Input } from '@angular/core';
import { Button } from 'src/app/models/models';

@Component({
  selector: 'app-label-displayer',
  templateUrl: './label-displayer.component.html',
  styleUrls: ['./label-displayer.component.css']
})
export class LabelDisplayerComponent implements OnInit {

  constructor() { }

  @Input() name: string;
  @Input() buttons: Button[];

  ngOnInit() {
  }

}
