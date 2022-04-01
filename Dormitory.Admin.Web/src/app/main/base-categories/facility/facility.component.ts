import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FacilityDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { FacilityServiceProxy } from 'src/app/service/admin-service/facility-service-proxy';

@Component({
  selector: 'app-facility',
  templateUrl: './facility.component.html',
})
export class FacilityComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listFacility!: PageResultBase<FacilityDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private facilityService: FacilityServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      totalCount: ['', [Validators.required]],
      status: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListFacility("", this.pageIndex, 10)
  }

  getListFacility(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.facilityService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listFacility = x;
      this.isSpinning = false;
    })
  }

  deleteFacility(id: number) {
    this.facilityService.delete(id).subscribe(() => {
      this.getListFacility("", this.pageIndex, 10)
    })
  }

  createOrUpdateFacility(data: any) {
    this.facilityService.createOrUpdate(data).subscribe(x => {
      this.getListFacility("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string, data?: FacilityDto): void {
    if (data != null) {
      this.validateForm.patchValue(data);
    }
    else {
      this.validateForm.reset();
      this.validateForm.value.id = 0
    }
    this.modalTitle = modalTitle;
    this.isVisible = true;
  }

  handleOk(): void {
    this.submitForm();
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  submitForm(): void {
    this.createOrUpdateFacility(this.validateForm.value);
  }
}
