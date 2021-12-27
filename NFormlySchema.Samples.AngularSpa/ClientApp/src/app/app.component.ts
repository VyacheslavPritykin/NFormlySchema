import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormlyFieldConfig, FormlyFormOptions } from '@ngx-formly/core';
import { AppService } from './app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'FormlyTesting';

  form = new FormGroup({});
  model = {};
  options: FormlyFormOptions = {};
  fields: FormlyFieldConfig[];

  constructor(private svc: AppService) { }

  async ngOnInit() {
    let _fields = await this.svc.getForm_CustomObjectFieldArrayRoot();
    this.fields = _fields;
  }


  onSubmit() {
    console.log(this.model);
  }
}
