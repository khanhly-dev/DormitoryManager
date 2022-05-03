import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from '../extension/route-guard';
import { AreaComponent } from './base-categories/area/area.component';
import { ContractTimeConfigComponent } from './base-categories/contract-time-config/contract-time-config.component';
import { CriteriaComponent } from './base-categories/criteria/criteria.component';
import { FacilityComponent } from './base-categories/facility/facility.component';
import { LayoutComponent } from './layout/layout.component';
import { RoomComponent } from './base-categories/room/room.component';
import { ServiceComponent } from './base-categories/service/service.component';
import { ContractPendingComponent } from './manage/contract-pending/contract-pending.component';
import { WaitingRoomComponent } from './manage/waiting-room/waiting-room.component';
import { CompletedContractComponent } from './manage/completed-contract/completed-contract.component';
import { ServiceFeeComponent } from './manage/service-fee/service-fee.component';
import { ContractFeeComponent } from './manage/contract-fee/contract-fee.component';
import { StudentListComponent } from './manage/student-list/student-list.component';
import { DisciplineComponent } from './manage/discipline/discipline.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserListComponent } from './manage/user-list/user-list.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, canActivate: [AuthGuardService], children: [
      { path: 'service', component: ServiceComponent, canActivate: [AuthGuardService] },
      { path: 'area', component: AreaComponent, canActivate: [AuthGuardService] },
      { path: 'criteria', component: CriteriaComponent, canActivate: [AuthGuardService] },
      { path: 'facility', component: FacilityComponent, canActivate: [AuthGuardService] },
      { path: 'room', component: RoomComponent, canActivate: [AuthGuardService] },
      { path: 'contract-config', component: ContractTimeConfigComponent, canActivate: [AuthGuardService] },
      { path: 'contract-pending', component: ContractPendingComponent, canActivate: [AuthGuardService] },
      { path: 'waiting-room', component: WaitingRoomComponent, canActivate: [AuthGuardService] },
      { path: 'completed-contract', component: CompletedContractComponent, canActivate: [AuthGuardService] },
      { path: 'contract-fee', component: ContractFeeComponent, canActivate: [AuthGuardService] },
      { path: 'service-fee', component: ServiceFeeComponent, canActivate: [AuthGuardService] },
      { path: 'student-list', component: StudentListComponent, canActivate: [AuthGuardService] },
      { path: 'user-list', component: UserListComponent, canActivate: [AuthGuardService] },
      { path: 'discipline', component: DisciplineComponent, canActivate: [AuthGuardService] },
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuardService] },
      { path: '', redirectTo: 'dashboard', canActivate: [AuthGuardService] },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
