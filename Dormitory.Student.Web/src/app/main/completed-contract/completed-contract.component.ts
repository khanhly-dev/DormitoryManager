import { Component, OnInit } from '@angular/core';
import { ContractPendingDto, ExtendContractTime, StudentInfoDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { SignUpServiceProxy } from 'src/app/service/student-service/sign-up-service-proxy';
import { StudentServiceProxy } from 'src/app/service/student-service/student-service-proxy';

@Component({
  selector: 'app-completed-contract',
  templateUrl: './completed-contract.component.html',
  styleUrls: ['./completed-contract.component.css']
})
export class CompletedContractComponent implements OnInit {
  modalTitle: string = "";
  student!: StudentInfoDto;
  userId: number = 0;
  listContractPending!: PageResultBase<ContractPendingDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isVisible1 = false;
  isSpinning = false;
  extendContractTime!: ExtendContractTime;
  summerContractTime!: ExtendContractTime;
  CanCreateExtendContract: boolean = false;
  CanCreateSummerContract: boolean = false;

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

  checkCanCreateExtendContract(studentId: number) {
    this.studentService.checkCreateExtendContract(studentId).subscribe(x => {
      this.CanCreateExtendContract = x;
    })
  }

  checkCanCreateSummerContract(studentId: number) {
    this.studentService.checkCreateSummerContract(studentId).subscribe(x => {
      this.CanCreateSummerContract = x;
    })
  }

  getListContractPending(keyWord: string, studentId: number, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.studentService.getListStudentConfirmContract(keyWord, studentId, pageIndex, pageSize).subscribe(x => {
      this.listContractPending = x;
      this.listContractPending.items = this.listContractPending.items.filter(x => x.contractCompletedStatus == 1)
      this.isSpinning = false
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
  getStudentByUserId(userId: number) {
    this.studentService.getStudentByUserId(userId).subscribe(x => {
      this.student = x;
      this.getListContractPending("", this.student.id, this.pageIndex, 10)
      this.checkCanCreateExtendContract(x.id)
      this.checkCanCreateSummerContract(x.id)
    })
  }
  createExtendContract() {
    this.signUpService.extendContract(this.student.id).subscribe(x => {
      this.getListContractPending("", this.student.id, this.pageIndex, 10)
      this.checkCanCreateExtendContract(this.student.id)
      this.checkCanCreateSummerContract(this.student.id)
      if (x.responseStatus == 'success') {
        alert("Gia hạn thành công")
      }
      else {
        alert("Gia hạn thành công")
      }
    })
  }
  createSummerContract() {
    this.signUpService.summerContract(this.student.id).subscribe(x => {
      this.getListContractPending("", this.student.id, this.pageIndex, 10)
      this.checkCanCreateExtendContract(this.student.id)
      this.checkCanCreateSummerContract(this.student.id)
      if (x.responseStatus == 'success') {
        alert("Gia hạn thành công")
      }
      else {
        alert("Gia hạn thành công")
      }
    })
  }
  showModal(): void {
    this.signUpService.getExtendContractTime(this.student.id).subscribe(x => {
      this.extendContractTime = x;
    })
    this.isVisible = true;
  }

  handleOk(): void {
    this.createExtendContract();
    this.isVisible = false;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  showModal1(): void {
    this.signUpService.getSummerContractTime(this.student.id).subscribe(x => {
      this.summerContractTime = x;
    })
    this.isVisible1 = true;
  }

  handleOk1(): void {
    this.createSummerContract();
    this.isVisible1 = false;
  }

  handleCancel1(): void {
    this.isVisible1 = false;
  }
}
