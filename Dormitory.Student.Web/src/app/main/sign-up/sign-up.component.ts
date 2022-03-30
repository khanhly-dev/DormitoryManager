import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CriteriaDto, StudentInfoDto } from 'src/app/dto/output-dto';
import { SignUpServiceProxy } from 'src/app/service/student-service/sign-up-service-proxy';
import { StudentServiceProxy } from 'src/app/service/student-service/student-service-proxy';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  listCriteria : CriteriaDto[] = [];
  student!: StudentInfoDto;
  userId : number = 0;
  DesiredPrice : number = 0;
  listCriteriaIdSelected : number[] = [];

  constructor(private studentService: StudentServiceProxy, private signUpService: SignUpServiceProxy) {}

  ngOnInit(): void {
    if(localStorage.getItem("userId") != null || localStorage.getItem("userId") != undefined)
    {
      this.userId = Number(localStorage.getItem("userId"));
    }
    this.getStudentByUserId(this.userId);
    this.getListCriteria();
  }

  getStudentByUserId(userId : number)
  {
    this.studentService.getStudentByUserId(userId).subscribe(x => {
      this.student = x;
    })
  }
  getListCriteria()
  {
    this.signUpService.getListCriteria().subscribe(x => {
      this.listCriteria = x;
    })
  }
  submitContractPending()
  {
    this.listCriteriaIdSelected = this.listCriteria.filter(x => x.checked == true).map(x => x.value)
  }

  test()
  {
    this.submitContractPending()
    console.log(this.listCriteriaIdSelected)
  }
}
