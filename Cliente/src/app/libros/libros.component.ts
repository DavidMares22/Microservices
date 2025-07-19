import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-libros',
  templateUrl: './libros.component.html',
  styleUrl: './libros.component.css'
})
export class LibrosComponent {
  libros: string[] = ['Libro 1', 'Libro 2', 'Libro 3'];

  guardarLibro(form: NgForm) {
    if (form.valid) {
      this.libros.push(form.value.libro); // Push only the string
      console.log('Libro guardado:', form.value);
      form.reset();
    } else {
      console.log('Formulario inválido');
    }
  }

  eliminarLibro(libro: string) {
    this.libros = this.libros.filter(l => l !== libro);
    console.log('Libro eliminado:', libro);
  }

}
