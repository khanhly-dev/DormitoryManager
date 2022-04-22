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
  listContractPending!: PageResultBase<ContractPendingDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isSpinning = false;
  statusFilter : number = -1;
  fromDateFilter!: Date;
  toDateFilter!: Date;

  constructor(private contractService: ContracServiceProxy) {
 
  }

  ngOnInit(): void {
    this.getListContractPending("", this.pageIndex, 10)
  }
  changeStatusFilter()
  {

  }
  onChange(data: any)
  {

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

  submitForm(): void {
    this.adminConfirmAll();
  }

  adminConfirm(contractId: number, confirmStatus: number) {
    if (confirmStatus == 1) {
      let item = this.listContractPending.items.find(x => x.id == contractId);
      if (item) {
        if (item.roomId) {
          alert("Đã xếp phòng cho sinh viên này, không thể từ chối")
          return
        }
      }
      else {
        this.contractService.adminConfirmContract(contractId, confirmStatus).subscribe(x => {
          this.getListContractPending("", 1, 10);
        })
      }
    }
    this.contractService.adminConfirmContract(contractId, confirmStatus).subscribe(x => {
      this.getListContractPending("", 1, 10);
    })
  }
  adminConfirmAll() {
    this.contractService.adminConfirmAllContract().subscribe(x => {
      this.getListContractPending("", 1, 10);
      if (x.status == 'success') {
        alert(`Duyệt tự động thành công, có ${x.count} đơn được duyệt`)
      }
      else {
        alert("Duyệt tự động không thành công")
      }
    })
  }
}
