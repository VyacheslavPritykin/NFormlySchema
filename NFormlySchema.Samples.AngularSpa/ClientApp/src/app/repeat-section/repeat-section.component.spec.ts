import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepeatSectionComponent } from './repeat-section.component';

describe('RepeatSectionComponent', () => {
  let component: RepeatSectionComponent;
  let fixture: ComponentFixture<RepeatSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RepeatSectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RepeatSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
