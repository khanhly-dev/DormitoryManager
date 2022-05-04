import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserDto, UserInfoDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { UserServiceProxy } from 'src/app/service/admin-service/user-service-proxy';
import { LoginServiceProxy } from 'src/app/service/core-service/core-service-proxy';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  listUser!: PageResultBase<UserInfoDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isVisible1 = false;
  isSpinning = false;
  currentUser!: UserInfoDto;
  validateForm!: FormGroup;
  createForm!: FormGroup;
  currentAccount!: UserDto

  constructor(private userService: UserServiceProxy, private fb: FormBuilder, private coreService : LoginServiceProxy) {
    this.validateForm = this.fb.group({
      id: [],
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      email: ['', [Validators.required]],
      tenant: ['', [Validators.required]],
      userInfoId: ['', [Validators.required]],
    });

    this.createForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      dob: ['', [Validators.required]],
      position: ['', [Validators.required]],
      adress: ['', [Validators.required]],
      gender: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListUser("", this.pageIndex, 10)
  }

  getListUser(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.userService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listUser = x;
      this.isSpinning = false
    })
  }

  deleteUser(id: number)
  {
    this.userService.deleteUser(id).subscribe(x => {
      this.getListUser("", this.pageIndex, 10)
      alert(`Xoá thành công`)
    })
  }

  deleteAccount(id: number)
  {
    this.userService.deleteAccount(id).subscribe(x => {
      this.getListUser("", this.pageIndex, 10)
      alert(`Xoá thành công`)
    })

    this.isVisible1 = false
  }

  getAccountByUser(userInfoId : number)
  {
    this.userService.getAccountByUser(userInfoId,1).subscribe(x => {
      this.currentAccount = x;
    })
  }
  showModal(): void {
    this.isVisible = true;
  }

  handleOk(): void {
    this.isVisible = false;
    this.userService.addOrUpdateUser(this.createForm.value).subscribe(x => {
      this.getListUser("", this.pageIndex, 10)
      alert(`Thêm cán bộ thành công`)
    })
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  showModal1(data: UserInfoDto): void {
    this.currentUser = data
    this.getAccountByUser(data.id)
    this.isVisible1 = true;
  }

  handleOk1(): void {
    this.isVisible1 = false;
    this.submitForm();
  }

  handleCancel1(): void {
    this.isVisible1 = false;
  }

  submitForm(): void {
    this.validateForm.controls['tenant'].setValue(1);
    this.validateForm.controls['userInfoId'].setValue(this.currentUser.id);
    this.coreService.register(this.validateForm.value).subscribe(x => {
      alert(`Tạo tài khoản thành công`)
    })
  }
}
