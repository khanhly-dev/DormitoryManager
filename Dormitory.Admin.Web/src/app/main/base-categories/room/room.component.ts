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
  areaSelect : AreaDto[] = [];
  facilitySelect : BaseSelectDto[] = [];
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isVisible1 = false;
  isVisible2 = false;
  isSpinning = false;
  listFaciliy : FacilityInRoom[] = [];

  constructor(
    private roomService: RoomServiceProxy, 
    private facilityService: FacilityServiceProxy, 
    private fb: FormBuilder,
    private areaService: AreaServiceProxy, ) {
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
      id: [],
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

  getListRoom(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.roomService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listRoom = x;
      this.isSpinning = false;
    })
  }

  getListFacilitySelect()
  {
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

  getFacilityByRoomId(roomId: number)
  {
    this.facilityService.getListFacilityByRoomId(roomId).subscribe(x => {
      this.listFaciliy = x;
    })
  }

  submitForm(): void {
    this.createOrUpdateRoom(this.validateForm.value);
  }

  facilityDubmitForm(): void {
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

  showModal1(roomId : number): void {
    this.getFacilityByRoomId(roomId);
    this.isVisible1 = true;
  }

  handleOk1(): void {
    this.isVisible1 = false;
  }

  handleCancel1(): void {
    this.isVisible1 = false;
  }

  showModal2(): void {
    this.isVisible2 = true;
  }

  handleOk2(): void {
    this.isVisible2 = false;
  }

  handleCancel2(): void {
    this.isVisible2 = false;
  }
}
