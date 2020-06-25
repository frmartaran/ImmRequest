import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-logout',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css']
})
export class ConfirmationComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ConfirmationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  close:string = "closed";

  dialogTitle: string;
  
  dialogText: string;

  ngOnInit() {
    this.dialogText = this.data.elementDialogText;
    this.dialogTitle = this.data.elementDialogTitle;
  }

}
