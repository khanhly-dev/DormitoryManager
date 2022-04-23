import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddRoomServiceRequest, BaseSelectDto, BillServiceDto, RoomDto, RoomServiceDto } from 'src/app/dto/output-dto';
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
  visible = false;
  isVisible1 = false;
  isSpinning = false;
  validateForm!: FormGroup;
  updateForm!: FormGroup;
  serviceSelect: BaseSelectDto[] = [];
  roomSelect: BaseSelectDto[] = [];
  selectedRoomId : number = 0;
  roomServiceList : RoomServiceDto[] = []
  collapseStatus: boolean = false;
  collapseActive!: number
  selectedRoomService: number = 0;
  listBill : BillServiceDto[] = [];
  selectedBillId : number = 0;

  rfDataModal!: FormGroup;
  selectedRoomBill : any;
  selectServiceBill: any;
  fromDate!: Date;
  toDate!: Date;

  paidFilter! : boolean;

  constructor(
    private roomService: RoomServiceProxy,
    private fb: FormBuilder,
    private serviceService: ServiceServiceProxy,
  ) {
    this.updateForm = this.fb.group({
      datePaid: ['', [Validators.required]],
      moneyPaid: ['', [Validators.required]],
    });

    this.rfDataModal = this.fb.group({
      listData: this.fb.array([]),
    });
  }

  get datas(): FormArray {
    return this.rfDataModal.get('listData') as FormArray;
  }

  test()
  {
    console.log(this.datas.value)
  }

  ngOnInit(): void {
    this.getListRoom("", this.pageIndex, 10);
    this.getListRoomSelect();
    this.getListSelect();
  }

  changePaidFilter()
  {
    this.isSpinning = true;
    this.roomService.getList("", this.pageIndex, 10).subscribe(x => {
      this.listRoom = x;
      if (this.paidFilter != undefined) {
        this.listRoom.items = this.listRoom.items.filter(x => x.isPaid == this.paidFilter)
      }
      else {
        this.listRoom = x;
      }
      this.isSpinning = false;
    })
  }

  deleteBillService(billId : number)
  {
    this.serviceService.deleteBillService(billId).subscribe(x => {
      this.getListBill(this.selectedRoomId);
      this.getListRoom("", this.pageIndex, 10);
      if (x.responseStatus = 'success') {
        alert("Xóa thành công")
      }
      else {
        alert("Xóa không thành công")
      }
    })
  }

  getListRoom(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.roomService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listRoom = x;
      this.isSpinning = false;
    })
  }

  addServiceIntoBill(service: BaseSelectDto) {
    console.log(service)
    this.datas.push(
      this.fb.group({
        serviceId: service.id,
        name: [service.name, Validators.required],
        statBegin: [0, Validators.required],
        statEnd: [0, Validators.required],
      }),
    );
  }

  removeServiceFromBill(index: number) {
    this.datas.removeAt(index);
  }

  getListSelect() {
    this.serviceService.getListSelect().subscribe(x => {
      this.serviceSelect = x;
    })
  }

  getListRoomSelect()
  {
    this.roomService.getListSelect().subscribe(x => {
      this.roomSelect = x;
    })
  }

  getServiceFeeBillId()
  {
    this.serviceService.getServiceByBill(this.selectedBillId).subscribe(x => {
      this.roomServiceList = x;
    })
  }

  getListBill(roomId: number){
    this.collapseStatus = !this.collapseStatus
    this.collapseActive = roomId
    this.roomService.getListBill(roomId).subscribe(x => {
      this.listBill = x;
    })
  }

  updateRoomServiceFee(roomServiceId : number)
  {
    this.serviceService.updateServicePaid(roomServiceId, this.updateForm.value).subscribe(x => {
      this.getListBill(this.selectedRoomId);
      this.getListRoom("", this.pageIndex, 10);
      this.getServiceFeeBillId();
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
      this.getServiceFeeBillId();
      if (x.responseStatus = 'success') {
        alert("Xoá thành công")
      }
      else {
        alert("Xoá không thành công")
      }
    })
  }

  addServiceForRoom()
  {
    if(this.fromDate == undefined || this.fromDate == null)
    {
      alert("Phải chọn thời gian của hoá đơn")
      return
    }
    if(this.toDate == undefined || this.toDate == null)
    {
      alert("Phải chọn thời gian của hoá đơn")
      return
    }
    if(this.selectedRoomBill == undefined || this.selectedRoomBill == null)
    {
      alert("Phải chọn phòng")
      return
    }
    if(this.datas.value == undefined || this.datas.value == null || this.datas.value.length == 0)
    {
      alert("Phải chọn dịch vụ cho phòng")
      return
    }
    this.serviceService.addServiceForRoom(JSON.stringify(this.datas.value), this.selectedRoomBill, this.fromDate, this.toDate).subscribe(x => {
      this.getListRoom("", this.pageIndex, 10);
      this.getServiceFeeBillId();
      if (x.responseStatus = 'success') {
        this.visible = false;
        alert("Cập nhật thành công")
      }
      else {
        alert("Cập nhật không thành công")
      }
    })
  }

  open(): void {
    this.visible = true;
  }

  close(): void {
    this.visible = false;
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

  showModal(billId : number): void {
    this.selectedBillId = billId;
    this.getServiceFeeBillId();
    this.isVisible = true;
  }

  handleOk(): void {
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }
}
