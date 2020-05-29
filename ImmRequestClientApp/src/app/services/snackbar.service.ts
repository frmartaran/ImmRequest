import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { SnackbarInput } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
    public notifications$: Subject<SnackbarInput> = new Subject();
}