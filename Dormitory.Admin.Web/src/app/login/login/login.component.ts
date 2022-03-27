import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginServiceProxy } from 'src/app/service/core-service/core-service-proxy';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  validateForm!: FormGroup;

  submitForm(): void {
    if (this.validateForm.valid) {
      let formValue = this.validateForm.value;
      this.loginService.login(formValue.userName, formValue.password, 1)
        .subscribe((x) => {
          console.log(x)
          if (x.isLoginSuccess && x.access_token !== null && x.access_token !== undefined && x.access_token !== "") {
            localStorage.setItem("access_token", x.access_token);
            localStorage.setItem("user", x.userName);
            setTimeout(() => {
              alert("Phiên đăng nhập đã hết hạn")
              localStorage.clear();
              this.router.navigate(["/login"]);
            }, 3600000);
            this.router.navigate(["/main"]);
          }
          else {
            this.router.navigate(["/login"]);
            alert('Tài khoản hoặc mật khẩu không chính xác! Vui lòng đăng nhập lại!')
          }
        })

    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  constructor(
    private fb: FormBuilder,
    private loginService: LoginServiceProxy,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
    if (localStorage.getItem("access_token") != null || localStorage.getItem("access_token") != undefined) {
      this.router.navigate(["/main"]);
    }
  }
}

