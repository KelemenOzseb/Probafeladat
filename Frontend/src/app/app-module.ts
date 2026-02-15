import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Camel } from './models/camel/camel';
import { CamelList } from './camel-list/camel-list';
import { CamelFactory } from './camel-factory/camel-factory';

@NgModule({
  declarations: [
    App,
    Camel,
    CamelList,
    CamelFactory
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
  ],
  bootstrap: [App]
})
export class AppModule { }
