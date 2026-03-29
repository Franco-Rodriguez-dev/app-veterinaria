import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarEditarPersona } from './agregar-editar-persona';

describe('AgregarEditarPersona', () => {
  let component: AgregarEditarPersona;
  let fixture: ComponentFixture<AgregarEditarPersona>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgregarEditarPersona]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgregarEditarPersona);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
