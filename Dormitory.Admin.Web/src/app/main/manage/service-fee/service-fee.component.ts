import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseSelectDto, RoomDto, RoomServiceDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { RoomServiceProxy } from 'src/app/service/admin-service/room-service-proxy';
import { ServiceServiceProxy } from 'src/app/service/admin-service/service-service-proxy';

@Component({
  selector: 'app-service-fee',
  templateUrl: './service-fee.component.html',
  styleUrls: ['./service-fee.component.css']
})
export class ServiceFeeComponent implements OnInit {
  listRoom!: PageResultBase<RoomDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isVisible1 = false;
  isSpinning = false;
  validateForm!: FormGroup;
  updateForm!: FormGroup;
  serviceSelect: BaseSelectDto[] = [];
  selectedRoomId : number = 0;
  roomServiceList : RoomServiceDto[] = []
  collapseStatus: boolean = false;
  collapseActive!: number
  selectedRoomService: number = 0;

  constructor(
    private roomService: RoomServiceProxy,
    private fb: FormBuilder,
    private serviceService: ServiceServiceProxy,
  ) {
    this.validateForm = this.fb.group({
      roomId: [],
      serviceId: ['', [Validators.required]],
      fromDate: ['', [Validators.required]],
      toDate: ['', [Validators.required]],
      statBegin: ['', [Validators.required]],
      statEnd: ['', [Validators.required]],
    });

    this.updateForm = this.fb.group({
      datePaid: ['', [Validators.required]],
      moneyPaid: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListRoom("", this.pageIndex, 10);
  }

  getListRoom(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.roomService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listRoom = x;
      this.isSpinning = false;
    })
  }

  getListSelect() {
    this.serviceService.getListSelect().subscribe(x => {
      this.serviceSelect = x;
    })
  }

  getServiceFeeByRoom(roomId: number)
  {
    this.collapseStatus = !this.collapseStatus
    this.collapseActive = roomId
    this.serviceService.getServiceByRoom(roomId).subscribe(x => {
      this.roomServiceList = x;
    })
  }

  updateRoomServiceFee(roomServiceId : number)
  {
    this.serviceService.updateServicePaid(roomServiceId, this.updateForm.value).subscribe(x => {
      this.getListRoom("", this.pageIndex, 10);
      this.getServiceFeeByRoom(this.selectedRoomId);
      if (x.responseStatus = 'success') {
        alert("Cập nhật thành công")
      }
      else {
        alert("Cập nhật không thành công")
      }
    })
  }

  deleteRoomService(roomServiceId : number)
  {
    this.serviceService.deleteRoomService(roomServiceId).subscribe(x => {
      this.getListRoom("", this.pageIndex, 10);
      this.getServiceFeeByRoom(this.selectedRoomId);
      if (x.responseStatus = 'success') {
        alert("Xoá thành công")
      }
      else {
        alert("Xoá không thành công")
      }
    })
  }

  showModal(roomId : number): void {
    this.selectedRoomId = roomId;
    this.getListSelect();
    this.isVisible = true;
  }

  handleOk(): void {
    this.submitForm();
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  showModal1(roomServiceId : number): void {
    this.selectedRoomService = roomServiceId
    this.isVisible1 = true;
  }

  handleOk1(): void {
    this.updateRoomServiceFee(this.selectedRoomService);
    this.isVisible1 = false;
  }

  handleCancel1(): void {
    this.isVisible1 = false;
  }

  submitForm(): void {
    this.validateForm.controls['roomId'].setValue(this.selectedRoomId)
    this.serviceService.addServiceForRoom(this.validateForm.value).subscribe(x => {
      this.getListRoom("", this.pageIndex, 10);
      this.getServiceFeeByRoom(this.selectedRoomId);
      if (x.responseStatus = 'success') {
        alert("Cập nhật thành công")
      }
      else {
        alert("Cập nhật không thành công")
      }
    })
  }
}
