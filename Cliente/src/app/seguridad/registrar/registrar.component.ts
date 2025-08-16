import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrl: './registrar.component.css'
})
export class RegistrarComponent {

  registrarUsuario(f: NgForm) {
    if (f.valid) {
      const usuario = {
        email: f.value.email,
        password: f.value.password
      };
      console.log('Usuario registrado:', usuario);
    }

  }

}
