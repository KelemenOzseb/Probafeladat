import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CamelList } from './camel-list/camel-list';
import { CamelFactory } from './camel-factory/camel-factory';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { errorInterceptorInterceptor } from './error-interceptor-interceptor';

@NgModule({
  declarations: [
    App,
    CamelList,
    CamelFactory
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([errorInterceptorInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
