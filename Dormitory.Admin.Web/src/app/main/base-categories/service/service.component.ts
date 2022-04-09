import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ServiceDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ServiceServiceProxy } from 'src/app/service/admin-service/service-service-proxy';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
})
export class ServiceComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listService!: PageResultBase<ServiceDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private serviceService: ServiceServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      price: ['', [Validators.required]],
      unit: ['', [Validators.required]],
      serviceType: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListService("", this.pageIndex, 10)
  }

  getListService(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.serviceService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listService = x;
      this.isSpinning = false;
    })
  }

  deleteService(id: number) {
    this.serviceService.delete(id).subscribe(() => {
      this.getListService("", this.pageIndex, 10)
    })
  }

  createOrUpdateService(data: any) {
    this.serviceService.createOrUpdate(data).subscribe(x => {
      this.getListService("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string, data?: ServiceDto): void {
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
    this.createOrUpdateService(this.validateForm.value);
  }
}
