import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Camel } from '../models/camel';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CamelService {
  camels = new BehaviorSubject<Camel[]>([])
  camels$ = this.camels.asObservable()

  constructor(private http:HttpClient){this.getCamels()}

  getCamels(): void {
    this.http.get<Camel[]>(environment.apis.camel).subscribe(res => {
        this.camels.next(res)
    }
    )
  }

  addCamel(body: Camel): void {
    this.http.post<Camel>(environment.apis.camel, body).subscribe(res => {
        const current = this.camels.value
        this.camels.next([...current, res])
    }
    )
  }

  updateCamel(body: Camel): void {
    this.http.put<Camel>(environment.apis.camel + `/${body.id}`, body).subscribe({
      next: () => {
        this.getCamels()
      }
    }
    )
  }
}
