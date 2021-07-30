import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Health } from '../models/health';


@Injectable()
export class TylerTrackerApi {
    constructor(private http: HttpClient) { }

  createHealthData(health: Health) {
    return this.http.post(`api/health/create-health`, health);
  }
}
