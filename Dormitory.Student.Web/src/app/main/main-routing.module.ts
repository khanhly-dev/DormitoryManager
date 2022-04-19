import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from '../extension/route-guard';
import { CompletedContractComponent } from './completed-contract/completed-contract.component';
import { ContractFeeComponent } from './contract-fee/contract-fee.component';
import { DisciplineComponent } from './discipline/discipline.component';
import { LayoutComponent } from './layout/layout.component';
import { ServiceFeeComponent } from './service-fee/service-fee.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { StudentConfirmComponent } from './student-confirm/student-confirm.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, canActivate: [AuthGuardService], children: [
      { path: '', redirectTo: "student-confirm" },
      { path: 'sign-up', component: SignUpComponent, canActivate: [AuthGuardService] },
      { path: 'student-confirm', component: StudentConfirmComponent, canActivate: [AuthGuardService] },
      { path: 'completed-contract', component: CompletedContractComponent, canActivate: [AuthGuardService] },
      { path: 'contract-fee', component: ContractFeeComponent, canActivate: [AuthGuardService] },
      { path: 'service-fee', component: ServiceFeeComponent, canActivate: [AuthGuardService] },
      { path: 'discipline', component: DisciplineComponent, canActivate: [AuthGuardService] }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
