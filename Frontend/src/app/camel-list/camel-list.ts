import { Component, EventEmitter, Output } from '@angular/core';
import { CamelService } from '../services/camel-service';
import { Router } from '@angular/router';
import { Camel } from '../models/camel';

@Component({
  selector: 'app-camel-list',
  standalone: false,
  templateUrl: './camel-list.html',
  styleUrl: './camel-list.sass',
})
export class CamelList {
  constructor(public camelService:CamelService, private router:Router) {}

}
