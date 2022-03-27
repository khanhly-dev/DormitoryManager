import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseSelectDto, RoomDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { AreaServiceProxy } from 'src/app/service/admin-service/area-service-proxy';
import { RoomServiceProxy } from 'src/app/service/admin-service/room-service-proxy';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
})
export class RoomComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listArea!: PageResultBase<RoomDto>;
  areaSelect : BaseSelectDto[] = [];
  pageIndex!: number;
  pageSize!: number;
  isVisible = false;

  constructor(
    private roomService: RoomServiceProxy, 
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
    });
  }

  ngOnInit(): void {
    this.getListRoom("", 1, 10);
    this.getListAreaSelect();
  }

  getListRoom(keyWord: string, pageIndex: number, pageSize: number) {
    this.roomService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listArea = x;
    })
  }

  getListAreaSelect() {
    this.areaService.getListSelect().subscribe(x => {
      this.areaSelect = x;
    })
  }

  deleteRoom(id: number) {
    this.roomService.delete(id).subscribe(() => {
      this.getListRoom("", 1, 10)
    })
  }

  createOrUpdateRoom(data: any) {
    this.roomService.createOrUpdate(data).subscribe(x => {
      this.getListRoom("", 1, 10)
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

  submitForm(): void {
    this.createOrUpdateRoom(this.validateForm.value);
  }
}
