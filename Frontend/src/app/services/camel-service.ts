import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { Camel } from '../models/camel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CamelService {
  backendUrl = environment.backendUrl + 'camels'
  camels = new BehaviorSubject<Camel[]>([])
  camels$ = this.camels.asObservable()

  constructor(private http:HttpClient){}

  getCamels(): void {
    this.http.get<Camel[]>(this.backendUrl).subscribe(res =>{
        this.camels.next(res)
    }
    )
  }

  addCamel(body: Camel): void {
    this.http.post<Camel>(this.backendUrl, body).subscribe(res => {
        const current = this.camels.value
        this.camels.next([...current, res])
    }
    )
  }

  updateCamel(body: Camel): void {
    this.http.put<Camel>(this.backendUrl + `${body.id}`, body).subscribe({
      next: () => {
        this.getCamels()
      }
    }
    )
  }
}
