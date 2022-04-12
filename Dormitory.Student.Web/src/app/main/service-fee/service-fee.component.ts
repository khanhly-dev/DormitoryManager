import { Component, OnInit } from '@angular/core';
import { BillServiceDto, ContractFeeStatusDto, RoomServiceDto, StudentInfoDto } from 'src/app/dto/output-dto';
import { ContracServiceProxy } from 'src/app/service/student-service/contract-service-proxy';
import { RoomServiceProxy } from 'src/app/service/student-service/room-service-proxy';
import { ServiceServiceProxy } from 'src/app/service/student-service/service-service-proxy';
import { StudentServiceProxy } from 'src/app/service/student-service/student-service-proxy';

@Component({
  selector: 'app-service-fee',
  templateUrl: './service-fee.component.html',
  styleUrls: ['./service-fee.component.css']
})
export class ServiceFeeComponent implements OnInit {
  roomServiceList : RoomServiceDto[] = []
  listBill : BillServiceDto[] = [];
  isVisible = false;
  userId: number = 0;
  isSpinning = false;
  student!: StudentInfoDto;
  currentRoomId: number = 0;
  
  constructor(
    private serviceService : ServiceServiceProxy,
    private roomService : RoomServiceProxy,
    private studentService: StudentServiceProxy,
    private contractService: ContracServiceProxy
  ) {
    if (localStorage.getItem("userId") != null || localStorage.getItem("userId") != undefined) {
      this.userId = Number(localStorage.getItem("userId"));
    }
  }

  ngOnInit(): void {
    this.getStudentByUserId(this.userId);
   
  }
  
  getStudentByUserId(userId: number) {
    this.studentService.getStudentByUserId(userId).subscribe(x => {
      this.student = x;
      this.getContractByStudentId();
    })
  }

  getContractByStudentId() {
    this.isSpinning = true
    this.contractService.getListContractByStudentId(this.student.id).subscribe(x => {
      x.map(x=> x.roomId).forEach(x => {
        this.getListBill(x);
      })
      this.isSpinning = false
    })
  }

  getServiceFeeBillId(billId : number)
  {
    this.serviceService.getServiceByBill(billId).subscribe(x => {
      this.roomServiceList = x;
    })
  }

  getListBill(roomId: number){
    this.roomService.getListBill(roomId).subscribe(x => {
      this.listBill = x;
    })
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  showModal(billId : number): void {
    this.getServiceFeeBillId(billId);
    this.isVisible = true;
  }

  handleOk(): void {
    this.isVisible = false;
  }
}
