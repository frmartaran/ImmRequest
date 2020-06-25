import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material';
import { FlatNode, RequestValue } from 'src/app/models/models';
import { FlatTreeControl } from '@angular/cdk/tree';

@Component({
  selector: 'app-show-request-values',
  templateUrl: './show-request-values.component.html',
  styleUrls: ['./show-request-values.component.css']
})
export class ShowRequestValuesComponent implements OnInit {

  public nodes: FlatNode[];

  private _transformer = (value: RequestValue, level: number) => {
    return {
      expandable: !!value.value && value.value.length > 0,
      name: value.fieldName,
      level: level,
    };
  }

  public treeControl = new FlatTreeControl<FlatNode>(
    node => node.level, node => node.expandable);

  public treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.value.map(value => <RequestValue>{
      fieldName: value
    }));

  public dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(public dialogRef: MatDialogRef<ShowRequestValuesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.dataSource.data = this.data;
  }

  hasChild = (_: number, node: FlatNode) => node.expandable;

}