import { Component, OnInit } from '@angular/core';
import { StudentDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { StudentServiceProxy } from 'src/app/service/admin-service/student-service-proxy';

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
  isSpinning = false;
  currentStudent! : StudentDto;


  constructor(private studentService: StudentServiceProxy) {
    
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
}
