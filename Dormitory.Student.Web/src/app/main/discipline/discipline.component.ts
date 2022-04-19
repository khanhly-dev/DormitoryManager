import { Component, OnInit } from '@angular/core';
import { BaseSelectDto, DisciplineDto } from 'src/app/dto/output-dto';
import { PageResultBase } from 'src/app/dto/page-result-base';
import { StudentServiceProxy } from 'src/app/service/student-service/student-service-proxy';

@Component({
  selector: 'app-discipline',
  templateUrl: './discipline.component.html',
  styleUrls: ['./discipline.component.css']
})
export class DisciplineComponent implements OnInit {
  listDiscipline!: PageResultBase<DisciplineDto>;
  pageIndex: number = 1;
  pageSize!: number;
  isVisible = false;
  isSpinning = false;

  constructor(private studentService: StudentServiceProxy) {
  }

  ngOnInit(): void {
    this.getListDiscipline("", this.pageIndex, 10);
  }

  getListDiscipline(keyWord: string, pageIndex: number, pageSize: number) {
    this.isSpinning = true;
    this.studentService.getListDiscipline(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listDiscipline = x;
      this.isSpinning = false
    })
  }
}
