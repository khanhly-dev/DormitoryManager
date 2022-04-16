import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContractConfig } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracConfigServiceProxy } from 'src/app/service/admin-service/contract-config-service-proxy';

@Component({
  selector: 'app-contract-time-config',
  templateUrl: './contract-time-config.component.html',
})
export class ContractTimeConfigComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listContractConfigPending!: PageResultBase<ContractConfig>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private contractConfigService: ContracConfigServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      fromDate: ['', [Validators.required]],
      toDate: ['', [Validators.required]],
      isSummerSemester: [false, [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.getListConfig("", this.pageIndex, 10)
  }

  getListConfig(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.contractConfigService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listContractConfigPending = x;
      this.isSpinning = false;
    })
  }

  deleteConfig(id: number) {
    this.contractConfigService.delete(id).subscribe(() => {
      this.getListConfig("", this.pageIndex, 10)
    })
  }

  createOrUpdateConfig(data: any) {
    this.contractConfigService.createOrUpdate(data).subscribe(x => {
      this.getListConfig("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string, data?: ContractConfig): void {
    if (data != null) {
      this.validateForm.patchValue(data);
      this.validateForm.controls['toDate'].setValue(data.toDate.toString());
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
    this.createOrUpdateConfig(this.validateForm.value);
  }
}
