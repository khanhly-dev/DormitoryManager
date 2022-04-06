import { Component, OnInit } from '@angular/core';
import { ContractDto, RoomSelectDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracServiceProxy } from 'src/app/service/admin-service/contract-service-proxy';
import { RoomServiceProxy } from 'src/app/service/admin-service/room-service-proxy';

@Component({
  selector: 'app-completed-contract',
  templateUrl: './completed-contract.component.html',
  styleUrls: ['./completed-contract.component.css']
})
export class CompletedContractComponent implements OnInit {
  modalTitle: string = "";
  listContract!: PageResultBase<ContractDto>;
  emptyRoom!: RoomSelectDto[];
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;
  now = new Date();
  selectedRoom: any;
  selectedContract: number = 0;

  constructor(private contractService: ContracServiceProxy, private roomService: RoomServiceProxy) {
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

  getListEmptyRoom(gender: number) {
    this.roomService.getListEmptyRoom().subscribe(x => {
      this.emptyRoom = x.filter(x => x.genderRoom == gender || !x.genderRoom);
    })
  }

  deleteContract(id: number) {
    this.contractService.delete(id).subscribe((x) => {
      this.getListContract("", this.pageIndex, 10)
      if (x.responseStatus = 'success') {
        alert("Xoá thành công")
      }
      else {
        alert("Xoá không thành công")
      }
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

  changeRoom(contractId: number) {
    this.contractService.changeRoom(contractId, this.selectedRoom).subscribe(x => {
      this.getListContract("", this.pageIndex, 10);
      if (x.responseStatus = 'success') {
        alert("Đổi phòng thành công")
      }
      else {
        alert("Đổi phòng không thành công")
      }
    })
  }

  showModal(data: ContractDto): void {
    this.getListEmptyRoom(data.gender)
    this.selectedContract = data.id;
    this.isVisible = true;
  }

  handleOk(): void {
    this.changeRoom(this.selectedContract)
    this.isVisible = false;
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
  }
}
