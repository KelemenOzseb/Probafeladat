import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CamelList } from './camel-list';

describe('CamelList', () => {
  let component: CamelList;
  let fixture: ComponentFixture<CamelList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CamelList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CamelList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
