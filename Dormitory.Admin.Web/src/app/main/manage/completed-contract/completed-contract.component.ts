import { Component, OnInit } from '@angular/core';
import { BaseSelectDto, ContractConfigSelect, ContractDto, RoomSelectDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaServiceProxy } from 'src/app/service/admin-service/area-service-proxy';
import { ContracConfigServiceProxy } from 'src/app/service/admin-service/contract-config-service-proxy';
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

  semesterFilter: number = 0
  areaFilter: number = 0
  roomFilter: number = 0
  statusFilter: number = -1
  typeFilter: number = -1;

  //select
  roomSelect: BaseSelectDto[] = []
  areaSelect: BaseSelectDto[] = []
  contractConfigSelect: ContractConfigSelect[] = []

  constructor(
    private contractService: ContracServiceProxy,
    private roomService: RoomServiceProxy,
    private areaService: AreaServiceProxy,
    private configService: ContracConfigServiceProxy) {
  }

  ngOnInit(): void {
    this.getListContract("", this.pageIndex, 10)
    this.getRoomSelect();
    this.getAreaSelect();
    this.getContractConfigSelect();
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

  onFilter() {
    this.contractService.getList("", this.pageIndex, 10).subscribe(x => {
      this.listContract = x;
      if (this.semesterFilter != 0) {
        debugger
        this.listContract.items = this.listContract.items.filter(x => x.semesterId == this.semesterFilter)
      }
      if (this.roomFilter != 0) {
        this.listContract.items = this.listContract.items.filter(x => x.roomId == this.roomFilter)
      }
      if (this.areaFilter != 0) {
        this.listContract.items = this.listContract.items.filter(x => x.areaId == this.areaFilter)
      }
      if (this.typeFilter != -1) {
        this.listContract.items = this.listContract.items.filter(x => this.getContractType(x) == this.typeFilter)
      }
      if (this.statusFilter != -1) {
        this.listContract.items = this.listContract.items.filter(x => this.getContractStatus(x) == this.statusFilter)
      }
      this.isSpinning = false
    })
  }

  getContractType(data: ContractDto) {
    if (!data.isExtendContract && !data.isSummerContract) {
      return 0
    }
    if (data.isExtendContract && !data.isSummerContract) {
      return 1
    }
    if (!data.isExtendContract && data.isSummerContract) {
      return 2
    }
    return -1
  }

  getContractStatus(data: ContractDto) {
    if (data.isDelete == false && !this.compareDate(data.toDate)) {
      return 2
    }
    if (data.isDelete == false && this.compareDate(data.toDate)) {
      return 1
    }
    if (data.isDelete == true) {
      return 0
    }
    return -1
  }

  reset() {
    this.getListContract("", this.pageIndex, 10)
    this.areaFilter = 0;
    this.roomFilter = 0;
    this.statusFilter = -1;
    this.typeFilter = -1;
    this.semesterFilter = 0;
  }

  deleteContract(id: number) {
    this.contractService.delete(id).subscribe((x) => {
      this.getListContract("", this.pageIndex, 10)
      if (x.responseStatus = 'success') {
        alert("Huỷ thành công")
      }
      else {
        alert("Huỷ không thành công")
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

  //filter 
  getRoomSelect() {
    this.roomService.getListSelect().subscribe(x => {
      this.roomSelect = x;
    })
  }
  getAreaSelect() {
    this.areaService.getListSelect().subscribe(x => {
      this.areaSelect = x
    })
  }
  getContractConfigSelect() {
    this.configService.getListSelect().subscribe(x => {
      this.contractConfigSelect = x
    })
  }
}
