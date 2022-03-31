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
  listPrice : number [] = [];
  desiredPrice! : number;

  constructor(private studentService: StudentServiceProxy, private signUpService: SignUpServiceProxy) {}

  ngOnInit(): void {
    if(localStorage.getItem("userId") != null || localStorage.getItem("userId") != undefined)
    {
      this.userId = Number(localStorage.getItem("userId"));
    }
    this.getStudentByUserId(this.userId);
    this.getListCriteria();
    this.getRecomendPrice();
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

  getRecomendPrice()
  {
    this.studentService.getRecomendPrice().subscribe(x => {
      this.listPrice = x;
    })
  }
  submitContractPending()
  {
    this.listCriteriaIdSelected = this.listCriteria.filter(x => x.checked == true).map(x => x.value)
    if(this.listCriteriaIdSelected.length > 0)
    {
      this.signUpService.setStudentPoint(this.student.id, this.listCriteriaIdSelected.toString()).subscribe();
    }
    this.signUpService.signUpDormitory(this.student.id, this.desiredPrice).subscribe(x => {
      if(x.responseStatus == "success")
      {
        alert("Gửi đơn đăng ký thành công")
      }
      else
      {
        alert("Gửi đơn đăng ký thất bại")
      }
    });
  }
}
