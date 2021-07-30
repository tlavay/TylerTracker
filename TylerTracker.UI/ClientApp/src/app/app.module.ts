import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { HealthMainComponent } from './health-main/health-main.component';
import { LineGraphComponent } from './graphs/line-graph.component';

import { TylerTrackerApi } from './services/tyler-tracker-api'

import { NgBootstrapFormValidationModule } from 'ng-bootstrap-form-validation';
import { GoogleChartsModule } from 'angular-google-charts';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    HealthMainComponent,
    LineGraphComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgBootstrapFormValidationModule.forRoot(),
    NgBootstrapFormValidationModule,
    GoogleChartsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'health-main', component: HealthMainComponent },
    ],
      { relativeLinkResolution: 'legacy' })
  ],
  providers: [TylerTrackerApi],
  bootstrap: [AppComponent]
})
export class AppModule { }
