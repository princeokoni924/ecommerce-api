import { Component, inject, Inject, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../angularCore/Services/account.service';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormField,
    MatInput,
    MatError,
    MatLabel
  ],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';
constructor(@Self() public controlDir: NgControl){
this.controlDir.valueAccessor = this;
}
  writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
    
  }
  registerOnTouched(fn: any): void {
    
  }
  // setDisabledState?(isDisabled: boolean): void {
   
  // }

  /* geter function for typeScript benefit.
  the reseason function is so that i can use the control
  in my templete
  */
  get control(){
    return this.controlDir.control as FormControl
  }
}
