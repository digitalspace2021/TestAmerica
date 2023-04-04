import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashBoardComponent } from './pages/dash-board/dash-board.component';

const routes: Routes = [
  {
    path: "",
    component: DashBoardComponent,
    loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule), 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
