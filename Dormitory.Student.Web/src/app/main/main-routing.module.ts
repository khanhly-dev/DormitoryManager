import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from '../extension/route-guard';
import { LayoutComponent } from './layout/layout.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { StudentConfirmComponent } from './student-confirm/student-confirm.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, canActivate: [AuthGuardService], children: [
      { path: '', redirectTo: "student-confirm" },
      { path: 'sign-up', component: SignUpComponent, canActivate: [AuthGuardService] },
      { path: 'student-confirm', component: StudentConfirmComponent, canActivate: [AuthGuardService] }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
