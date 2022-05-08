import { Component, OnInit } from '@angular/core';
import { BaseSelectDto, ContractDto, ContractPendingDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracServiceProxy } from 'src/app/service/admin-service/contract-service-proxy';
import { RoomServiceProxy } from 'src/app/service/admin-service/room-service-proxy';

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
  statusFilter : number = -1;
  roomFilter: number = 0
  roomSelect: BaseSelectDto[] = []

  constructor(private contractService: ContracServiceProxy,  private roomService: RoomServiceProxy,) {

  }

  ngOnInit(): void {
    this.getListContractPending("", this.pageIndex, 10)
    this.getRoomSelect();
  }
  getRoomSelect() {
    this.roomService.getListSelect().subscribe(x => {
      this.roomSelect = x;
    })
  }
  onRoomFilter()
  {
    this.isSpinning = true
    this.contractService.getListAdminConfirmContractPending("", this.pageIndex, 10).subscribe(x => {
      this.listContractPending = x;
      if(this.roomFilter != 0)
      {
        this.listContractPending.items = this.listContractPending.items.filter(x => x.roomId == this.roomFilter)
      }
      this.isSpinning = false
    })
  }
  changeStatusFilter()
  {
    this.isSpinning = true;
    this.contractService.getListAdminConfirmContractPending("", this.pageIndex, 10).subscribe(x => {
      this.listContractPending = x;
      if (this.statusFilter != -1) {
        this.listContractPending.items = this.listContractPending.items.filter(x => x.studentConfirmStatus == this.statusFilter)
      }
      else {
        this.listContractPending = x;
      }
      this.isSpinning = false;
    })
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
    this.listContractPending.items.sort((a ,b) => a.desiredPrice - b.desiredPrice)
    let listContractId = this.listContractPending.items.filter(x => !x.roomId).map(x => x.id)
    this.contractService.autoScheduleRoom(listContractId).subscribe(x => {
      this.getListContractPending("", 1, 10);
    })
  }
}
