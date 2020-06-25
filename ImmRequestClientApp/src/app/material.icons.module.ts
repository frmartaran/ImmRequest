import {
    MatCardModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatStepperModule,
    MatInputModule,
    MatDialogModule,
    MatMenuModule,
    MatRadioModule,
    MatTableModule,
    MatPaginatorModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatSortModule,
    MatTreeModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
} from '@angular/material';
import { NgModule } from '@angular/core';

const MaterialModules = [
    MatCardModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatStepperModule,
    MatInputModule,
    MatDialogModule,
    MatMenuModule,
    MatRadioModule,
    MatTableModule,
    MatPaginatorModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTreeModule,
    MatSortModule,
    MatSelectModule  
];

@NgModule({
 exports: [ MaterialModules ]
})

export class MaterialModule {}