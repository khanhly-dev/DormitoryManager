import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { SignUpComponent } from './sign-up/sign-up.component';

import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

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
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { StudentConfirmComponent } from './student-confirm/student-confirm.component';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { CompletedContractComponent } from './completed-contract/completed-contract.component';
import { ServiceFeeComponent } from './service-fee/service-fee.component';
import { ContractFeeComponent } from './contract-fee/contract-fee.component';
import { DisciplineComponent } from './discipline/discipline.component';


@NgModule({
  declarations: [
    LayoutComponent,
    SignUpComponent,
    StudentConfirmComponent,
    CompletedContractComponent,
    ServiceFeeComponent,
    ContractFeeComponent,
    DisciplineComponent,
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
    NzCardModule,
    NzCheckboxModule,
    NzSpinModule
  ]
})
export class MainModule { }
