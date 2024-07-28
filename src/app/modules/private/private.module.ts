import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivateRoutingModule } from './private-routing.module';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
  ],
  imports: [
    PrivateRoutingModule,
    CommonModule,
    RouterModule,
    FormsModule
  ],
  exports: [],
  bootstrap: [],
})
export class PrivateModule { }