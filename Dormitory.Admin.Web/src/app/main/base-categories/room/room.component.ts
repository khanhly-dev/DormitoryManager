import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AreaDto, BaseSelectDto, FacilityInRoom, RoomDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaServiceProxy } from 'src/app/service/admin-service/area-service-proxy';
import { FacilityServiceProxy } from 'src/app/service/admin-service/facility-service-proxy';
import { RoomServiceProxy } from 'src/app/service/admin-service/room-service-proxy';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
})
export class RoomComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  facilityForm!: FormGroup;
  listRoom!: PageResultBase<RoomDto>;
  areaSelect: AreaDto[] = [];
  facilitySelect: BaseSelectDto[] = [];
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isVisible1 = false;
  isVisible2 = false;
  isSpinning = false;
  listFaciliy: FacilityInRoom[] = [];
  roomSelectedId: number = 0;
  areaFilter: number = 0;

  constructor(
    private roomService: RoomServiceProxy,
    private facilityService: FacilityServiceProxy,
    private fb: FormBuilder,
    private areaService: AreaServiceProxy,) {
    this.validateForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      price: ['', [Validators.required]],
      areaId: ['', [Validators.required]],
      maxSlot: ['', [Validators.required]],
      minSlot: ['', [Validators.required]],
      filledSlot: ['', [Validators.required]],
      avaiableSlot: ['', [Validators.required]],
    });

    this.facilityForm = this.fb.group({
      id: [0],
      roomId: ['', [Validators.required]],
      facilityId: ['', [Validators.required]],
      count: ['', [Validators.required]],
      status: ['', [Validators.required]],
    })
  }

  ngOnInit(): void {
    this.getListRoom("", this.pageIndex, 10);
    this.getListAreaSelect();
    this.getListFacilitySelect()
  }
  changeAreaFilter() {
    this.isSpinning = true;
    this.roomService.getList("", this.pageIndex, 10).subscribe(x => {
      this.listRoom = x;
      if (this.areaFilter != 0) {
        this.listRoom.items = this.listRoom.items.filter(x => x.areaId == this.areaFilter)
      }
      else {
        this.listRoom = x;
      }
      this.isSpinning = false;
    })
  }

  getListRoom(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.roomService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listRoom = x;
      this.isSpinning = false;
    })
  }

  getListFacilitySelect() {
    this.facilityService.getListFacilitySelect().subscribe(x => {
      this.facilitySelect = x
    })
  }

  getListAreaSelect() {
    this.areaService.getListSelect().subscribe(x => {
      this.areaSelect = x;
    })
  }

  deleteRoom(id: number) {
    this.roomService.delete(id).subscribe(() => {
      this.getListRoom("", this.pageIndex, 10)
    })
  }

  createOrUpdateRoom(data: any) {
    this.roomService.createOrUpdate(data).subscribe(x => {
      this.getListRoom("", this.pageIndex, 10)
    })
  }

  getFacilityByRoomId(roomId: number) {
    this.facilityService.getListFacilityByRoomId(roomId).subscribe(x => {
      this.listFaciliy = x;
    })
  }

  submitForm(): void {
    this.createOrUpdateRoom(this.validateForm.value);
  }

  facilitySubmitForm(): void {
    this.facilityForm.controls['roomId'].setValue(this.roomSelectedId);
    this.facilityService.addFacilityIntoRoom(this.facilityForm.value).subscribe(x => {
      this.getFacilityByRoomId(this.roomSelectedId)
      this.isVisible2 = false
      if (x.responseStatus = 'success') {
        alert("Thêm thành công")
      }
      else {
        alert("Thêm không thành công")
      }
    })
  }

  deleteFacilityInRoom(id: number) {
    this.facilityService.deleteFacilityInRoom(id).subscribe(x => {
      this.getFacilityByRoomId(this.roomSelectedId)
      if (x.responseStatus = 'success') {
        alert("Xoá thành công")
      }
      else {
        alert("Xoá không thành công")
      }
    })
  }

  showModal(modalTitle: string, data?: RoomDto): void {
    if (data != null) {
      this.validateForm.patchValue(data);
    }
    else {
      this.validateForm.reset();
      this.validateForm.value.id = 0
    }
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

  showModal1(roomId: number): void {
    this.roomSelectedId = roomId;
    this.getFacilityByRoomId(roomId);
    this.isVisible1 = true;
  }

  close(): void {
    this.isVisible1 = false;
  }

  showModal2(): void {
    this.isVisible2 = true;
  }

  handleOk2(): void {
    this.facilitySubmitForm();
  }

  handleCancel2(): void {
    this.isVisible2 = false;
  }
}
