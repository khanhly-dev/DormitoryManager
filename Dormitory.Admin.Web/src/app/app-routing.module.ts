import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule), // Lazy load account module
    data: { preload: true },
  },
  {
    path: 'test',
    loadChildren: () => import('./test/test.module').then(m => m.TestModule), // Lazy load account module
    data: { preload: true },
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
