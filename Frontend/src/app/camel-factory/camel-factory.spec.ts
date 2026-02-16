import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { CamelFactory } from './camel-factory';
import { ActivatedRoute, Router } from '@angular/router';
import { CamelService } from '../services/camel-service';

describe('CamelFactory (form validation only)', () => {
  let component: CamelFactory;
  let fixture: ComponentFixture<CamelFactory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CamelFactory],
      imports: [ReactiveFormsModule],
      providers: [
        FormBuilder,
        { provide: ActivatedRoute, useValue: {} },
        { provide: Router, useValue: {} },
        { provide: CamelService, useValue: {} }
      ]
    })
    .overrideComponent(CamelFactory, {
      set: { template: '' }
    })
    .compileComponents();

    fixture = TestBed.createComponent(CamelFactory);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('the forms should be invalid when it is blank', () => {
    expect(component.camelForm.valid).toBeFalsy()
  })

  it('name validation works', () => {
    const name = component.camelForm.get("name")

    name?.setValue('')
    expect(name?.hasError('required')).toBeTruthy()

    name?.setValue('A')
    expect(name?.hasError('minlength')).toBeTruthy()

    name?.setValue('Camel')
    expect(name?.valid).toBe(true)
  })


});
