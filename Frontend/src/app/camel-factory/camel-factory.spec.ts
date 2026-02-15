import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CamelFactory } from './camel-factory';

describe('CamelFactory', () => {
  let component: CamelFactory;
  let fixture: ComponentFixture<CamelFactory>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CamelFactory]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CamelFactory);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
