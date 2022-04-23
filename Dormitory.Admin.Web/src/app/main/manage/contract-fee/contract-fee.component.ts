import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContractDto, ContractFeeStatusDto, StudentDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { ContracServiceProxy } from 'src/app/service/admin-service/contract-service-proxy';
import { StudentServiceProxy } from 'src/app/service/admin-service/student-service-proxy';

@Component({
  selector: 'app-contract-fee',
  templateUrl: './contract-fee.component.html',
  styleUrls: ['./contract-fee.component.css']
})
export class ContractFeeComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listStudent!: PageResultBase<StudentDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;
  collapseActive!: number
  collapseStatus: boolean = false;
  currentStudentContract: ContractFeeStatusDto[] = []
  selectedContractId: number = 0;
  selectedStudentId: number = 0;
  paidFilter! : boolean;

  constructor(private studentService: StudentServiceProxy, private fb: FormBuilder, private contractService: ContracServiceProxy) {
    this.validateForm = this.fb.group({
      datePaid: ['', [Validators.required]],
      moneyPaid: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListStudent("", this.pageIndex, 10)
  }

  changePaidFilter()
  {
    this.isSpinning = true;
    this.studentService.getList("", this.pageIndex, 10).subscribe(x => {
      this.listStudent = x;
      if (this.paidFilter != undefined) {
        this.listStudent.items = this.listStudent.items.filter(x => x.paymentStatus == this.paidFilter)
      }
      else {
        this.listStudent = x;
      }
      this.isSpinning = false;
    })
  }

  getListStudent(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true
    this.studentService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listStudent = x;
      this.isSpinning = false
    })
  }
  getContractByStudentId(studentId: number) {
    this.collapseStatus = !this.collapseStatus
    this.collapseActive = studentId
    this.isSpinning = true
    this.contractService.getListContractByStudentId(studentId).subscribe(x => {
      this.currentStudentContract = x;
      this.isSpinning = false
    })
  }
  updateContractFee(contractId: number) {
    this.studentService.updateContractPaid(contractId, this.validateForm.value).subscribe(x => {
      this.getListStudent("", this.pageIndex, 10)
      this.getContractByStudentId(this.selectedStudentId);
      if (x.responseStatus = 'success') {
        alert("Cập nhật thành công")
      }
      else {
        alert("Cập nhật không thành công")
      }
    })
  }
  showModal(data: ContractFeeStatusDto): void {

    this.selectedContractId = data.id
    this.selectedStudentId = data.studentId
    this.isVisible = true;
  }

  handleOk(): void {
    this.isVisible = false;
    this.submitForm();
  }

  handleCancel(): void {
    this.isVisible = false;
  }
  submitForm(): void {
    this.updateContractFee(this.selectedContractId)
  }
}
