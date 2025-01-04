import { Component, inject } from '@angular/core';
import { ShopService } from '../../../angularCore/Services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatSelect } from '@angular/material/select';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-dialog',
  standalone: true,
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton,
    FormsModule
  ],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.scss'
})
export class FilterDialogComponent {
shopService = inject(ShopService);
private dialogRefService = inject(MatDialogRef<FilterDialogComponent>)
// inject data
data = inject(MAT_DIALOG_DATA)

selectedBrands: string[] = this.data.selectedBrands;
selectedTypes: string[] = this.data.selectedTypes;

// apply filter method
applyFilter()
{
 this.dialogRefService.close({
 selectedBrands: this.selectedBrands,
 selectedTypes: this.selectedTypes
 })
}
}
