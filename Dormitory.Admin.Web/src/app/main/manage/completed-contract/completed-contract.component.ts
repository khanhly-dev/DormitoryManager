import { Component, OnInit } from '@angular/core';
import { ContractDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracServiceProxy } from 'src/app/service/admin-service/contract-service-proxy';

@Component({
  selector: 'app-completed-contract',
  templateUrl: './completed-contract.component.html',
  styleUrls: ['./completed-contract.component.css']
})
export class CompletedContractComponent implements OnInit {
  modalTitle: string = "";
  listContract!: PageResultBase<ContractDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;
  now = new Date();

  constructor(private contractService: ContracServiceProxy) {
  }

  ngOnInit(): void {
    this.getListContract("", this.pageIndex, 10)
  }

  getListContract(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.contractService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listContract = x;
      this.isSpinning = false
    })
  }

  deleteContract(id: number) {
    this.contractService.delete(id).subscribe(() => {
      this.getListContract("", this.pageIndex, 10)
    })
  }

  compareDate(date: Date): boolean {
    let convertDate = new Date(date);
    let now = new Date();
    if (convertDate.getTime() > now.getTime()) {
      return true
    }
    else {
      return false
    }
  }
  showModal(): void {
    this.isVisible = true;
  }

  handleOk(): void {
    console.log('Button ok clicked!');
    this.isVisible = false;
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }
}
