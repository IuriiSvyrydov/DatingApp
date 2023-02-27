import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import {  TabsModule} from 'ngx-bootstrap/tabs';
import { ToastrModule } from 'ngx-toastr';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { NgxSpinnerModule } from 'ngx-spinner';
import {BsDatepickerModule  } from "ngx-bootstrap/datepicker";
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxSpinnerModule.forRoot({
      type: 'line-scale-party'
    }),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
      ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxGalleryModule,
    BsDatepickerModule.forRoot(),
    FileUploadModule
  ],
  exports:[
    BsDropdownModule,
    FileUploadModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    BsDatepickerModule,

  ]
})
export class SharedModule { }
