import { Component, OnInit } from '@angular/core';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { ImporterService } from 'src/app/services/importer.service';
import { BehaviorSubject } from 'rxjs';
import { NgForm } from '@angular/forms';
import { HtmlHelpers } from 'src/app/helpers/html.helper';

@Component({
  selector: 'app-importer',
  templateUrl: './importer.component.html',
  styleUrls: ['./importer.component.css']
})
export class ImporterComponent implements OnInit {

  constructor(private importerService: ImporterService,
    private snackbarService: SnackbarService) { }

  public importers: BehaviorSubject<string[]>;

  private file: File;

  private importer: string;

  ngOnInit() {
    this.importers = new BehaviorSubject([]);
    this.importerService.getImporters().subscribe((allImporters)=>{
      this.importers.next(allImporters);
    })
  }

  import(){
    this.importerService.import(this.importer, this.file)
      .subscribe((res) => {
        this.snackbarService.notifications$.next({
          message: res,
          action: "Success !",
          config: this.snackbarService.configSuccess
        })
      }, (error) => {
        this.snackbarService.notifications$.next({
          message: HtmlHelpers.getHtmlErrorMessage(error),
          action: "Error !",
          config: this.snackbarService.configError
        })
      });
  }

  setImporter(importerName){
    this.importer = importerName;
  }

  setFile(eventTarget){
    this.file = eventTarget.files[0];
    let reader = new FileReader();
    reader.readAsText(this.file);
  }

}
