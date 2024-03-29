import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

/* Pages */
import {TodoListComponent} from './components/todo-list/todo-list.component';


const routes: Routes = [
  { path:  '', pathMatch: 'full', redirectTo:  'todo' },
  { path:  'todo', pathMatch: 'full', component:  TodoListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
