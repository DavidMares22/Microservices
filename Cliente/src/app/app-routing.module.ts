import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
 

 
import {AutoresComponent} from './autores/autores.component';
import { InicioComponent } from './inicio.component';
import { LibrosComponent } from './libros/libros.component';


const routes: Routes = [
  { path: '', component: InicioComponent }  ,

  { path: 'autores', component: AutoresComponent },
  { path: 'libros', component: LibrosComponent },

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule {}
