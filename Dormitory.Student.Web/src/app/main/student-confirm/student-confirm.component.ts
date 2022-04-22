import { Component, OnInit } from '@angular/core';
import { ContractPendingDto, StudentInfoDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { SignUpServiceProxy } from 'src/app/service/student-service/sign-up-service-proxy';
import { StudentServiceProxy } from 'src/app/service/student-service/student-service-proxy';

@Component({
  selector: 'app-student-confirm',
  templateUrl: './student-confirm.component.html',
  styleUrls: ['./student-confirm.component.css']
})
export class StudentConfirmComponent implements OnInit {
  modalTitle: string = "";
  student!: StudentInfoDto;
  userId: number = 0;
  listContractPending!: PageResultBase<ContractPendingDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(
    private studentService: StudentServiceProxy,
    private signUpService: SignUpServiceProxy) {

  }

  ngOnInit(): void {
    if (localStorage.getItem("userId") != null || localStorage.getItem("userId") != undefined) {
      this.userId = Number(localStorage.getItem("userId"));
    }
    this.getStudentByUserId(this.userId);
  }

  getListContractPending(keyWord: string, studentId: number, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.studentService.getListStudentConfirmContract(keyWord, studentId, pageIndex, pageSize).subscribe(x => {
      this.listContractPending = x;
      this.listContractPending.items = this.listContractPending.items.filter(x => x.studentConfirmStatus == 0)
      this.isSpinning = false
    })
  }
  getStudentByUserId(userId: number) {
    this.studentService.getStudentByUserId(userId).subscribe(x => {
      this.student = x;
      this.getListContractPending("", this.student.id, this.pageIndex, 10)
    })
  }
  studentConfirm(contractId: number, confirmStatus: number) {
    this.signUpService.studentConfirm(contractId, confirmStatus).subscribe(x => {
      this.getListContractPending("", this.student.id, this.pageIndex, 10)
      if (x.responseStatus == 'success') {
        alert("Xác nhận hợp đồng thành công");
      }
      else {
        alert("Xác nhận hợp đồng thành công");
      }

    })
  }
  deleteSignUpForm(contractId: number) {
    this.signUpService.delete(contractId).subscribe(x => {
      this.getListContractPending("", this.student.id, this.pageIndex, 10)
      if (x.responseStatus == 'success') {
        alert("Xoá thành công")
      }
      else {
        alert("Xoá không thành công")
      }
    })
  }
}
