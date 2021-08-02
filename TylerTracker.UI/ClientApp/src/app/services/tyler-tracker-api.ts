import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Health } from '../models/health';
import { Observable } from 'rxjs';

@Injectable()
export class TylerTrackerApi {
  constructor(private http: HttpClient) { }

  createHealthData(health: Health) {
    return this.http.post(`api/health/create-health`, health);
  }

  getLast6MonthsOfHealthData() : Observable<Health[]> {
    return this.http.get<Health[]>(`api/health/get-last-6-months-of-health-data`);
  }
}
