import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { FlexLayoutModule } from '@angular/flex-layout';
import { FormlyModule } from '@ngx-formly/core';
import { FormlyMaterialModule } from '@ngx-formly/material';
import { NgProgressModule } from 'ngx-progressbar';
import { NgProgressHttpModule } from 'ngx-progressbar/http';
import { NgProgressRouterModule } from 'ngx-progressbar/router';
import { NgxPermissionsModule } from 'ngx-permissions';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from 'app/routes/material/material.module';
import { MaterialExtensionsModule } from 'app/material-extensions.module';
import { BreadcrumbComponent } from '@shared/components/breadcrumb/breadcrumb.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { ErrorCodeComponent } from '@shared/components/error-code/error-code.component';
import { DisableControlDirective } from '@shared/directives/disable-control.directive';
import { SafeUrlPipe } from '@shared/pipes/safe-url.pipe';
import { ToObservablePipe } from '@shared/pipes/to-observable.pipe';
const MODULES: any[] = [
  CommonModule,
  RouterModule,
  ReactiveFormsModule,
  FormsModule,
  MaterialModule,
  MaterialExtensionsModule,
  FlexLayoutModule,
  FormlyModule,
  FormlyMaterialModule,
  NgProgressModule,
  NgProgressRouterModule,
  NgProgressHttpModule,
  NgxPermissionsModule,
  ToastrModule,
  TranslateModule,
];
const COMPONENTS: any[] = [BreadcrumbComponent, PageHeaderComponent, ErrorCodeComponent];
const COMPONENTS_DYNAMIC: any[] = [];
const DIRECTIVES: any[] = [DisableControlDirective];
const PIPES: any[] = [SafeUrlPipe, ToObservablePipe];

@NgModule({
  imports: [...MODULES],
  exports: [...MODULES, ...COMPONENTS, ...DIRECTIVES, ...PIPES],
  declarations: [...COMPONENTS, ...COMPONENTS_DYNAMIC, ...DIRECTIVES, ...PIPES],
})
export class SharedModule {}
