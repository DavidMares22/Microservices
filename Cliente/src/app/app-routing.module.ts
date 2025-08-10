import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
 

 
import {AutoresComponent} from './autores/autores.component';
import { InicioComponent } from './inicio.component';
import { LibrosComponent } from './libros/libros.component';
import { RegistrarComponent } from './seguridad/registrar/registrar.component';
import { LoginComponent } from './seguridad/login/login.component';


const routes: Routes = [
  { path: '', component: InicioComponent }  ,
  { path: 'autores', component: AutoresComponent },
  { path: 'libros', component: LibrosComponent },
  { path: 'registrar', component: RegistrarComponent },
  { path: 'login', component: LoginComponent },

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule {}
