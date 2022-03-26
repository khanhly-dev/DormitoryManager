import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { LayoutComponent } from './layout/layout.component';

import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { AreaComponent } from './area/area.component';
import { RoomComponent } from './room/room.component';
import { FacilityComponent } from './facility/facility.component';
import { CriteriaComponent } from './criteria/criteria.component';


@NgModule({
  declarations: [
    LayoutComponent,
    AreaComponent,
    RoomComponent,
    FacilityComponent,
    CriteriaComponent,
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    NzLayoutModule,
    NzBreadCrumbModule,
    NzListModule,
    NzIconModule,
    NzMenuModule,
    NzDropDownModule
  ]
})
export class MainModule { }
