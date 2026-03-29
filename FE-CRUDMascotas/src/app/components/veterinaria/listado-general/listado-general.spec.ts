import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListadoGeneralComponent } from './listado-general';

describe('ListadoGeneral', () => {
  let component: ListadoGeneralComponent;
  let fixture: ComponentFixture<ListadoGeneralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListadoGeneralComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListadoGeneralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
