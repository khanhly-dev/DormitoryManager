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
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
