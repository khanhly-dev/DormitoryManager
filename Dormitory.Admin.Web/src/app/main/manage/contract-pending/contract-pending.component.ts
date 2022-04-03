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
  isSpinning = false;

  constructor(private contractService: ContracServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      minPoint: [0,[Validators.required]],
      maxPoint: [0,[Validators.required]],
      confirmStatus: [[Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListContractPending("", this.pageIndex, 10)
  }

  getListContractPending(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.contractService.getListContractPending(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listContractPending = x;
      this.isSpinning = false
    })
  }

  deleteContract(id: number) {
    this.contractService.delete(id).subscribe(() => {
      this.getListContractPending("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string): void {
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
    this.adminConfirmAll(this.validateForm.value);
  }

  adminConfirm(contractId: number, confirmStatus: number) {
    this.contractService.adminConfirmContract(contractId, confirmStatus).subscribe(x => {
      this.getListContractPending("", 1, 10);
    })
  }
  adminConfirmAll(data: any) {
    this.contractService.adminConfirmAllContract(data).subscribe(x => {
      this.getListContractPending("", 1, 10);
    })
  }
}
