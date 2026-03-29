import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarEditarVeterinaria } from './agregar-editar-veterinaria';

describe('AgregarEditarVeterinaria', () => {
  let component: AgregarEditarVeterinaria;
  let fixture: ComponentFixture<AgregarEditarVeterinaria>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgregarEditarVeterinaria]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgregarEditarVeterinaria);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
