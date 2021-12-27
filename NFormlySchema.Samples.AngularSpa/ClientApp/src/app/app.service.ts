import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private http: HttpClient) { }


  async getForm_CustomObjectFieldArrayRoot(): Promise<any[]> {
    return this.http.get<any[]>('/api/FormlyConfigs/GetForm_CustomObjectFieldArrayRoot')
      .pipe(
        tap(t => console.log("getForm_CustomObjectFieldArrayRoot", t))
      )
      .toPromise();
  }
}
