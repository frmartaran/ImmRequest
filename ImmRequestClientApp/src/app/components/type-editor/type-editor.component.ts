import { Component, OnInit } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseField } from 'src/app/models/models';

@Component({
  selector: 'app-type-editor',
  templateUrl: './type-editor.component.html',
  styleUrls: ['./type-editor.component.css']
})

export class TypeEditorComponent implements OnInit {

  constructor(private snackBarService: SnackbarService) { }

  public action: string;

  public id: number;

  public name: string;

  public fields: BaseField[];

  ngOnInit() {
  }

}
