import { Component, OnInit } from '@angular/core';
import { ContractFeeStatusDto, StudentInfoDto } from 'src/app/dto/output-dto';
import { ContracServiceProxy } from 'src/app/service/student-service/contract-service-proxy';
import { StudentServiceProxy } from 'src/app/service/student-service/student-service-proxy';

@Component({
  selector: 'app-contract-fee',
  templateUrl: './contract-fee.component.html',
  styleUrls: ['./contract-fee.component.css']
})
export class ContractFeeComponent implements OnInit {
  pageIndex: number = 1;
  pageSize!: number;
  isSpinning = false;
  currentStudentContract: ContractFeeStatusDto[] = []
  userId: number = 0;
  student!: StudentInfoDto;


  constructor(private studentService: StudentServiceProxy,private contractService: ContracServiceProxy) {
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
      this.currentStudentContract = x;
      this.isSpinning = false
    })
  }
}
