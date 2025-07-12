import { Component } from '@angular/core';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})
export class UsuarioComponent {

  usuarios: string[]  = ['Luis', 'Fernando', 'Maria'] 
  usuarioNombre: string = '';
  visible: boolean = false;

  
  constructor() {
    setTimeout(() => {
      this.visible = true;
    }, 3000);
  }


  onAgregarUsuario(){
    this.usuarios.push(this.usuarioNombre);
  }


}
