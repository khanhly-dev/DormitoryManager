import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from '../extension/route-guard';
import { AreaComponent } from './area/area.component';
import { ContractTimeConfigComponent } from './contract-time-config/contract-time-config.component';
import { CriteriaComponent } from './criteria/criteria.component';
import { FacilityComponent } from './facility/facility.component';
import { LayoutComponent } from './layout/layout.component';
import { RoomComponent } from './room/room.component';
import { ServiceComponent } from './service/service.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, canActivate: [AuthGuardService], children: [
      { path: 'service', component: ServiceComponent, canActivate: [AuthGuardService] },
      { path: 'area', component: AreaComponent, canActivate: [AuthGuardService] },
      { path: 'criteria', component: CriteriaComponent, canActivate: [AuthGuardService] },
      { path: 'facility', component: FacilityComponent, canActivate: [AuthGuardService] },
      { path: 'room', component: RoomComponent, canActivate: [AuthGuardService] },
      { path: 'contract-config', component: ContractTimeConfigComponent, canActivate: [AuthGuardService] },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
