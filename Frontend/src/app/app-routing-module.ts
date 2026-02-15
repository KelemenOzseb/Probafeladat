import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CamelList } from './camel-list/camel-list';
import { CamelFactory } from './camel-factory/camel-factory';

const routes: Routes = [
  {path: "", redirectTo: "camel-list", pathMatch: "full"},
  {path: "camel-list", component: CamelList},
  {path: "camel-factory/:id", component: CamelFactory},
  {path: "camel-factory", component: CamelFactory},
  {path: "**", redirectTo: "camel-list"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
