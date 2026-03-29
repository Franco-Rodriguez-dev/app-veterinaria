import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerVeterinaria } from './ver-veterinaria';

describe('VerVeterinaria', () => {
  let component: VerVeterinaria;
  let fixture: ComponentFixture<VerVeterinaria>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VerVeterinaria]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VerVeterinaria);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
