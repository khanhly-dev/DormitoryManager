import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AreaDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaServiceProxy } from 'src/app/service/admin-service/area-service-proxy';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
})
export class AreaComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listArea!: PageResultBase<AreaDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private areaService: AreaServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      totalRoom: [''],
    });
  }

  ngOnInit(): void {
    this.getListArea("", this.pageIndex, 10)
  }

  getListArea(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.areaService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listArea = x;
      this.isSpinning = false
    })
  }

  deleteArea(id: number) {
    this.isSpinning = true;
    this.areaService.delete(id).subscribe(() => {
      this.getListArea("", this.pageIndex, 10)
      this.isSpinning = false
    })
  }

  createOrUpdateArea(data: any) {
    this.isSpinning = true
    this.areaService.createOrUpdate(data).subscribe(x => {
      this.getListArea("", this.pageIndex, 10)
      this.isSpinning = false
    })
  }

  showModal(modalTitle: string, data?: AreaDto): void {
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
    this.createOrUpdateArea(this.validateForm.value);
  }
}
