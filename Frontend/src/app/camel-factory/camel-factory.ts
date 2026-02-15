import { Component, Input } from '@angular/core';
import { Camel } from '../models/camel';

@Component({
  selector: 'app-camel-factory',
  standalone: false,
  templateUrl: './camel-factory.html',
  styleUrl: './camel-factory.sass',
})
export class CamelFactory {
  @Input() camelInput: Camel = new Camel()
}
