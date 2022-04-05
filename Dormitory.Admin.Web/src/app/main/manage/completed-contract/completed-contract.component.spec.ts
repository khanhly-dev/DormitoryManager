import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompletedContractComponent } from './completed-contract.component';

describe('CompletedContractComponent', () => {
  let component: CompletedContractComponent;
  let fixture: ComponentFixture<CompletedContractComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompletedContractComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompletedContractComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
