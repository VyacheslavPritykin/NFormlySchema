import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';


import { FormlyBootstrapModule } from '@ngx-formly/bootstrap';
import { FormlyModule } from '@ngx-formly/core';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PanelWrapperComponent } from './panel-wrapper/panel-wrapper.component';
import { RepeatSectionComponent } from './repeat-section/repeat-section.component';

@NgModule({
  declarations: [
    AppComponent,
    PanelWrapperComponent,
    RepeatSectionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormlyBootstrapModule,
    FormlyModule.forRoot({
      types: [
        { name: 'repeat', component: RepeatSectionComponent },
      ],
      wrappers: [
        { name: 'panel', component: PanelWrapperComponent },
      ],
      validationMessages: [
        { name: 'required', message: 'This field is required' },
      ],
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
