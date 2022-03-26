import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractTimeConfigComponent } from './contract-time-config.component';

describe('ContractTimeConfigComponent', () => {
  let component: ContractTimeConfigComponent;
  let fixture: ComponentFixture<ContractTimeConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContractTimeConfigComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractTimeConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
