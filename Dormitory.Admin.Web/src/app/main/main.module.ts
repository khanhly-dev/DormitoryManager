import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { LayoutComponent } from './layout/layout.component';

import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AreaComponent } from './base-categories/area/area.component';
import { RoomComponent } from './base-categories/room/room.component';
import { FacilityComponent } from './base-categories/facility/facility.component';
import { CriteriaComponent } from './base-categories/criteria/criteria.component';
import { ContractTimeConfigComponent } from './base-categories/contract-time-config/contract-time-config.component';
import { ServiceComponent } from './base-categories/service/service.component';
import { ContractPendingComponent } from './manage/contract-pending/contract-pending.component';

import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { WaitingRoomComponent } from './manage/waiting-room/waiting-room.component';
import { CompletedContractComponent } from './manage/completed-contract/completed-contract.component';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { ContractFeeComponent } from './manage/contract-fee/contract-fee.component';
import { ServiceFeeComponent } from './manage/service-fee/service-fee.component';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { StudentListComponent } from './manage/student-list/student-list.component';
import { DisciplineComponent } from './manage/discipline/discipline.component';
import { DashboardComponent } from './dashboard/dashboard.component';
//mdbootrap
import { ChartsModule, WavesModule } from 'angular-bootstrap-md';
import { UserListComponent } from './manage/user-list/user-list.component'

@NgModule({
  declarations: [
    LayoutComponent,
    AreaComponent,
    RoomComponent,
    FacilityComponent,
    CriteriaComponent,
    ContractTimeConfigComponent,
    ServiceComponent,
    ContractPendingComponent,
    WaitingRoomComponent,
    CompletedContractComponent,
    ContractFeeComponent,
    ServiceFeeComponent,
    StudentListComponent,
    DisciplineComponent,
    DashboardComponent,
    UserListComponent,
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    NzLayoutModule,
    NzBreadCrumbModule,
    NzListModule,
    NzIconModule,
    NzMenuModule,
    NzDropDownModule,
    NzTableModule,
    NzButtonModule,
    NzPaginationModule,
    NzModalModule,
    NzFormModule,
    FormsModule,
    ReactiveFormsModule,
    NzInputNumberModule,
    NzInputModule,
    NzSelectModule,
    NzGridModule,
    NzAvatarModule,
    NzSpinModule,
    NzPopconfirmModule,
    NzCollapseModule,
    NzRadioModule,
    NzCardModule,
    NzDrawerModule,
    ChartsModule,
    WavesModule
  ]
})
export class MainModule { }
