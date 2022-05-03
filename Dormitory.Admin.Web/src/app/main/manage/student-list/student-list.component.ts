import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { StudentServiceProxy } from 'src/app/service/admin-service/student-service-proxy';
import { LoginServiceProxy } from 'src/app/service/core-service/core-service-proxy';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css']
})
export class StudentListComponent implements OnInit {
  listStudent!: PageResultBase<StudentDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isVisible1 = false;
  isSpinning = false;
  currentStudent!: StudentDto;
  validateForm!: FormGroup;

  constructor(private studentService: StudentServiceProxy, private fb: FormBuilder, private coreService : LoginServiceProxy) {
    this.validateForm = this.fb.group({
      id: [],
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      email: ['', [Validators.required]],
      tenant: ['', [Validators.required]],
      userInfoId: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListStudent("", this.pageIndex, 10)
  }

  getListStudent(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.studentService.getAll(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listStudent = x;
      this.isSpinning = false
    })
  }
  showModal(data: StudentDto): void {
    this.currentStudent = data
    this.isVisible = true;
  }

  handleOk(): void {
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  showModal1(data: StudentDto): void {
    this.currentStudent = data
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
    this.validateForm.controls['tenant'].setValue(0);
    this.validateForm.controls['userInfoId'].setValue(this.currentStudent.id);
    this.coreService.register(this.validateForm.value).subscribe(x => {
      if (x.status == 'success') {
        alert(`Tạo tài khoản thành công`)
      }
      else {
        alert("Tạo tài khoản không thành công")
      }
    })
  }
}
