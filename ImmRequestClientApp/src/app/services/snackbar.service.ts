import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { SnackbarInput } from '../models/models';
import { MatSnackBarConfig } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
    public notifications$: Subject<SnackbarInput> = new Subject();

    public configSuccess: MatSnackBarConfig = {
      panelClass: ['style-success'],    
    };
  
    public configError: MatSnackBarConfig = {
      panelClass: ['style-error'],
    };
}