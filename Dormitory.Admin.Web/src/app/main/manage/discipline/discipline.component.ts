import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseSelectDto, DisciplineDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { StudentServiceProxy } from 'src/app/service/admin-service/student-service-proxy';

@Component({
  selector: 'app-discipline',
  templateUrl: './discipline.component.html',
  styleUrls: ['./discipline.component.css']
})
export class DisciplineComponent implements OnInit {
  modalTitle: string = "";
  validateForm!: FormGroup;
  listDiscipline!: PageResultBase<DisciplineDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;
  studentSelect : BaseSelectDto[] = [];

  constructor(private studentService: StudentServiceProxy, private fb: FormBuilder) {
    this.validateForm = this.fb.group({
      id: [],
      studentId: ['', [Validators.required]],
      description: ['', [Validators.required]],
      punish: ['',[Validators.required]],
    });
  }

  ngOnInit(): void {
    this.getListDiscipline("", this.pageIndex, 10);
    this.getListStudentSelect();
  }

  getListDiscipline(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.studentService.getListDiscipline(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listDiscipline = x;
      this.isSpinning = false
    })
  }

  getListStudentSelect()
  {
    this.studentService.getListStudentSelect().subscribe(x => {
      this.studentSelect = x
    })
  }

  deleteDiscipline(id: number) {
    this.isSpinning = true;
    this.studentService.deleteDiscipline(id).subscribe(() => {
      this.getListDiscipline("", this.pageIndex, 10)
      this.isSpinning = false
    })
  }

  createOrUpdateDiscipline(data: any) {
    this.isSpinning = true
    this.studentService.addOrUpdateDiscipline(data).subscribe(x => {
      this.getListDiscipline("", this.pageIndex, 10)
      this.isSpinning = false
    })
  }

  showModal(modalTitle: string, data?: DisciplineDto): void {
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
    if(this.validateForm.controls['id'].value == null ||this.validateForm.controls['id'].value == undefined)
    {
      this.validateForm.controls['id'].setValue(0) == null
    }
    this.createOrUpdateDiscipline(this.validateForm.value);
  }
}
