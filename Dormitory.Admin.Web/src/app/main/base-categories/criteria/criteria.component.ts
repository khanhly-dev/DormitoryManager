import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CriteriaDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { CriteriaServiceProxy } from 'src/app/service/admin-service/criteria-service-proxy';

@Component({
  selector: 'app-criteria',
  templateUrl: './criteria.component.html',
})
export class CriteriaComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listCriteria!: PageResultBase<CriteriaDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private criteriaService: CriteriaServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      point: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListCriteria("", this.pageIndex, 10)
  }

  getListCriteria(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.criteriaService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listCriteria = x;
      this.isSpinning = false;
    })
  }

  deleteCriteria(id: number) {
    this.criteriaService.delete(id).subscribe(() => {
      this.getListCriteria("", this.pageIndex, 10)
    })
  }

  createOrUpdateCriteria(data: any) {
    this.criteriaService.createOrUpdate(data).subscribe(x => {
      this.getListCriteria("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string, data?: CriteriaDto): void {
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
    this.createOrUpdateCriteria(this.validateForm.value);
  }
}
