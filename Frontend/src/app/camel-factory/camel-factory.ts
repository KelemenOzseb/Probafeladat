import { Component, OnInit } from '@angular/core';
import { Camel } from '../models/camel';
import { ActivatedRoute, Router } from '@angular/router';
import { CamelService } from '../services/camel-service';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ValidationError } from '@angular/forms/signals';

@Component({
  selector: 'app-camel-factory',
  standalone: false,
  templateUrl: './camel-factory.html',
  styleUrl: './camel-factory.sass',
})
export class CamelFactory implements OnInit{
  camelForm: FormGroup;
  isEdit = false;
  id: string | null = null;

  constructor(
    private formbBuilder: FormBuilder, private route: ActivatedRoute, 
    private router: Router, private camelService: CamelService) 
  {
    this.camelForm = this.formbBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(2)]],
      color: [null],
      humpCount: [1, [Validators.required, Validators.min(1), Validators.max(2)]],
      lastFed: [null, [this.pastDateValidator()]]
    })
  }

  pastDateValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null; 
      }
      const selection = new Date(control.value)
      const today = new Date()
      return selection > today ? { futureDate: { value: control.value } } : null
    }
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')
    if (this.id) {
      this.isEdit = true;
      this.loadCamelData(this.id)
    }
    else
      this.isEdit = false
  }

  private loadCamelData(id: string) {
    this.camelService.camels$.subscribe(camels => {
      const found = camels.find(c => c.id === id)
      if (found) {
        this.camelForm.patchValue(found)
      }
    })
  }

  save(): void {
    if (this.camelForm.invalid) return
    const camelData = this.camelForm.value
    console.log(camelData)
    if (this.isEdit) {
      this.camelService.updateCamel(camelData)
    } else {
      this.camelService.addCamel(camelData)
    }
    this.router.navigate(["/camel-list"])
  }
}
