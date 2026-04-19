import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { VeterinariaService } from './veterinaria';

describe('Veterinaria', () => {
  let service: VeterinariaService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(VeterinariaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
