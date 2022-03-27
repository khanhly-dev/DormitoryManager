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

  constructor(private areaService: AreaServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      totalRoom: [''],
    });
  }

  ngOnInit(): void {
    this.getListArea("", 1, 10)
  }

  getListArea(keyWord: string, pageIndex: number, pageSize: number) {
    this.areaService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listArea = x;
    })
  }

  deleteArea(id: number) {
    this.areaService.delete(id).subscribe(() => {
      this.getListArea("", 1, 10)
    })
  }

  createOrUpdateArea(data: any) {
    this.areaService.createOrUpdate(data).subscribe(x => {
      this.getListArea("", 1, 10)
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
