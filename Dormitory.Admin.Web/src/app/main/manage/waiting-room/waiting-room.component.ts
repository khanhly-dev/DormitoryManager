import { Component, OnInit } from '@angular/core';
import { ContractDto, ContractPendingDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracServiceProxy } from 'src/app/service/admin-service/contract-service-proxy';

@Component({
  selector: 'app-waiting-room',
  templateUrl: './waiting-room.component.html',
  styleUrls: ['./waiting-room.component.css']
})
export class WaitingRoomComponent implements OnInit {
  modalTitle: string = "";
  listContractPending!: PageResultBase<ContractPendingDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private contractService: ContracServiceProxy) {

  }

  ngOnInit(): void {
    this.getListContractPending("", this.pageIndex, 10)
  }

  getListContractPending(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.contractService.getListAdminConfirmContractPending(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listContractPending = x;
      this.isSpinning = false
    })
  }

  deleteContract(id: number) {
    this.contractService.delete(id).subscribe(() => {
      this.getListContractPending("", this.pageIndex, 10)
    })
  }

  showModal(modalTitle: string, data?: ContractDto): void {

  }

  handleOk(): void {
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  scheduleRoom(contractId: number) {
    this.contractService.scheduleRoom(contractId).subscribe(x => {
      this.getListContractPending("", 1, 10);
    })
  }

  autoScheduleRoom() {
    let listContractId = this.listContractPending.items.filter(x => !x.roomId).map(x => x.id)
    this.contractService.autoScheduleRoom(listContractId).subscribe(x => {
      this.getListContractPending("", 1, 10);
    })
  }
}
