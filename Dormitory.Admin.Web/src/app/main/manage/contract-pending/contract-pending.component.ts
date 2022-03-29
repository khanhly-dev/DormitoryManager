import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContractDto, ContractPendingDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracServiceProxy } from 'src/app/service/admin-service/contract-service-proxy';

@Component({
  selector: 'app-contract-pending',
  templateUrl: './contract-pending.component.html',
  styleUrls: ['./contract-pending.component.css']
})
export class ContractPendingComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listContractPending!: PageResultBase<ContractPendingDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;

  constructor(private contractService: ContracServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      totalRoom: [''],
    });
  }

  ngOnInit(): void {
    this.getListContractPending("", this.pageIndex, 10)
  }

  getListContractPending(keyWord: string, pageIndex: number, pageSize: number) {
    this.contractService.getListContractPending(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listContractPending = x;
    })
  }

  deleteContract(id: number) {
    this.contractService.delete(id).subscribe(() => {
      this.getListContractPending("", this.pageIndex, 10)
    })
  }

  createOrUpdateContract(data: any) {
    this.contractService.createOrUpdate(data).subscribe(x => {
      this.getListContractPending("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string, data?: ContractDto): void {
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
    this.createOrUpdateContract(this.validateForm.value);
  }
}
